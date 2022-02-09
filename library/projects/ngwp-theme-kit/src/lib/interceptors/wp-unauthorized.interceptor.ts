import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { WpConfigurationService } from "../services/wp-configuration.service";

@Injectable()
export class WpUnauthorizedInterceptor implements HttpInterceptor {
  constructor(private _wpConfigurationService: WpConfigurationService) { }
  
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(tap(() => { },
      (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status !== 401) {
            return;
          }

          const redirectUrl = encodeURIComponent(window.location.href)
          window.location.href = `${this._wpConfigurationService.configuration.siteUrl}/wp-login.php?redirect_to=${redirectUrl}`;
        }
      }));
  }
}