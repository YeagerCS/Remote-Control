export const alphabet = "abcdefghijklmnopqrstuvwxyz"

export const EncryptCaesarCipher = (input: string, shift: number) => {
    let cipher = "";

    for(let i = 0; i < input.length; i++){
        const initialIndex = alphabet.indexOf(input[i].toLowerCase())
        let newChar = getEncryptionCharAfterShift(shift, initialIndex)

        if(input[i] == "" || input[i] == " "){
            newChar = " ";
        }

        cipher += newChar;
    }

    return cipher;
}

const getEncryptionCharAfterShift = (shift: number, index: number): string => {
    const maxIndex = alphabet.length - 1;
    let newIndex = index + shift;

    if(newIndex > maxIndex){
        const overlap = newIndex - maxIndex;
        newIndex = overlap; 
    }

    return alphabet.charAt(newIndex);
}

export const DecryptCaesarCipher = (input: string, shift: number) => {
    let decipher = "";

    for(let i = 0; i < input.length; i++){
        const finalIndex = alphabet.indexOf(input[i].toLowerCase())
        let newChar = getDecryptionCharAfterShift(shift, finalIndex);

        if(input[i] == "" || input[i] == " "){
            newChar = " ";
        }

        decipher += newChar;
    }

    return decipher;
}

const getDecryptionCharAfterShift = (shift: number, index: number) => {
    const maxIndex = alphabet.length - 1;
    let newIndex = index - shift;

    if(newIndex < 0){
        const overlap = maxIndex + newIndex;
        newIndex = overlap;
    }

    return alphabet.charAt(newIndex);
}