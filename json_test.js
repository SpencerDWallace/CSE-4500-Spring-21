/*
Spencer Wallace - 007463307
CSE 4500 - JSON(Core Project)
01/25/2021
*/

let http = require('http');

const stu = '{"name": "Spencer Wallace", "id": "007463307",' +
    ' "major": "Computer Science and Engineering", "year": 3}';

let obj = JSON.parse(stu);

console.log("----------before stringify----------\n");
console.log(obj);

console.log("----------after stringify----------\n");
console.log(JSON.stringify(obj));

function onRequest(request, response){
    response.writeHead(500, {'Content-Type': 'text/plain'});
    response.write(JSON.stringify(obj));
    response.end();
}

http.createServer(onRequest).listen(8888);



