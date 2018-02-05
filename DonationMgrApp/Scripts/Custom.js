$(function () {
    $(".deleteOgr").click(function () {
        var ogrid = $(this).attr("data-id");
        if (ogrid != '') {
            if (confirm('Are you sure you want to delete record?')) {
                $.post("/Organisation/Delete", { "id": ogrid }, function (data) {

                    if (data){                                               
                        window.location.href = '/Organisation/Index';
                        alert("Record has been deleted successfully.");
                    }

                });

            }
        }
    });
});