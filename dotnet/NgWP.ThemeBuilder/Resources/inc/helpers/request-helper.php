<?php

if (!function_exists("getallheaders"))
{
    function getallheaders()
    {
        $headers = [];
            
        foreach ($_SERVER as $name => $value)
            if (substr($name, 0, 5) == 'HTTP_')
                $headers[str_replace(' ', '-', ucwords(strtolower(str_replace('_', ' ', substr($name, 5)))))] = $value;
            
        return $headers;
    }
}

function try_read_nonce_from_headers() {
    $nonce = null;
    $headers = array_change_key_case(getallheaders(), CASE_LOWER);
        
    foreach ($headers as $name => $value) {
        if ($name == "x-wp-nonce") {
            $nonce = $value;
            break;
        }
    }

    return $nonce;
}