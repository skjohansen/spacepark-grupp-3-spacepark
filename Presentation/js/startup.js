
/*
    Uppstartsfunktioner
*/
function appendParkingLot(id, name){

    let opt = $('<option value="'+id+'">'+name+'</option>');
    $("#park-form-parkinglot").append(opt);
}


// 
let settingsurl = "http://localhost:5001/api/v1.0/parkinglots";
var errormessage = "";

var jqxhr = $.getJSON( settingsurl, function() {
        console.log("Requesting parkinglots from API.");
    }).done(function(data) {
        console.log("Parsing items.");

        var items = [];
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
        $(".loading").fadeOut();
        if (errormessage.length > 0) {
            appendError(errormessage);  
        }
});
