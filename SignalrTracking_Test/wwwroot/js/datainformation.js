// Create a connection to the DataInformationHub
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/datainformationhub")
    .build();

////// Receive the message from the hub
////connection.on("ReceiveMessage", function (message) {
////    // Handle the received message
////    console.log("Received message:", message);

////    // Display the message to the user
////    var messageContainer = document.getElementById("messageContainer");
////    var newMessage = document.createElement("div");
////    newMessage.innerHTML = `<p>${message}</p>`;
////    messageContainer.appendChild(newMessage);
////});

////// Start the connection
////connection.start().then(function () {
////    console.log("SignalR connection established.");

////    // Start the timer to check for updates periodically
////    startTimer();
////}).catch(function (err) {
////    console.error("Error establishing SignalR connection: " + err);
////});

////// Function to start the timer
////function startTimer() {
////    // Set the interval for checking updates (e.g., every 10 seconds)
////    var interval = 10000;

////    // Start the timer
////    setInterval(function () {
////        // Call the server endpoint to check for updates
////        checkForUpdates();
////    }, interval);
////}

////function checkForUpdates() {
////    // Make an AJAX request to your server endpoint
////    fetch("https://localhost:7053/Home/CheckMsg")
////        .then(response => response.json())
////        .then(data => {
////            // Process the received data and update the UI
////            console.log('Received data:', data);
////        })
////        .catch(error => {
////            // Handle any errors that occur during the request
////            console.error('Error:', error);
////        });
////}
//////// Function to check for updates
//////function checkForUpdates() {
//////    // Make an AJAX request to your server endpoint
//////    $.ajax({
//////        url: "/Home/CheckMsg",
//////        type: "GET",
//////        dataType: "json",
//////        success: function (data) {
//////            // Process the received data and update the UI
//////            console.log('Received data:', data);
//////        },
//////        error: function (error) {
//////            // Handle any errors that occur during the request
//////            console.error('Error:', error);
//////        }
//////    });
//////}
// Function to update the UI with the received message
function updateUI(message) {
    // Display the message in the message container
    var messageContainer = document.getElementById("messageContainer");
    var newMessage = document.createElement("div");
    newMessage.innerHTML = `<p>${message}</p>`;
    messageContainer.appendChild(newMessage);
}

// Function to check for updates
function checkForUpdates() {
    // Make an AJAX request to your server endpoint
    fetch("https://localhost:7053/Home/CheckMsg")
        .then(response => response.json())
        .then(data => {
            // Process the received data and update the UI
            console.log('Received data:', data);
            if (data.message) {
                updateUI(data.message);
            }
        })
        .catch(error => {
            // Handle any errors that occur during the request
            console.error('Error:', error);
        });
}

// Start the timer and check for updates
startTimer();

// Function to start the timer
function startTimer() {
    // Set the interval for checking updates (e.g., every 10 seconds)
    var interval = 10000;

    // Start the timer
    setInterval(function () {
        // Call the server endpoint to check for updates
        checkForUpdates();
    }, interval);
}


