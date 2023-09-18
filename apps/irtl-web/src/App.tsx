import React from 'react';

import {
    BrowserRouter,
    Routes,
    Route,
} from "react-router-dom";

import logo from './logo.svg';
import './App.css';
import {AddLinkPage, IntroPage} from "./pages";
import {ThemeProvider} from '@mui/material/styles';
import theme from './theme';
import {Banner, Navbar} from "./components";

function App() {
    
    console.log("Render App");
    
    return (
        // <div className="App">
        //   <header className="App-header">
        //     <img src={logo} className="App-logo" alt="logo" />
        //     <p>
        //       Edit <code>src/App.tsx</code> and save to reload.
        //     </p>
        //     <a
        //       className="App-link"
        //       href="https://reactjs.org"
        //       target="_blank"
        //       rel="noopener noreferrer"
        //     >
        //       Learn React
        //     </a>
        //   </header>
        // </div>
        <ThemeProvider theme={theme}>
            <BrowserRouter>
                <div className="App">
                    <Banner/>
                    <Navbar/>
                    <Routes>
                        <Route path="/" element={<IntroPage/>}/>
                        <Route path="/add-link" element={<AddLinkPage/>}/>
                    </Routes>
                </div>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;
