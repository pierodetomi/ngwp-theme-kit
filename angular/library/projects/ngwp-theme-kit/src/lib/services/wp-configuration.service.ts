import { Injectable } from '@angular/core';
import { IWpConfiguration } from '../models/wp-configuration.interface';

@Injectable({
  providedIn: 'root'
})
export class WpConfigurationService {
  public configuration: IWpConfiguration = {} as IWpConfiguration;

  private get _hasWpConfiguration(): boolean {
    const w = window as any;
    return w._wpConfiguration !== undefined && w._wpConfiguration !== null;
  }

  constructor() {
    this._readWpConfiguration();
  }

  private _readWpConfiguration() {
    if (this._hasWpConfiguration) {
      this.configuration = (window as any)._wpConfiguration as IWpConfiguration;
    }
  }
}
