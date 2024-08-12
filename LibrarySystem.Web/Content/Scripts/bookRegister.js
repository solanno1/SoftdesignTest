document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');

    form.addEventListener('submit', function (event) {
        const username = document.querySelector('input[name="Title"]').value;
        const password = document.querySelector('input[name="Author"]').value;

        if (username === '' || password === '') {
            alert('Por favor, preencha todos os campos.');
            event.preventDefault();
        }
    });
});
