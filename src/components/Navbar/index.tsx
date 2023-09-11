import React from 'react';
import './Navbar.css'

const Navbar = () => {
    return (
        <nav>
            <ul>
                <div className="left-links">
                    <li><a href="/">Home</a></li>
                    <li><a href="/add-link">Add Link</a></li>
                    <li><a href="/topics">Topics</a></li>
                    <li><a href="/activity">Activity</a></li>
                </div>
                <li className="about-link"><a href="/about">About</a></li>
            </ul>
        </nav>
    );
}

export default Navbar;