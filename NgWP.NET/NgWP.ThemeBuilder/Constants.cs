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
?>
";

            public const string StyleStart = @"
/*
Theme Name: {{themeName}}
Theme URI: https://github.com/tbd/
Author: Piero De Tomi
Author URI: https://pierodetomi.it/
Description: WordPress template developed with Angular frontend
Requires at least: 5.9
Tested up to: 5.9
Requires PHP: 5.6
Version: 1.0
License: GNU General Public License v2 or later
License URI: http://www.gnu.org/licenses/gpl-2.0.html
Text Domain: wpngtheme
Tags: angular

WP-NG Theme WordPress Theme, (C) 2022 Piero De Tomi
WP-NG Theme is distributed under the terms of the GNU GPL.
*/
";

            public const string WPVariables = @"
<script type=""text/javascript"">
    window._wpVariables = {
        siteUrl: '<?php echo $siteUrl; ?>',
        api: { nonce: '<?php echo $nonce; ?>' }
    };
</script>
";

            public const string ScriptBaseUrl = "<?php echo $baseUrl; ?>/";
        }
    }
}
