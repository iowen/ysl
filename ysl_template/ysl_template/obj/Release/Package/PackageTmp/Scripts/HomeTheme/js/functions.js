(function($) {
	"use strict";

	//  Mobile menu
	/* =================================================== */
	$(document).ready(function () {
	
		$('nav').meanmenu();
		
	});
	
})(jQuery);
	

(function($) {

	//  Smooth scroll
	/* =================================================== */
	$('a[href*=#]:not([href=#])').click(function() {
	
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') 
			|| location.hostname == this.hostname) {

			var target = $(this.hash);
			target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
			   if (target.length) {
				 $('html,body').animate({
					 scrollTop: target.offset().top
				}, 1500);
				return false;
			}
		}
		
	});
	
})(jQuery);


(function($) {
	
	//  Fixed nav bar
	/* =================================================== */
	
	$(function(){ 
	 
	  if (!!$('.nav-top').offset()) { // make sure ".nav-top" element exists
	 
		var stickyTop = $('.nav-top').offset().top; // returns number 
	 
		$(window).scroll(function(){ // scroll event
	 
		  var windowTop = $(window).scrollTop(); // returns number 
	 
		  if (stickyTop < windowTop){
			$('.nav-top').addClass('sticky');
		  }
		  else {
			$('.nav-top').removeClass('sticky');
		  }
	 
		});
	 
	  }
	 
	});	
	
})(jQuery);


(function($) {

	//  forms infield labels 
	/* =================================================== */
	$(window).load(function(){
	
		if ( $.isFunction($.fn.inFieldLabels) ) {
			$(function(){ $("label").inFieldLabels(); });
		}
		
	});
	
})(jQuery);


(function($) {

	//  Flexslider
	/* =================================================== */
	 $(window).load(function(){
	 
		if ( $.isFunction($.fn.flexslider) ) {
			$('.flexslider').flexslider({
				animation: "face",
				animationLoop: false,
				direction: "horizontal",
				easing: "swing",
				directionNav: false,
				start: function(slider){
				$('body').removeClass('loading');
				}
			});
		}
		
	});
	
})(jQuery);


	//  Infinity Slider
	/* =================================================== */
	
	$(document).ready(function() {
		
		if(typeof(infinitySlider) == "function") {
		
			if ( $("#slide01").size() ) {
			
				infinitySlider(
					infinitySliderId = 'slide01',
					infinitySliderCommandsClass = 'iS-Commands',
					infinitySliderPreviousButtonClass = 'iS-Previous',
					infinitySliderNextButtonClass = 'iS-Next',
					infinitySliderDotsClass = 'iS-Dots',
					infinitySliderDotClass = 'iS-Dot',
					infinitySliderDotActiveClass = 'iS-Dotactive',
					infinitySliderPlayButtonActiveClass ='iS-Playactive',
					infinitySliderStopButtonActiveClass ='iS-Stopactive',
					infinitySliderLoopIndicator ='iS-Loopline',
					infinitySliderContentClass ='iS-Content',   
					infinitySliderItemsClass = 'iS-Items',
					infinitySliderItemClass = 'iS-Item',
					infinitySliderAutoStartLoop = false,
					infinitySliderKeyboardNavigation = true,
					infinitySliderTouchNavigation = 'mobile',
					infinitySliderStarterSlide = 1
				)
				
			}
			
			if ( $("#slide02").size() ) {
			
				infinitySlider(
					infinitySliderId = 'slide02',
					infinitySliderCommandsClass = 'iS-Commands',
					infinitySliderPreviousButtonClass = 'iS-Previous',
					infinitySliderNextButtonClass = 'iS-Next',
					infinitySliderDotsClass = 'iS-Dots',
					infinitySliderDotClass = 'iS-Dot',
					infinitySliderDotActiveClass = 'iS-Dotactive',
					infinitySliderPlayButtonActiveClass ='iS-Playactive',
					infinitySliderStopButtonActiveClass ='iS-Stopactive',
					infinitySliderLoopIndicator ='iS-Loopline',
					infinitySliderContentClass ='iS-Content',   
					infinitySliderItemsClass = 'iS-Items',
					infinitySliderItemClass = 'iS-Item',
					infinitySliderAutoStartLoop = false,
					infinitySliderKeyboardNavigation = true,
					infinitySliderTouchNavigation = 'mobile',
					infinitySliderStarterSlide = 1
				)
				
			}
				
		}

	});
	

