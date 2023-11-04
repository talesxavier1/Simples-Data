
const init = () => {
    window.GridUtils={
        "criarPaginacao": criarPaginacao
    }
}


const criarPaginacao = (args={
    container,
    count,
    paginacaoPreviousId,
    paginacaoNextId
}) => {
    criarPaginacao_Validacao(args)


}

const criarPaginacao_Validacao = (args) => {

    /* --------------- container ----------------- */
    if (!args.container) {
        throw (`[criarPaginacao_Validacao] - paginacaoContainer não enconrado.`);
    }
    if (typeof args.container != "string") {
        throw (`[criarPaginacao_Validacao] - paginacaoContainer não é uma string.`);
    }
    let objectPaginacaoContainer = $(`#${args.container}`);
    if (objectPaginacaoContainer.length == 0) {
        throw (`[criarPaginacao_Validacao] - Não foi possível encontra paginacaoContainer com id ${args.container}`);
    }
    /* ------------------------------------------- */

    /* ----------------- count ------------------- */
    if (!args.count) {
        throw (`[criarPaginacao_Validacao] - count não enconrado.`);
    }
    if (typeof args.count != "number") {
        throw (`[criarPaginacao_Validacao] - paginacaoContainer não é um number.`);
    }
    /* ------------------------------------------- */

    /* ---------- paginacaoPreviousId ------------ */
    if (args.paginacaoPreviousId) {
        if (typeof args.paginacaoPreviousId != "string") {
            throw (`[criarPaginacao_Validacao] - paginacaoContainer não é uma string.`);
        }
        let objectPaginacaoPrevious = $(`#${args.paginacaoPreviousId}`);
        if (objectPaginacaoPrevious.length == 0) {
            throw (`[criarPaginacao_Validacao] - Não foi possível encontra PaginacaoPrevious com id ${args.paginacaoPreviousId}`);
        }
    }
    /* ------------------------------------------- */

    /* ------------ paginacaoNextId -------------- */
    if (args.paginacaoNextId) {
        if (typeof args.paginacaoNextId != "string") {
            throw (`[criarPaginacao_Validacao] - paginacaoContainer não é uma string.`);
        }
        let objectPaginacaoPrevious = $(`#${args.paginacaoNextId}`);
        if (objectPaginacaoPrevious.length == 0) {
            throw (`[criarPaginacao_Validacao] - Não foi possível encontra paginacaoNext com id ${args.paginacaoNextId}`);
        }
    }
    /* ------------------------------------------- */
    
}

(() => {
    init();
})()