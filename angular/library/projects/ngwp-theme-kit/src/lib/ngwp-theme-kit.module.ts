import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { WpNonceInterceptor } from './interceptors/wp-nonce.interceptor';
import { WpUnauthorizedInterceptor } from './interceptors/wp-unauthorized.interceptor';
import { PostService } from './services/post.service';
import { SettingsService } from './services/settings.service';
import { WpConfigurationService } from './services/wp-configuration.service';

@NgModule({
  declarations: [],
  imports: [],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: WpNonceInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: WpUnauthorizedInterceptor, multi: true },
    PostService,
    SettingsService,
    WpConfigurationService
  ],
  exports: []
})
export class NgwpThemeKitModule { }
