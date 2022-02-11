import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMedia } from '../models/media.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class MediaService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/media';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public getById(id: number): Observable<IMedia> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<IMedia>(url);
  }
}
