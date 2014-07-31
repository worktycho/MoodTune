


function skipAnalyse(nametrack)
{
    changeTrackInfo(nametrack, function(info) {info.skiped += 1; return info})
}

function changeTrackInfo(name, callback)
{
    name = "SONGDATA_" + name;
    var item = localStorage.getItem(name)
    if (item) {
        if (typeof (item) == "number") {
            item = { skiped: item, played: 0 };
        }
    }
    else {
        item = { skiped: 0, played: 0 };
    }
    item = callback(item);
    localStorage.setItem(name, item);
}

function ListenedAnalyse(nametrack) {
    changeTrackInfo(nametrack, function (info) { info.played += 1; return info })
}