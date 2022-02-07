import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { IWordPressVariables } from './app/models/wordpress.variables';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

if ((window as any)._wpVariables) {
  const wpVariables = (window as any)._wpVariables as IWordPressVariables;
  environment.wpVariables = wpVariables;
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
