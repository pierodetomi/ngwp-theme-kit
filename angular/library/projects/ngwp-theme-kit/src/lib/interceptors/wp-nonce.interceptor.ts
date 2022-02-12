import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WpConfigurationService } from '../services/wp-configuration.service';

@Injectable()
export class WpNonceInterceptor implements HttpInterceptor {
  constructor(private _wpConfigurationService: WpConfigurationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authConfig = this._wpConfigurationService.configuration.auth;

    if (!authConfig?.isAuthenticated) {
      return next.handle(req);
    }

    const authenticatedRequest = req.clone({
      headers: req.headers.append('X-WP-Nonce', authConfig.nonce)
    });

    return next.handle(authenticatedRequest);
  }
}