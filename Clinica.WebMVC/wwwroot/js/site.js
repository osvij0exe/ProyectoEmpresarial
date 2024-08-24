// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*                  hamburger menú                */

let menu = document.querySelector('#menu-btn');
let mynavbar = document.querySelector('.myNavbar');

menu.onclick = () => {
    menu.classList.toggle('bi-x-lg');
    mynavbar.classList.toggle('active');
}


window.onscroll = () => {
    menu.classList.remove('bi-x-lg');
    mynavbar.classList.remove('active');
}


