
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
            .then(data => {
                console.log(data)
                $('.header-cart').html(data)
            });
    })

    $(document).on('click', '.deletebasket', function (e) {
        e.preventDefault();

        let url = $(this).attr('href')
        console.log(url)

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => { $('.header-cart').html(data) });
    })

    $(document).on('click', '.deletefromcart', function (e) {
        e.preventDefault();

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => {
                $('.basketcontainer').html(data);
                fetch('/basket/getbasket')
                    .then(res => res.text())
                    .then(data => { $('.header-cart').html(data) });
            })
    })

    $(document).on('click', '.subCount', function (e) {
        e.preventDefault();

        let count = $(this).next().val();

        if (count > 1) {
            count--;
            $(this).next().val(count)

            let url = $(this).attr('href') + '?count=' + count;
            console.log(url)
            fetch(url)
                .then(res => res.text())
                .then(data => {
                    $('.basketcontainer').html(data);
                    fetch('/basket/getbasket')
                        .then(res => res.text())
                        .then(data => { $('.header-cart').html(data) });
                })
        }
    })
    $(document).on('click', '.addCount', function (e) {
        e.preventDefault();

        let count = $(this).prev().val();

        if (count < 1) {
            count = 1
        }

        count++;
        $(this).prev().val(count)

        let url = $(this).attr('href') + '?count=' + count;
        console.log(url)
        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('.basketcontainer').html(data);
                fetch('/basket/getbasket')
                    .then(res => res.text())
                    .then(data => { $('.header-cart').html(data) });
            })
    })

    $(document).on('click', '.productModal', function (e) {
        e.preventDefault();
        console.log($(this).attr('href'))
        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => {
                $('.modal-body').html(data);


            })
    })


    //$(".offcanvas-close, .minicart-close,.offcanvas-overlay").on('click', function () {
    //    console.log("saal")
    //    $("body").removeClass('fix');
    //    $(".offcanvas-search-inner, .minicart-inner").removeClass('show')
    //})
})