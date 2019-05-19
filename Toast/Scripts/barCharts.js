

$(function (xLabel, yLabel, xScale, yScale, myData) {

    /**
     * Options for Line chart
     */

    var barData = {
        labels: ["Best Evaluator", "Best Table Topics", "Best Functionary", "Best Speaker"],
        datasets: [
            {
                label: "Curr Month",
                backgroundColor: "rgba(98,203,49,0.5)",
                borderColor: "rgba(98,203,49,0.8)",
                highlightFill: "rgba(98,203,49,0.75)",
                highlightStroke: "rgba(98,203,49,1)",
                borderWidth: 1,
                data: [3, 4, 7, 6]
            },
            {
                label: "Prev Month",
                backgroundColor: "rgba(220,220,220,0.5)",
                borderColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(220,220,220,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                borderWidth: 1,
                data: [2, 4, 5, 6]
            }
        ]
    };

    var barOptions = {
        responsive: true,
        maintainAspectRatio: false,//use available height
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    steps: 1,
                    stepValue: 5,
                    max: 10
                }
            }]
        }
    };

    var ctx = document.getElementById("award-bar-chart").getContext("2d");
    new Chart(ctx, { type: 'bar', data: barData, options: barOptions });

    /**
     * Flot charts 2 data and options
     */
    var chartIncomeData = [
        {
            label: "line",
            data: [[1, 10], [2, 26], [3, 16], [4, 36], [5, 32], [6, 51]] // jsData[0]
        }
    ];

    var chartIncomeOptions = {
        series: {
            lines: {
                show: true,
                lineWidth: 0,
                fill: true,
                fillColor: "#64cc34"

            }
        },
        colors: ["#62cb31"],
        grid: {
            show: false
        },
        legend: {
            show: false
        }
    };

    $.plot($("#flot-income-chart"), chartIncomeData, chartIncomeOptions);

    /*
     * Chart Radar
    */
    var radarData = {
        labels: ["Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running"],
        datasets: [
            {
                label: "Skills",
                backgroundColor: "rgba(98,203,49,0.2)",
                borderColor: "rgba(98,203,49,1)",
                pointBackgroundColor: "rgba(98,203,49,1)",
                pointBorderColor: "#fff",
                pointHoverBackgroundColor: "#fff",
                pointHoverBorderColor: "#62cb31",
                borderWidth: 1,
                data: [65, 59, 66, 45, 56, 55, 40]
            }
        ]
    };

    var radarOptions = {
        responsive: true
    };

    var ctx = document.getElementById("radarChart").getContext("2d");
    new Chart(ctx, { type: 'radar', data: radarData, options: radarOptions });


});