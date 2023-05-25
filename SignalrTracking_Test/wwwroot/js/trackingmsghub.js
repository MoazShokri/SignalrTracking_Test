//var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/trackingmsghub").build();

//connection.on("ReceiveMessage", function (message) {
//    // Handle the received message
//    console.log("Received message:", message);

//    // Update the UI with the received message
//    var messageContainer = document.getElementById("messageContainer");
//    var newMessage = document.createElement("div");
//    newMessage.innerHTML = `
//        <p>Message from ${message.userName}:</p>
//        <p>Vehicle ID: ${message.vehicleId}</p>
//        <p>Sent Date/Time: ${message.sentDateTime}</p>
//        <p>Message: ${message.message}</p>
//        <hr>
//    `;
//    messageContainer.prepend(newMessage);
//});

//connection.start()
//    .then(function () {
//        console.log("SignalR connected");
//        $("#sendMessageButton").removeAttr("disabled"); // Enable the send button
//    })
//    .catch(function (error) {
//        console.error("SignalR connection error:", error);
//    });

//function sendMessage() {
//    if (connection.state !== "Connected") {
//        console.error("SignalR connection is not in the 'Connected' state.");
//        return;
//    }

//    var vehicleId = document.getElementById("vehicleId").value;
//    var messageContent = document.getElementById("messageContent").value;

//    var message = {
//        vehicleId: vehicleId,
//        message: messageContent
//    };

//    connection.invoke("SendMessage", message)
//        .catch(function (error) {
//            console.error("Failed to send message:", error);
//        });

//    document.getElementById("messageContent").value = "";
//}
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/trackingmsghub").build();

connection.on("ReceiveMessage", function (message) {
    // Handle the received message
    console.log("Received message:", message);

    // Update the UI with the received message
    var messageContainer = document.getElementById("messageContainer");
    var newMessage = document.createElement("div");
    newMessage.innerHTML = `
            <p>Message from ${message.userName}:</p>
            <p>Vehicle ID: ${message.vehicleId}</p>
            <p>Sent Date/Time: ${message.sentDateTime}</p>
            <p>Message: ${message.message}</p>
            <hr>
        `;
    messageContainer.prepend(newMessage);
});

connection.start().then(function () {
    console.log("SignalR connected");
}).catch(function (error) {
    console.error("SignalR connection error:", error);
});

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var vehicleId = $("#vehicleId").val();
    var messageContent = $("#messageContent").val();

    var message = {
        vehicleId: vehicleId,
        message: messageContent
    };

    connection.invoke("SendMessage", message).catch(function (error) {
        console.error("Failed to send message:", error);
    });

    $("#messageContent").val("");
});