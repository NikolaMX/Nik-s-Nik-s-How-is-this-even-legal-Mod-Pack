(function () {
    const sizesNet = ['16', '16.5', '18', '20', '22', '22.5', '24', '26', '28', '30', '32', '34'];
    const sizesProd = ['10', '12', '14', '16', '16.5', '18', '20']
    var mysettings = {
        hceb_netgainfsize: {
            title: 'Net Gain and Efficiency font size (change and press F5)',
            type: 'select',
            options: sizesNet,
            default: '26'
        },
        hceb_productionfsize: {
            title: 'Metal & Energy plus and minus production (change and press F5)',
            type: 'select',
            options: sizesProd,
            default: '16'
        }
    };

    _.extend(api.settings.definitions.camera.settings, mysettings);

    model.settingDefinitions(api.settings.definitions);

    // $('.camera').find('.sub-group').last().after(
    //     "<div class=\"sub-group-title\"><loc>High Contrast Economy Bar</loc></div>\n\n\n\n" +
    //     "<div class=\"sub-group ri\">\n" +
    //     "   <div class=\"option\" data-bind=\"template: { name: 'setting-template', data: $root.settingsItemMap()['camera.hceb_netgainfsize'] }\"></div>\n" +
    //     "</div>\n"
    // );



    var $group = $('<div class="sub-group hceb"></div>').appendTo('.option-list.camera .form-group');
    $group.append('<div class="sub-group-title">High Contrast Economy Bar</div>');
    $group.append('<div class="break-line"></div>')
    Object.keys(mysettings).forEach(function (setting) {
        $group.append('<div class="option" data-bind="template: { name: \'setting-template\', data: $root.settingsItemMap()[\'camera.' + setting + '\'] }"></div>')
    });

    $(".break-line").css({
        "flex-basis": "100%",
        "width": "0"
    })
    $(".sub-group-title").css({
        "width": "100%<"
    })

})()

