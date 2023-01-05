
var chatBox = $("#ChatBox");
//var chatBox = document.getElementById("ChatBox");
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();
//connection.start().then(function () {
//    document.getElementById("sendButton").disabled = false;
//    document.getElementById("sendPVButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});
////Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;
//document.getElementById("sendPVButton").disabled = true;


//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
   
//    li.textContent = `${user} says ${message}`;
//});
//connection.on("ReceivePVMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
    
//    li.textContent = `${user} says ${message}`;
//});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    debugger
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

//document.getElementById("sendPVButton").addEventListener("click", function (event) {
//    debugger
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendPrivateMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});


    function showChatDialog() {
        chatBox.css("display", "block");
    }

    function Init() {
        setTimeout(showChatDialog, 1000);


        var NewMessageForm = $("#NewMessageForm");
        NewMessageForm.on("submit", function (e) {

            e.preventDefault();
            var message = e.target[0].value;
            e.target[0].value = '';
            sendMessage(message);
        });

    }


function sendMessage(text) {
    connection.invoke('SendNewMessage', " بازدید کننده ", text);
}


//درسافت پیام از سرور
connection.on('getNewMessage', getMessage);

function getMessage(sender, message, time) {

    $("#Messages").append("<li><div><span class='name'>" + sender + "</span><span class='time'>" + time + "</span></div><div class='message'>" + message + "</div></li>")
};

    $(document).ready(function () {
        Init();
    });


