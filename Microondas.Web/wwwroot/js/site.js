let intervalId;
let remainingTime;

// Função para adicionar dígitos ao input
function addDigit(digit) {
    const tempoInput = document.getElementById('tempo-aquecimento');
    tempoInput.value += digit;
}

// Função para limpar o input
function clearInput() {
    document.getElementById('tempo-aquecimento').value = '';
}

// Função para formatar o tempo em minutos e segundos
function formatarTempo(tempoSegundos) {
    const minutos = Math.floor(tempoSegundos / 60);
    const segundos = tempoSegundos % 60;
    return `${minutos}:${segundos.toString().padStart(2, '0')}`;
}

// Função para iniciar o cronômetro
function iniciarCronometro(tempo, potencia) {
    const startTime = Date.now();
    const endTime = startTime + tempo * 1000;
    remainingTime = tempo;
    let progressoStr = '';

    intervalId = setInterval(() => {
        const currentTime = Date.now();
        remainingTime = Math.max(Math.floor((endTime - currentTime) / 1000), 0);

        document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(remainingTime)}`;
        document.getElementById('potencia').textContent = `Potência: ${potencia}`;

        // Atualiza a string de progresso
        progressoStr += '.'.repeat(potencia) + ' ';
        document.getElementById('progresso-str').textContent = progressoStr.trim();

        if (remainingTime === 0) {
            clearInterval(intervalId);
            document.getElementById('estado').textContent = "Estado: Aquecimento concluído";
            document.getElementById('progresso-str').textContent += " Aquecimento concluído.";
        }
    }, 1000);
}

// Função para exibir a potência selecionada no front-end (sem atualizar o menu)
function exibirPotenciaSelecionada() {
    const potencia = document.getElementById('potencia-aquecimento').value || 10;
    document.getElementById('potencia-selecionada').textContent = `Potência Selecionada: ${potencia}`;
}

// Função para iniciar o aquecimento
async function iniciarAquecimento() {
    const tempo = parseInt(document.getElementById('tempo-aquecimento').value) || 30;
    const potencia = parseInt(document.getElementById('potencia-aquecimento').value) || 10;

    try {
        const response = await fetch('/iniciar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ tempo, potencia })
        });

        const data = await response.json();

        alert(data.message);

        // Atualiza o status com os dados da resposta
        const status = data.status;
        document.getElementById('estado').textContent = `Estado: ${status.estado}`;
        document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(status.tempoRestante)}`;
        document.getElementById('potencia').textContent = `Potência: ${status.potencia}`;
        document.getElementById('progresso-str').textContent = ''; // Limpa a string de progresso

        // Limpa os campos de entrada
        document.getElementById('tempo-aquecimento').value = '';
        document.getElementById('potencia-aquecimento').value = '';

        // Reinicia o cronômetro com o tempo restante e a potência
        clearInterval(intervalId);
        iniciarCronometro(status.tempoRestante, status.potencia);
    } catch (error) {
        console.error('Erro ao iniciar o aquecimento:', error);
    }
}

// Função para pausar ou cancelar
async function pausarOuCancelar() {
    clearInterval(intervalId); // Para o cronômetro

    try {
        const response = await fetch('/pausar', { method: 'POST' });
        const data = await response.json();

        alert(data.message);

        // Atualiza o status com os dados da resposta
        const status = data.status;
        document.getElementById('estado').textContent = `Estado: ${status.estado}`;
        document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(status.tempoRestante)}`;
        document.getElementById('potencia').textContent = `Potência: ${status.potencia || 10}`;

        if (status.estado === "Em funcionamento") {
            iniciarCronometro(status.tempoRestante, status.potencia); // Reinicia o cronômetro
        } else if (status.estado === "Cancelado") {
            clearInterval(intervalId); // Cancela o cronômetro
            document.getElementById('tempo').textContent = "Tempo Restante: 0 segundos";
            document.getElementById('estado').textContent = "Estado: Cancelado";
            document.getElementById('progresso-str').textContent = "";
            document.getElementById('potencia').textContent = "Potência: Desconhecido";
        }
    } catch (error) {
        console.error('Erro ao pausar/cancelar:', error);
    }
}

// Função para acrescentar tempo
async function acrescentarTempo() {
    const segundos = parseInt(document.getElementById('tempo-extra').value);

    try {
        const response = await fetch('/acrescentar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ segundos })
        });

        const data = await response.json();

        alert(data.message);

        // Atualiza o status com os dados da resposta
        const status = data.status;
        document.getElementById('estado').textContent = `Estado: ${status.estado}`;
        document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(status.tempoRestante)}`;
        document.getElementById('potencia').textContent = `Potência: ${status.potencia}`;

        if (status.estado === "Em funcionamento") {
            iniciarCronometro(status.tempoRestante, status.potencia); // Reinicia o cronômetro
        }
    } catch (error) {
        console.error('Erro ao acrescentar tempo:', error);
    }
}

