import React, { Component, Fragment } from 'react';

export class Pagination extends Component {
    constructor(props) {
        super(props);
        this.state = { currentPage: 1 };
    }

    range = (size, startAt) => {
        return [...Array(size).keys()].map(i => i + startAt);
    }

    fetchPageNumbers = () => {
        const { currentPage } = this.state;
        const { totalPages, pagesToDisplay } = this.props;
        var startPage;
        if (totalPages <= pagesToDisplay) {
            return this.range(totalPages, 1);
        }
        else {
            var midPoint = Math.floor(pagesToDisplay / 2);
            if (currentPage <= midPoint) {
                startPage = 1;
            } else if (currentPage + (midPoint - 1) >= totalPages) {
                startPage = totalPages - pagesToDisplay + 1;
            } else {
                startPage = currentPage - midPoint;
            }

            return [...this.range(pagesToDisplay, startPage)];            
        }
    }

    render() {
        const { totalPages } = this.props;
        if (!totalPages) return null;
        const { currentPage } = this.state;
        const pages = this.fetchPageNumbers();

        return (
            <Fragment>
                <div className="row">
                    <div className="col">
                        <ul className="pagination pagination-sm">
                            <li key="first" className="page-item">
                                <button className="page-link" onClick={() => this.gotoPage(1)}>
                                    <span>First</span>
                                </button>
                            </li>
                            <li key="previous" className="page-item">
                                <button disabled={currentPage <= 1} className="page-link" onClick={() => this.gotoPage(this.state.currentPage - 1)}>
                                    <span>Previous</span>
                                </button>
                            </li>

                            {pages.map((page, index) => {
                                return (
                                    <li key={index} className={`page-item${currentPage === page ? ' active' : ''}`}>
                                        <button className="page-link" onClick={() => page !== '...' ? this.gotoPage(page) : null}>{page}</button>
                                    </li>
                                );
                            })}
                            <li key="next" className="page-item">
                                <button disabled={currentPage === totalPages} className="page-link" onClick={() => this.gotoPage(this.state.currentPage + 1)}>
                                    <span>Next</span>
                                </button>
                            </li>
                            <li key="last" className="page-item">
                                <button className="page-link" onClick={() => this.gotoPage(totalPages)}>
                                    <span>Last</span>
                                </button>
                            </li>
                        </ul>
                    </div>
                    <div className="col-auto">
                        <span>Page Size: </span>
                        <select id="pageSize" onChange={this.onPageSizeChange} value={this.props.pageSize}>
                            {this.props.pageSizeOptions.map((value, index) => {
                                return (<option key={index} value={value}>{value}</option>);
                            })};
                        </select>
                        <span> Showing {this.props.filteredRecords} records filtered from {this.props.totalRecords}</span>
                    </div>
                </div>
            </Fragment>
        );
    }

    componentDidMount() {
        const { totalPages } = this.props;
        if (totalPages) this.gotoPage(1);        
    }

    onPageSizeChange = (e) => {
        const { onPageChanged = f => f } = this.props;
        const pageSize = e.target.value;
        const currentPage = 1; //reset
        const paginationData = {
            pageSize,
            currentPage
        };
        this.setState({ pageSize, currentPage }, () => onPageChanged(paginationData));
    }

    gotoPage = (page) => {
        const { totalPages, pageSize, onPageChanged = f => f } = this.props;
        const currentPage = Math.max(0, Math.min(page, totalPages));
        const paginationData = {
            currentPage,
            pageSize,
        };

        this.setState({ currentPage }, () => onPageChanged(paginationData));
    }
}