
$(document).ready(function () {
    $('#primaryTelephone').inputmask('(99) 99999-9999');
    $('#secondaryTelephone').inputmask('(99) 99999-9999');
    $('#cpf').inputmask('999.999.999-99');
    $('#cnpj').inputmask('99.999.999/9999-99');
    $('#rg').inputmask('99.999.999-9');

    $('input').removeAttr('autocomplete');
});
