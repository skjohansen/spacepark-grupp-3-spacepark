/* 
    program.js
*/

{
    let url = baseurl + "drivers";
    var errormessage = "";

    // Påbörja ny parkering
    $("#start-parking").on("click", function() {
        if($("#park-form-name").val() < 1 && $("#park-form-driverid").val() < 1) {
            appendError("Du måste ange namn eller förarid");
            return;
        }

        var dataObject = JSON.stringify({
            'name': $('#park-form-name').val()
        });

        // Gör kontroll av namn
        pausePage();
        $.ajax({ 
            url: url,
            data: dataObject,
            type: "POST",
            contentType: "application/json; charset=utf-8"
        }).done(function( data, statusText, xhr ) {
            if(xhr.status == 201) {
                console.log(statusText + ". Driver with id " + data.driverId + " was created.");
                // Få ut en parkingspot

                let url = baseurl + 'parkinglots';
                $.ajax({
                    url : url,
                    data: JSON.stringify({
                        ParkinglotId : $('#park-form-parkinglot').val(),
                        Shipsize : $('#park-form-shipsize').val()
                    }),
                    type: "POST",
                    contentType: "application/json; charset=utf-8"
                }).done(function(response) {
                    // Fått ut en parkingspot!
                    console.log("parkingspot operation complete." + response.parkingspotId);
                });
            }
            else {
                errormessage = "Only people in Star Wars are allowed to park.";
                appendError(errormessage);
            }
        }).always(function() {
            unPausePage();
        });
    });
}