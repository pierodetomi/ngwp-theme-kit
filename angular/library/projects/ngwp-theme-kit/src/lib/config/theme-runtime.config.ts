import { Type } from '@angular/core';

export interface IThemeRuntimeConfig {
  pageTemplateComponent: Type<any>;
  postTemplateComponent: Type<any>;
}

export const ThemeRuntimeConfig: IThemeRuntimeConfig = {
  pageTemplateComponent: null,
  postTemplateComponent: null
};
