jQuery('.owl-banner-top').owlCarousel({
    loop:true,
    margin:0,
    center: true,
    dots: false,
    nav:false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
});
jQuery('.owl-blog-post').owlCarousel({
    loop:false,
    margin:50,
    nav:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        },
        1000:{
            items:3
        }
    }
});



$(document).ready(function(){
  $(".read-more").click(function () {
    $('.custom-height').addClass('show-custom-height');
    $('.un-read-more').show();
    $('.read-more').hide();
  });
  $(".un-read-more").click(function () {
    $('.custom-height').removeClass('show-custom-height');
    $('.read-more').show();
    $('.un-read-more').hide();
  });
});