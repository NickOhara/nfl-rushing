import React, { Component } from 'react';

export class Search extends Component {
    static displayName = Search.name;

    handleChange = (e) => {
        const { onSearchChanged = f => f } = this.props;
        onSearchChanged(e.target.value);
    }

    render() {
        return (
            <input
                type="text"
                style={{ float: 'right'}}
                placeholder={ this.props.placeholder }
                onChange={(e) => this.handleChange(e)}
            />
        );
    }
}
