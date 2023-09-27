// Include your assignment 1 below.
// Read the PDF for instruction on what to do.
// Ensure that you look at the "Marking Key" section of the PDF
// to not lose any marks.


import 'bootstrap/dist/css/bootstrap.min.css';

import { getWeather } from './api/base';
import { renderWeather } from './dom/weather';

const weatherform = document.querySelector("#weather-search");

weatherform.addEventListener('submit', e => {
    e.preventDefault();

    let citynamesearch = document.querySelector('input').value;

    console.log(citynamesearch);

    getWeather(citynamesearch)
    .then(data => {
		console.log(`.then now running`);
		let report=renderWeather(data);

		let element = document.querySelector("#weather-container");
        
        console.log(element.innerHTML);
        console.log(report);
        
        element.innerHTML += report;
	  })
	  .catch(err => console.log(err));
});