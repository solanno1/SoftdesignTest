document.addEventListener('DOMContentLoaded', function () {
    const rentButtons = document.querySelectorAll('button.alugar');

    rentButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            if (!confirm('Você tem certeza que deseja alugar este livro?')) {
                event.preventDefault();
            }
        });
    });
});
