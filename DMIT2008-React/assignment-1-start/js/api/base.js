// replace your api key 
const API_KEY = "43d8805ecc697a9e82a1d60b47d2c801"

// create getWeather function here
async function getWeather(cityname)
{
    var url = `https://api.openweathermap.org/data/2.5/weather?q=${cityname}&appid=${API_KEY}&units=metric`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
                  'Content-Type': 'application/json'
              },
      });
      console.log(`response: ${response}`);
      const responseData = await response.json();
      console.log(`responseData: ${responseData}`);

      return responseData;

    //  const responseData = await                          

    // {"coord":{"lon":-113.4687,"lat":53.5501},"weather":[{"id":803,"main":"Clouds","description":"broken clouds","icon":"04n"}],"base":"stations","main":{"temp":-14.14,"feels_like":-19.88,"temp_min":-15.54,"temp_max":-13.01,"pressure":1012,"humidity":80},"visibility":10000,"wind":{"speed":2.57,"deg":190},"clouds":{"all":67},"dt":1675414824,"sys":{"type":2,"id":2074442,"country":"CA","sunrise":1675437361,"sunset":1675469959},"timezone":-25200,"id":5946768,"name":"Edmonton","cod":200};

    //  return responseData;
}
// export the getWeather function
export {getWeather};