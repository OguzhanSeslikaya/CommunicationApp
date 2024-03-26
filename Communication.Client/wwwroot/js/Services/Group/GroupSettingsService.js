function declineRequest(url, data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
        location.reload();
    });
}

function acceptRequest(url, data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
        location.reload();
    });
}

function kickMember(url, data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
        location.reload();
    });
}