<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
} // Exit if accessed directly

require_once("inc/helpers/theme-helper.php");

{{theme-support-entries}}
// ES:
// add_theme_support("title-tag");
// add_theme_support("post-thumbnails");

add_action("customize_register", "configure_customizer");
//add_action("after_setup_theme", "register_primary_menu");
//add_action("admin_menu", "update_admin_menu");

function configure_customizer($wp_customize) {
    {{sections-code}}
    
    {{settings-code}}
}

// if( function_exists("get_theme_file_path") ){
// 	/** @noinspection PhpIncludeInspection */
// 	require_once( get_theme_file_path( '/inc/oneartist-framework.php' ) );
// }
// else{
// 	/** @noinspection PhpIncludeInspection */
// 	require_once( get_template_directory() . '/inc/oneartist-framework.php' );
// }

// $oneartistFramework = new OneArtistFramework();
// $oneartistFramework->construct();
