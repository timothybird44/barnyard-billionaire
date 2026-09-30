import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Leaderboard from './pages/Leaderboard';
import HowTo from './pages/HowTo';
import Privacy from './pages/Privacy';

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/leaderboard" element={<Leaderboard />} />
        <Route path="/how-to" element={<HowTo />} />
        <Route path="/privacy-policy" element={<Privacy />} />
      </Routes>
    </Router>
  );
}

export default App;