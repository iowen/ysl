	jQuery(document).ready(function(){
		(function($) {
		  $.fn.sorted = function(customOptions) {
			var options = {
			  reversed: false,
			  by: function(a) { return a.text(); }
			};
			$.extend(options, customOptions);
			$data = $(this);
			arr = $data.get();
			arr.sort(function(a, b) {
			  var valA = options.by($(a));
			  var valB = options.by($(b));
			  if (options.reversed) {
				return (valA < valB) ? 1 : (valA > valB) ? -1 : 0;				
			  } else {		
				return (valA < valB) ? -1 : (valA > valB) ? 1 : 0;	
			  }
			});
			return $(arr);
		  };
		});
	});
	
//  portfolio filter
/* =================================================== */
	jQuery(document).ready(function () {
	
		var $filter = $('#filters a');
		var $portfolio = $('#portfolio_list');
		var $data = $portfolio.clone();

		$filter.click(function(e) {
		  
			if ($($(this)).attr("title") == 'all') {
				
				var $filteredData = $data.find('li');
				  
			} else {
				
				var $filteredData = $data.find('li[data-type*=' + $($(this)).attr("title") + ']');

			}
				
			$portfolio.quicksand($filteredData, {
				adjustHeight: 'dynamic',
				duration: 800
			});
			
			jQuery('#filters a').removeClass('active');
			jQuery(this).addClass('active');
			
			 e.preventDefault();
		  
		});

	});	

