import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { WpConfigurationService } from "../services/wp-configuration.service";

@Injectable()
export class WpNonceInterceptor implements HttpInterceptor {
  constructor(private _wpConfigurationService: WpConfigurationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiConfig = this._wpConfigurationService.configuration.api;

    if (!apiConfig || !apiConfig?.nonce) {
      return next.handle(req);
    }

    const authenticatedRequest = req.clone({
      headers: req.headers.append('X-WP-Nonce', apiConfig.nonce)
    });

    return next.handle(authenticatedRequest);
  }
}