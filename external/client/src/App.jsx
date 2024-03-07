import { useState } from 'react'
import Terminal from './components/Terminal'
import {DecryptCaesarCipher, EncryptCaesarCipher} from './services/CaesarCipher'
import Encrypt from './components/Encrypt'
import Decrypt from './components/Decrypt'
import IPReceiver from './components/IPReceiver'
import Router from './router/Router'

function App() {
  

  return (
    <>
      <Router/>
    </>
  )
}

export default App
