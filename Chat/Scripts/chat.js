var displayName;

$(document).ready(function () {

    function chatViewModel() {
        var self = this;

        function chatMessage(name, message) {
            this.name = ko.observable(name);
            this.message = ko.observable(message);
        }

        self.message = ko.observable("");
        self.chatBody = ko.observableArray();
        self.users = ko.observableArray();

        self.AddChatMessage = function (name, message) {
            self.chatBody.push(new chatMessage(name, message));
        };

        self.ChatMessageKeyup = function (data, event) {
            if (self.message().length == 0)
                return;
            
            var code = event.which;
            if (code == 13)
                self.SendMessage();
        };

        self.SendMessage = function () {
            sendMessage(self.message());
            self.message("");
        };
    }
    
    var viewModel = new chatViewModel();
    ko.applyBindings(viewModel);

    $.getJSON('/api/ChatApi/GetAllUsers').done(function (data) {
        $.each(data, function (key, item) {
            viewModel.users.push(item);
        });
    });

    var chat = $.connection.chatHub;

    chat.client.broadcastMessage = function (name, message) {
        viewModel.AddChatMessage(name, message);
        var body = $('#chatBody');
        body.scrollTop(body.prop("scrollHeight"));
    };

    $('#chat-message').focus();

    $.connection.hub.start().done(function () {
        //I can do something after connection.
    });

    //UserLogIn
    chat.client.broadcastUserLogIn = function (username) {
        viewModel.users.push(username);
        viewModel.users.sort();
    };

    //UsetLogOut
    chat.client.broadcastUserLogOut = function (username) {
        viewModel.users.remove(username);
    };

    function sendMessage(message) {
        chat.server.send(displayName, message);
    }

    //KeepAlive
    setInterval(function () {
        $.post('/api/ChatApi/KeepAlive', { '': '@Model.Username' });
    }, 30000);
});