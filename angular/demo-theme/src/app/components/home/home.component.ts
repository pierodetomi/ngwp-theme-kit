import { Component, OnInit } from '@angular/core';
import { IPost, PostService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {
  public posts: IPost[] = [];

  constructor(private _postService: PostService) { }

  ngOnInit(): void {
    this._postService
      .get(1, 10)
      .subscribe((posts) => {
        this.posts = posts;
      });
  }
}
