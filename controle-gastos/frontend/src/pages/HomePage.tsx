import { Link } from 'react-router-dom';

const atalhos = [
    {
        to: '/pessoas',
        icon: '\u{1F465}',
        titulo: 'Pessoas',
        descricao: 'Cadastre e gerencie as pessoas da residência.',
    },
    {
        to: '/transacoes',
        icon: '\u{1F4B8}',
        titulo: 'Transações',
        descricao: 'Registre receitas e despesas vinculadas a cada pessoa.',
    },
    {
        to: '/totais',
        icon: '\u{1F4CA}',
        titulo: 'Totais',
        descricao: 'Acompanhe o resumo financeiro individual e geral.',
    },
];

export function HomePage() {
    return (
        <div>
            <div className="home-hero">
                <h1>Controle de Gastos Residenciais</h1>
                <p>
                    Gerencie as finanças da sua residência de forma simples: cadastre pessoas,
                    registre transações e acompanhe os totais de receitas, despesas e saldo.
                </p>
            </div>

            <div className="home-grid">
                {atalhos.map((atalho) => (
                    <Link key={atalho.to} to={atalho.to} className="card home-card">
                        <div className="card-icon">{atalho.icon}</div>
                        <h2>{atalho.titulo}</h2>
                        <p>{atalho.descricao}</p>
                    </Link>
                ))}
            </div>
        </div>
    );
}
