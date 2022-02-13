import { Component, OnInit } from '@angular/core';
import { IImageAttachment, IPost, PostService, ThemeSettingsService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  public title = '';
  
  public logo: IImageAttachment = null;

  public posts: IPost[] = [];

  public settings: any = null;

  constructor(private _postService: PostService, private _themeSettingsService: ThemeSettingsService) { }

  ngOnInit(): void {
    this._themeSettingsService
      .get<string>('title')
      .subscribe((title) => {
        this.title = title;
      });

    this._themeSettingsService
      .getImage('logo')
      .subscribe((logo) => {
        this.logo = logo;
      });

    this._postService
      .get(1, 10)
      .subscribe((posts) => {
        this.posts = posts;
      });
  }
}
