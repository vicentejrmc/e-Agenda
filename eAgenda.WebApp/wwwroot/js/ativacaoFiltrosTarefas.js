document.addEventListener("DOMContentLoaded", function () {
    const params = new URLSearchParams(window.location.search);
    const status = params.get("status");

    const botoesFiltro = document.querySelectorAll('.btn-filtro');

    for (const btn of botoesFiltro) {
        const href = btn.getAttribute('href') || "";

        // Destaca "Todas as Tarefas" se não houver filtro
        if (
            (status === null && !href.includes("status=")) ||
            (status === "Pendente" && href.includes("status=Pendente")) ||
            (status === "Concluído" && href.includes("status=Concluído"))
        ) {
            btn.classList.remove('btn-outline-primary');
            btn.classList.add('btn-primary');
        } else {
            btn.classList.remove('btn-primary');
            btn.classList.add('btn-outline-primary');
        }
    }
});