from flask import Flask, request
from flask_socketio import SocketIO, emit
from flask_cors import CORS

app = Flask(__name__)
CORS(app)
socketio = SocketIO(app, cors_allowed_origins="*")

emitted_ips = []

def array_contains(arr, ip):
    for entry in arr:
        if entry["ip"] == ip:
            return True
        
    return False

@app.route("/ip", methods=['POST'])
def get_ip():
    try:
        global emitted_ips
        data = request.get_json()
        ip = data.get("ip")
        id = data.get("id")

        if not array_contains(emitted_ips, ip):
            emitted_ips.append({ 'ip': ip, 'id': id })

        socketio.emit('ip', ip)

        return "Ok"
    except Exception as e:
        return str(e), 400
    

@app.route("/removeIP", methods=["POST"])
def remove_ip_by_ip():
    try:
        global emitted_ips
        data = request.get_json()
        ip = data.get("ip")
        filtered = []

        for entry in emitted_ips:
            if entry["ip"] != ip:
                filtered.append(entry)
        
        emitted_ips = filtered
        print("OK")
        return "Ok"
    except Exception as e:
        return str(e), 400


@app.route("/remove", methods=['POST'])
def remove_ip():
    try:
        global emitted_ips
        print("Theee")
        data = request.get_data(as_text=True)
        extracted_ip = None
        filtered = []

        for entry in emitted_ips:
            print(entry)
            if entry["id"] == data:
                extracted_ip = entry["id"]
            else:
                filtered.append(entry)
        
        emitted_ips = filtered

        socketio.emit("remove_ip", extracted_ip)
        return "Ok"
    except Exception as e:
        print(e)

@socketio.on('connect')
def handle_connect():
    print("Client connected.")
    emit("all_ips", emitted_ips)

@socketio.on("disconnect")
def handle_disconnect():
    print("Client disconnected")


if __name__  == "__main__":
    socketio.run(app, debug=True, host="0.0.0.0")