<?php
    /**
     * @package {{theme-name}}
     */

    if ( ! defined( 'ABSPATH' ) ) {
	    exit; // Exit if accessed directly.
    }

    require_once("inc/helpers/request-helper.php");

    $is_get_request = $_SERVER["REQUEST_METHOD"] === "GET";
    $is_ngwp_api_request = $is_get_request && isset($_GET["mode"]) && htmlspecialchars($_GET["mode"]) == "ngwp_api";

    if ($is_ngwp_api_request) {
        header('Content-Type: application/json; charset=utf-8');
        
        $nonce = try_ead_nonce_from_headers();
        $is_request_authenticated = wp_verify_nonce($nonce, "wp_rest");
        
        $method = htmlspecialchars($_GET["api_method"]);

        switch ($method) {
            case "get_setting":
                

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
                break;

            // case "todo --- other case":
            //     // Example: require that the user is currently authenticated
            //     if (!$is_request_authenticated) {
            //         header("HTTP/1.1 401 Unauthorized");
            //         exit;
            //     }
            //     break;

            default:
                http_response_code(404);
                exit;
        }
        
        http_response_code(200);
        echo json_encode($json);
        exit;
    }

    $baseUrl = esc_url( get_template_directory_uri() );
    $siteUrl = esc_url( site_url() );
    $nonce = wp_create_nonce("wp_rest");
    $user_id = get_current_user_id();
?><!DOCTYPE html>

<html <?php language_attributes(); ?>>
<head>
    <meta charset="<?php bloginfo( 'charset' ); ?>">
    <link rel="profile" href="https://gmpg.org/xfn/11">

    {{head-code}}

    <?php wp_head(); ?>
</head>
<body <?php body_class(); ?>>
    <?php wp_body_open(); ?>

    {{body-code}}

    <script type=""text/javascript"">
        window._wpConfiguration = {
            siteUrl: '<?php echo $siteUrl; ?>',
            auth: {
                isAuthenticated: <?php if (is_user_logged_in() == 1 || is_user_logged_in() == true) { echo "true"; } else { echo "false"; } ?>,
                userId: <?php echo $user_id; ?>,
                nonce: '<?php echo $nonce; ?>'
            }
        };
    </script>

    {{scripts-code}}

    <?php wp_footer(); ?>
</body>
</html>