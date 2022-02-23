<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
} // Exit if accessed directly

add_action("rest_api_init", function() {
    register_rest_route("ngwp", "/theme-settings/", array("methods" => "GET", "callback" => "get_theme_setting"));
    register_rest_route("ngwp", "/theme-settings/", array("methods" => "POST", "callback" => "set_theme_setting"));
    register_rest_route("ngwp", "/theme-menus/", array("methods" => "GET", "callback" => "get_theme_menu"));
    register_rest_route("ngwp", "/widget-areas/", array("methods" => "GET", "callback" => "get_widget_area"));
});

function get_theme_setting() {
    $name = htmlspecialchars($_GET["name"]);
    $default = htmlspecialchars($_GET["default"]) ?: null;
    $setting = get_theme_mod($name, $default);

    if (isset($_GET["type"]) && htmlspecialchars($_GET["type"]) == "img") {
        $attachment_id = $setting;
        $size = htmlspecialchars($_GET["size"]) ?: "thumbnail";
        $icon = htmlspecialchars($_GET["icon"]) ?: false;
                    
        $setting = wp_get_attachment_image_src($attachment_id, $size, $icon);
    }
                
    $json = new stdclass();
    $json->setting = $setting;

    ok($json);
}

function set_theme_setting() {
    $nonce = try_read_nonce_from_headers();
    $is_request_authenticated = wp_verify_nonce($nonce, "wp_rest");

    if (!$is_request_authenticated) {
        unauthorized();
        exit;
    }

    $settings = json_decode(stripslashes(file_get_contents("php://input")));

    $json = new stdclass();
    $json->success = true;
    
    foreach ($settings as $name => $value)
        if (!set_theme_mod($name, $value)) {
            $json->success = false;
            ok($json);
        }
    
    ok($json);
}

function get_theme_menu() {
    $location = htmlspecialchars($_GET["location"]);
    $locations = get_nav_menu_locations();
    
    $menu_id = $locations[$location];
    $menu_items = wp_get_nav_menu_items($menu_id);

    $menu = wp_get_nav_menu_object($menu_id);
    $menu->items = $menu_items ?: [];
    
    ok($menu);
}

function get_widget_area() {
    $id = htmlspecialchars($_GET["id"]);
    
    header("Content-Type: text/html");
    dynamic_sidebar($id);
}

function ok($data) {
    header("Content-Type: application/json");
    
    http_response_code(200);
    echo json_encode($data);
    exit;
}

function unauthorized() {
    header("HTTP/1.1 401 Unauthorized");
    exit;
}

function internal_server_error() {
    header("HTTP/1.1 500 Internal Server Error");
    exit;
}