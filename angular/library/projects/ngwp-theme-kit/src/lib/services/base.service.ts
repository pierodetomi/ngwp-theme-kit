import { WpConfigurationService } from './wp-configuration.service';

export abstract class BaseService {
  private _siteUrl: string;

  public abstract get endpointPath(): string;

  constructor(private _wpConfigurationService: WpConfigurationService) { }

  protected get siteUrl(): string {
    if (this._siteUrl) {
      return this._siteUrl;
    }
    
    this._siteUrl = this._wpConfigurationService.configuration.siteUrl;

    if (this._siteUrl.endsWith('/')) {
      this._siteUrl = this._siteUrl.substring(0, this._siteUrl.length - 1);
    }

    return this._siteUrl;
  }

  protected get baseUrl(): string {
    return `${this.siteUrl}${this.endpointPath}`;
  }
}
