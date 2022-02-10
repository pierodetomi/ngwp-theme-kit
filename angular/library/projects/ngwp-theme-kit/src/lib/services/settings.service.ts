import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService extends BaseService {
  private get _baseUrl(): string {
    return `${this.siteUrl}/wp-json/wp/v2/settings`;
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public get(): Observable<any> {
    const url = this._baseUrl;
    return this._http.get(url);
  }
}
