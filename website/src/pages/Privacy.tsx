import { BookOpen, Gamepad2, Wallet, Trophy } from 'lucide-react';
import Footer from '../components/Footer';

export default function Privacy() {
  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-4xl font-bold text-white mb-8 flex items-center gap-3">
            <BookOpen className="h-8 w-8 text-green-400" />
            Privacy Policy
          </h1>

          <h3 className="text-xl text-gray-300 font-bold mb-4">Last Updated: June 23, 2025</h3>

          <p className="text-gray-300 mb-4">
            This Privacy Policy describes how Barnyard Billionaire ("we," "us," or "our") collects, uses, and shares information when you play our mobile game, "Barnyard Billionaire" (the "Game"). We are committed to protecting your privacy.
          </p>
          <p className="text-gray-300 mb-4">
            Please read this Privacy Policy carefully. By installing, accessing, or using the Game, you agree to the collection and use of information in accordance with this policy. If you do not agree with the terms of this policy, please do not use the Game.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            1. Information We Collect
          </h4>
          <p className="text-gray-300 mb-4">
            We primarily collect non-personally identifiable information directly through the Game and through third-party services integrated into the Game. We do not directly collect sensitive personal information such such as your name, address, or email unless you voluntarily provide it (e.g., when contacting support).
          </p>
          <p className="text-gray-300 mb-4">
            a. Information You Voluntarily Provide
            Support Communications: If you contact our support team, we may collect information you provide, such as your email address, device information, and details about your inquiry, to help resolve your issue.
          </p>
          <p className="text-gray-300 mb-4">
            Referral Code: Your game generates a unique referral code. This code is used within the game for referral mechanics and is not linked to your personal identity.
          </p>
          <p className="text-gray-300 mb-4">
            b. Information Collected Automatically (Non-Personally Identifiable Information)
            When you use the Game, we may automatically collect certain non-personally identifiable information about your device and gameplay, including:
          </p>
          <p className="text-gray-300 mb-4">
            Device Information: Device type, operating system, unique device identifiers (like IDFV for iOS), IP address, general location (e.g., country or city derived from IP address), and mobile network information.
          </p>
          <p className="text-gray-300 mb-4">
            Gameplay Data: Your progress in the Game, scores, achievements, in-game purchases, session length, and interactions with other players (if applicable).
          </p>
          <p className="text-gray-300 mb-4">
            Usage Data: Information about how you interact with the Game, such as features used, time spent, and performance data.
          </p>
          <p className="text-gray-300 mb-4">
            Advertising Identifiers: Identifiers like the Identifier for Advertisers (IDFA) on iOS, or Android Advertising ID (AAID) on Android, which are used by advertising networks for delivering personalized ads and measuring ad effectiveness. These can be reset by you through your device settings.
          </p>
          <p className="text-gray-300 mb-4">
            c. Information from Third-Party Services
            We integrate various third-party services into the Game to provide core functionalities like advertising, analytics, and platform services. These third parties may collect information directly from your device according to their own privacy policies. These services may include:
          </p>
          <p className="text-gray-300 mb-4">
            Unity Technologies:
          </p>
          <p className="text-gray-300 mb-4">
            Unity Ads: For displaying rewarded video and interstitial advertisements. Unity Ads collects various device and usage data to serve relevant ads and measure ad performance.
          </p>
          <p className="text-gray-300 mb-4">
            Unity Analytics: For understanding how users play the Game, which helps us improve gameplay and features. It collects gameplay data, device information, and usage patterns.
          </p>
          <p className="text-gray-300 mb-4">
            Unity Mediation (LevelPlay / IronSource): As a mediation platform, it connects to various ad networks (like Unity Ads, Google AdMob, etc.). It collects data to optimize ad delivery, measure performance, and prevent fraud.
          </p>
          <p className="text-gray-300 mb-4">
            Google AdMob (via LevelPlay/Unity Mediation): A common ad network that collects data for serving personalized advertisements.
          </p>
          <p className="text-gray-300 mb-4">
            Apple (for iOS users): Information collected via the Apple App Store, Game Center (if you use it), and the Identifier for Advertisers (IDFA) for ad tracking.
          </p>
          <p className="text-gray-300 mb-4">
            We do not control the data collection practices of these third parties. We encourage you to review their respective privacy policies to understand their data collection, use, and disclosure practices.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            2. How We Use Your Information
          </h4>
          <p className="text-gray-300 mb-4">
            We use the information collected for the following purposes:
          </p>
          <p className="text-gray-300 mb-4">
            To Operate and Maintain the Game: To provide you with the Game's features, content, and services.
          </p>
          <p className="text-gray-300 mb-4">
            To Improve and Optimize the Game: To understand how you use the Game, identify bugs, and enhance gameplay, performance, and user experience.
          </p>
          <p className="text-gray-300 mb-4">
            For Analytics and Research: To analyze trends, monitor usage, and gather demographic information about our user base.
          </p>
          <p className="text-gray-300 mb-4">
            To Serve Advertisements: To display relevant advertisements within the Game, measure the effectiveness of ad campaigns, and personalize ad content.
          </p>
          <p className="text-gray-300 mb-4">
            To Provide Customer Support: To respond to your inquiries, troubleshoot issues, and provide technical assistance.
          </p>
          <p className="text-gray-300 mb-4">
            For Internal Operations: For purposes such as fraud prevention, security, and legal compliance.
          </p>
          <p className="text-gray-300 mb-4">
            To Facilitate Referral Mechanics: To implement and track in-game referral bonuses using your unique referral code.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            3. How We Share Your Information
          </h4>
          <p className="text-gray-300 mb-4">
            We may share your information with third parties in the following circumstances:
          </p>
          <p className="text-gray-300 mb-4">
            With Third-Party Service Providers: We share non-personally identifiable information with the integrated third-party services (e.g., Unity Ads, LevelPlay/IronSource, Unity Analytics) to enable them to provide their services. These providers are obligated to protect your information.
          </p>
          <p className="text-gray-300 mb-4">
            For Legal Reasons: We may disclose your information if required to do so by law or in response to valid requests by public authorities (e.g., a court order or government agency).
          </p>
          <p className="text-gray-300 mb-4">
            To Protect Our Rights: We may disclose information when we believe it is necessary to investigate, prevent, or take action regarding illegal activities, suspected fraud, situations involving potential threats to the physical safety of any person, violations of our terms of service, or as otherwise required by law.
          </p>
          <p className="text-gray-300 mb-4">
            Business Transfers: In the event of a merger, acquisition, reorganization, bankruptcy, or other sale of all or a portion of our assets, your information may be transferred to the acquiring entity.
          </p>
          <p className="text-gray-300 mb-4">
            With Your Consent: We may share your information with your consent or at your direction.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            4. Data Retention
          </h4>
          <p className="text-gray-300 mb-4">
            We retain collected information for as long as necessary to fulfill the purposes outlined in this Privacy Policy, unless a longer retention period is required or permitted by law. Once no longer needed, we will delete or anonymize your information.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            5. Data Security
          </h4>
          <p className="text-gray-300 mb-4">
            We implement reasonable security measures to protect your information from unauthorized access, alteration, disclosure, or destruction. However, no method of transmission over the internet or electronic storage is 100% secure. Therefore, we cannot guarantee its absolute security.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            6. Your Choices and Rights
          </h4>
          <p className="text-gray-300 mb-4">
            Advertising Identifiers: You can reset your device's advertising identifier (IDFA on iOS, AAID on Android) at any time through your device settings. This may limit your ability to receive personalized ads.
          </p>
          <p className="text-gray-300 mb-4">
            Tracking Opt-Out (iOS 14.5+): On iOS devices running iOS 14.5 or later, you may be presented with an App Tracking Transparency (ATT) prompt. You have the choice to grant or deny permission for apps to track you across other companies' apps and websites. Denying permission may result in less personalized advertising.
          </p>
          <p className="text-gray-300 mb-4">
            Data Access and Deletion: Depending on your jurisdiction, you may have rights to access, update, or request deletion of your personal data. Please contact us using the information below if you wish to exercise these rights. We will respond to your request in accordance with applicable laws.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            7. Children's Privacy
          </h4>
          <p className="text-gray-300 mb-4">
            The Game is not directed to children under the age of 13 (or other applicable age of consent in your jurisdiction). We do not knowingly collect personally identifiable information from children under these ages. If we become aware that we have inadvertently collected personal data from a child under the applicable age without parental consent, we will take steps to delete such information from our records. If you believe we may have collected information from a child, please contact us immediately.
          </p>
          <h4 className="text-gray-300 font-bold mb-4">
            8. Changes to This Privacy Policy
          </h4>
          <p className="text-gray-300 mb-4">
            We may update our Privacy Policy from time to time. We will notify you of any changes by posting the new Privacy Policy on this page and updating the "Last Updated" date. We encourage you to review this Privacy Policy periodically for any changes. Your continued use of the Game after any modifications to this Privacy Policy will constitute your acknowledgment of the modifications and your consent to abide and be bound by the modified Privacy Policy.
          </p>
        </div>
      </div>
      <div className="mt-16">
        <Footer />
      </div>
    </div>
  );
}