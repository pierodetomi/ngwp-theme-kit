import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();

} else {
  // Local testing mode
  (window as any)._wpConfiguration = {
    siteUrl: 'http://wpdev03.local',
    auth: {
      isAuthenticated: false,
      userId: 0,
      nonce: ''
    },
    demo: true
  };
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
