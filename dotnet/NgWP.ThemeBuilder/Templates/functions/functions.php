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
add_action("customize_controls_enqueue_scripts", "register_customizer_js");

function register_theme_menus() {
    {{menus-registration-code}}

    {{menus-creation-code}}
}

function configure_customizer($wp_customize) {
    {{sections-code}}
    
    {{settings-code}}
}

function register_customizer_js() {
    wp_enqueue_script(
        "custom-customize",
        get_template_directory_uri()."/js/customizer.js",
        array("jquery", "customize-controls"),
        false,
        true
    );
}