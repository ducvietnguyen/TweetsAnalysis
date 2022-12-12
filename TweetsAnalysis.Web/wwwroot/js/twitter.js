$(() => {
    
    const averageTweetPerMinuteCanvas = document.getElementById('averageTweetPerMinuteCanvas');
    const connection = new signalR.HubConnectionBuilder()
        .withAutomaticReconnect()
        .withUrl("/twitterAnalysisHub").build();

    

    const averageTweetPerMinuteChart = new Chart(averageTweetPerMinuteCanvas, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: 'Average tweets per minute',
                data: [],
                borderWidth: 1
            }]
        }
    });
    
    connection.start().then(function () {
    }).catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("ReceiveTweetsAnalytic", function (totalTweetsReceived, averageTweetPerMinute) {

        $("#totalTweetsReceived").text(totalTweetsReceived);
        
        const date = dayjs().format("HH:mm:ss");
       
        removeData(averageTweetPerMinuteChart);
        addData(averageTweetPerMinuteChart, date, averageTweetPerMinute);
    });
    
    connection.on("ReceiveError", function (message) {
        $("#errorMessage").text(message);
    })

    function addData(chart, label, data) {
        chart.data.labels.push(label);
        chart.data.datasets.forEach((dataset) => {
            dataset.data.push(data);
        });
        chart.update();
    }

    function removeData(chart) {
        chart.data.datasets.forEach((dataset) => {
            if (dataset.data.length > 10) {
                chart.data.labels.shift();
                dataset.data.shift();
            }
        });
        chart.update();
    }
})

