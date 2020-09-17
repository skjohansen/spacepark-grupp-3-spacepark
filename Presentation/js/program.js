/* 
    program.js
*/

{
    let url = "https://localhost:44314/api/v1.0/drivers";

    var errormessage = "";
    $("#start-parking").on("click", function() {
        if($("#park-form-name").val() < 1 && $("#park-form-driverid").val() < 1) {
            appendError("Du måste ange namn eller förarid");
            return;
        }

        var dataObject = JSON.stringify({
            'name': $('#park-form-name').val()
        });

        $.ajax({ 
            url: url,
            data: dataObject,
            type: "POST",
            contentType: "application/json; charset=utf-8",

        })
        .done(function( data ) {
            console.log("Posting done.");
            alert(data.name)
        })
        .fail(function() {
            console.log("Posting failed.");
        });



        // $.post( url )
        // .done(function(data) {
        //     console.log("Posting done.");
        //     console.log(data);
        // })
        // .always(function() {
        //     console.log("Posting finished.");
        // });

        // $.ajax({
        //     url: url,
        //     type: "POST",
        //     data: dataObject,
        //     contentType: "application/json; charset=utf-8",
        //     dataType: "json",
        //     success: function (response) {
        //         console.log("from success: " + response.responseText);
        //     },
        //     error: function (response) {
        //         console.log("from error: " + response);
        //     },
        //     failure: function (response) {
        //         console.log("from failure: " + response.responseText);
        //     }
        // })


    });
}