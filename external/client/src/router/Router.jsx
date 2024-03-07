import React from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import IPReceiver from '../components/IPReceiver'
import Terminal from '../components/Terminal'
import Wallpaper from '../components/Wallpaper'
import CaesarDashboard from '../components/CaesarDashboard'

export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route exact path='/' element={<IPReceiver/>}/>
                <Route path="/terminal" element={<Terminal/>}/>
                <Route path="/the" element={<Wallpaper/>}/>
                <Route path='/caesar' element={<CaesarDashboard/>}/>
            </Routes>
        </BrowserRouter>
    )
}
