export interface IWpConfiguration {
  siteUrl: string;
  auth: {
    isAuthenticated: boolean,
    userId: number,
    nonce: string
  };
}
