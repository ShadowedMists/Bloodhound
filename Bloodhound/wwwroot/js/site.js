// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMdcTables() {
    var tables = document.querySelectorAll('.mdc-data-table__table')

    for (var i = 0; i < tables.length; i++) {
        var table = tables[i],
            theads = table.querySelectorAll('thead'),
            tbody = table.querySelector('tbody'),
            trs = tbody.querySelectorAll('td'),
            tds = tbody.querySelectorAll('td')

        for (var j = 0; j < theads.length; j++) {
            var thead = theads[j],
                htrs = thead.querySelectorAll('tr'),
                ths = thead.querySelectorAll('th')

            for (var k = 0; k < htrs.length; k++) {
                var tr = htrs[k]
                if (!tr.classList.contains('mdc-data-table__header-row'))
                    tr.classList.add('mdc-data-table__header-row')
            }

            for (var k = 0; k < ths.length; k++) {
                var th = ths[k]
                if (!th.classList.contains('mdc-data-table__header-cell'))
                    th.classList.add('mdc-data-table__header-cell')
            }
        }

        if (!tbody.classList.contains('mdc-data-table__content'))
            tbody.classList.add('mdc-data-table__content')

        for (var j = 0; j < trs.length; j++) {
            var tr = trs[j]
            if (!tr.classList.contains('mdc-data-table__row'))
                tr.classList.add('mdc-data-table__row')
        }

        for (var j = 0; j < tds.length; j++) {
            var td = tds[j]
            if (!td.classList.contains('mdc-data-table__cell'))
                td.classList.add('mdc-data-table__cell')
        }
    }
}

function mdcSelectRegister(item, index) {

    var select = new mdc.select.MDCSelect(item)

    select.listen('MDCSelect:change', (e) => {
        var t = e.srcElement.dataset.target
        if (t === undefined || t === null || t === '') return

        var c = e.srcElement.querySelector('#' + t)
        if (c == null) return

        c.value = e.detail.value
    })
}

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.mdc-select').forEach(mdcSelectRegister)
    initMdcTables()
})