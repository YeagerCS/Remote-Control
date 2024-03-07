import React from 'react'
import Encrypt from './Encrypt'
import Decrypt from './Decrypt'

export default function CaesarDashboard() {
    return (
        <div className='wrapper'>
            <Encrypt/>
            <Decrypt/>
        </div>
    )
}
