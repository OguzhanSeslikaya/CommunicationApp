signalRStart('/userMessages');
signalROn('message', function (r) {
    if (groupId == r.groupId) {
        if (targetUserId == r.senderId) {
            $('.message-box').append(`<p b-med6ecfe48 class="p-left">${r.message}<p/>`);
            var element = $(".message-box");
            element.scrollTop(element.prop("scrollHeight"));
        }
    }
}); 

signalROn('voiceCallReceive', function (r) {
    callId = r.callId;
    voiceCallReceiveAlert(r);
});

signalROn('voiceCallRequest', function (r) { 
    if (groupId == r.groupId) {
        voiceCallRequestAlert(r);
    }
});

signalROn('endCall', function () {
    swal.close()
});

signalROn('voiceCall', function (r) {
    try {
        var blobURL = URL.createObjectURL(base64ToBlob(r.voice, "audio/wav"));

        var audioElement = new Audio();

        audioElement.src = blobURL;


        audioElement.play()
            .catch(error => console.error(error));
    } catch (error) {
        console.error('Error:', error);
    }
});

function base64ToBlob(base64Data, contentType) {

    const prefix = "data:audio/wav;base64,";
    base64Data = base64Data.slice(base64Data.indexOf(prefix) + prefix.length);

    contentType = contentType || '';
    const sliceSize = 1024;
    const byteCharacters = atob(base64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    return new Blob(byteArrays, { type: contentType });
}