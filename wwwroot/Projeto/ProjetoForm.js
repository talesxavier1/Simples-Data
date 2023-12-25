

const text = $("#text").dxTextArea({
    value: "ss",
    maxLength: 500,
    label: "Country"
});


$('#form-container').on('submit', (e) => {
    DevExpress.ui.notify({
        message: 'You have submitted the form',
        position: {
            my: 'center top',
            at: 'center top',
        },
    }, 'success', 3000);

    //e.preventDefault();
});
