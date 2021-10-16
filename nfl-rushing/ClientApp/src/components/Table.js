import React, { Component } from 'react';

export class Table extends Component {
    static displayName = Table.name;

    constructor(props) {
        super(props);

        this.updateSortOrder = this.updateSortOrder.bind(this);
        this.renderTable = this.renderTable.bind(this);
    }

    updateSortOrder = (column) => {
        const { onSortOrderChanged = f => f } = this.props;
        onSortOrderChanged(column);
    }

    renderTable = (data) => {
        return (
            <div>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            {this.props.headers.map((column, index) =>
                                <th key={index} onClick={() => column.isSortable ? this.updateSortOrder(column) : null}>
                                    {column.header}
                                    <span>
                                        {this.props.sortBy.split('_')[0] === column.header.toLowerCase()
                                            ? this.props.sortBy.split('_')[1] === "desc"
                                                ? ' 🔽'
                                                : ' 🔼'
                                            : ''}
                                    </span>
                                </th>)
                            }
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((row, index) =>
                            <tr key={index}>{Object.keys(row).map(column =>
                                <td key={column}>{row[column]}</td>
                                )}
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {

        var table = this.props.data ? this.renderTable(this.props.data) : "Loading...";

        return (
            <div>
                <h1 id="tabelLabel" >{this.props.title}</h1>
                {table}
            </div>
        );
    }
}
