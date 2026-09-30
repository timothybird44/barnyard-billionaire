import { Apple, DollarSign, Facebook, Instagram, PlayCircle, Sprout, Timer, Trophy, Twitter } from 'lucide-react';

export default function Footer() {
  return (
    <footer className="bg-gray-900 text-gray-300 py-12">
        <div className="container mx-auto px-4">
          <div className="flex flex-col md:flex-row justify-between items-center gap-8">
            <div className="flex items-center gap-2">
              <span className="text-xl font-bold text-white">Barnyard Billionaire</span>
            </div>
            <div className="flex gap-6">
              <a href="https://x.com/BarnyardBillion" target="_blank" className="hover:text-green-400 transition">
                <Twitter className="h-6 w-6" />
              </a>
              <a href="https://www.instagram.com/barnyardbillion/" target="_blank" className="hover:text-green-400 transition">
                <Instagram className="h-6 w-6" />
              </a>
            </div>
            <div className="text-sm text-gray-400">
              © 2025 Barnyard Billionaire. All rights reserved.
              <br />
              <a href="/privacy-policy" className="hover:text-green-400 transition">
                Privacy Policy
              </a>
            </div>
          </div>
        </div>
      </footer>
  );
}