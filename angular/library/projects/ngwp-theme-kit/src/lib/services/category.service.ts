import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICategory } from '../models/category.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/categories';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public get(): Observable<ICategory[]> {
    const url = this.baseUrl;
    return this._http.get<ICategory[]>(url);
  }

  public getById(id: number): Observable<ICategory> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<ICategory>(url);
  }
}
