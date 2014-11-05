#!/usr/bin/env python3
import sys
import socket
import threading
import subprocess


SERVER_ADDRESS = ("localhost", 4433)
ROBOT_PROGRAM = r"nslookup"
#cat =C:\Users\user\Documents\GitHub\Final-Year-Project\Controllor-v1.4\bin\Debug\乐高停车机器人.exe

def connect_robot():
    return subprocess.Popen(ROBOT_PROGRAM, stdin=subprocess.PIPE)
    

def connect_server():
    return socket.create_connection(SERVER_ADDRESS)


def fordward_cmd(server, robot):
    while True:
        cmd = server.recv(4096).decode('utf8')
        print(cmd, end='')
        robot.stdin.write(cmd.encode('gb18030'))
        robot.stdin.flush()


def main():
    robot = connect_robot()
    server = connect_server()
    fordward_cmd(server, robot)
    

if __name__ == '__main__':
    main()
