import React, { Component } from 'react';
import Header from '../Header';

class Home extends Component {
    render() {
        return (
<div>
<Header/>
<section className="section-wrapper">
    <div className="container">
        <div className="row  d-flex h-100">
            <div className="col-md-3 justify-content-center align-self-center">
                <div className="section-title">
                    <h2>My Activity</h2>
                </div>
            </div>
            <div className="col-md-9 justify-content-center align-self-center">
                <div className="row">
                    <div className="col-sm-6">
                        <div>HTML</div>
                        <div className="progress">
                            <div className="progress-bar theme-bg-color" role="progressbar" style={{width: 100+'%'}} aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">100%</div>
                        </div>
                    </div>
                    <div className="col-sm-6">
                        <div>CSS</div>
                        <div className="progress">
                            <div className="progress-bar theme-bg-color" role="progressbar" style={{width: 100+'%'}} aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">100%</div>
                        </div>
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
</section>
   
</div>
        );
    }
}

export default Home;