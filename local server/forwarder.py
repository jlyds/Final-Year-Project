#!/usr/bin/env python3
import socket
import threading


LOCAL_LISTEN = ('127.0.0.1', 10725)
CLIENT_LISTEN = ('0.0.0.0', 4433)

client = None

def listen(sock):
    global client
    while True:
        client, address = sock.accept()
        print('%s:%s connected.' % (address[0], address[1]))


def main():
    global client
    local = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    local.bind(LOCAL_LISTEN)
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(CLIENT_LISTEN)
    server.listen(2)
    threading.Thread(target=listen, args=(server,)).start()

    while True:
        cmd, address = local.recvfrom(2048)
        print("cmd: %s" % cmd)
        try:
            client.send(cmd)
        except:
            print('send fail')


if __name__ == '__main__':
    main()
