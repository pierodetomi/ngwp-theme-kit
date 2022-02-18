let WpCustomizerService = function () {
    let self = {};

    let _previewCallbacks = [];

    self.registerPreviewCallback = function (callback) {
        _previewCallbacks.push(callback);
    };

    self.updatePreview = function (settingId, settingValue) {
        _previewCallbacks.forEach(callback => {
            callback(settingId, settingValue);
        });
    };

    return self;
};