/*
 * Public API Surface of ngwp-theme-kit
 */

export * from './lib/ngwp-theme-kit.service';
export * from './lib/ngwp-theme-kit.component';
export * from './lib/ngwp-theme-kit.module';

// Models
export { IPost } from './lib/models/post.interface';
export { IWpConfiguration } from './lib/models/wp-configuration.interface';

// Services
export * from './lib/services/post.service';
export * from './lib/services/settings.service';
export * from './lib/services/wp-configuration.service';