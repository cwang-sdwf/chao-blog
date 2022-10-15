import React, { Component } from "react";

class Header extends Component {
    render() {
        return (
            <div className="header">
                <div className="container">
                    <div className="row">
                        <div className="col-3">
                            <img src={"/images/img-profile.jpg"} className="img-responsive" alt="123"/>
                        </div>
                        <div className="col-9">
                            <h1>CHAO WANG</h1>
                            <h4 className="theme-color">Full Stack Developer</h4>
                            <br></br>
                            <p>Credibly embrace visionary internal or "organic" sources and business benefits. Collaboratively integrate efficient portals rather than customized customer service. Assertively deliver frictionless services via leveraged interfaces. Conveniently evisculate accurate sources and process-centric expertise.Energistically fabricate customized imperatives through cooperative catalysts for change.</p>
                            <div className="row">
                                <div className="col-12">
                                    <div className="person-details">
                                        Mandarin (NATIVE), English
                                    </div>
                                    <p className="small-txt">Language</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        );
    }
}

export default Header;
