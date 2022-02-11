import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../models/user.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/wp/v2/users';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public get(
    pageNumber = 1,
    pageSize = 10,
    context: 'view' | 'embed' | 'edit' = 'view',
    search: string | null = null
  ): Observable<IUser[]> {

    const params = [];
    
    params.push(`page=${pageNumber}`);
    params.push(`per_page=${pageSize}`);
    
    if (search !== null && search.length > 0) {
      params.push(`search=${search}`);
    }

    if (context?.length > 0) {
      params.push(`context=${context}`);
    }
    
    const url = `${this.baseUrl}?${params.join('&')}`;
    return this._http.get<IUser[]>(url);
  }

  public getById(id: number): Observable<IUser> {
    const url = `${this.baseUrl}/${id}`;
    return this._http.get<IUser>(url);
  }
}
