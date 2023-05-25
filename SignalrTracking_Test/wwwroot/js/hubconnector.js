// Establish a SignalR connection
var dataTable;

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/hubconnector") // Adjust the hub URL if necessary
    .build();

$(document).ready(function () { 
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Home/GetAllVehicles"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "vehicletype", "width": "15%" },
            { "data": "platenumber", "width": "15%" },
            { "data": "imei", "width": "15%" },
            { "data": "applicationuserid", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href=""
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}

connection.on("ReceiveVehicleList", () => {
    dataTable.ajax.reload();
    toastr.success("New vehicle recived");
});

function fulfilled() {
    //do something on start
}
function rejected() {
    //rejected logs
}

connection.start().then(fulfilled, rejected);
// Start the connection
//connection.start()
//    .then(() => {
//        console.log("Connected to the SignalR hub");
//    })
//    .catch((error) => {
//        console.error("Error connecting to the SignalR hub:", error);
//    });

//// Receive the list of vehicles from the server
//connection.on("ReceiveVehicleList", (vehicles) => {
//    // Handle the received list of vehicles
//    console.log("Received vehicle list:", vehicles);
//    // You can update your UI or perform any other necessary operations with the vehicle list
//});

//// Receive notification about a user connection
//connection.on("ReceiveUserConnected", (userId, userName) => {
//    // Handle the received user connection notification
//    console.log(`User ${userName} (ID: ${userId}) connected`);
//    // You can update your UI or perform any other necessary operations
//});

//// Other client-side code and event handlers...
