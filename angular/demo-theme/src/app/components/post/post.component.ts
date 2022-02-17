import { Component, OnInit } from '@angular/core';
import { WpConfigurationService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.less']
})
export class PostComponent implements OnInit {
  public id = 0;

  constructor(private _wpConfigurationService: WpConfigurationService) { }
  
  ngOnInit(): void {
    const postId = this._wpConfigurationService.configuration.postOrPageId;

    console.log(`POST id: ${postId}`);
    this.id = postId;
  }
}
