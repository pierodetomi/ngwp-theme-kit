import { Directive, ElementRef, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

import { ThemeSettingsService } from '../services/theme-settings.service';
import { WpConfigurationService } from '../services/wp-configuration.service';

@Directive({
  selector: '[ngwpThemeSetting]'
})
export class ThemeSettingDirective implements OnInit, OnChanges {
  @Input() public settingId = '';

  @Input() public settingType: 'text' | 'img' = 'text';

  constructor(
    private _elementRef: ElementRef,
    private _wpConfigurationService: WpConfigurationService,
    private _themeSettingsService: ThemeSettingsService
  ) { }

  ngOnInit(): void {
    // if (this._wpConfigurationService.configuration.isCustomizer) {
    //   this._wpConfigurationService.configuration.customizerService.registerPreviewCallback((settingId, settingValue) => {
    //     if (!this.settingId || this.settingId !== settingId) {
    //       return;
    //     }

    //     this._applyValue(settingValue);
    //   });
    // }
  }

  ngOnChanges(changes: SimpleChanges): void {
    // if (this._wpConfigurationService.configuration.isCustomizer) {
    //   return;
    // }

    const hasSettingId = changes['settingId']?.currentValue !== undefined && changes['settingId']?.currentValue !== null;
    const isSettingIdChanged = hasSettingId && changes['settingId'].currentValue !== changes['settingId'].previousValue;

    if (!isSettingIdChanged) {
      return;
    }

    this._loadSettingValue();
  }

  private _loadSettingValue() {
    if (!this.settingId) {
      return;
    }

    switch (this.settingType) {
      case 'text':
        this._themeSettingsService
          .get<string>(this.settingId)
          .subscribe((value) => {
            this._applyValue(value);
          });
        break;

      case 'img':
        this._themeSettingsService
          .getImage(this.settingId)
          .subscribe((attachment) => {
            this._applyValue(attachment.src);
          });
        break;

      default:
        throw `The value "${this.settingType}" is not supported for "settingType" parameter`;
    }
  }

  private _applyValue(value: string) {
    const htmlElement = (this._elementRef.nativeElement as HTMLElement);

    switch (this.settingType) {
      case 'text':
        htmlElement.innerHTML = value;
        break;

      case 'img':
        if (htmlElement.tagName.toLowerCase() === 'img') {
          htmlElement.setAttribute('src', value);

        } else if (htmlElement.children.length > 0 && htmlElement.children[0].tagName.toLowerCase() === 'img') {
          const img = htmlElement.children[0];
          img.setAttribute('src', value);

        } else {
          const img = document.createElement('img');
          img.setAttribute('src', value);

          htmlElement.appendChild(img);
        }
        break;

      default:
        throw `The value "${this.settingType}" is not supported for "settingType" parameter`;
    }
  }
}