(function($) {

	//  portfolio hover effects
	/* =================================================== */
	$(document).ready(function(){
	
		$('#portfolio, .portfolio_masonry').on('mouseenter', 'ul li', function() {
			$(this).find('.label').stop().animate({bottom: 0}, 200, 'easeOutQuad');
			$(this).find('img').stop().animate({top: -40}, 500, 'easeOutQuad');				
		});
		
		$('#portfolio, .portfolio_masonry').on('mouseleave', 'ul li', function() {
			$(this).find('.label').stop().animate({bottom: -50}, 200, 'easeInQuad');
			$(this).find('img').stop().animate({top: 0}, 300, 'easeOutQuad');								
		});	
			
	});

})(jQuery);


(function($) {

	//  skills
	/* =================================================== */
	$(document).ready(function(){
	
		$('.skillbar').each(function(){
		
			$(this).find('.skillbar-bar').animate({
				width:$(this).attr('data-percent')
			},6000);
			
		});
		
	});	
	
})(jQuery);


(function($) {

	//  Colorbox
	/* =================================================== */
	$(window).load(function(){
	
		if ( $.isFunction($.fn.colorbox) ) {
				$('.position').colorbox({
					rel: 'lightbox'
				});
		}
		
	});
	
})(jQuery);


(function($) {

	//  home masonry
	/* =================================================== */
	$(document).ready(function(){
	
		// Prepare layout options.
			var options = {
			container: $('#columns'), // Optional, used for some extra CSS styling
			offset: 30, // Optional, the distance between grid items
			outerOffset: 0, // Optional the distance from grid to parent
			autoResize: true, // This will auto-update the layout when the browser window is resized.
			};
			
			if ( $.isFunction($.fn.imagesLoaded) ) {
				// Get a reference to your grid items.
				var handler = $('#columns .intro_col');
				$('#columns .intro_col').imagesLoaded(function() {
				  // Prepare layout options.
				  // Get a reference to your grid items.
				  // Call the layout function.
				  handler.wookmark(options);
				});
			}
			
			// Call the layout function.
			if ( $.isFunction($.fn.wookmark) ) {
				handler.wookmark(options);
			}
		  
    });
	
})(jQuery);


(function($) {

	// vimeo controls
	/* =================================================== */

	$(document).ready(function(){
	
		if ( $("#vimeo-player").size() ) {
		
			var iframe = $('#vimeo-player')[0];
			var player = $f(iframe);

			$('#stop-vimeo').click(function() {
				player.api('pause');
			});


			$('#play-vimeo').click(function(){
				player.api('play');
			})	
		
		}
		
	});
	
	
})(jQuery);


(function($) {

	// youtube controls
	/* =================================================== */

	$(document).ready(function(){
	
		function callPlayer(frame_id, func, args) {
			if (window.$ && frame_id instanceof $) frame_id = frame_id.get(0).id;
			var iframe = document.getElementById(frame_id);
			if (iframe && iframe.tagName.toUpperCase() != 'IFRAME') {
				iframe = iframe.getElementsByTagName('iframe')[0];
			}
			if (iframe) {
				// Frame exists, 
				iframe.contentWindow.postMessage(JSON.stringify({
					"event": "command",
					"func": func,
					"args": args || [],
					"id": frame_id
				}), "*");
			}
		}
			
	});
	
	
})(jQuery);
