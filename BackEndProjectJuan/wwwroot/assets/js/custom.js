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