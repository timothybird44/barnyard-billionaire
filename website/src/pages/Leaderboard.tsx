import { Trophy, Medal, Award, ChevronLeft, ChevronRight, Search } from 'lucide-react';
import Footer from '../components/Footer';
import { useState, useEffect, useRef } from 'react';
import { useSearchParams } from 'react-router-dom'; // Import useSearchParams

interface Player {
  rank: string;
  username: string;
  score: string;
  referrals: string;
  earnings: string;
}

interface PayoutData {
  currentPrizePool: string | null;
  nextPayout: string | null;
}

export default function Leaderboard() {
  const [currentPage, setCurrentPage] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [leaderboardData, setLeaderboardData] = useState<Player[]>([]);
  const [payoutData, setPayoutData] = useState<PayoutData>({ currentPrizePool: null, nextPayout: null });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const playersPerPage = 10;
  const [searchParams] = useSearchParams(); // Get URL query parameters
  const howToEarnRef = useRef<HTMLDivElement>(null); // Create a ref for the "how-to-earn" div

  useEffect(() => {
    const fetchLeaderboard = async () => {
      setLoading(true);
      setError(null);
      try {
        const leaderboardResponse = await fetch('https://www.barnyardbillionaire.com/api/get_leaderboard.php');
        if (!leaderboardResponse.ok) {
          throw new Error(`HTTP error! status: ${leaderboardResponse.status}`);
        }
        const leaderboardResult = await leaderboardResponse.json() as Player[];
        setLeaderboardData(leaderboardResult);

        const payoutResponse = await fetch('https://www.barnyardbillionaire.com/api/get_payout.php');
        if (!payoutResponse.ok) {
          throw new Error(`HTTP error! status: ${payoutResponse.status}`);
        }
        const payoutResult = await payoutResponse.json() as string[];
        setPayoutData({ currentPrizePool: payoutResult[0] || null, nextPayout: payoutResult[1] || null });
      } catch (e: any) {
        setError(e.message || 'Failed to fetch data.');
        console.error('Error fetching data:', e);
      } finally {
        setLoading(false);
      }
    };

    fetchLeaderboard();
  }, []);

  useEffect(() => {
    if (searchParams.get('how-to-earn')) {
      if (howToEarnRef.current) {
        howToEarnRef.current.scrollIntoView({ behavior: 'smooth' });
      }
    }
    else {
      window.scrollTo(0, 0);
    }
  }, [searchParams, loading]);

  // Filter players based on search query
  const filteredPlayers = leaderboardData.filter(player =>
    player.username.toLowerCase().includes(searchQuery.toLowerCase())
  );

  const totalPages = Math.ceil(filteredPlayers.length / playersPerPage);
  const indexOfLastPlayer = currentPage * playersPerPage;
  const indexOfFirstPlayer = indexOfLastPlayer - playersPerPage;
  const currentPlayers = filteredPlayers.slice(indexOfFirstPlayer, indexOfLastPlayer);

  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  // Reset to first page when search query changes
  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
    setCurrentPage(1);
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
        <div className="container mx-auto px-4">
          <div className="max-w-4xl mx-auto">
            <h1 className="text-4xl font-bold text-white mb-8 flex items-center gap-3">
              <Trophy className="h-8 w-8 text-green-400" />
              Global Leaderboard
            </h1>

            <div className="mt-8 bg-gray-800 rounded-xl p-6">
              <div className="grid grid-cols-1 md:grid-cols-1 gap-4">
                <div className="bg-gray-700 p-4 rounded-lg">
                  <h2 className="text-xl font-semibold text-white mb-4">Current Prize Pool</h2>
                  <h3 className="text-xl font-semibold text-green-400 mb-4">${payoutData.currentPrizePool} USD</h3>
                  <p className="text-gray-300 mb-4">Next Payout: {payoutData.nextPayout}</p>
                </div>
              </div>
            </div>

            <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
              <div className="container mx-auto px-4">
                <div className="max-w-4xl mx-auto text-center text-white">
                  <p>Loading leaderboard data...</p>
                </div>
              </div>
            </div>

            <div id="how-to-earn" className="mt-8 bg-gray-800 rounded-xl p-6" ref={howToEarnRef}>
              <h2 className="text-xl font-semibold text-white mb-4">How to Earn Rewards</h2>
              <p className="text-gray-300 mb-4">
                Every week, the players on our leaderboard earn real money rewards! The prize pool consists of <b>50%</b> of all revenue generated for the week.
              </p>
              <p className="text-gray-300 mb-4">
                The rewards are distrubuted based on your score compared to the sum of all submitted scores. The higher your score, the bigger your share of the prize pool!
              </p>
              <p className="text-gray-300 mb-4">
                Your Earnings = Prize Pool * (Your Score / Sum of All Scores)
              </p>
              <br />
              <p className="text-gray-300 mb-4"><small><sup>*</sup> Actual earnings may vary due to variable fees and charges. We reserve the right to withhold payouts if deemed necessary.</small></p>
            </div>
          </div>
        </div>
        <div className="mt-16">
          <Footer />
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
        <div className="container mx-auto px-4">
          <div className="max-w-4xl mx-auto">
            <h1 className="text-4xl font-bold text-white mb-8 flex items-center gap-3">
              <Trophy className="h-8 w-8 text-green-400" />
              Global Leaderboard
            </h1>

            <div className="mt-8 bg-gray-800 rounded-xl p-6">
              <div className="grid grid-cols-1 md:grid-cols-1 gap-4">
                <div className="bg-gray-700 p-4 rounded-lg">
                  <h2 className="text-xl font-semibold text-white mb-4">Current Prize Pool</h2>
                  <h3 className="text-xl font-semibold text-green-400 mb-4">${payoutData.currentPrizePool} USD</h3>
                  <p className="text-gray-300 mb-4">Next Payout: {payoutData.nextPayout}</p>
                </div>
              </div>
            </div>

            <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
              <div className="container mx-auto px-4">
                <div className="max-w-4xl mx-auto text-center text-red-500">
                  <p>Error loading leaderboard data: {error}</p>
                </div>
              </div>
            </div>

            <div id="how-to-earn" className="mt-8 bg-gray-800 rounded-xl p-6" ref={howToEarnRef}>
              <h2 className="text-xl font-semibold text-white mb-4">How to Earn Rewards</h2>
              <p className="text-gray-300 mb-4">
                Every week, the players on our leaderboard earn real money rewards! The prize pool consists of <b>50%</b> of all revenue generated for the week.
              </p>
              <p className="text-gray-300 mb-4">
                The rewards are distrubuted based on your score compared to the sum of all submitted scores. The higher your score, the bigger your share of the prize pool!
              </p>
              <p className="text-gray-300 mb-4">
                Your Earnings = Prize Pool * (Your Score / Sum of All Scores)
              </p>
              <br />
              <p className="text-gray-300 mb-4"><small><sup>*</sup> Actual earnings may vary due to variable fees and charges. We reserve the right to withhold payouts if deemed necessary.</small></p>
            </div>
          </div>
        </div>
        <div className="mt-16">
          <Footer />
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-4xl font-bold text-white mb-8 flex items-center gap-3">
            <Trophy className="h-8 w-8 text-green-400" />
            Global Leaderboard
          </h1>

          <div className="mt-8 bg-gray-800 rounded-xl p-6">
            <div className="grid grid-cols-1 md:grid-cols-1 gap-4">
              <div className="bg-gray-700 p-4 rounded-lg">
                <h2 className="text-xl font-semibold text-white mb-4">Current Prize Pool</h2>
                <h3 className="text-xl font-semibold text-green-400 mb-4">${payoutData.currentPrizePool} USD</h3>
                <p className="text-gray-300 mb-4">Next Payout: {payoutData.nextPayout}</p>
              </div>
            </div>
          </div>

          <div className="mt-8 bg-gray-800 rounded-xl shadow-xl overflow-hidden">
            {/* Search Bar */}
            <div className="p-4 bg-gray-700">
              <div className="relative">
                <input
                  type="text"
                  placeholder="Search players..."
                  value={searchQuery}
                  onChange={handleSearchChange}
                  className="w-full bg-gray-800 text-white px-4 py-2 pl-10 rounded-lg focus:outline-none focus:ring-2 focus:ring-green-400"
                />
                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
              </div>
            </div>

            <div className="grid grid-cols-5 gap-4 p-4 bg-gray-700 text-gray-300 font-semibold">
              <div>Rank</div>
              <div>Player</div>
              <div>Score</div>
              <div>Referrals</div>
              <div>Earnings<sup>*</sup></div>
            </div>

            <div className="divide-y divide-gray-700">
              {currentPlayers.length > 0 ? (
                currentPlayers.map((player) => (
                  <div key={player.rank} className="grid grid-cols-5 gap-4 p-4 items-center hover:bg-gray-700/50 transition-colors">
                    <div className="flex items-center gap-2">
                      {player.rank === "1" && <Trophy className="h-5 w-5 text-yellow-400" />}
                      {player.rank === "2" && <Medal className="h-5 w-5 text-gray-300" />}
                      {player.rank === "3" && <Award className="h-5 w-5 text-amber-600" />}
                      <span className={`font-semibold ${
                        player.rank === "1" ? 'text-yellow-400' :
                        player.rank === "2" ? 'text-gray-300' :
                        player.rank === "3" ? 'text-amber-600' : 'text-gray-400'
                      }`}>
                        #{player.rank}
                      </span>
                    </div>
                    <div className="text-white">{player.username}</div>
                    <div className="text-gray-300">{player.score}</div>
                    <div className="text-gray-300">{player.referrals}</div>
                    <div className="text-green-400">${(parseFloat(player.earnings) * parseFloat(payoutData.currentPrizePool)).toFixed(2)} USD</div>
                  </div>
                ))
              ) : (
                <div className="p-8 text-center text-gray-400">
                  No players found matching your search.
                </div>
              )}
            </div>

            {/* Pagination */}
            <div className="flex items-center justify-between p-4 border-t border-gray-700">
              <div className="text-sm text-gray-400">
                Showing {filteredPlayers.length > 0 ? indexOfFirstPlayer + 1 : 0}-{Math.min(indexOfLastPlayer, filteredPlayers.length)} of {filteredPlayers.length}
              </div>
              <div className="flex items-center gap-2">
                <button
                  onClick={() => paginate(currentPage - 1)}
                  disabled={currentPage === 1}
                  className={`p-2 rounded-lg transition-colors ${
                    currentPage === 1
                      ? 'text-gray-600 cursor-not-allowed'
                      : 'text-gray-300 hover:bg-gray-700'
                  }`}
                >
                  <ChevronLeft className="h-5 w-5" />
                </button>
                {Array.from({ length: totalPages }).map((_, index) => (
                  <button
                    key={index}
                    onClick={() => paginate(index + 1)}
                    className={`w-8 h-8 rounded-lg transition-colors ${
                      currentPage === index + 1
                        ? 'bg-green-400 text-gray-900'
                        : 'text-gray-300 hover:bg-gray-700'
                    }`}
                  >
                    {index + 1}
                  </button>
                ))}
                <button
                  onClick={() => paginate(currentPage + 1)}
                  disabled={currentPage === totalPages}
                  className={`p-2 rounded-lg transition-colors ${
                    currentPage === totalPages
                      ? 'text-gray-600 cursor-not-allowed'
                      : 'text-gray-300 hover:bg-gray-700'
                  }`}
                >
                  <ChevronRight className="h-5 w-5" />
                </button>
              </div>
            </div>
          </div>

          <div id="how-to-earn" className="mt-8 bg-gray-800 rounded-xl p-6" ref={howToEarnRef}>
            <h2 className="text-xl font-semibold text-white mb-4">How to Earn Rewards</h2>
            <p className="text-gray-300 mb-4">
              Every week, the players on our leaderboard earn real money rewards! The prize pool consists of <b>50%</b> of all revenue generated for the week.
            </p>
            <p className="text-gray-300 mb-4">
              The rewards are distrubuted based on your score compared to the sum of all submitted scores. The higher your score, the bigger your share of the prize pool!
            </p>
            <p className="text-gray-300 mb-4">
              Your Earnings = Prize Pool * (Your Score / Sum of All Scores)
            </p>
            <br />
            <p className="text-gray-300 mb-4"><small><sup>*</sup> Actual earnings may vary due to variable fees and charges. We reserve the right to withhold payouts for any player if deemed necessary.</small></p>
          </div>
        </div>
      </div>
      <div className="mt-16">
        <Footer />
      </div>
    </div>
  );
}