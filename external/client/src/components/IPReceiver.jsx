import { useEffect, useState } from "react";
import { io } from "socket.io-client";
import Terminal from "./Terminal";
import { useNavigate } from "react-router-dom";

const IPReceiver = () => {
    const [ips, setIps] = useState([])
    const navigate = useNavigate()

    useEffect(() => {
        const socket = io('http://localhost:5000/'); // Replace with your Python server URL

        socket.on('connect', () => {
            console.log('Connected to WebSocket');
        });

        socket.on('disconnect', () => {
            console.log('Disconnected from WebSocket');
        });

        socket.on("all_ips", (data) => {
            console.log(data);
            setIps(data.map(entry => entry.ip))
            checkValidConnections(data)
        })

        socket.on('ip', (data) => {
            console.log('Received IP:', data);
            setIps(prev => [...prev, data])
            checkValidConnection(data)
        });

        socket.on('remove_ip', (data) => {
            console.log("Remove Ips: " + data);
            setIps(prev => prev.filter(ip => ip !== data))
        })

        return () => {
            socket.disconnect();
        };
    }, []);


    const checkValidConnection = (ip) => {
        const socket = new WebSocket(`ws://${ip}:8080`);

        let isOpen = false;

        const openHandler = () => {
            console.log(`Valid WebSocket connection on ${ip}:8080`);
            isOpen = true;
        };

        const closeHandler = async () => {
            console.log(`WebSocket connection closed on ${ip}:8080`);

            if (!isOpen) {
                console.log(`Invalid WebSocket connection on ${ip}:8080`);
                setIps(prev => prev.filter(prevIp => prevIp !== ip));
                await pythonRemoveIP(ip)
            }

            socket.removeEventListener('open', openHandler);
            socket.removeEventListener('close', closeHandler);
        };

        socket.addEventListener('open', openHandler);
        socket.addEventListener('close', closeHandler);
    };

    const pythonRemoveIP = async (ip) => {
        const response = await fetch("http://localhost:5000/removeIP", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ ip: ip })
        })

        const result = await response.json()
        console.log(result);
    }

    const checkValidConnections = (data) => {
        data.forEach(entry => checkValidConnection(entry.ip));
    };

    const handleSelectDevice = (ip) => {
        navigate("/terminal", {
            state: {
                ip: ip
            }
        })
    }

    return (
        <div>
            <header>
                <h1 style={{ textAlign: "center" }}>Remotely Connected Devices</h1>
            </header>
            <div className="parent-the">
                {ips.length > 0 ? ips.map((ip, index) => (
                    <div key={index} className="the" onClick={() => handleSelectDevice(ip)}>
                        <div>
                            <div>
                                <img src="src/assets/skull.png" alt="" id="lappi" />
                            </div>
                            <div>{ip}</div>
                        </div>
                    </div>
                )) :
                    <div>
                        <h3>No devices connected.</h3>
                    </div>
                }
            </div>
        </div>
    )
}

export default IPReceiver;