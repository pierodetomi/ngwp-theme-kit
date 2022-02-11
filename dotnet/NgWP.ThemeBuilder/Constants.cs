namespace NgWP.ThemeBuilder
{
    public static class Constants
    {
        public static class Paths
        {
            public const string IndexStart = "Resources/index-start.php";
        }

        public static class PageFragments
        {
            public const string PageStart = @"
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
?>
";

            public const string StyleStart = @"
/*
Theme Name: {{name}}
Theme URI: https://github.com/tbd/
Author: {{author}}
Author URI: https://pierodetomi.it/
Description: {{description}}
Requires at least: 5.9
Tested up to: 5.9
Requires PHP: 5.6
Version: 1.0
License: GNU General Public License v2 or later
License URI: http://www.gnu.org/licenses/gpl-2.0.html
Text Domain: wpngtheme
Tags: angular

{{name}} WordPress Theme, (C) {{year}} {{author}}
{{name}} is distributed under the terms of the GNU GPL.
*/
";

            public const string WPVariables = @"
<script type=""text/javascript"">
    window._wpConfiguration = {
        siteUrl: '<?php echo $siteUrl; ?>',
        auth: {
            isAuthenticated: <?php echo is_user_logged_in(); ?>,
            userId: <?php echo $user_id; ?>,
            nonce: '<?php echo $nonce; ?>'
        }
    };
</script>
";

            public const string ScriptBaseUrl = "<?php echo $baseUrl; ?>/";

            public const string FunctionsStart = @"
<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
} // Exit if accessed directly

require_once(""inc/helpers/theme-helper.php"");

add_action(""customize_register"", ""configure_customizer"");
";
        }
    }
}
