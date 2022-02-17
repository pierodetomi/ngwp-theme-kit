import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgwpThemeKitModule } from 'ngwp-theme-kit';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { MenuComponent } from './components/menu/menu.component';
import { PageComponent } from './components/page/page.component';
import { PostComponent } from './components/post/post.component';
import { TestComponent } from './components/test/test.component';

NgwpThemeKitModule.setPageTemplate(PageComponent);
NgwpThemeKitModule.setPostTemplate(PostComponent);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TestComponent,
    MenuComponent,
    PageComponent,
    PostComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgwpThemeKitModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
