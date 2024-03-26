

$(document).ajaxSend(function () {
    $("#overlay").fadeIn(300);
});

function ajaxPost(url, data, callBack = (result) => { }) {
    return $.post(url, data, function (result) {
        callBack(result);
    }).always(function () {
        setTimeout(function () {
            $("#overlay").fadeOut(300);
        },100);
    });
}

function ajaxPostWithXHR(url, data) {
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        success: function (r) {
            setTimeout(function () {
                $("#overlay").fadeOut(300);
            }, 100);
            location.reload();
        },
        xhr: function () {
            var filexhr = $.ajaxSettings.xhr();
            if (filexhr.upload) {
                filexhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        $('.upload-percentage').html("%" + Math.trunc(e.loaded / e.total * 100));
                    }
                },false);
            };
            return filexhr;
        }
    });

}


$(document).ready(function () {
    $("#createPostForm").on("submit", function (e) {
        e.preventDefault();
        ajaxPostWithXHR("/Group/CreatePost", new FormData(document.getElementById("createPostForm")));
    })
});
