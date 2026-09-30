import React from 'react';
import useEmblaCarousel from 'embla-carousel-react';
import { ChevronDown, ChevronLeft, ChevronRight } from 'lucide-react';

export function FeatureCard({ icon, title, description, delay }: { icon: React.ReactNode; title: string; description: string; delay: number }) {
  const [isVisible, setIsVisible] = React.useState(false);
  const cardRef = React.useRef<HTMLDivElement>(null);

  React.useEffect(() => {
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

  return (
    <div
      ref={cardRef}
      className={`bg-gray-700 p-6 rounded-xl transform transition-all duration-700 ${
        isVisible
          ? 'opacity-100 translate-y-0'
          : 'opacity-0 translate-y-16'
      }`}
      style={{ transitionDelay: `${delay}ms` }}
    >
      <div className="mb-4">{icon}</div>
      <h3 className="text-xl font-semibold text-white mb-2">{title}</h3>
      <p className="text-gray-300">{description}</p>
    </div>
  );
}

export function GameplayCarousel() {
  const [emblaRef, emblaApi] = useEmblaCarousel({
    loop: true,
    align: 'center',
    containScroll: false,
    dragFree: true,
  });

  const [prevBtnEnabled, setPrevBtnEnabled] = React.useState(true);
  const [nextBtnEnabled, setNextBtnEnabled] = React.useState(true);
  const [selectedIndex, setSelectedIndex] = React.useState(0);

  const scrollPrev = React.useCallback(() => emblaApi && emblaApi.scrollPrev(), [emblaApi]);
  const scrollNext = React.useCallback(() => emblaApi && emblaApi.scrollNext(), [emblaApi]);

  const onSelect = React.useCallback(() => {
    if (!emblaApi) return;
    setPrevBtnEnabled(emblaApi.canScrollPrev());
    setNextBtnEnabled(emblaApi.canScrollNext());
    setSelectedIndex(emblaApi.selectedScrollSnap());
  }, [emblaApi]);

  React.useEffect(() => {
    if (!emblaApi) return;

    onSelect();
    emblaApi.on('select', onSelect);
    emblaApi.on('reInit', onSelect);

    // Auto-rotation setup
    const autoplayInterval = setInterval(() => {
      if (emblaApi.canScrollNext()) {
        emblaApi.scrollNext();
      } else {
        emblaApi.scrollTo(0); // Reset to first slide if at end
      }
    }, 7000);

    // Cleanup
    return () => {
      clearInterval(autoplayInterval);
      emblaApi.off('select', onSelect);
      emblaApi.off('reInit', onSelect);
    };
  }, [emblaApi, onSelect]);

  const screenshots = [
    {
      src: "images/Game_main.png",
      title: "Farm Management",
      description: "Manage and grow your farm with ease"
    },
    {
      src: "images/Game_farmers.png",
      title: "Hire Farmers",
      description: "Hire farmers to automate your farm and increase efficiency"
    },
    {
      src: "images/Game_upgrades.png",
      title: "Upgrades",
      description: "Enhance your farm with powerful upgrades"
    },
    {
      src: "images/Game_submit.png",
      title: "Submit Score",
      description: "Submit your score to earn your place on the leaderboard and receive rewards"
    }
  ];

  return (
    <div className="relative max-w-[1200px] mx-auto px-4">
      <div className="overflow-hidden" ref={emblaRef}>
        <div className="flex">
          {screenshots.map((screenshot, index) => (
            <div 
              key={index} 
              className="flex-[0_0_90%] min-w-0 sm:flex-[0_0_45%] lg:flex-[0_0_30%] pl-4"
            >
              <div className="group relative aspect-[9/16] rounded-2xl overflow-hidden transition-transform duration-300 transform hover:scale-[1.02]">
                <img
                  src={screenshot.src}
                  alt={screenshot.title}
                  className="w-full h-full object-cover"
                />
                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/0 to-black/0 opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  <div className="absolute bottom-0 left-0 right-0 p-6">
                    <h3 className="text-xl font-semibold text-white mb-2">{screenshot.title}</h3>
                    <p className="text-gray-300">{screenshot.description}</p>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
      
      <button
        className={`absolute left-4 top-1/2 -translate-y-1/2 bg-white/10 backdrop-blur-sm hover:bg-white/20 rounded-full p-2 transition-all ${
          prevBtnEnabled ? 'opacity-100' : 'opacity-50 cursor-not-allowed'
        }`}
        onClick={scrollPrev}
        disabled={!prevBtnEnabled}
      >
        <ChevronLeft className="h-6 w-6 text-white" />
      </button>
      
      <button
        className={`absolute right-4 top-1/2 -translate-y-1/2 bg-white/10 backdrop-blur-sm hover:bg-white/20 rounded-full p-2 transition-all ${
          nextBtnEnabled ? 'opacity-100' : 'opacity-50 cursor-not-allowed'
        }`}
        onClick={scrollNext}
        disabled={!nextBtnEnabled}
      >
        <ChevronRight className="h-6 w-6 text-white" />
      </button>

      {/* Dot Navigation */}
      <div className="flex justify-center gap-2 mt-6">
        {screenshots.map((_, index) => (
          <button
            key={index}
            className={`w-2.5 h-2.5 rounded-full transition-all duration-300 ${
              index === selectedIndex
                ? 'bg-green-400 scale-125'
                : 'bg-white/30 hover:bg-white/50'
            }`}
            onClick={() => emblaApi?.scrollTo(index)}
            aria-label={`Go to slide ${index + 1}`}
          />
        ))}
      </div>
    </div>
  );
}

export function FaqItem({ question, answer }: { question: string; answer: string }) {
  const [isOpen, setIsOpen] = React.useState(false);

  return (
    <div className="border-b border-gray-700">
      <button
        className="w-full flex items-center justify-between py-4 text-left"
        onClick={() => setIsOpen(!isOpen)}
      >
        <span className="text-lg font-semibold text-white">{question}</span>
        <ChevronDown className={`h-5 w-5 text-green-400 transition-transform ${isOpen ? 'rotate-180' : ''}`} />
      </button>
      {isOpen && (
        <div className="pb-4 text-gray-300">
          {answer}
        </div>
      )}
    </div>
  );
}