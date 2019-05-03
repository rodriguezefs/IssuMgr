$(document).ready(function () {
    $('#Issu_LstLbl').multiselect({
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

    $('#Issu_LstLbl').on('change', function () {
        $("#LblInputs").empty();
        $("#Issu_LstLbl option:selected").each(function (i, op) {
            let lxClr = $(op).attr('data-clr');
            let lxBkClr = $(op).attr('data-bkclr');
            let lxLblId = $(op).val();
            let lxLbl = $(op).text();
            let lxStr = '' +
                '<input type="hidden" name="LstLbl[' + i + '].LblId" id="LstLbl_' + i + '__LblId" value = "' + lxLblId + '"/>' +
                '<input type="hidden" name="LstLbl[' + i + '].Lbl" id="LstLbl_' + i + '__Lbl" value = "' + lxLbl + '"/>' +
                '<input type="hidden" name="LstLbl[' + i + '].Clr" id="LstLbl_' + i + '__Clr" value = "' + lxClr + '"/>' +
                '<input type="hidden" name="LstLbl[' + i + '].BkClr" id="LstLbl_' + i + '__BkClr" value = "' + lxBkClr + '"/>'
            $("#LblInputs").append(lxStr);
        });
    });
});