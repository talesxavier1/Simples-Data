const BTN_CONFIRMAR = $('#btn_confirmar').dxButton({
    stylingMode: "danger",//'contained',
    text: 'CONFIRMAR',
    type: 'submit',
    width: 120,
    onClick() {
        $('#form').submit();
    }
}).dxButton("instance");

const BTN_VOLTAR = $('#btn_voltar').dxButton({
    stylingMode: 'contained',
    text: 'Contained',
    type: 'danger',
    width: 120,
    onClick() {

    }
}).dxButton("instance");
