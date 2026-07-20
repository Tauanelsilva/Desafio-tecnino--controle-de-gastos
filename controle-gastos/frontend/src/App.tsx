import { BrowserRouter, Routes, Route, Link, useLocation } from 'react-router-dom';
import { PessoasPage } from './pages/PessoasPage';
import { TransacoesPage } from './pages/TransacoesPage';
import { TotaisPage } from './pages/TotaisPage';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Navigation() {
    const location = useLocation();
    
    return (
        <nav className="nav-container">
            <Link 
                to="/pessoas" 
                className={`nav-link ${location.pathname === '/pessoas' ? 'active' : ''}`}
                style={location.pathname === '/pessoas' ? { backgroundColor: 'var(--color-bg)', color: 'var(--color-text-main)' } : {}}
            >
                Pessoas
            </Link>
            <Link 
                to="/transacoes" 
                className={`nav-link ${location.pathname === '/transacoes' ? 'active' : ''}`}
                style={location.pathname === '/transacoes' ? { backgroundColor: 'var(--color-bg)', color: 'var(--color-text-main)' } : {}}
            >
                Transações
            </Link>
            <Link 
                to="/totais" 
                className={`nav-link ${location.pathname === '/totais' ? 'active' : ''}`}
                style={location.pathname === '/totais' ? { backgroundColor: 'var(--color-bg)', color: 'var(--color-text-main)' } : {}}
            >
                Totais
            </Link>
        </nav>
    );
}

function App() {
    return (
        <BrowserRouter>
            <div className="app-container">
                <Navigation />
                <Routes>
                    <Route path="/" element={<PessoasPage />} />
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
