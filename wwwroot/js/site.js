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
                    <span class="name">${data}</span><i class="fa fa-remove" onclick="deleteMember(this)"></i><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
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

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        console.log('submit changes to server');

        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");
        console.log(`/Home/UpdateMember?index=${index}&newName=${newName}`);


        //edit name
        $.ajax({
            url: `/Home/UpdateMember?index=${index}&newName=${newName}`,
            type: "PUT",
            success: function (data) {
                console.log("succes");

            },
            error: function (data) {
                alert(`Failed to edit`);
            },
        });


    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
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


