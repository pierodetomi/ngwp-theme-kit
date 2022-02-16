const {{safe-id}}Value = document.getElementById('field_{{id}}')?.value;

if ({{safe-id}}Value) {
    settings['{{id}}'] = {{safe-id}}Value;
}
