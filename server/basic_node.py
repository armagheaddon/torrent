from socket import *
import threading

def handle_request(addr, data):
    pass

server_addr = ('10.0.0.18', 50000)

buff_size = 1024
sock = socket(AF_INET, SOCK_DGRAM)

files = ["test.txt"]

sock.sendto(server_addr, "online".encode())
if sock.recvfrom(buff_size) != "ack":
    print("encountered a problem")
    exit()

msg = "MF:test.txt"
sock.sendto(server_addr, msg.encode())

while True:
    data, addr = sock.recvfrom(buff_size)
    handler = threading.Thread(target=handle_request, args=(addr,data))
    handler.start()


