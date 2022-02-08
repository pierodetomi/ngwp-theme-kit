import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class WpNonceInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (environment.wpVariables.api?.nonce) {
      const authenticatedRequest = req.clone({ headers: req.headers.append('X-WP-Nonce', environment.wpVariables.api.nonce) });
      return next.handle(authenticatedRequest);
    }
    
    return next.handle(req);
  }
}