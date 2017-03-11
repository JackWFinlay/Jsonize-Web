$(document).ready(function () {

    var hamburgerOpen = false;

    var openMenu = function () {

        $("#nav").animate({ height: "290px" }, 250);
        $("#menu > li").css("display", "block");

        hamburgerOpen = true;
    };

    var closeMenu = function () {

        if ($(window).width() <= 1070) {
            $("#nav").animate({ height: "100px" }, 250, function () {
                $("#menu > li:not(:first-child)").css("display", "none");
            });


        }

        hamburgerOpen = false;
    };

    $("#hamburger").click(function () {
        if (!hamburgerOpen) {
            openMenu();
        } else {
            closeMenu();
        }

    });

    $("a.anchor").not(".button").click(function (event) {
        event.preventDefault();

        $("html, body").animate({
            scrollTop: $($.attr(this, "href")).offset().top
        }, 400, function () {
            closeMenu();
        }
		);

    });

    $(window).scroll(function () {
        if ($(window).scrollTop() > ($(window).height() - 100)) {
            $("#nav").addClass("scroll-fix");
        } else if (!hamburgerOpen) {
            $("#nav").addClass("hide-scroll-fix");
            $("#nav").removeClass("scroll-fix");

            setTimeout(function () {
                $("#nav").removeClass("hide-scroll-fix");
            }, 500);
        }

    });

    $(window).resize(function () {
        if ($(window).width() > 1070) {
            $("#menu > li").css("display", "inline-block");
        } else {
            $("#menu > li:not(:first-child)").css("display", "none");
        }

    });

    $("#search-box").bind("keypress keydown keyup", function (e) {
        if (e.keyCode === 13) { e.preventDefault(); }
    });

    $("#submit").click(function() {
        submit();
    });

    function submit() {
        var emptyTextNodeHandling = "ignore";
        var nullValueHandling = "ignore";
        var textTrimHandling = "trim";
        var classAttributeHandling = "array";
        var renderJavascript = "false";

        if ($("#emptyTextNodeHandlingInclude").is(":checked")) {
            emptyTextNodeHandling = "include";
        } else if ($("#emptyTextNodeHandlingIgnore").is(":checked")) {
            emptyTextNodeHandling = "ignore";
        }

        if ($("#nullValueHandlingInclude").is(":checked")) {
            nullValueHandling = "include";
        } else if ($("#emptyTextNodeHandlingIgnore").is(":checked")) {
            nullValueHandling = "ignore";
        }

        if ($("#textTrimHandlingTrim").is(":checked")) {
            textTrimHandling = "trim";
        } else if ($("#emptyTextNodeHandlingInclude").is(":checked")) {
            textTrimHandling = "include";
        }

        if ($("#classAttributeHandlingArray").is(":checked")) {
            classAttributeHandling = "array";
        } else if ($("#classAttributeHandlingString").is(":checked")) {
            classAttributeHandling = "string";
        }

        if ($("#renderJavascriptFalse").is(":checked")) {
            renderJavascript = "false";
        } else if ($("#renderJavascriptTrue").is(":checked")) {
            renderJavascript = "true";
        }

        if (/^(http|https):\/\//.test($("#search-box").val())) {
            if (renderJavascript) {
                $("#result").text("Loading...Please Wait...Rendering Javascript takes additional time...");
            } else {
                $("#result").text("Loading...Please Wait...");
            }

            $.get("/api/convert",
                {
                    url: $("#search-box").val(),
                    format: "string",
                    emptyTextNodeHandling: emptyTextNodeHandling,
                    nullValueHandling: nullValueHandling,
                    textTrimHandling: textTrimHandling,
                    classAttributeHandling: classAttributeHandling,
                    renderJavascript: renderJavascript
                })
                .done(function(data) {
                    $("#result").text(data);
                })
                .fail(function(data) {
                    $("#result").text("Incorrect usage.");
                });
        } else {
            $("#result").text("Please include protocol (http:// or https://) in Url");
        }
    };
});
