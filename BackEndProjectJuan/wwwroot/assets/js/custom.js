
$(document).ready(function () {
    $(document).on('keyup', '.searchinput', function () {

        let url = $(this).data('url') + '?search=' + $(this).val();
        console.log(url)

        if ($(this).val().trim().length > 2) {
            fetch(url)
                .then(res => res.text())
                .then(data => {
                    console.log(data)
                    $('.searchlist').html(data);
                })
        }
        else {
            $('.searchlist').html("");
        }
    })

})



$(document).ready(function () {

    $(document).on('click', '.addbasket', function (e) {
        e.preventDefault();

        let url = $(this).attr('href')
        console.log(url)

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => { $('.header-cart').html(data) });
    })

    $(document).on('click', '.deletebasket', function (e) {
        e.preventDefault();

        let url = $(this).attr('href')
        console.log(url)

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => { $('.header-cart').html(data) });
    })
    $(document).on('click', '.minicart-btn', function (e) {
        console.log('salam')
    })
    


    //$(".offcanvas-close, .minicart-close,.offcanvas-overlay").on('click', function () {
    //    console.log("saal")
    //    $("body").removeClass('fix');
    //    $(".offcanvas-search-inner, .minicart-inner").removeClass('show')
    //})
})