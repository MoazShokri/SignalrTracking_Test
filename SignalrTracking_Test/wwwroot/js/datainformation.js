// Create a connection to the DataInformationHub
//var connection = new signalR.HubConnectionBuilder()
//    .withUrl("/hubs/datainformationhub")
//    .build();

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
// Receive the message from the hub




//// Start code used
//var lastDisplayedMessage = null;
//var intervalId = null;

//connection.on("ReceiveMessage", function (message) {
//    // Handle the received message
//    console.log("Received message:", message);
//    // Update the UI only if the message is not the same as the last displayed message
//    if (message !== lastDisplayedMessage) {
//        updateUI(message);
//        lastDisplayedMessage = message;
//        restartTimer(); // Restart the timer after a new message arrives
//        // TODO : Code Use
//        //// Update the UI only if the message is not the same as the last displayed message
//        //updateUI(message);
//    };
//});
//    // Start the connection
//    connection.start().then(function () {
//        console.log("SignalR connection established.");

//        // Start the timer to check for updates periodically
//        startTimer();
//    }).catch(function (err) {
//        console.error("Error establishing SignalR connection: " + err);
//    });
//    // Function to update the UI with the received message
//    function updateUI(message) {
//        // Display the message in the message container
//        //var messageContainer = document.getElementById("messageContainer");

//        // Display the message in the message container
//        var messageContainer = document.getElementById("messageContainer");
//        var noNewMessageContainer = document.getElementById("noNewMessageContainer");

//        // Hide the no new message container and show the message container
//        noNewMessageContainer.style.display = "none";
//        messageContainer.style.display = "block";

//        // Clear the existing messages
//        messageContainer.innerHTML = "";

//        // Display the message in the message container
//        var newMessage = document.createElement("div");
//        newMessage.innerHTML = `<p>${message}</p>`;
//        messageContainer.appendChild(newMessage);
//    }
//    // Function to display a message indicating no new message
//   function displayNoNewMessage() {
//     var messageContainer = document.getElementById("messageContainer");
//     var noNewMessageContainer = document.getElementById("noNewMessageContainer");

//     // Hide the message container and show the no new message container
//       messageContainer.style.display = "none";
//     noNewMessageContainer.style.display = "block";
//   }
//    // Function to check for updates
//    function checkForUpdates() {
//        // Make an AJAX request to your server endpoint
//        fetch("https://localhost:7053/Home/CheckMsg")
//            .then(response => response.json())
//            .then(data => {
//                // Process the received data and update the UI
//                console.log('Received data:', data);
//                if (data.message) { // && data.message !== lastDisplayedMessage
//                    updateUI(data.message);
//                    lastDisplayedMessage = data.message;
//                } else {
//                    displayNoNewMessage();
//                }
//                // Check if the car is active or inactive
//                //var isCarActive = data.isCarActive;
//                //// Enable/disable message sending based on the car's status
//                //var sendMessageBtn = document.getElementById("sendMessageBtn");
//                //sendMessageBtn.disabled = !isCarActive;
//            })
//            .catch(error => {
//                // Handle any errors that occur during the request
//                console.error('Error:', error);
//            });
//    }
//    // Old Code Proccess
//    // Start the timer and check for updates
//    //startTimer();

//    // Function to start the timer
//    function startTimer() {
//        // Set the interval for checking updates (e.g., every 10 seconds)
//        var interval = 10000;

//        //// Start the timer // Old Code Proccess
//        //setInterval(function () {
//        //    // Call the server endpoint to check for updates
//        //    checkForUpdates();
//        //}, interval);
//        // Start the timer
//        intervalId = setInterval(function () {
//            // Call the server endpoint to check for updates
//            checkForUpdates();
//        }, interval);
//}
//// Function to restart the timer
//function restartTimer() {
//    // Clear the previous timer
//    clearInterval(intervalId);

//    // Start the timer again
//    startTimer();
//}
//// end code used


var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/datainformationhub")
    .build();

var lastDisplayedMessage = null;
var intervalId = null;

connection.on("ReceiveMessage", function (message) {
    console.log("Received message:", message);

    if (message !== lastDisplayedMessage) {
        updateUI(message);
        lastDisplayedMessage = message;
        restartTimer();
    }
});

connection.start()
    .then(function () {
        console.log("SignalR connection established.");
        startTimer();
    })
    .catch(function (err) {
        console.error("Error establishing SignalR connection: " + err);
    });

function updateUI(message) {
    var messageContainer = document.getElementById("messageContainer");
    var noNewMessageContainer = document.getElementById("noNewMessageContainer");
    if (messageContainer && noNewMessageContainer) {
        noNewMessageContainer.style.display = "none";
        messageContainer.style.display = "block";
        messageContainer.innerHTML = "";

        var newMessage = document.createElement("div");
        newMessage.innerHTML = `<p>${message}</p>`;
        messageContainer.appendChild(newMessage);
    } else {
        console.error("Failed to find message container elements.");

    }
}

function displayNoNewMessage() {
    var messageContainer = document.getElementById("messageContainer");
    var noNewMessageContainer = document.getElementById("noNewMessageContainer");

    messageContainer.style.display = "none";
    noNewMessageContainer.style.display = "block";
}

function checkForUpdates() {
    fetch("https://localhost:7053/Home/CheckMsg")
        .then(response => response.json())
        .then(data => {
            console.log('Received data:', data);
            if (data.message) {
                updateUI(data.message);
                lastDisplayedMessage = data.message;
            } else {
                displayNoNewMessage();
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function startTimer() {
    var interval = 10000;
    intervalId = setInterval(checkForUpdates, interval);
}

function restartTimer() {
    clearInterval(intervalId);
    startTimer();
}


