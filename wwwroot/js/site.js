$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`
                <li class="member">
                    <span class="name">${data}</span><span class="delete fa fa-pencil"></span><i class="fa fa-remove" onclick="deleteMember(this)"></i>
                </li>`);

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

    var index = $(object).parent().index(); // take the real time index from the list 
    console.log(index); 
    $.ajax({
        url: `/Home/RemoveMember?index=${index}`,
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
