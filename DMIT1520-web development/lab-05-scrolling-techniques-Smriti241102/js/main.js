// This ultimately wasn't used because building a fully-functional hamburger navigation might take too much time.

document.querySelector('.menu').addEventListener('click', () => {    
    document.querySelector('body').classList.toggle('show-nav');
});  