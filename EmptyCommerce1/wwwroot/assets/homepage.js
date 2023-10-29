function testClick(param) {
    //document.querySelector(".loading-box").style.display = 'block'; // show
    console.log('call testClick:', param);
    var postData = param.split(";");
    var data = postData[0];
    var url = postData[1];
    //data.requestFrom = "axios";
    axios.post(url, data)
        .then(function (result) {
            console.log('result.data.statusCode:', result.data.statusCode);
        })
        .catch(function (error) {
            //notification.error("Can not add the product to the cart.\n" + error.response.statusText);
        })
        .finally(function () {
            //document.querySelector(".loading-box").style.display = 'none'; // show
        });

    //return false;
}