import { AfterViewInit, Component, ComponentRef, ViewContainerRef } from '@angular/core';

import { ThemeRuntimeConfig } from '../../config/theme-runtime.config';
import { WpConfigurationService } from '../../services/wp-configuration.service';

@Component({
  selector: 'ngwp-theme-template-host',
  template: ''
})
export class ThemeTemplateHostComponent implements AfterViewInit {
  constructor(
    private _wpConfigurationService: WpConfigurationService,
    private _viewContainerRef: ViewContainerRef
  ) { }

  ngAfterViewInit(): void {
    const hasPageTemplate = ThemeRuntimeConfig.pageTemplateComponent !== null;
    const hasPostTemplate = ThemeRuntimeConfig.postTemplateComponent !== null;

    this._viewContainerRef.clear();

    let componentRef: ComponentRef<any> = null;

    if (this._wpConfigurationService.configuration.isPage && hasPageTemplate) {
      componentRef = this._viewContainerRef.createComponent(ThemeRuntimeConfig.pageTemplateComponent);

    } else if (this._wpConfigurationService.configuration.isPost && hasPostTemplate) {
      componentRef = this._viewContainerRef.createComponent(ThemeRuntimeConfig.postTemplateComponent);
    }

    if (componentRef) {
      //   componentRef.instance.data = {}; // TODO: Inject meaningful data
      componentRef.changeDetectorRef.detectChanges();
    }
  }
}
