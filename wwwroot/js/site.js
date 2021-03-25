$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li>${data}</li>`);

                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });
    })

    $("#clear").click(function () {
        $("#newcomer").val("");
    })
});


function deleteMember(object) {
    console.log(id);
    var id = $(object).parent().index();
    console.log(id); // take the real time index from the list 
    $.ajax({
        url: `/Home/RemoveMember?index=${id}`,
        type: 'DELETE',
        success: function (data) {
            console.log(object);
            $(object).parent().remove();
        },
        error: function (response) {
            alert(response);
            console.log(response);
        },
    });
}
