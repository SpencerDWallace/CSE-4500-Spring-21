const http = require('http');
let sunrise;

    fetch('https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=today')
        .then(res => res.json()) //parses json
        .then(data => {console.log(data.results.astronomical_twilight_end);
        sunrise = data});

    function onRequest(request, response){
        response.writeHead(500, {'Content-Type': 'text/html'});
        response.write(JSON.stringify(sunrise));
        response.end();
    }

    http.createServer(onRequest).listen(8888);

