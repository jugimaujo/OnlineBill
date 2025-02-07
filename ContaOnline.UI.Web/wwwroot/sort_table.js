var table = document.querySelector('.table.table-sort');

if (table) {
    let thead_cells = table.querySelectorAll('thead>tr>*');

    if (thead_cells) {
        thead_cells = Array.from(thead_cells);

        thead_cells.forEach((thead_cell, index) => {
            if (thead_cell.classList.value == 'table-head-command') return;

            thead_cell.addEventListener('click', function () {

                let isChanged = 0;

                thead_cells.forEach(thead_cell_undecorate => {
                    if (thead_cell_undecorate.classList.value === 'asc') {
                        thead_cell_undecorate.classList[0] = '';
                        thead_cell_undecorate.classList.value = '';
                        isChanged = 1;
                    }
                });
                
                if (isChanged != 1) this.classList.toggle('asc');

                sortTableByColumn(table, index, !this.classList.contains('asc'));
            });
        });
    }
}

function sortTableByColumn(table, column, desc=false)
{
    let tbody = table.querySelector('tbody');
        rows = tbody.querySelectorAll('tr');

    rows = rows.isArray ? rows : Object.values(rows);

    function compare(a, b) {
        let aText = a.children[column].innerText.trim();
            bText = b.children[column].innerText.trim();

        return (aText < bText) ? -1 : 1;
    }

    rows.sort(compare);

    if (desc) rows.reverse();

    tbody.innerText = '';

    let i = 0;
    while (i < rows.length) {
        tbody.append(rows[i]);
        i++;
    }

}