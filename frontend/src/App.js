import React from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './views/Home';
import Login from './views/Login';
import Register from './views/Register';
import Applications from './views/Applications';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/home" element={<Applications />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
