
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/trackingmsghub").build();

connection.on("ReceiveMessage", function (message) {
    console.log("Received message:", message);
});

connection.start().then(function () {
    console.log("SignalR connected");
}).catch(function (error) {
    console.error("SignalR connection error:", error);
});

// Function to send a message
function sendMessage() {
    var vehicleId = document.getElementById("vehicleId").value;
    var messageContent = document.getElementById("messageContent").value;

    // Make an AJAX request to send the message to the server
    fetch("https://localhost:7053/Home/SendMessageToVehicle", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            vehicleId: vehicleId,
            message: messageContent
        })
    })
        .then(response => {
            if (response.ok) {
                console.log("Message sent!");
                // Optionally update the UI or perform any other actions
            } else {
                console.error("Failed to send message:", response.statusText);
                // Handle the failure case
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Add an event listener to the send message button
var sendMessageBtn = document.getElementById("sendMessageBtn");
sendMessageBtn.addEventListener("click", sendMessage);

