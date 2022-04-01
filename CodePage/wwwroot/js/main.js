$(function(){
    let scroll=$(window).scrollTop();
    $(window).scroll(function(){
        if($(window).scrollTop()>300){
            $(".navbar").addClass("scrolled");
        }else{
            $(".navbar").removeClass("scrolled");
        }
    })
    $('.owl-carousel').owlCarousel({
        loop:true,
        margin: 0,
        mouseDrag:true,
        autoplay: true,
        dots: false,
        nav: false,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
    if ($("#messageAdded").val()) {
        alert($("#messageAdded").val());
    }
})