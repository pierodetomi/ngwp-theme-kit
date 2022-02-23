import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule, Type } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ThemeTemplateHostComponent } from './components/theme-template-host/theme-template-host.component';
import { ThemeRuntimeConfig } from './config/theme-runtime.config';
import { ThemeSettingDirective } from './directives/theme-setting.directive';
import { WidgetAreaDirective } from './directives/widget-area.directive';
import { WpNonceInterceptor } from './interceptors/wp-nonce.interceptor';
import { WpUnauthorizedInterceptor } from './interceptors/wp-unauthorized.interceptor';

const catchAll: Routes = [
  { path: '**', component: ThemeTemplateHostComponent }
];

@NgModule({
  declarations: [
    ThemeTemplateHostComponent,
    ThemeSettingDirective,
    WidgetAreaDirective
  ],
  imports: [
    HttpClientModule,
    RouterModule.forRoot(catchAll)
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: WpNonceInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: WpUnauthorizedInterceptor, multi: true }
  ],
  exports: [
    ThemeSettingDirective,
    WidgetAreaDirective
  ]
})
export class NgwpThemeKitModule {
  public static setPageTemplate(pageTemplateComponent: Type<unknown>) {
    ThemeRuntimeConfig.pageTemplateComponent = pageTemplateComponent;
  }

  public static setPostTemplate(postTemplateComponent: Type<unknown>) {
    ThemeRuntimeConfig.postTemplateComponent = postTemplateComponent;
  }
}
