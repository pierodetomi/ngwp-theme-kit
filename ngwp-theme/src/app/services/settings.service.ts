import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService extends BaseService {
  private get _baseUrl(): string {
    return `${this.siteUrl}/wp-json/wp/v2/settings`;
  }

  constructor(private _http: HttpClient) {
    super();
  }

  public get(): Observable<any> {
    const url = this._baseUrl;
    return this._http.get(url);
  }
}
