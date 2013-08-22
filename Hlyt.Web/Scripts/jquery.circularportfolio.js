;(function ($) 
{
    $.fn.cirpor = function() {
        move($('#coach-about'), 2000, 2);

        this.each(function()
        {
            $( this ).click(function (e) {  
                e.preventDefault();
            });
            $( this ).mouseover(
                function () {
                    var $this = $(this);
                    move($this, 800, 1);
                }
            );
        });
    };
    
    function move( $elem, speed, turns )
    {
        var id = $elem.attr('id');
        var $circle = $('#circle-' + id);

        if ($circle.css('opacity') == 1)
            return;

        $('#image-' + id).stop(true, true).fadeIn(650).siblings().not(this).fadeOut(650);

        $('#coach .coach-circle').each(function (i) {
            var $theCircle = $(this);
            if ($theCircle.css('opacity') == 1)
                $theCircle.stop()
                .animate({
                    path: new $.path.arc({
                        center: [241, 201],
                        radius: 260,
                        start: 65,
                        end: -110,
                        dir: -1
                    }),
                    opacity: '0'
                }, 1500);
            else
                $theCircle.stop()
                .animate({ opacity: '0' }, 200);
        });

        var end = 65 - 360 * (turns - 1);
        $circle.stop()
            .animate({
                path: new $.path.arc({
                    center: [241, 201],
                    radius: 260,
                    start: 180,
                    end: end,
                    dir: -1
                }),
                opacity: '1'
            }, speed);
    }
}(jQuery));