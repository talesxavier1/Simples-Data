$('#switch_filtrar').dxSwitch({
    onValueChanged(data) {
        GRID.option('filterRow.visible', data.value);
    }
});

$('#btn_adicionar').dxButton({
    text: 'ADICIONAR',
    type: 'success',
    width: 120,
    onClick() {
        window.location.replace(`${window.location.origin}/projeto/page`);
    }
});

const BTN_EDITAR = $('#btn_editar').dxButton({
    stylingMode: 'contained',
    text: 'EDITAR',
    type: 'default',
    disabled: true,
    width: 120,
    onClick() {
        window.location.replace(`${window.location.origin}/projeto/page/${GRID.getSelectedRowKeys()[0]}`);
    }
}).dxButton("instance");

const BTN_EXCLUIR = $('#btn_excluir').dxButton({
    stylingMode: 'contained',
    text: 'EXCLUIR',
    type: 'danger',
    disabled: true,
    width: 120,
    onClick() {
        window.location.replace(`${window.location.origin}/projeto/delete/${GRID.getSelectedRowKeys()[0]}`);
    }
}).dxButton("instance");

const GRID = $('#gridContainer').dxDataGrid({
    dataSource: {
        store: {
            type: 'odata',
            version: 4,
            url: `https://localhost:7102/odata/v1/ProjetoModels`,
            key: '_id',
            beforeSend: function (options) {
                options.params["$orderby"] = "dataInfoModel/createDate desc"
            }
        },
    },
    width: '100%',
    filterRow: {
        visible: false,
        applyFilter: 'auto',
    },
    remoteOperations: true,
    showBorders: true,
    columns: [
        {
            dataField: '_id',
            caption: 'ID',
        },
        {
            dataField: 'nomeProjeto',
            caption: 'Nome do Projeto',
        },
        {
            dataField: 'dataBaseDesenvolvimento',
            caption: 'Banco Desenvolvimento',
        },
        {
            dataField: 'dataBaseHomologacao',
            caption: 'Banco Homologacao',
        },
        {
            dataField: 'dataBaseProducao',
            caption: 'Banco Producao',
        },
        {
            dataField: 'status',
            caption: 'Status',
        }
    ],
    selection: {
        mode: 'multiple',
    },
    height: 625,
    pager: {
        visible: true,
        allowedPageSizes: [20, 30, 40, 50, 'all'],
        showPageSizeSelector: true,
        showNavigationButtons: true,
        pageSize: 10
    },
    onSelectionChanged: (params) => {
        BTN_EDITAR.option("disabled", !(params.selectedRowKeys.length == 1));
        BTN_EXCLUIR.option("disabled", !(params.selectedRowKeys.length == 1));
    },
    showColumnLines: true,
    locale: 'pt'
}).dxDataGrid("instance");


