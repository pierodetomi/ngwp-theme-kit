import { Component, OnInit } from '@angular/core';
import { IPost } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  public posts: IPost[] = [];

  public settings: any = null;

  constructor(private _postService: PostService, private _settingsService: SettingsService) { }

  ngOnInit(): void {
    this._postService
      .get(1, 10)
      .subscribe((posts) => {
        this.posts = posts;
      });

    this._settingsService
      .get()
      .subscribe((settings) => {
        this.settings = settings;
      });
  }
}
