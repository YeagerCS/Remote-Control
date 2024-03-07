import { useEffect, useState } from "react";
import { DecryptCaesarCipher } from "../services/CaesarCipher";

const COUNT = 25;

const Decrypt = () => {
    const [input, setInput] = useState("")
    const [decryptions, setDecryptions] = useState([])    

    const handleDecrypt = () => {
        setDecryptions([])
        for(let i = 1; i <= 25; i++){
            const decrypt = DecryptCaesarCipher(input, i)
            console.log(decrypt);
            setDecryptions(prev => [...prev, decrypt])
        }
    }

    useEffect(() => {
    }, [decryptions])

    return(
        <div className="form">
            <h1>Decrypt</h1>
            <div className="form-group">
                <label htmlFor="input">String</label>
                <input type="text" 
                    value={input}
                    onChange={e => setInput(e.target.value)}
                />
            </div>
            <div className="form-group">
                <button onClick={handleDecrypt}>Decrypt</button>
                <table>
                    <thead>
                        <tr>
                            <th>Shift</th>
                            <th>String</th>
                        </tr>
                    </thead>
                    <tbody>
                        {decryptions && decryptions.map((decryption, index) => (
                            <tr key={index}>
                                <td>{index + 1}</td>
                                <td>{decryption}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default Decrypt;