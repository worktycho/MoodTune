
var UpdateCallback;

function skipAnalyse(nametrack)
{
    console.log("Skiped")
    changeTrackInfo(nametrack, function(info) {info.skiped += 1; return info})
}

function changeTrackInfo(name, callback)
{
    name = "SONGDATA_" + name;
    var item = localStorage.getItem(name)
    if (item == "[object Object]") {
        item = { skiped: 0, played: 0 }
    } else if (item != null) {
        item = JSON.parse(item);
    } else {
        item = { skiped: 0, played: 0 };
    }
    if (typeof (item) == "number") {
        item = { skiped: item, played: 0 };
    } else if (!(typeof (item) == "object")) {
        item = { skiped: 0, played: 0 };
    }

    item = callback(item);
    localStorage.setItem(name, JSON.stringify(item));
}

function ListenedAnalyse(nametrack) {
    console.log("Listened")
    changeTrackInfo(nametrack, function (info) { info.played += 1; return info })
}