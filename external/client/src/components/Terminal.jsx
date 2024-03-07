import React, { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom';


export default function Terminal() {
  const [response, setResponse] = useState("")
  const [commandInput, setCommandInput] = useState("")
  const [path, setPath] = useState("")
  const [uri, setUri] = useState("")
  const location = useLocation()

  const { ip } = location.state;

  const [commandHistory, setCommandHistory] = useState([]);
  const [historyIndex, setHistoryIndex] = useState(-1);

  useEffect(() => {
    if(uri){
      const socket = new WebSocket(uri)

      socket.addEventListener('open', (event) => {
          console.log("Connection opened: " + event);
      })

      socket.addEventListener('message', (event) => {
          const message = event.data;
          console.log("Received Message from server: "+ message);
          setPath(message)
      })

      socket.addEventListener("close", (event) => {
          console.log("WebSocket connection closed: "+ event);
      })


      return () => {
          socket.close();
      }
    }
  }, [uri])

  useEffect(() => {
    setUri("ws://" + ip + ":8080/")
  }, [ip])

  const handleKeyDown = (e) => {
    if(e.key === "Enter"){
        handleSendMessage();

        setCommandHistory((prevHistory) => [commandInput, ...prevHistory]);
        setHistoryIndex(-1);

        setCommandInput("")
    } else if (e.key === 'ArrowUp' || e.key === 'ArrowDown') {
        handleHistoryNavigation(e.key);
    }
  }

  const handleHistoryNavigation = (arrowKey) => {
    if (commandHistory.length > 0) {
        let newIndex;

        if (arrowKey === 'ArrowUp' && historyIndex < commandHistory.length - 1) {
            newIndex = historyIndex + 1;
        } else if (arrowKey === 'ArrowDown' && historyIndex > -1) {
            newIndex = historyIndex - 1;
        } else {
            return;
        }

      setCommandInput(commandHistory[newIndex]);
      setHistoryIndex(newIndex);
    }
  };

  const isPath = (path) => {
    const windowsPathRegex = /^[A-Za-z]:[\\\/].*$/;
    return windowsPathRegex.test(path);
  }

  const handleSendMessage = () => {
    const socket = new WebSocket(uri)

    socket.addEventListener('open', (event) => {
        console.log("Connection opened: " + event);
        socket.send(commandInput)
    })

    socket.addEventListener('message', (event) => {
        const message = event.data;
        console.log("Received Message from server: "+ message);
        if(isPath(message)){
            setPath(message)
            setResponse("")
        } else{
            setResponse(message)
        }
    })

    socket.addEventListener("close", (event) => {
        console.log("WebSocket connection closed: "+ event);
    })
  }

  return (
    <div>
        <div>
            <span>{'>'}</span>
            <span style={{ marginLeft: '5px', wordWrap: "break-word" }}>{path} </span>
            <input
                className='cmd-input'
                value={commandInput}
                onKeyDown={handleKeyDown}
                onChange={(e) => setCommandInput(e.target.value)}
                placeholder='Command...'
            />
        </div>
        <div className='console-output'>
            <pre style={{wordWrap: "break-word"}}>{response}</pre>
        </div>
    </div>
  )
}
