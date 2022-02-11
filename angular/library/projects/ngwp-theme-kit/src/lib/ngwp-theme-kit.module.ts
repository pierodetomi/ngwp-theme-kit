import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { WpNonceInterceptor } from './interceptors/wp-nonce.interceptor';
import { WpUnauthorizedInterceptor } from './interceptors/wp-unauthorized.interceptor';

@NgModule({
  declarations: [],
  imports: [
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: WpNonceInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: WpUnauthorizedInterceptor, multi: true }
  ],
  exports: []
})
export class NgwpThemeKitModule { }
