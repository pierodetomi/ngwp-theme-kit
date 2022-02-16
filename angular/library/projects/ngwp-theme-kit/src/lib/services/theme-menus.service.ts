import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { IMenuItem } from '../models/menu-item.interface';
import { IMenu } from '../models/menu.interface';
import { BaseService } from './base.service';
import { WpConfigurationService } from './wp-configuration.service';

@Injectable({
  providedIn: 'root'
})
export class ThemeMenusService extends BaseService {
  public get endpointPath(): string {
    return '/wp-json/ngwp/theme-menus';
  }

  constructor(private _http: HttpClient, wpConfigurationService: WpConfigurationService) {
    super(wpConfigurationService);
  }

  public get(location: string): Observable<IMenu> {
    const url = `${this.baseUrl}?location=${location}`;
    
    return this._http
      .get<IMenu>(url)
      .pipe(map((menu) => {
        return this._buildMenuHierarchy(menu);
      }));
  }

  private _buildMenuHierarchy(menu: IMenu): IMenu {
    const rootItemsToRemove: IMenuItem[] = [];

    menu.items.forEach(item => {
      const hasParent = item.menu_item_parent !== '0';
      const parentId = hasParent ? +item.menu_item_parent : 0;

      if (hasParent) {
        // Find the parent menu item & add current item to its children
        const parent = menu.items.find(_ => _.ID === parentId);
        
        if (!parent.children)
          parent.children = [];

        parent.children.push(item);

        // Mark this item as "to remove" from the root level of menu items
        rootItemsToRemove.push(item);
      }
    });

    rootItemsToRemove.forEach(itemToRemove => {
      const indexToRemove = menu.items.indexOf(itemToRemove);
      menu.items.splice(indexToRemove, 1);
    });

    return menu;
  }
}
