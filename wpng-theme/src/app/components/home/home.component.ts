import { Component, OnInit } from '@angular/core';
import { IPost } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';

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
