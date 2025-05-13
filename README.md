 ByteShield Cybersecurity Chatbot

 Overview
ByteShield is a console-based C# chatbot designed for the PROG6221 Portfolio of Evidence. It educates users on cybersecurity topics like password safety, 
phishing, and VPN usage. The chatbot features a voice greeting, ASCII logo, dynamic responses, sentiment detection, and user memory for personalized interactions,
meeting both Part 1 and Part 2 requirements.

 Features
 Part 1
- **Voice Greeting**: Plays `sound.wav` on startup.
- **ASCII Logo**: Converts `logo.png` to ASCII art.
- **User Interaction**: Prompts for user’s name and personalizes responses.
- **Cybersecurity Responses**: Answers questions on topics like "password," "phishing," "vpn."
- **Input Validation**: Handles empty/invalid inputs.
- **Enhanced UI**: Uses colored text, borders, and spacing.

Part 2
- **Keyword Recognition**: Recognizes keywords like "password," "scam," "vpn."
- **Random Responses**: Provides varied responses for "phishing," "password," "vpn."
- **Conversation Flow**: Maintains topic context for follow-up questions (e.g., "more").
- **Memory and Recall**: Stores and recalls user interests (e.g., "I’m interested in vpn").
- **Sentiment Detection**: Detects "stressed," "curious," and "frustrated" sentiments.
- **Delegates**: Uses `ResponseProcessor` delegate for response processing.
- **UI Enhancement**: Adds typing effect for conversational feel.

 Usage
1. Run the program: 
2. Enter your name when prompted.
3. Ask about cybersecurity topics (e.g., "password safety," "phishing tips").
4. Declare interests: "I’m interested in vpn" to personalize responses.
5. Use follow-up questions: "more" or "details" to continue the current topic.
6. Trigger sentiments: "I’m worried about phishing" (stressed), "How does a vpn work?" (curious), "I’m frustrated with passwords" (frustrated).
7. Ask general questions: "How are you?" or "What’s your purpose?"
8. Type "exit" to quit.
