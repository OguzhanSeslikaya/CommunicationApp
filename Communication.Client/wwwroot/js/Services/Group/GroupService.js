function leaveGroup(url,data,callBackData){
    ajaxPost(url, data, (r) => {
        if (r.succeeded) {
            /*$(`#${callBackData}`).fadeOut('slow');*/
            location.reload();
        }
    });
}

function addGroup(url, data) {
    ajaxPost(url, data, (r) => {

        document.getElementById("groupName").value = "";

        if (r.succeeded) {
            //$('.groups-table tbody').append(`<tr b-tmnw7iftqs class="groups-tr" style="display:none;" id=${r.group.id}><td b-tmnw7iftqs class="groups-td">${r.group.groupName}</td><td b-tmnw7iftqs class="groups-td">5</td><td b-tmnw7iftqs class="groups-td">${r.group.myRole}</td><td b-tmnw7iftqs class="groups-td"><button b-tmnw7iftqs class="group-btn" onclick="goGroup('/Group/goGroup',{id:'@group.id'})">Go</button></td><td b-tmnw7iftqs class="groups-td"><button b-tmnw7iftqs class="group-btn" onclick="leaveGroup('/Home/leaveGroup',{id:'${r.group.id}',groupName:'${r.group.groupName}',myRole:'${r.group.myRole}'},'${r.group.id}')">Leave</button></td></tr>`);
            //$(`#${r.group.id}`).fadeIn('slow');
            location.reload();
        } else if (r.succeeded == false) {
            if (r.message) {
                validationAlert(r.message);
            }
        }

    });
}

function goGroup(url,data) {

    ajaxPost(url, data, (r) => {
        location.href = "/group/inGroup";
    });

}

function downloadPostFile(url,data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
    });
}

function slideCreatePost() {


    $(".post-form-in-group").slideToggle("slow");
}


function createPost(url, data){
    let formData = new FormData();
    formData.append("file", data.filePath);
    ajaxPost(url, data, (r) => {
        console.log(r);
    });
}

function RequestToJoinGroup(url, data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
        location.reload();
        console.log(r);
    });
}

function removeRequest(url, data) {
    ajaxPost(url, data, (r) => {
        console.log(r);
        location.reload();
        console.log(r);
    });
}