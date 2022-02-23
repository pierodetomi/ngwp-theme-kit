import { Directive, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';

import { WidgetAreaService } from '../services/widget-area.service';

@Directive({
  selector: '[ngwpWidgetArea]'
})
export class WidgetAreaDirective implements OnChanges {
  @Input() public widgetAreaId = '';

  constructor(private _elementRef: ElementRef, private _widgetAreaService: WidgetAreaService) { }

  ngOnChanges(changes: SimpleChanges): void {
    const hasWidgetAreaId = changes['widgetAreaId']?.currentValue !== undefined && changes['widgetAreaId']?.currentValue !== null;
    const isWidgetAreaIdChanged = hasWidgetAreaId && changes['widgetAreaId'].currentValue !== changes['widgetAreaId'].previousValue;

    if (!isWidgetAreaIdChanged) {
      return;
    }

    this._loadWidgetArea();
  }

  private _loadWidgetArea() {
    if (!this.widgetAreaId) {
      return;
    }

    this._widgetAreaService
      .getWidgetAreaHtml(this.widgetAreaId)
      .subscribe((html) => {
        const hasContent = html?.length > 0;

        if (!hasContent) {
          return;
        }

        this._updateWidgetHtml(html);
      });
  }

  private _updateWidgetHtml(html: string) {
    const container = this._elementRef.nativeElement as HTMLElement;
    
    /**
     * Creating a "contextual fragment" will allow the execution
     * of any scripts included in the received HTML.
     */
    const range = document.createRange();
    const fragment = range.createContextualFragment(html);
    container.appendChild(fragment);
  }
}
