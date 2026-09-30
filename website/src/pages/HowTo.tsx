import { BookOpen, Gamepad2, Wallet, Trophy } from 'lucide-react';
import Footer from '../components/Footer';
import { useRef, useState, useEffect } from 'react';

interface GuideCardProps {
  icon: React.ReactNode;
  title: string;
  steps: string[];
  delay: number;
}

function GuideCard({ icon, title, steps, delay }: GuideCardProps) {
  const [isVisible, setIsVisible] = useState(false);
  const cardRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          setIsVisible(true);
          observer.unobserve(entry.target);
        }
      },
      {
        threshold: 0.1,
      }
    );

    if (cardRef.current) {
      observer.observe(cardRef.current);
    }

    return () => {
      if (cardRef.current) {
        observer.unobserve(cardRef.current);
      }
    };
  }, []);

  window.scrollTo(0, 0);

  return (
    <div
      ref={cardRef}
      className={`bg-gray-800 rounded-xl p-6 shadow-xl transform transition-all duration-700 ${
        isVisible
          ? 'opacity-100 translate-y-0'
          : 'opacity-0 translate-y-16'
      }`}
      style={{ transitionDelay: `${delay}ms` }}
    >
      <div className="flex items-center gap-3 mb-6">
        <div className="p-3 rounded-lg bg-green-400/20 text-green-400">
          {icon}
        </div>
        <h2 className="text-2xl font-semibold text-white">{title}</h2>
      </div>

      <div className="space-y-4">
        {steps.map((step, stepIndex) => (
          <div key={stepIndex} className="flex items-start gap-4">
            <div className="flex-shrink-0 w-8 h-8 rounded-full bg-gray-700 flex items-center justify-center text-green-400 font-semibold">
              {stepIndex + 1}
            </div>
            <p className="text-gray-300 pt-1">{step}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default function HowTo() {
  const guides = [
    {
      icon: <Gamepad2 className="h-6 w-6" />,
      title: "Getting Started",
      steps: [
        "Download the game from App Store or Google Play",
        "Unlock your first farm",
        "Collect, grow, and upgrade your farms"
      ]
    },
    {
      icon: <Trophy className="h-6 w-6" />,
      title: "Earning Points",
      steps: [
        "Regularly collect resources from your animals",
        "Increase the production of your farms",
        "Purchase farmers to help automate your farms",
        "Upgrade your farms to increase their efficiency"
      ]
    },
    {
      icon: <Wallet className="h-6 w-6" />,
      title: "Claiming Rewards",
      steps: [
        "Connect your Solana wallet to the game",
        "Earn your spot on the weekly leaderboard",
        "Rewards are distributed every week",
        "Higher scores earn larger shares of the prize pool"
      ]
    }
  ];

  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-4xl font-bold text-white mb-8 flex items-center gap-3">
            <BookOpen className="h-8 w-8 text-green-400" />
            How To Play
          </h1>

          <div className="grid gap-8">
            {guides.map((guide, index) => (
              <GuideCard
                key={index}
                icon={guide.icon}
                title={guide.title}
                steps={guide.steps}
                delay={index * 200}
              />
            ))}
          </div>
        </div>
      </div>
      <div className="mt-16">
        <Footer />
      </div>
    </div>
  );
}