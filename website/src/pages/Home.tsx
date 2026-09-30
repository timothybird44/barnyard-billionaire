import { FeatureCard, GameplayCarousel, FaqItem } from '../components/HomeComponents';
import Footer from '../components/Footer';
import { Apple, DollarSign, Facebook, Instagram, PlayCircle, Sprout, Timer, Trophy, Twitter } from 'lucide-react';

export default function Home() {
  window.scrollTo(0, 0);
  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800">
      {/* Hero Section */}
      <section className="pt-32 pb-16 md:pt-40 md:pb-24">
        <div className="container mx-auto px-4">
          <div className="flex flex-col md:flex-row items-center gap-8 md:gap-16">
            <div className="flex-1">
              <h1 className="text-4xl md:text-6xl font-bold text-white mb-4">
                Become a Barnyard Billionare
              </h1>
              <p className="text-lg md:text-xl text-gray-300 mb-8">
                Start small, think big! Create a thriving farming empire with diverse livestock, from collecting eggs to herding sheep in this addictive idle game.
              </p>
              <div className="flex flex-col sm:flex-row gap-4 mb-8">
                <button className="flex items-center justify-center gap-2 bg-white text-gray-900 px-6 py-3 rounded-lg hover:bg-gray-200 transition">
                  <Apple className="h-6 w-6" />
                  <span>App Store</span>
                </button>
                <button className="flex items-center justify-center gap-2 bg-green-500 text-white px-6 py-3 rounded-lg hover:bg-green-600 transition">
                  <PlayCircle className="h-6 w-6" />
                  <span>Google Play</span>
                </button>
              </div>
            </div>
            <div className="flex-1">
              <img
                src="images/Logo.jpeg"
                alt="Barnyard Billionaire Logo"
                className="rounded-2xl shadow-2xl"
              />
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section id="features" className="py-16 bg-gray-800">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl md:text-4xl font-bold text-center text-white mb-12">
            Game Features
          </h2>
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-8">
            <FeatureCard
              icon={<Timer className="h-8 w-8 text-green-400" />}
              title="Idle Farming"
              description="Your animals continue producing resources even while you're away!"
              delay={0}
            />
            <FeatureCard
              icon={<Sprout className="h-8 w-8 text-green-400" />}
              title="Build Your Farm"
              description="Collect every animal and build your farm."
              delay={200}
            />
            <FeatureCard
              icon={<DollarSign className="h-8 w-8 text-green-400" />}
              title="Earn Real Rewards"
              description="Earn real rewards for looking after and growing your farm."
              delay={400}
            />
            <FeatureCard
              icon={<Trophy className="h-8 w-8 text-green-400" />}
              title="Leaderboard"
              description="Grow your farm to rise to the top. Become the most profitable farmer."
              delay={600}
            />
          </div>
        </div>
      </section>

      {/* Gameplay Section */}
      <section id="gameplay" className="py-16 bg-gray-900">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl md:text-4xl font-bold text-center text-white mb-12">
            Gameplay
          </h2>
          <GameplayCarousel />
        </div>
      </section>

      {/* FAQ Section */}
      <section id="faq" className="py-16 bg-gray-800">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl md:text-4xl font-bold text-center text-white mb-12">
            Frequently Asked Questions
          </h2>
          <div className="max-w-3xl mx-auto space-y-6">
            <FaqItem
              question="Is the game free to play?"
              answer="Yes! Barnyard Billionaire is free to download and play. Optional in-app purchases are available to remove ads from your experience."
            />
            <FaqItem
              question="How do I earn rewards?"
              answer="We provide revenue sharing with our players! The prize pool consists of 50% of all revenue generated for the week. Simply submit your highest score with your Solana wallet each week and you will be paid out based on your score."
            />
            <FaqItem
              question="How do I create a Solana wallet?"
              answer="You can install the Phantom wallet as a web extension or mobile app. Follow the prompts to setup your wallet and connect it in game to earn rewards!"
            />
          </div>
        </div>
      </section>

      {/* Footer */}
      <Footer />
    </div>
  );
}