<?php

class ThemeHelper {
    public static function add_section($wp_customize, $section_id, $title, $description) {
        $wp_customize->add_section($section_id, array(
            "title" => __( $title ),
            "description" => __( $description ),
            "panel" => "", // Not typically needed.
            "capability" => "edit_theme_options"
        ) );
    }
    
    public static function add_setting($wp_customize, $setting_id, $section, $label, $description, $default, $sanitize_callback, $control_type) {
        $wp_customize->add_setting($setting_id, array(
            "type" => "theme_mod",
            "capability" => "edit_theme_options",
            "default" => $default,
            "transport" => "refresh", // or postMessage
            "sanitize_callback" => $sanitize_callback
        ));

        $wp_customize->add_control($setting_id, array(
            "type" => $control_type,
            //"priority" => $order, // Within the section.
            "section" => $section, // Required, core or custom.
            "label" => __( $label ),
            "description" => __( $description )
        ));
    }

    public static function add_setting_text($wp_customize, $setting_id, $section, $label, $description = null, $default = null) {
        $setting_options = array(
            "type" => "theme_mod",
            "capability" => "edit_theme_options",
            "default" => "",
            "transport" => "refresh", // or postMessage
        );

        if($default != null)
            $setting_options["default"] = $default;
        
        $control_options = array(
            "type" => "text",
            "section" => $section, // Required, core or custom.
            "label" => __( $label ),
        );
        
        if($description != null)
            $control_options["description"] = __( $description );
        
        $wp_customize->add_setting($setting_id, $setting_options);
        $wp_customize->add_control($setting_id, $control_options);
    }

    public static function add_setting_textarea($wp_customize, $setting_id, $section, $label, $description = null, $default = null) {
        $setting_options = array(
            "type" => "theme_mod",
            "capability" => "edit_theme_options",
            "default" => "",
            "transport" => "refresh", // or postMessage
        );

        if($default != null)
            $setting_options["default"] = $default;
            
        $control_options = array(
            "type" => "textarea",
            "section" => $section, // Required, core or custom.
            "label" => __( $label ),
        );
            
        if($description != null)
            $control_options["description"] = __( $description );
            
        $wp_customize->add_setting($setting_id, $setting_options);
        $wp_customize->add_control($setting_id, $control_options);
    }

    public static function add_setting_img($wp_customize, $setting_id, $section, $label, $description) {
        $wp_customize->add_setting($setting_id, array(
            "type" => "theme_mod",
            "capability" => "edit_theme_options",
            "default" => "",
            "transport" => "refresh", // or postMessage
        ));

        $imgControl = new WP_Customize_Media_Control($wp_customize, $setting_id, array(
            "label" => __( $label ),
            "description" => __( $description ),
            'section' => $section,
            'mime_type' => 'image',
          ) );

        $wp_customize->add_control($imgControl);
    }

    public static function create_menu_if_not_existing($name, $location, $entries) {
        // Check if the menu exists
        $is_menu_existing = wp_get_nav_menu_object($name);

        if ($is_menu_existing) {
            return;
        }
 
        // If it doesn't exist, let's create it.
        $menu_id = wp_create_nav_menu($name);

        // Set up default menu items
        foreach ($entries as $entry) {
            wp_update_nav_menu_item($menu_id, 0, array(
                "menu-item-title"   =>  __($entry, "textdomain"),
                "menu-item-url"     => "#",
                "menu-item-status"  => "publish"
            ));
        }

        // Set menu location
        $locations = get_theme_mod("nav_menu_locations");
        $locations[$location] = $menu_id;
        set_theme_mod("nav_menu_locations", $locations);
    }
}