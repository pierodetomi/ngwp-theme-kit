<?php
    /**
     * @package NgWP
     */

    if ( ! defined( 'ABSPATH' ) ) {
	    exit; // Exit if accessed directly.
    }

    $baseUrl = esc_url( get_template_directory_uri() );
    $siteUrl = esc_url( site_url() );
    $nonce = wp_create_nonce( 'wp_rest' );
?>