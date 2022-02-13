/*
 * Public API Surface of ngwp-theme-kit
 */

export * from './lib/ngwp-theme-kit.module';

// Models
export { ICategory } from './lib/models/category.interface';
export { IImageAttachment } from './lib/models/image-attachment.interface';
export { IMedia } from './lib/models/media.interface';
export { IPost } from './lib/models/post.interface';
export { IUser } from './lib/models/user.interface';
export { IWpConfiguration } from './lib/models/wp-configuration.interface';

// Services
export * from './lib/services/post.service';
export * from './lib/services/settings.service';
export * from './lib/services/theme-settings.service';
export * from './lib/services/wp-configuration.service';