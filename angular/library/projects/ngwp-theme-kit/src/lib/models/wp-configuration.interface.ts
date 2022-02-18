export interface IWpConfiguration {
  siteUrl: string;
  auth: {
    isAuthenticated: boolean;
    userId: number;
    nonce: string;
  };
  isCustomizer: boolean;
  isPost: boolean;
  isPage: boolean;
  postOrPageId?: number;
  customizerService?: {
    registerPreviewCallback: (callback: (settingId: string, settingValue: any) => void) => void;
  };
}
