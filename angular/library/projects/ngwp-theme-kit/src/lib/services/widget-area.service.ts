import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class WidgetAreaService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/ngwp/widget-areas';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public getWidgetAreaHtml(id: string): Observable<string> {
    const url = `${this.baseUrl}?id=${encodeURIComponent(id)}`;
    return this._http.get(url, { responseType: 'text' });
  }
}
