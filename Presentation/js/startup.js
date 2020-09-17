
/*
    startup.js
*/

{
    // skapa parkinglot option
    function appendParkingLot(id, name){

        let opt = $('<option value="'+id+'">'+name+'</option>');
        $("#park-form-parkinglot").append(opt);
    }

    // localhost med Docker Compose
    //let url = "http://localhost:5001/api/v1.0/parkinglots";

    // manuell url
    let url = "https://localhost:5001/api/v1.0/parkinglots";
    var errormessage = "";

    // hämta parkinglots
    var jqxhr = $.getJSON(url, function() {
            console.log("Requesting parkinglots from API.");
        }).done(function(data) {
            console.log("Parsing items.");

            $.each(data, function(i, item) {
                $('<option value="'+item.parkinglotId+'">'+item.name+'</option>').appendTo("#park-form-parkinglot");
            });
            console.log("Done parsing items.")
        }).fail(function() {
            errormessage = "Failed to connect to database.";
        })
        .always(function() {
            console.log("Finished loading.");
            connected = true;
            unPausePage();
            if (errormessage.length > 0) {
                appendError(errormessage);  
            }
    });
}
