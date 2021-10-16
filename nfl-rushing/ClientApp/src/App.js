import React, { Component } from 'react';
import { Layout } from './components/Layout';
import { Table } from './components/Table';
import { Pagination } from './components/Pagination';
import { Search } from './components/Search';
import './custom.css'

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            currentPage: 1,
            pagesToDisplay: 10,
            pageSize: 10,
            totalRecords: null,
            sortBy: "yds_desc",
            search: ''
        };
    }

    componentDidMount() {
        this.fetchPlayerData();
    }

    componentDidUpdate(prevProps, prevState, snapshot) {
        if (prevState.sortBy !== this.state.sortBy || this.state.currentPage !== prevState.currentPage || this.state.pageSize !== prevState.pageSize || this.state.search !== prevState.search) {
            this.fetchPlayerData();
        }
    }

    onPageChanged = (data) => {
        const { currentPage, pageSize } = data;
        this.setState({
            pageSize: pageSize,
            currentPage: currentPage
        });
    }

    onSearchChanged = (data) => {
        this.setState({
            search: data
        });
    }

    addSorting = (sortOrder, sortColumn) => {
        return `${sortOrder === "desc" ? sortColumn + '_desc' : sortColumn}`;
    }

    setSortOrder = (column) => {
        const { sortBy } = this.state;
        var sortColumn = sortBy.split('_')[0];
        var sortOrder = sortBy.split('_')[1];
        var newOrder = "desc"; //default
        //same column flip order
        if (sortColumn === column.header.toLowerCase()) {
            if (sortOrder === "desc") {
                newOrder = "asc";
            }
            else {
                newOrder = "desc";
            }
        }

        this.setState({ sortBy: this.addSorting(newOrder, column.header.toLowerCase()) })
    }

    async fetchPlayerData() {
        const { sortBy, currentPage, pageSize, search } = this.state;
        const response = await fetch(`playerStats?sortBy=${sortBy}&page=${currentPage}&pageSize=${pageSize}&search=${search}`);
        var data = await response.json();
        this.setState({
            data: data.item1,
            filteredRecords: data.item2,
            totalRecords: data.item3
        });
    }

    render() {
        const { data, sortBy, search, totalRecords, filteredRecords, pageSize, pagesToDisplay } = this.state;
        return (        
            <Layout>
                <Search
                    placeholder="Search"
                    onSearchChanged={this.onSearchChanged} />
                <Table
                    headers={[
                        { header: "Player", isSortable: true, filterable: true },
                        { header: "Team", isSortable: true },
                        { header: "Pos", isSortable: true },
                        { header: "Att", isSortable: true },
                        { header: "Att/G", isSortable: true },
                        { header: "Yds", isSortable: true },
                        { header: "Avg", isSortable: false },
                        { header: "Yds/G", isSortable: true },
                        { header: "TD", isSortable: true },
                        { header: "Lng", isSortable: true },
                        { header: "1st", isSortable: false },
                        { header: "1st%", isSortable: false },
                        { header: "20+", isSortable: false },
                        { header: "40+", isSortable: false },
                        { header: "FUM", isSortable: true }
                    ]}
                    data={data}
                    sortBy={sortBy}
                    onSortOrderChanged={this.setSortOrder}
                    title="Player Stats" />
                <Pagination
                    totalRecords={totalRecords}
                    filteredRecords={filteredRecords}
                    pageSize={pageSize}
                    pageSizeOptions={[10,25,50,100]}
                    pagesToDisplay={pagesToDisplay}
                    totalPages={Math.ceil(filteredRecords / pageSize)}
                    onPageChanged={this.onPageChanged} />
                <a
                    style={{ float: 'right' }}
                    href={`/File?sortBy=${sortBy}&search=${search}`}>
                    Export to Excel
                </a>
            </Layout> );
    }
}
