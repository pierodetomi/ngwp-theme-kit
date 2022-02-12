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