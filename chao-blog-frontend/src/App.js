import logo from './logo.svg';
import './App.css';
import Home from './Components/Home';
import Login from './Components/Login';
import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {
  return (
    <>
     <BrowserRouter>
        <Routes>
              <Route path="/" element={<Login />}>  
              </Route>
              <Route path="/home" element={<Home />}>
              </Route>
        </Routes>
     </BrowserRouter>
    
    </>

    
    
  );
}

export default App;
