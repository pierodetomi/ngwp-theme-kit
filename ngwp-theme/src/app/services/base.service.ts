import { environment } from "src/environments/environment";

export abstract class BaseService {
  protected get siteUrl(): string {
    let siteUrl = environment.wpVariables.siteUrl;

    if (siteUrl.endsWith('/')) {
      siteUrl = siteUrl.substring(0, siteUrl.length - 1);
    }

    return siteUrl;
  }
}
