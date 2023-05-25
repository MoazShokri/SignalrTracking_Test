// Create a connection to the hub

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/vehiclemovementhub").build();

connection.on("ReceiveMessage", function (vehicleId, message) {
    // Display the received message in the UI
    var li = document.createElement("li");
    li.textContent = "Vehicle ID: " + vehicleId + " - Message: " + message;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(function () {
    console.log("SignalR connection established.");
    // Load initial messages on page load
    loadInitialMessages();
}).catch(function (err) {
    console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault();

    var vehicleId = document.getElementById("vehicleId").value;
    var message = document.getElementById("message").value;

    connection.invoke("SendMessage", vehicleId, message)
        .then(function () {
            // Clear the input fields after sending the message
            document.getElementById("vehicleId").value = "";
            document.getElementById("message").value = "";
        })
        .catch(function (err) {
            console.error(err.toString());
        });
});
function loadInitialMessages() {
    // Get the vehicle ID and message from the query string or other source
    var urlParams = new URLSearchParams(window.location.search);
    var vehicleId = urlParams.get('vehicleId');
    var message = urlParams.get('message');

    // Make an API call or use any other method to retrieve the initial messages for the vehicle
    // Replace the following line with your own logic
    var initialMessages = [
        { vehicleId: vehicleId, message: message }
    ];

    // Display the initial messages in the UI
    for (var i = 0; i < initialMessages.length; i++) {
        var message = initialMessages[i];
        var li = document.createElement("li");
        li.textContent = "Vehicle ID: " + message.vehicleId + " - Message: " + message.message;
        document.getElementById("messageList").appendChild(li);
    }
}