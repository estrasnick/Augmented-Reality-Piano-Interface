(function($) 
{
    var supported = (window.File && window.FileReader && window.FileList && window.Blob);

    // extend the api
    var api = $.fn.alphaTab.fn;
    
    api.drop = function(element, context, args) 
    {
        var drop = element.data('alphaTab.drop');
        if(!drop)
        {
            var self = this;
            if(!supported) { $.error('File API not supported'); }

            element
            .on('dragenter', function(e) {
                e.stopPropagation();
                e.preventDefault();
                element.addClass('drop');
            })
            .on('dragover', function(e) {
                e.stopPropagation();
                e.preventDefault();
            })
            .on('drop', function(e) {
                element.removeClass('drop');
                e.preventDefault();
                // when dropping files, load them using FileReader
                var files = e.originalEvent.dataTransfer.files;
                if(files.length > 0) {
                    var reader = new FileReader();
                    reader.onload = (function(e) {
                        // call load
                        api.load(element, context, e.target.result);
                    });
                    reader.readAsArrayBuffer(files[0]);
                }
            });
        }
    };
   

})(jQuery);