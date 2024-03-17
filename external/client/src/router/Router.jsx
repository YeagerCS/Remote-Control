import React from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import IPReceiver from '../components/IPReceiver'
import Terminal from '../components/Terminal'

export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route exact path='/' element={<IPReceiver/>}/>
                <Route path="/terminal" element={<Terminal/>}/>
            </Routes>
        </BrowserRouter>
    )
}
