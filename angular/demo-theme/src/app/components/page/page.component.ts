import { Component, OnInit } from '@angular/core';
import { WpConfigurationService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.less']
})
export class PageComponent implements OnInit {
  public id = 0;

  constructor(private _wpConfigurationService: WpConfigurationService) { }

  ngOnInit(): void {
    const postId = this._wpConfigurationService.configuration.postOrPageId;

    console.log(`PAGE id: ${postId}`);
    this.id = postId;
  }
}
