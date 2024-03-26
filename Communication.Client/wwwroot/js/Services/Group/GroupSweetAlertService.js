function voiceCallRequestAlert(data) {
    Swal.fire({
        title: `${data.senderName} named user call you`,
        showDenyButton: true,
        confirmButtonText: "accept",
        denyButtonText: `reject`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            ajaxPost("/Group/createCall", { callerId: data.senderId, calledId: data.receiverId }, (r) => {
                callId = r.callId;
                if (r.succeeded) {
                    signalRInvoke('userSendVoiceCallReceive', { senderId: data.receiverId, receiverId: data.senderId, callId: r.callId });
                    sendVoice(data.senderId);
                    Swal.fire({
                        title: `${data.senderName} in call`,
                        showDenyButton: false,
                        confirmButtonText: "end call",
                    }).then((result) => {
                        signalRInvoke("endCallRequest", data.senderId);
                        endCall();
                        console.log("cagri sonlandirildi.");
                    });
                }
            });
            
            
        } else if (result.isDenied) {
            /*Swal.fire("Call rejected", "", "error");*/
        }
    });
}

function voiceCallReceiveAlert(data) {
    Swal.fire({
            title: `${targetUserName} in call`,
            showDenyButton: false,
            confirmButtonText: "end call",
    }).then((result) => {
        signalRInvoke("endCallRequest", data.senderId);
            endCall();
            console.log("cagri sonlandirildi.");
    });
    sendVoice(data.senderId);    
}