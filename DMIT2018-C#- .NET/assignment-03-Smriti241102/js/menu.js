
function myFunction() {
  var menuItems = document.getElementById("menu-items");
  var menuContainer = document.getElementById("menu")
  var searchBar = document.getElementById("searchbar")
  if (menuItems.style.display === "flex") {
    menuItems.style.display = "none";
    menuContainer.style.flexDirection = "column"
    searchBar.style.display= "none"
  } else {
    menuItems.style.display = "flex";
    menuItems.style.flexDirection = "column";
    menuContainer.style.flexDirection = "column";
    searchBar.style.display= "flex";
  }
}

function Validate(){
  var requiredInputs = document.getElementsByClassName("requireD");
  console.log(requiredInputs.length);
  console.log(requiredInputs);
  Array.from(requiredInputs).forEach(element=>{
    if(element.value == ""){
      element.style.borderColor = "rgb(235, 0, 0)";
    }
  })
}
