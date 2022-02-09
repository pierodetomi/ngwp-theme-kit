import { WpConfigurationService } from "./wp-configuration.service";

export abstract class BaseService {
  constructor(private _wpConfigurationService: WpConfigurationService) { }

  protected get siteUrl(): string {
    let siteUrl = this._wpConfigurationService.configuration.siteUrl;

    if (siteUrl.endsWith('/')) {
      siteUrl = siteUrl.substring(0, siteUrl.length - 1);
    }

    return siteUrl;
  }
}
