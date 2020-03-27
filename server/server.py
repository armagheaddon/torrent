from socket import *

HOST = '10.0.0.18'  
PORT = 50000

class Server():

    def __init__(self):
        self.buff_size = 1024

        self.sock = socket(AF_INET, SOCK_DGRAM)
        self.sock.bind((HOST, PORT))

        self.sock_listen()
        
    def sock_listen(self):
        while True:
            data, addr = self.sock.recvfrom(self.buff_size)
            print('received: {0} from {1}'.format(data, addr))
            self.sock.sendto("got it".encode(), addr)
            self.sock.close()

Server()