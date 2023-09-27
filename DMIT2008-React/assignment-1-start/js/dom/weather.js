/*
HTML Structure 

<div class="mt-2 card" >
  <div class="card-body">
    <h5 class="card-title">CITY_NAME_HERE, COUNTRY_CODE_HERE</h5>
    <h6 class="card-subtitle mb-2 text-muted">CURRENT_WEATHER_DEGREES_HERE</h6>
    <p class="card-text">WEATHER_DESCRIPTION_HERE</p>
  </div>
</div>

*/

// renderWeather function
renderWeather = (weather) =>
{
  let output ='';
  output+=
  `
  <div class="mt-2 card" >
  <div class="card-body">
    <h5 class="card-title">${weather.name}, ${weather.sys.country}</h5>
    <h6 class="card-subtitle mb-2 text-muted">${weather.main.temp} C</h6>
    <p class="card-text">${weather.weather[0].description}</p>
  </div>
</div>
  `

  return output;
}

// export the renderWeather function
export {renderWeather};
