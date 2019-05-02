$(document).ready(function () {
    $('#LstLbl').multiselect({
        enableClickableOptGroups: true,
        includeSelectAllOption: true,
        selectAllText: 'Seleccionar todos',
        selectAllValue: '*',
        allSelectedText: 'Todas',
        nonSelectedText: 'Ninguna',
        nSelectedText: ' -  Selecionados',
        maxHeight: 200
    });
    //$('#Issu_LstLbl').multiselect('selectAll', false);
    $('#Issu_LstLbl').multiselect('updateButtonText');

    $('#Lbls').on('change', function () {
        $("#LblInputs").empty();
        $("#Lbls option:selected").each(function (i, op) {
            let lxClr = $(op).attr('data-clr');
            let lxBkClr = $(op).attr('data-bkclr');
            let lxLblId = $(op).val();
            let lxLbl = $(op).text();
            let lxStr = '' +
                '<input type="text" name="LstLbl[' + i + '].LblId" id="LstLbl_' + i + '__LblId" value = "' + lxLblId + '"/>' +
                '<input type="text" name="LstLbl[' + i + '].Lbl" id="LstLbl_' + i + '__Lbl" value = "' + lxLbl + '"/>' +
                '<input type="text" name="LstLbl[' + i + '].Clr" id="LstLbl_' + i + '__Clr" value = "' + lxClr + '"/>' +
                '<input type="text" name="LstLbl[' + i + '].BkClr" id="LstLbl_' + i + '__BkClr" value = "' + lxBkClr + '"/>'
            $("#LblInputs").append(lxStr);
        });
    });
});