wp.customize.bind('ready', function () {
    wp.customize.bind('change', function (setting) {
        window.wpCustomizer.updatePreview(setting.id, setting.get());
    });

    // wp.customize.previewer.bind('ready', function (message) {
    //     let settings = {};

    //     let settingNames = Object.keys(message.settingValidities);

    //     if (settingNames?.length > 0)
    //         settingNames.forEach(settingName => {
    //             let control = wp.customize.control(settingName);

    //             if (!control || !control.elements || control.elements.length === 0) {
    //                 return;
    //             }

    //             let valueAccessor = control.elements[0];

    //             if (typeof valueAccessor !== 'function') {
    //                 return;
    //             }

    //             settings[settingName] = valueAccessor();
    //         });

    //     window.wpCustomizer.updatePreview(settings);
    // });
});