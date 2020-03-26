from socket import *

HOST = 'localhost'  
PORT = 50001   

class Server():

    def __init__(self):
        self.sock = socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.bind((HOST, PORT))  
        self.sock_listen()  
        
    def sock_listen(self):
        self.sock.listen(10)
        while True:
            client, addr = server.accept()
            print('Connected by: ', addr)
            self.handle_client(client)

    def handle_client(self, client):
        data = client.recv(1024)

        client.send(data)

    def create_torrent(self, file_name):
        pass