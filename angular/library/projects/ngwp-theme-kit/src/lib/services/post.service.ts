import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../models/post.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/posts';
  }
  
  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public getById(id: number): Observable<IPost> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<IPost>(url);
  }

  public get(pageNumber: number, pageSize: number, search: string | null = null, categories: number[] | null = null): Observable<IPost[]> {
    let url = `${this.baseUrl}?page=${pageNumber}&per_page=${pageSize}`;

    if (search !== undefined && search != null && search?.length > 0)
      url = `${url}&search=${encodeURIComponent(search)}`;

    if (categories !== undefined && categories !== null && categories.length > 0) {
      let categoriesCsv = '';

      for (let i = 0; i < categories.length; i++) {
        if (i === 0) categoriesCsv = `${categories[i]}`;
        else categoriesCsv += `,${categories[i]}`;
      }

      url = `${url}&categories=${categoriesCsv}`;
    }

    return this._http.get<IPost[]>(url);
  }
}
