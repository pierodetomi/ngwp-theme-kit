<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
} // Exit if accessed directly

require_once("inc/helpers/request-helper.php");
require_once("inc/api/custom-endpoints.php");

require_once("inc/helpers/theme-helper.php");

require_once("options.php");

{{theme-support-entries}}

add_action("init", "register_theme_menus");
add_action("customize_register", "configure_customizer");

function register_theme_menus() {
    {{menus-registration-code}}

    {{menus-creation-code}}
}

function configure_customizer($wp_customize) {
    {{sections-code}}
    
    {{settings-code}}
}