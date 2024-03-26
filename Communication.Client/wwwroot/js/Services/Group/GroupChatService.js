let targetUserId;
let targetUserName;
var callId;



function displayChat() {
    $('.chat').toggleClass('chat-display');
}
function changeMessages(url, data) {
    targetUserId = data.targetUserId;
    targetUserName = data.targetUserName;
    $('.message-box').html('');
    ajaxPost(url, data, (r) => {
        for (let i = 0; i < r.messages.messages.length; i++) {
            if (r.messages.messages[i].didISend) {
                $('.message-box').append(`<p b-med6ecfe48 class="p-right">${r.messages.messages[i].message}<p/>`);
            } else {
                $('.message-box').append(`<p b-med6ecfe48 class="p-left">${r.messages.messages[i].message}<p/>`);
            }
        }
        var element = $(".message-box");
        element.scrollTop(element.prop("scrollHeight"));
    });
}

async function sendMessage(data) {
    if (targetUserId) {
        data.receiverId = targetUserId;
        $('#message-input-box').val('');
        $('.message-box').append(`<p b-med6ecfe48 class="p-right">${data.message}<p/>`);
        var element = $(".message-box");
        element.scrollTop(element.prop("scrollHeight"));
        await signalRInvoke("userSendMessageAsync", data);
    }  
}

function voiceCallRequest(data) {
    data.receiverId = targetUserId;
    signalRInvoke("userSendVoiceCallRequest", data);
}

var callIntervalId;
var longDataBlob = new Blob();

var audio_context1;
var recorder1;
var audio_stream1;

var audio_context2;
var recorder2;
var audio_stream2;
function sendVoice(receiverId) {
    let data = { voice: '', senderId: '', receiverId: '' };
    data.senderId = myUserId;
    data.receiverId = receiverId;

    audio_context1 = new AudioContext;
    audio_context2 = new AudioContext;

    navigator.mediaDevices.getUserMedia({ audio: true }).then(function (stream) {
        audio_stream1 = stream;
        audio_stream2 = stream;
        var input1 = audio_context1.createMediaStreamSource(stream);
        var input2 = audio_context2.createMediaStreamSource(stream);
        recorder1 = new Recorder(input1);
        recorder2 = new Recorder(input2);
        recorder1.record()
        recorder2.record();
        callIntervalId = setInterval(function () {

            recorderReload(data);
        }, 300);
    });
}


function recorderReload(postData) {
    recorder1.stop();

    recorder1.exportWAV(function (blob) {
        recorder1.clear();
        recorder1.record();
        getBase64(blob, blob.type, postData);
    }, "audio/wav");
}





function stopAudioCapture() {
    audio_stream1.getTracks().forEach(track => track.stop());

}

function endCall() {

    clearInterval(callIntervalId);

    recorder1.stop();
    recorder2.stop();
    recorder2.exportWAV(function (blob) {
        recorder2.clear();

        const reader = new FileReader();
        reader.onloadend = () => {
            const formData = new FormData();
            formData.append('callId', callId);
            formData.append('groupId', groupId);
            formData.append('data', reader.result);

            fetch("/Group/postVoiceBytes", {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                },
            })
            //ajaxPost("/Group/postVoiceBytes", { dataString: reader.result, callId: callId, groupId: groupId });
        };
        stopAudioCapture();
        reader.readAsDataURL(blob);

    }, "audio/wav");
        
        

    
}

function arrayBufferToBase64(arrayBuffer) {
    const uint8Array = new Uint8Array(arrayBuffer);
    const binaryString = String.fromCharCode.apply(null, uint8Array);
    return btoa(binaryString);
}

function getBase64(file, fileType, data) {

    var type = fileType.split("/")[0];
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        data.voice = reader.result;
        signalRInvoke("sendVoice", data);
    }   
}

