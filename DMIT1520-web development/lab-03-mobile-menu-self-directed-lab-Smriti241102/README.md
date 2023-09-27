# dmit1530-lab-03

## Lab 03: Toggle Navigation With Drop-Down Menu

**Weight**: 5% of your final grade

---

## Instructions

Clone a copy of this repository to your device so that you can develop it locally. When you finish, make sure to push your latest commit to GitHub Classroom. 

For this Lab, you will create a website with a fully responsive navigation. In the smallest (mobile) view, the user should be able to show and hide (toggle) the navigation with the click of a button. In the largest (desktop) view, the user should be able to hover or tab over a dropdown menu. 

This Lab will be  a Code-A-Long where you are expected to put in the final touches.

---

## The Build Methodology

Start by writing all of your HTML for ``index.html``. Remember to fill out all of your ``<meta>`` element information.

Next, you will need a ``<header>``, followed by a ``<main>``. 

Inside of your ``<header>``, you'll need a first-level heading, your monkey logo, and a ``<nav>`` with three drop-down menus. These menus should be structured as follows: 

    1. GAMES
        - CS:GO
        - DOTA 2
        - Fortnite
        - League of Legends
        - Overwatch
        - Pokémon
        - PUGB
    
    2. SCHEDULE
        - Australia & EU
        - East Asia
        - North America
    
    3. REGISTER
        - Individual Competitors
        - Team Events

Because of the different states and layouts of your navigation menu, you will need a number of wrappers and helper classes (ex. .inner-container, .wide-flex, and .flex-container).

The skeleton of your code should look something like this:

```HTML
	    <header>
        <div class="inner-container wide-flex">
            <div class="flex-container">
                <!-- First-Level Heading Here -->
                <div class="menu-icon">
                    <!-- Controller Icon Here -->
                </div> <!-- end of .menu-icon -->
            </div> <!-- end of .flex-container -->
            <nav>
                <ul class="main-menu">
                    <!--  -->
                </ul>
            </nav> 
        </div> <!-- end of .inner-container -->
    </header>
    <main>
        <!-- Main Content Here -->
    </main>
```

### CSS

With your instructor, work through your CSS. 

Begin with universal styles and helper classes, your mobile view, any classes necessary for your toggle menu and JavaScript, and finally your media queries. 


## JS

Be mindful of your CSS and the class names that you gave to specific elements when using the toggle script that we learned during our hamburger menu demo. 


### On Your Own

On your own, research at least two (2) new CSS properties on MDN. These properties should be ones that we have not covered in class. 

When you have chosen the properties you'd like to use, add them to your page. In order to make them work, you may add additional elements to the `<main>` area, including text and/or images.

Secondly, tell me what 2 new CSS properities you used and what they are doiing in the online text editor in Moodle. 

Some ideas include:

* animation
* backdrop-filter
* filter
* mix-blend-mode
* transform
