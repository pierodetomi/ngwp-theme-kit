**NgWP Theme Kit** is an Angular library that enables the creation of **WordPress** themes using **Angular**.

It follows the principle of _headless cms_, using the **WordPress REST APIs**.

# Getting Started

## Prerequisites
In order to make use of this module you need (of course) an Angular application.
Therefore you should create an app (if you hadn't already):

```bash
$ ng new my-app --routing
```

## Installing the library
You can install **NgWP Theme Kit** with **npm**:

```bash
$ npm i ngwp-theme-kit
```

## Importing the Angular module
Now you must import the ```NgwpThemeKitModule``` in your main module (usually ```app.module.ts```).

An example:

```js
...

import { NgwpThemeKitModule } from 'ngwp-theme-kit';

...

@NgModule({
  ...
  imports: [
    ...

    NgwpThemeKitModule

    ...
  ],
  ...
})
```

## Configuring page & post templates
In order to correctly bind to the page & post (single) WordPress templates, you must create to dedicated components: one component that will render the page content and one that will render the post content.

Once you have these two components, you must call the static registration methods provided by the ```NgwpThemeKitModule``` module, as follows:

```Typescript
NgwpThemeKitModule.setPageTemplate(PageComponent);
NgwpThemeKitModule.setPostTemplate(PostComponent);
```

You must do this registration as soon as possible in your application lifecycle. The suggested position is just before the declaration of your application's main module.

## Adding a ```themeconfig.json``` Configuration File
This is the configuration file that the custom build tool will use to generate the WordPress theme.

Here you specify some key information about your theme (such as its ```name```, ```author``` and a ```description```) along with supported **theme features** (e.g. ```post-thumbnails```) and the **customizer options**.

For example, here is the content of the [```themeconfig.json```](https://github.com/pierodetomi/ngwp-theme-kit/blob/master/angular/demo-theme/themeconfig.json) of the [**Demo Theme**](https://github.com/pierodetomi/ngwp-theme-kit/tree/master/angular/demo-theme) sample application that you can find in the repository of the library:

```json
{
  "name": "Demo Theme",
  "description": "A WordPress template developed with Angular frontend",
  "author": "Piero De Tomi",
  "themeFeatures": [
    "menus"
  ],
  "menus": [{
    "location": "header-menu",
    "description": "Header",
    "initialName": "Header Menu",
    "initialEntries": [ "Home", "Blog", "About Us", "Privacy" ],
    "onlyRegisterLocation": false
  },{
    "location": "footer-menu",
    "description": "Footer",
    "initialName": "Footer Menu",
    "initialEntries": [ "Footer Item 1", "Footer Item 2", "Footer Item 3", "Footer Item 4" ],
    "onlyRegisterLocation": false
  },{
    "location": "sidebar-menu",
    "description": "Sidebar Menu",
    "onlyRegisterLocation": true
  }],
  "sections": [{
    "id": "main",
    "name": "Main Config",
    "description": "Main configuration parameters",
    "settings": [{
      "id": "title",
      "label": "Title",
      "description": "The title of this blog",
      "defaultValue": null,
      "controlType": "text"
    },{
      "id": "author",
      "label": "Author",
      "description": "The author of this blog",
      "defaultValue": null,
      "controlType": "text"
    },{
      "id": "logo",
      "label": "Logo",
      "description": "The website logo",
      "defaultValue": null,
      "controlType": "image"
    }]
  }],
  "widgetAreas": [{
      "id": "header-widgets",
      "name": "Header Widgets",
      "description": "Widgets that will appear in the header of the page"
    },
    {
      "id": "sidebar-widgets",
      "name": "Sidebar Widgets",
      "description": "Widgets that will appear in the sidebar of the website"
    }
  ]
}
```

## Adding the Custom Build Script to ```package.json```
The last step is to modify the ```package.json``` file in order to add the scripts that will bundle up your WordPress theme.

Edit the ```build``` script inside the ```package.json``` file of your Angular project:

```json
{
  ...

  "scripts": {
    ...

    "build": "ng build && \"./node_modules/ngwp-theme-kit/bin/NgWP.ThemeBuilder.exe\" {{dist-path}} {{path-to-themeconfig.json}}",
    
    ...
  }

  ...
}
```

Here you'll have to replace:
- The ```{{dist-path}}``` token with your actual application's dist path (usually ```dist/<application name>```)
- The ```{{path-to-themeconfig.json}}``` token with the actual file path of your ```themeconfig.json``` configuration file

## Develop the theme
Now you can develop your Wordpress theme using Angular, just as you would develop any other Angular application.

You can make use of the services provided by the library, that will allow you to access Wordpress information (using WordPress REST API + some custom PHP API code).

For example, you can retrieve the first 10 posts with the following code:

```Typescript
export class Sample implements OnInit {
  public posts: IPost[] = [];

  constructor(private _postService: PostService) { }

  ngOnInit(): void {
    this._postService
      .get(1, 10)
      .subscribe((posts) => {
        this.posts = posts;
      });
  }
}
```

You can view some more example usages on the ```demo-theme``` application inside this repository.

## Test the theme (locally)
You can test some of the functionality of your theme locally with ```ng serve```.
You just need to update the ```siteUrl``` property in the ```main.ts``` file (in the "Local testing mode" section) setting it to your WordPress development environment.

```Typescript
// Local testing mode
  (window as any)._wpConfiguration = {
    siteUrl: '<<your_wp_site_url_here>>',
    auth: {
      isAuthenticated: false,
      userId: 0,
      nonce: ''
    },
    demo: true
  };
```

You'll be able to read "public" information as posts and categories, but you will **NOT** be able to access theme settings and more advanced information.
To do this you'll need to install & test your theme directly into WordPress (please read on for this).

## Build the theme package
In order to build your theme and make it ready for WordPress, you just need to run the command that you previously added to ```package.json``` file:

```bash
npm run build
```

However, you can rename this script as you like. You know... it's only an npm script after all.

## Install in WordPress
After building the theme, the ```dist``` folder of your application ({{application-name}} will be your application name) will contain your theme ready to be installed in WordPress as any other theme.

The package will contain a default theme thumbnail (the ```screenshot.png``` file) that you can always replace with your own if you wish.

## See your theme in action
Now it's time to see your theme in action! Open your WordPress website and have fun :)