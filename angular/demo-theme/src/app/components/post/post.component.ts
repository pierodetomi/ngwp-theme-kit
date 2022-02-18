import { Component, NgZone, OnInit } from '@angular/core';
import { WpConfigurationService } from 'ngwp-theme-kit';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.less']
})
export class PostComponent implements OnInit {
  public id = 0;

  public message: string = null;

  constructor(private _wpConfigurationService: WpConfigurationService, private _zone: NgZone) { }
  
  ngOnInit(): void {
    if (this._wpConfigurationService.configuration.isCustomizer) {
      this._wpConfigurationService.configuration.customizerService!.registerPreviewCallback(() => {
        this._zone.run(() => {
          this.message = 'Customizer has been updated!';
        });
      });
    }

    const postId = this._wpConfigurationService.configuration.postOrPageId;

    console.log(`POST id: ${postId}`);
    this.id = postId;
  }

  public doSomething() {
    this.message = 'Something happened!';
  }
}
