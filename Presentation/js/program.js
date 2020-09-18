/* 
    program.js
*/

{
    let url = "https://localhost:5001/api/v1.0/drivers";

    // Påbörja ny parkering

    var errormessage = "";
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
            contentType: "application/json; charset=utf-8",

        }).done(function( data, statusText, xhr ) {
            if(xhr.status == 201) {
                console.log(statusText + ". "+ data.name + " with id: " + data.driverId + " is part of Star Wars universe.");
            }
            else {
                errormessage = "not part of star wars";
                appendError(errormessage);
            }
        }).fail(function() {
            console.log("Request failed");
        }).always(function() {
            unPausePage();
        });
    });
}