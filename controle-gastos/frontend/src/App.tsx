import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Navbar from './components/layout/Navbar';
import HomePage from './pages/HomePage';
import PessoasPage from './pages/PessoasPage';
import TransacoesPage from './pages/TransacoesPage';
import TotaisPage from './pages/TotaisPage';

function App() {
  const pageStyle: React.CSSProperties = {
    minHeight: '100vh',
    backgroundColor: '#f8fafc',
  };

  return (
    <Router>
      <div style={pageStyle}>
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/pessoas" element={<PessoasPage />} />
          <Route path="/transacoes" element={<TransacoesPage />} />
          <Route path="/totais" element={<TotaisPage />} />
        </Routes>
        <ToastContainer
          position="top-right"
          autoClose={3000}
          hideProgressBar={false}
          newestOnTop
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
      </div>
    </Router>
  );
}

export default App;
