from socket import *
from node import Node

HOST = '10.0.0.18'
PORT = 50000

class Server():

    def __init__(self):
        self.buff_size = 1024

        self.sock = socket(AF_INET, SOCK_DGRAM)
        self.sock.bind((HOST, PORT))

        self.sock_listen()

        self.file_list = []
        self.file_sources = {}
        self.nodes = []

    def sock_listen(self):
        while True:
            data, addr = self.sock.recvfrom(self.buff_size)
            print('received: {0} from {1}'.format(data, addr))
            self.handle_message(data, addr)
            
    def handle_message(self, data, addr):
        if data == "online":
            print("added "+ addr + " to node list")
            nd = Node(addr[0], addr[1])
            self.nodes.append(nd)
            self.sock.sendto(addr, "ack")

        elif data.startswith("MF:"):
            files = data[3:].split(',')
            for name in files:
                if name not in self.file_list:
                    self.file_list.append(name)

                if name not in self.file_sources.keys():
                    self.file_sources[name] = [addr]
                else:
                    self.file_sources[name].append(addr)

        else:
            if data in self.file_list:
                if data in self.file_sources.keys():
                    if len(self.file_sources[data]) > 5:
                        msg = ",".join([n.ip + str(n.port) for n in self.file_sources[data][:5]])
                    else:
                        msg = ",".join([n.ip + str(n.port) for n in self.file_sources[data]])
                    msg = "%05d:"%(len(msg)) + msg
                    self.send(addr, msg)
                    print("sent: "+msg)
                else:
                    # msg = HOST+str(PORT)
                    # msg = "%05d:"%(len(msg)) + msg
                    # self.send(addr, msg)
                    # self.sock.close()
                    self.send(addr,"00042:No nodes currently available for this file")
            else:
                self.send(addr, "00019:file name not found")
        
    def send(self, addr, msg):
        self.sock.sendto(msg.encode(), addr)

Server()