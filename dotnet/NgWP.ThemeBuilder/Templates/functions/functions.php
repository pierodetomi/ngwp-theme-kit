<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
} // Exit if accessed directly

require_once("inc/helpers/theme-helper.php");

{{theme-support-entries}}

add_action("customize_register", "configure_customizer");

function configure_customizer($wp_customize) {
    {{sections-code}}
    
    {{settings-code}}
}