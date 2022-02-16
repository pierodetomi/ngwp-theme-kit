import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { IImageAttachment } from '../models/image-attachment.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class ThemeSettingsService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/ngwp/theme-settings';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public get<T>(name: string, defaultValue: string = null, ...additionalParams: { key: string, value: string }[]): Observable<T> {
    let url = `${this.baseUrl}?name=${encodeURIComponent(name)}`;

    if (defaultValue?.length > 0) {
      url = `${url}&default=${encodeURIComponent(defaultValue)}`;
    }

    if (additionalParams?.length > 0) {
      additionalParams.forEach(parameter => {
        url = `${url}&${parameter.key}=${encodeURIComponent(parameter.value)}`;
      });
    }

    return this._http
      .get(url)
      .pipe(map((response) => {
        const settingRawValue = (response as any).setting;

        if (additionalParams.some(_ => _.key === 'type' && _.value === 'img')) {
          const imageAttachment = {
            src: settingRawValue[0],
            width: settingRawValue[1],
            height: settingRawValue[2],
            isResized: settingRawValue[3]
          } as IImageAttachment;

          return imageAttachment as any; // This is required only for TS compilation purposes...
        }

        return settingRawValue as T;
      }));
  }

  public getImage(name: string, size = 'thumbnail', icon = false): Observable<IImageAttachment> {
    const additionalParams = [
      { key: 'type', value: 'img' },
      { key: 'size', value: size },
      { key: 'icon', value: icon ? 'true' : 'false' }
    ];
    return this.get<IImageAttachment>(name, undefined, ...additionalParams);
  }
}
