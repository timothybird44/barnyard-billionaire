import { Menu, Sprout, X } from 'lucide-react';
import { Link, useLocation } from 'react-router-dom';
import { useState } from 'react';

export default function Navbar() {
  const location = useLocation();
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  
  const isActive = (path: string) => {
    return location.pathname === path ? 'text-green-400' : 'text-gray-300 hover:text-green-400';
  };

  return (
    <div className="fixed w-full z-50 px-4 py-3 top-4">
      <nav className="container mx-auto bg-gray-900/80 backdrop-blur-sm rounded-2xl border border-gray-800 shadow-lg transition-all duration-300 hover:bg-gray-900/95 hover:border-gray-700 hover:shadow-xl">
        <div className="px-4 py-3 flex items-center justify-between">
          <Link to="/" className="flex items-center gap-2">
            <span className="text-xl font-bold text-white">Barnyard Billionaire</span>
          </Link>
          
          {/* Desktop Navigation */}
          <div className="hidden md:flex items-center gap-6">
            <Link to="/" className={`transition-colors ${isActive('/')}`}>
              Home
            </Link>
            <Link to="/leaderboard" className={`transition-colors ${isActive('/leaderboard')}`}>
              Leaderboard
            </Link>
            <Link to="/how-to" className={`transition-colors ${isActive('/how-to')}`}>
              How To
            </Link>
          </div>

          {/* Mobile Menu Button */}
          <button
            className="md:hidden p-2 text-gray-300 hover:text-green-400 transition-colors"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
            aria-label="Toggle menu"
          >
            {isMenuOpen ? (
              <X className="h-6 w-6" />
            ) : (
              <Menu className="h-6 w-6" />
            )}
          </button>
        </div>

        {/* Mobile Navigation */}
        {isMenuOpen && (
          <div className="md:hidden px-4 py-3 border-t border-gray-800">
            <div className="flex flex-col gap-4">
              <Link
                to="/"
                className={`transition-colors ${isActive('/')}`}
                onClick={() => setIsMenuOpen(false)}
              >
                Home
              </Link>
              <Link
                to="/leaderboard"
                className={`transition-colors ${isActive('/leaderboard')}`}
                onClick={() => setIsMenuOpen(false)}
              >
                Leaderboard
              </Link>
              <Link
                to="/how-to"
                className={`transition-colors ${isActive('/how-to')}`}
                onClick={() => setIsMenuOpen(false)}
              >
                How To
              </Link>
            </div>
          </div>
        )}
      </nav>
    </div>
  );
}