// Função para iniciar programa de aquecimento pré-definido
async function iniciarPrograma() {
    const programaNome = document.getElementById('programa-selecionado').value;

    try {
        const response = await fetch('/iniciarPrograma', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome: programaNome })
        });

        const data = await response.json();

        alert(data.message);

        // Atualiza o status com os dados da resposta
        const status = data.status;
        document.getElementById('estado').textContent = `Estado: ${status.estado}`;
        document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(status.tempoRestante)}`;
        document.getElementById('potencia').textContent = `Potência: ${status.potencia}`;
        document.getElementById('progresso-str').textContent = ''; // Limpa a string de progresso

        // Reinicia o cronômetro com o tempo restante e a potência
        clearInterval(intervalId);
        iniciarCronometro(status.tempoRestante, status.potencia);
    } catch (error) {
        console.error('Erro ao iniciar o programa de aquecimento:', error);
    }
}

// Função para cadastrar programa de aquecimento customizado
async function cadastrarPrograma() {
    const nome = document.getElementById('nome-programa').value;
    const alimento = document.getElementById('alimento-programa').value;
    const tempo = parseInt(document.getElementById('tempo-programa').value);
    const potencia = parseInt(document.getElementById('potencia-programa').value);
    const stringAquecimento = document.getElementById('string-aquecimento-programa').value;
    const instrucoes = document.getElementById('instrucoes-programa').value;

    const programa = {
        nome,
        alimento,
        tempo,
        potencia,
        stringAquecimento,
        instrucoes
    };

    try {
        const response = await fetch('/cadastrarPrograma', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(programa)
        });

        const data = await response.json();

        alert(data.message);

        if (data.success) {
            const select = document.getElementById('programa-selecionado');
            const option = document.createElement('option');
            option.value = programa.nome;
            option.innerHTML = `<i>${programa.nome}</i>`;
            select.appendChild(option);

            // Limpar os campos após o cadastro
            document.getElementById('nome-programa').value = '';
            document.getElementById('alimento-programa').value = '';
            document.getElementById('tempo-programa').value = '';
            document.getElementById('potencia-programa').value = '';
            document.getElementById('string-aquecimento-programa').value = '';
            document.getElementById('instrucoes-programa').value = '';
        }
    } catch (error) {
        console.error('Erro ao cadastrar programa de aquecimento:', error);
    }
}

// Iniciar a atualização de status ao carregar a página
window.onload = () => {
    fetch('/status')
        .then(response => response.json())
        .then(data => {
            document.getElementById('estado').textContent = `Estado: ${data.estado}`;
            document.getElementById('tempo').textContent = `Tempo Restante: ${formatarTempo(data.tempoRestante)}`;
            document.getElementById('potencia').textContent = `Potência: ${data.potencia || 10}`;
            document.getElementById('progresso-str').textContent = ''; // Limpa a string de progresso

            if (data.estado === "Em funcionamento") {
                iniciarCronometro(data.tempoRestante, data.potencia);
            }
        })
        .catch(error => console.error('Erro ao obter o status inicial:', error));
};
