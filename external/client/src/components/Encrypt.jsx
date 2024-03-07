import { useState } from "react"
import { EncryptCaesarCipher } from "../services/CaesarCipher"

const Encrypt = () => {
    const [input, setInput] = useState("")
    const [shift, setShift] = useState("")
    const [encrypted, setEncrypted] = useState("")

    const handleEncrypt = () => {
        const encrypted = EncryptCaesarCipher(input, Number(shift));
        setEncrypted(encrypted);
    }

    return(
        <div className='form'>
            <h1>Encrypt</h1>
            <div className='form-group'>
                <label htmlFor="input">String</label>
                <input type="text" name="input" id="input" 
                value={input}
                onChange={(e) => setInput(e.target.value)}
                />
            </div>
            <div className='form-group'>
                <label htmlFor="shift">Shift</label>
                <input type="text" name="shift" id="shift" 
                value={shift}
                onChange={(e) => setShift(e.target.value)}
                />
            </div>
            <div className='form-group'>
                <button onClick={handleEncrypt}>Encrypt</button>
                <p>{encrypted}</p>
            </div>
        </div>
    )
}

export default Encrypt;