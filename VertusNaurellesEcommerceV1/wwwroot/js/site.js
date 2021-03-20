
//scroll navbar---------------------------------------------------
let prevScrollpos = window.pageYOffset;
let linkLogo = document.getElementById("linkLogo")
window.onscroll = function() {
let currentScrollPos = window.pageYOffset;
  if (prevScrollpos > currentScrollPos) {
      document.getElementById("navbar").style.top = "0";
      linkLogo.style.display="block"
  } else {
     document.getElementById("navbar").style.top = "-50px";
      linkLogo.style.display = "none"
  }
  prevScrollpos = currentScrollPos;
}


// Configure Slider---------------------------------------------------
$('.carousel').carousel({
    interval: 4000,
    pause: 'null'
});











