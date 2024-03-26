let _connection;

function signalRStart(url) {
    _connection = new signalR.HubConnectionBuilder()
        .withUrl(url, {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => localStorage.getItem('access_token'),
            webSocketOptions: {
                maxReceiveMessageSize: 102400000, // Örnek: 100 MB
                maxSendMessageSize: 102400000 // Örnek: 100 MB
            }
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

    _connection.start().then(() => {
            console.log("connected");
        }).catch(error => setTimeout(() => {
            start(url)
        }, 2000));
}

async function signalRInvoke(methodName, data) {
    await _connection.invoke(methodName,data);
}

function signalROn(methodName, callBack = (...message) => { }){
    _connection.on(methodName, callBack);
}