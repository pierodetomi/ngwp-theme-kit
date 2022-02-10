**NgWP Theme Kit** is an Angular module that enables the creation of **WordPress** themes using **Angular**.

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

# Adding build scripts to package.json
The last step is to modify the ```package.json``` file in order to add the scripts that will bundle up your WordPress theme.

Add the following scripts to the ```package.json``` file inside your Angular project:

```json
{
  ...

  "scripts": {
    ...

    "build-theme": "ng build && \"./node_modules/ngwp-theme-kit/bin/NgWP.ThemeBuilder.exe\" {{dist-path}}",
    "build-theme-prod": "ng build --prod && \"./node_modules/ngwp-theme-kit/bin/NgWP.ThemeBuilder.exe\" {{dist-path}}"

    ...
  }

  ...
}
```

Here you'll have to replace the ```{{dist-path}}``` token with your actual application's dist path (usually ```dist/<application name>```).

## Develop the theme
TODO

## Build the theme package
TODO

## Install in WordPress
TODO

## See your theme in action
TODO