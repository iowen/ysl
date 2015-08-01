/*

liveSlide - InfinitySlider Extension
Version: 0.5
Author: Epeo
Website: demo.epeo.it/infinityslider

*/
$(document).ready(function() {
	$('.selectpicker').selectpicker();
	var alleffects = '<div class="selecteffectsingle"><select class="form-control selectpicker"><option disabled >--------Slide Screen--------</option><option>slidetopscreen</option><option>slideleftscreen</option><option>sliderightscreen</option><option>slidebottomscreen</option><option disabled >--------Slide--------</option><option>slidetop</option><option>slideleft</option><option>slideright</option><option>slidebottom</option><option disabled >--------Flip--------</option><option>flipleft</option><option>fliptop</option><option>flipbottom</option><option>flipright</option><option disabled >--------Spin--------</option><option>spinleft</option><option>spinright</option><option disabled >--------Zoom--------</option><option>zoomin</option><option>zoomout</option><option disabled >--------Fade--------</option><option>fadein</option></select><div class="effectless"><i class="fa fa-minus-square"></i></div>';
	function changeEffect(value, target_a, target_b) {
		var teffectin;
		var teffectOut;
		var teffectinBack;
		var teffectOutBack;		
		switch (value) { 
			case 'slidetopscreen': 
				teffectIn  = 'slidetopscreen';
				teffectOut  = 'slidebottomscreen';
				break;
			case 'slidebottomscreen': 
				teffectIn  = 'slidebottomscreen';
				teffectOut  = 'slidetopscreen';
				break;
			case 'slideleftscreen': 
				teffectIn  = 'slideleftscreen';
				teffectOut  = 'sliderightscreen';
				break;
			case 'sliderightscreen': 
				teffectIn  = 'sliderightscreen';
				teffectOut  = 'slideleftscreen';
				break;
			/**/
			case 'slidetop+fadein': 
				teffectIn  = 'slidetop fadein';
				teffectOut  = 'slidebottom fadein';
				break;
			case 'slidebottom+fadein': 
				teffectIn  = 'slidebottom fadein';
				teffectOut  = 'slidetop fadein';
				break;
			case 'slideleft+fadein': 
				teffectIn  = 'slideleft fadein';
				teffectOut  = 'slideright fadein';
				break;
			case 'slideright+fadein': 
				teffectIn  = 'slideright fadein';
				teffectOut  = 'slideleft fadein';
				break;
			/**/
			case 'fliptop': 
				teffectIn  = 'fliptop';
				teffectOut  = 'flipbottom';
				break;
			case 'flipbottom': 
				teffectIn  = 'flipbottom';
				teffectOut  = 'fliptop';
				break;
			case 'flipleft': 
				teffectIn  = 'flipleft';
				teffectOut  = 'flipright';
				break;
			case 'flipright': 
				teffectIn  = 'flipright';
				teffectOut  = 'flipleft';
				break;
			/**/
			case 'spinleft': 
				teffectIn  = 'spinleft';
				teffectOut  = 'spinright';
				break;
			case 'spinright': 
				teffectIn  = 'spinright';
				teffectOut  = 'spinleft';
				break;
			/**/
			case 'zoomin': 
				teffectIn  = 'zoomin';
				teffectOut  = 'zoomin';
				break;
			case 'zoomout+fadein': 
				teffectIn  = 'zoomout fadein';
				teffectOut  = 'zoomout fadein';
				break;
			/**/
			case 'fadein': 
				teffectIn  = 'fadein';
				teffectOut  = 'fadein';
				break;
		}
		$('.iS-Item').attr('data-effectin','none');
		$('.iS-Item').attr('data-effectout','none');				
		$('.iS-Next').click();
		setTimeout(function() {
		target_a.attr('data-effectin',teffectIn);
		target_a.attr('data-effectout',teffectOut);
		target_b.attr('data-effectin',teffectIn);
		target_b.attr('data-effectout',teffectOut);
/**/		
		infinitySlider(
			infinitySliderId = 'slide01',
			infinitySliderCommandsClass = 'iS-Commands',
			infinitySliderPreviousButtonClass = 'iS-Previous',
			infinitySliderNextButtonClass = 'iS-Next',
			infinitySliderDotsClass = 'iS-Dots',
			infinitySliderDotClass = 'iS-Dot',
			infinitySliderDotActiveClass = 'iS-Dotactive',
			infinitySliderPlayButtonClass ='iS-Play',
			infinitySliderStopButtonClass ='iS-Stop',
			infinitySliderLoopIndicator ='iS-Loopline',
			infinitySliderContentClass ='iS-Content',		
			infinitySliderItemsClass = 'iS-Items',
			infinitySliderItemClass = 'iS-Item',
			infinitySliderAutoStartLoop = false
		)
/**/
		$('.iS-Items').eq(0).addClass('iS-Preactive iS-Notime').removeClass('iS-Proactive iS-Active iS-Activede');
		$('.iS-Items').eq(1).addClass('iS-Active iS-Activede').removeClass('iS-Preactive iS-Proactive iS-Notime');
		},100);
		setTimeout(function() {
		$('.iS-Next').click();
		},150);
	}
	$('#image').change(function(){
		var target_a = $('.s1i1').parent('.iS-Item');
		var target_b = $('.s2i1').parent('.iS-Item');
		$('.s1i1').parent('.iS-Item').removeClass('quit');
		$('.s1t1').parent('.iS-Item').removeClass('quit');
		$('.s1x1').parent('.iS-Item').removeClass('quit');
		$('.s1b1').parent('.iS-Item').removeClass('quit');
		var value = $(this).val();
		changeEffect(value, target_a, target_b);
		$('.iconrepeat').hide();
		$(this).prev('.iconrepeat').show();
	});
	$('#title').change(function(){
		var target_a = $('.s1t1').parent('.iS-Item');
		var target_b = $('.s2t1').parent('.iS-Item');
		$('.s1i1').parent('.iS-Item').addClass('quit');
		$('.s1t1').parent('.iS-Item').removeClass('quit');
		$('.s1x1').parent('.iS-Item').addClass('quit');
		$('.s1b1').parent('.iS-Item').addClass('quit');
		var value = $(this).val();
		changeEffect(value, target_a, target_b);
		$('.iconrepeat').hide();
		$(this).prev('.iconrepeat').show();
	});
	$('#text').change(function(){
		var target_a = $('.s1x1').parent('.iS-Item');
		var target_b = $('.s2x1').parent('.iS-Item');
		$('.s1i1').parent('.iS-Item').addClass('quit');
		$('.s1t1').parent('.iS-Item').addClass('quit');
		$('.s1x1').parent('.iS-Item').removeClass('quit');
		$('.s1b1').parent('.iS-Item').addClass('quit');
		var value = $(this).val();
		changeEffect(value, target_a, target_b);
		$('.iconrepeat').hide();
		$(this).prev('.iconrepeat').show();
	});
	$('#button').change(function(){
		var target_a = $('.s1b1').parent('.iS-Item');
		var target_b = $('.s2b1').parent('.iS-Item');
		$('.s1i1').parent('.iS-Item').addClass('quit');
		$('.s1t1').parent('.iS-Item').addClass('quit');
		$('.s1x1').parent('.iS-Item').addClass('quit');
		$('.s1b1').parent('.iS-Item').removeClass('quit');
		var value = $(this).val();
		changeEffect(value, target_a, target_b);
		$('.iconrepeat').hide();
		$(this).prev('.iconrepeat').show();
	});
	$('.iconrepeat').click(function(){
		$('.iS-Item').addClass('quite');		
		$('.iS-Next').click();
		setTimeout(function() {
		$('.iS-Item').removeClass('quite');				
		$('.iS-Next').click();
		},150);
	});
	$('.effectless').css({opacity: 0.3});
	$('.effectmore').click(function(){
		var parent = $(this).parent();
		//$(this).next('.selecteffectsingle').clone(true).appendTo(parent);
		parent.append(alleffects);
		$('.selectpicker').selectpicker();			
		effectlessload();
		$('.effectless').css({opacity: 1});
	});
	function effectlessload() {
		$('.effectless').click(function(){
			var parent = $(this).parent().parent();
			var numofeff = parent.find('.selecteffectsingle').length;
			if(numofeff > 1) {
				$(this).parent().remove();
				if(numofeff == 2) {
					$('.effectless').css({opacity: 0.3});
				}
			}
		});
	}
	$('#effectall').click(function() {
		$('.iconrepeat').hide();
		$('.s1i1').parent('.iS-Item').removeClass('quit');
		$('.s1t1').parent('.iS-Item').removeClass('quit');
		$('.s1x1').parent('.iS-Item').removeClass('quit');
		$('.s1b1').parent('.iS-Item').removeClass('quit');
		$('.iS-Item').addClass('quite');		
		$('.iS-Next').click();
		setTimeout(function() {			
			var effectin_image = '';
			var effectout_image = '';
			var effectin_title = '';
			var effectout_title = '';
			var effectin_text = '';
			var effectout_text = '';
			var effectin_button = '';
			var effectout_button = '';
			$('#imageeffectin').find('select.form-control').each(function(){
				effectin_image = effectin_image+$(this).val()+' ';
			});
			$('#imageeffectout').find('select.form-control').each(function(){
				effectout_image = effectout_image+$(this).val()+' ';
			});
			$('#titleeffectin').find('select.form-control').each(function(){
				effectin_title = effectin_title+$(this).val()+' ';
			});
			$('#titleeffectout').find('select.form-control').each(function(){
				effectout_title = effectout_title+$(this).val()+' ';
			});
			$('#texteffectin').find('select.form-control').each(function(){
				effectin_text = effectin_text+$(this).val()+' ';
			});
			$('#texteffectout').find('select.form-control').each(function(){
				effectout_text = effectout_text+$(this).val()+' ';
			});
			$('#buttoneffectin').find('select.form-control').each(function(){
				effectin_button = effectin_button+$(this).val()+' ';
			});
			$('#buttoneffectout').find('select.form-control').each(function(){
				effectout_button = effectout_button+$(this).val()+' ';
			});
			$('.s1i1').parent('.iS-Item').attr('data-effectin',effectin_image);
			$('.s1i1').parent('.iS-Item').attr('data-effectout',effectout_image);
			$('.s1t1').parent('.iS-Item').attr('data-effectin',effectin_title);
			$('.s1t1').parent('.iS-Item').attr('data-effectout',effectout_title);
			$('.s1x1').parent('.iS-Item').attr('data-effectin',effectin_text);
			$('.s1x1').parent('.iS-Item').attr('data-effectout',effectout_text);
			$('.s1b1').parent('.iS-Item').attr('data-effectin',effectin_button);
			$('.s1b1').parent('.iS-Item').attr('data-effectout',effectout_button);
			/**/
			$('.s2i1').parent('.iS-Item').attr('data-effectin',effectin_image);
			$('.s2i1').parent('.iS-Item').attr('data-effectout',effectout_image);
			$('.s2t1').parent('.iS-Item').attr('data-effectin',effectin_title);
			$('.s2t1').parent('.iS-Item').attr('data-effectout',effectout_title);
			$('.s2x1').parent('.iS-Item').attr('data-effectin',effectin_text);
			$('.s2x1').parent('.iS-Item').attr('data-effectout',effectout_text);
			$('.s2b1').parent('.iS-Item').attr('data-effectin',effectin_button);
			$('.s2b1').parent('.iS-Item').attr('data-effectout',effectout_button);
			/**/
			infinitySlider(
				infinitySliderId = 'slide01',
				infinitySliderCommandsClass = 'iS-Commands',
				infinitySliderPreviousButtonClass = 'iS-Previous',
				infinitySliderNextButtonClass = 'iS-Next',
				infinitySliderDotsClass = 'iS-Dots',
				infinitySliderDotClass = 'iS-Dot',
				infinitySliderDotActiveClass = 'iS-Dotactive',
				infinitySliderPlayButtonClass ='iS-Play',
				infinitySliderStopButtonClass ='iS-Stop',
				infinitySliderLoopIndicator ='iS-Loopline',
				infinitySliderContentClass ='iS-Content',		
				infinitySliderItemsClass = 'iS-Items',
				infinitySliderItemClass = 'iS-Item',
				infinitySliderAutoStartLoop = false
			)
			$('.iS-Items').eq(0).addClass('iS-Preactive iS-Notime').removeClass('iS-Proactive iS-Active iS-Activede');
			$('.iS-Items').eq(1).addClass('iS-Active iS-Activede').removeClass('iS-Preactive iS-Proactive iS-Notime');
			/**/
		},100);
		setTimeout(function() {
			$('.iS-Item').removeClass('quite');				
			$('.iS-Next').click();
		},500);
		
	});
	$('#switchtoadvanced').click(function(){
		$('.fastsettings').slideUp(500);
		$('.advancedsettings').slideDown(500);
	});
	$('#switchtofast').click(function(){
		$('.advancedsettings').slideUp(500);
		$('.fastsettings').slideDown(500);
	});
});