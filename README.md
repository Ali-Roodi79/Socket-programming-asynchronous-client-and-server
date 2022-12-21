# Socket-programming-asynchronous-client-and-server

This project was one of my university projects in the advanced programming course in the second semester of the university.

This program includes two parts, the client and the server, To send text messages asynchronously, which are implemented using socket programming in C#.

Note!!!
> Both the client and the server must run simultaneously. 

The procedure is that you send a message to the server and the server sends the same message back to you.


## How to use:

- When you run the project in Visual Studio (Both Client & Server), These 2 console screens will be displayed and you can send message to Server:


![image](https://github.com/Ali-Roodi79/Socket-programming-asynchronous-client-and-server/blob/main/assets/MainConsolePage.png)

---

- For example, we send the sentence **"Hello server"** to the server, and the server must send us exactly the same message.:


![image](https://github.com/Ali-Roodi79/Socket-programming-asynchronous-client-and-server/blob/main/assets/Send%26ReceiveMessage.png)


- The process of sending and receiving messages is all done on the basis of **socket programming** and completely **asynchronously**.

- The messages are sent & receive using the IP address of the local host, **127.0.0.1** and port **9999**.
