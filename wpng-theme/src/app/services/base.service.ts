import { environment } from "src/environments/environment";

export abstract class BaseService {
  protected get baseUrl(): string {
    let siteUrl = environment.wpVariables.siteUrl;

    if (siteUrl.endsWith('/')) {
      siteUrl = siteUrl.substring(0, siteUrl.length - 1);
    }

    return `${siteUrl}/wp-json/wp/v2/posts`;
  }
}
