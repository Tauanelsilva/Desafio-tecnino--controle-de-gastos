import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import { HomePage } from './pages/HomePage';
import { PessoasPage } from './pages/PessoasPage';
import { TransacoesPage } from './pages/TransacoesPage';
import { TotaisPage } from './pages/TotaisPage';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const navItems = [
    { to: '/', label: 'Início' },
    { to: '/pessoas', label: 'Pessoas' },
    { to: '/transacoes', label: 'Transações' },
    { to: '/totais', label: 'Totais' },
];

function Navigation() {
    return (
        <nav className="nav-container">
            {navItems.map((item) => (
                <NavLink
                    key={item.to}
                    to={item.to}
                    end={item.to === '/'}
                    className={({ isActive }) => `nav-link ${isActive ? 'active' : ''}`}
                >
                    {item.label}
                </NavLink>
            ))}
        </nav>
    );
}

function App() {
    return (
        <BrowserRouter>
            <div className="app-container">
                <Navigation />
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/pessoas" element={<PessoasPage />} />
                    <Route path="/transacoes" element={<TransacoesPage />} />
                    <Route path="/totais" element={<TotaisPage />} />
                </Routes>
            </div>
            <ToastContainer position="bottom-right" />
        </BrowserRouter>
    );
}

export default App;
