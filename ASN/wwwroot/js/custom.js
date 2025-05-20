(function ($) {
    'use strict';
    $(function () {
        //search box 
        $('.search-box input').on("mouseenter focusin", function () {
            $('.search-box input').addClass('search-txt-btn');
        }).on("mouseleave focusout", function () {
            if ($('.search-box input').val().trim() == '') {
                $('.search-box input').removeClass('search-txt-btn');
            }
        });

        //file reader/uploader
        var readURL = function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(input).parent().parent().parent().find('.profile-pic').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $(document).on('change', ".file-upload", function () {
            readURL(this);
        });

        $(document).on('click', ".upload-button", function () {
            $(this).parent().find(".file-upload").click();
        });

        //steper
        /*Activate default tab contents*/
        var leftPos, newWidth, $magicLine, defaultActive;

        defaultActive = $(".tabs li.active a").attr("href");
        $(defaultActive).show();

        $(".tabs").append("<li id='magic-line'></li>");
        $magicLine = $("#magic-line");
        $magicLine
            .width($(".active").width())
            .css("left", $(".active a").position().left)
            .data("origLeft", $magicLine.position().left)
            .data("origWidth", $magicLine.width());

        $(".tabs li a").click(function () {
            var $this, tabId, leftVal, $tabContent;
            $this = $(this);
            $tabContent = $(".tabContent");
            $this.parent().addClass("active").siblings().removeClass("active");
            tabId = $this.attr("href");

            leftVal = $($tabContent).index($(tabId)) * $tabContent.width() * -1;
            $(".tabWrapper").stop().animate({ left: leftVal });

            $magicLine
                .data("origLeft", $this.position().left)
                .data("origWidth", $this.width() + 40);
            return false;
        });

        /*Magicline hover animation*/
        $(".tabs li")
            .find("a")
            .hover(
                function () {
                    var $thisBar = $(this);
                    leftPos = $thisBar.position().left;
                    newWidth = $thisBar.parent().width();
                    $magicLine.stop().animate({ left: leftPos, width: newWidth });
                },
                function () {
                    $magicLine.stop().animate({
                        left: $magicLine.data("origLeft"),
                        width: $magicLine.data("origWidth")
                    });
                }
            );


        $('.btnNext').click(function () {
            $('.tabs > .active').next('li').find('a').trigger('click');
        });

        $('.btnPrevious').click(function () {
            $('.tabs > .active').prev('li').find('a').trigger('click');
        });
        //accordionBtns
        const accordionBtns = document.querySelectorAll(".accordion");
        if (accordionBtns.length > 0) {
            accordionBtns.forEach((accordion) => {
                accordion.onclick = function () {
                    this.classList.toggle("is-open");

                    let content = this.nextElementSibling;
                    if (content != null && content != undefined) {
                        if (content.style.maxHeight) {
                            //this is if the accordion is open
                            content.style.maxHeight = null;
                        } else {
                            //if the accordion is currently closed
                            content.style.maxHeight = content.scrollHeight + "px";
                        }
                    }
                };
            });
        }

    });
})(jQuery);


