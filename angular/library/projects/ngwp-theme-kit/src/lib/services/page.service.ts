import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../models/post.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class PageService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/pages';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public getById(id: number): Observable<IPost> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<IPost>(url);
  }
}
