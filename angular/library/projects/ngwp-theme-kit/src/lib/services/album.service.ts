import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../models/post.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class AlbumService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/albums';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public getById(id: number): Observable<IPost> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<IPost>(url);
  }

  public get(pageNumber: number, pageSize: number, search: string, categories: number[]): Observable<IPost[]> {
    let url = `${this.baseUrl}?page=${pageNumber}&per_page=${pageSize}`;

    if (search?.length > 0)
      url = `${url}&search=${encodeURIComponent(search)}`;

    if (categories?.length > 0) {
      let categoriesCsv = null;

      for (let i = 0; i < categories.length; i++) {
        if (i === 0) categoriesCsv = `${categories[i]}`;
        else categoriesCsv = `${categoriesCsv},${categories[i]}`;
      }

      url = `${url}&categories=${categoriesCsv}`;
    }

    return this._http.get<IPost[]>(url);
  }
}
