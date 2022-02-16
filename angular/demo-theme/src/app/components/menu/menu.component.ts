import { Component, Input, OnInit } from '@angular/core';
import { IMenu, ThemeMenusService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.less']
})
export class MenuComponent implements OnInit {
  @Input() public location: string;

  public menu: IMenu;

  constructor(private _themeMenusService: ThemeMenusService) { }

  ngOnInit(): void {
    this._themeMenusService
      .get(this.location)
      .subscribe((menu) => {
        this.menu = menu;
      })
  }
}
