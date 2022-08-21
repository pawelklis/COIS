
var NotifcationsTest = {
    VerifyBrowserSupport: function () {
        return ("Notification" in window);
    },
    ShowNotification: function (msg, title, icon) {
        var img = '/css/icons/' + icon;
        var notification = new Notification(title, { body: msg, icon: img });

    },
    RequestForPermissionAndShow: function (msg, title, icon) {
        // Mamy prawo wyświetlać powiadomienia
        if (Notification.permission === "granted") {
            NotifcationsTest.ShowNotification(msg, title, icon);
        }
        // Brak wsparcia w Chrome dla właściwości permission
        else if (Notification.permission !== "denied") {
            Notification.requestPermission(function (permission) {
                // Dodajemy właściwość permission do obiektu Notification
                if (!("permission" in Notification)) {
                    Notification.permission = permission;
                }
                if (permission === "granted") {
                    NotifcationsTest.ShowNotification(msg, title, icon);
                }
            });
        }
    }
}

function Shown(msg, title, icon) {
    if (!NotifcationsTest.VerifyBrowserSupport()) {
        alert("Brak wsparcia dla Notifications API");
    }
    NotifcationsTest.RequestForPermissionAndShow(msg, title, icon);
};



  window.triggerFileDownload = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
  }
