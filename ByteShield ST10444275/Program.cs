using System;
using System.Collections;//for ArrayList
using System.Collections.Generic;
using System.Drawing;// For Bitmap and Size
using System.IO;//for file operations
using System.Media;//for Sound player

namespace ByteShield_ST10444275
{
    class Program
    {
        static void Main(string[] args)
        {
            try//how the program runs
            {
                //play welcome sound
                PlayVoiceGreeting();

                //show ASCII art logo
                DisplayLogo();

                //get users name 
                string name = GetName();

                //start the conversation loop
                Chat(name);
            }
            finally
            {
                Console.ResetColor();//resert colors if error occur
            }
        }

        //Play welcome sound wav
        static void PlayVoiceGreeting()
        {
            //get current location
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //remove bin debug folder paths 
            path = path.Replace("bin\\Debug\\", "").Replace("bin\\Release\\", "");
            //combine qith sound file name
            string soundPath = Path.Combine(path, "sound.wav");

            try
            {
                //create sound player and play sound
                SoundPlayer player = new SoundPlayer(soundPath);
                player.PlaySync();// plays and waits until finished playing
            }
            catch (Exception e)
            {
                Console.WriteLine("Sound error: " + e.Message);
            }
        }

        //Display logo as ASCII art
        static void DisplayLogo()
        {
            try
            {
                //get path of to logo image
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                basePath = basePath.Replace("bin\\Debug\\", "").Replace("bin\\Release\\", "");
                string imgPath = Path.Combine(basePath, "logo.png");
                
                //check if file exists
                if (!File.Exists(imgPath))
                {
                    //if file isnt found display error massage
                    Console.WriteLine("Logo file missing!");
                    return;
                }

                //Load and resize image
                Bitmap original = new Bitmap(imgPath);
                Size newSize = ResizeImage(original.Size, 70);//max width 70
                Bitmap resized = new Bitmap(original, newSize);

                //adjust console window width
                Console.WindowWidth = Math.Max(resized.Width + 10, Console.WindowWidth);
               //convert to ASCII art
                MakeAscii(resized);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Logo error: " + ex.Message);
            }
        }
        //converts bitmap to ascii char
        static void MakeAscii(Bitmap img)
        {
            for (int height = 0; height < img.Height; height++)
            {
                for (int width = 0; width < img.Width; width++)
                {
                    Color pixelColor = img.GetPixel(width, height);
                    int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    char asciiChar = blue > 200 ? '.' : blue > 150 ? '*' : blue > 100 ? 'o' : blue > 50 ? '#' : '@';
                    Console.Write(asciiChar);
                }
                Console.WriteLine(); // Move to the next row
            }

            Console.WriteLine();
        }
        // Calculate new image size while keeping aspect ratio
        static Size ResizeImage(Size original, int max)
        {
            // Calculate ratio (0.8 compensates for character height)
            double ratio = Math.Min(
                (double)max / original.Width,
                (double)max / original.Height * 2
            );
            return new Size(
                (int)(original.Width * ratio),//new width
                (int)(original.Height * ratio)//new height
            );
        }
        //get user name and show welcome massage
        static string GetName()
        {
            Console.WriteLine();//new line 
            Console.Write("Welcome to ByteShield a chat bot for Cyber Security ");
            Console.WriteLine();//new line 
            Console.Write("What's your name? ");
            string name = Console.ReadLine();

            //default name if empty
            
            do
            {
                Console.Write("Please enter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(name));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nWelcome {name}! Let's learn safety!");
            Console.WriteLine("========================================");
            Console.WriteLine("How can I help you??");
            Console.WriteLine("Type 'exit' to quit");
            Console.WriteLine("========================================\n");
            Console.ResetColor();

            return name;
        }
        //Response list
        static ArrayList responses = new ArrayList()
        {
             // Each entry contains:
            // [0] - keywords to match
            // [1] - response text
          new object[] {
        new string[] {"login" },
        "Definition: A login is the process of authenticating users to access a system or account, typically via credentials like usernames and passwords.\n" +
        "Pros: Secure logins (e.g., multi-factor authentication) reduce unauthorized access. Proper login systems track user activity for accountability.\n" +
        "Cons: Weak login systems are vulnerable to brute-force attacks. Over-reliance on passwords can lead to security gaps.\n" +
        "^How to Protect Yourself:\n- Use multi-factor authentication (MFA).\n- Avoid saving passwords in browsers.\n- Monitor login attempts for suspicious activity."
    },
    new object[] {
        new string[] { "password" },
        "Definition: A password is a secret string of characters used to verify identity during authentication.\n" +
        "Pros: Strong passwords prevent unauthorized account access. Password managers help generate/store complex passwords securely.\n" +
        "Cons: Users often reuse passwords across sites. Weak passwords (e.g., \"123456\") are easily cracked.\n" +
        "^How to Protect Yourself:\n- Use long, complex passwords (12+ chars, mix letters/numbers/symbols).\n- Avoid common phrases.\n- Use a password manager (e.g., LastPass)."
    },
    new object[] {
        new string[] { "phishing" }, 
        "Definition: Phishing is a cyberattack using disguised emails/websites to trick victims into revealing sensitive data.\n" +
        "Cons: Deceptive emails trick users. Phishing links install malware or steal credentials.\n" +
        "^How to Protect Yourself:\n- Verify sender emails.\n- Hover over links before clicking.\n- Never share passwords via email."
    },
    new object[] {
        new string[] { "malware" },
        "Definition: Malware (malicious software) is code designed to damage systems or steal data (e.g., viruses, ransomware).\n" +
        "Pros: Antivirus software detects/removes threats. Regular scans identify infections.\n" +
        "Cons: Can steal data or spy on activity. Zero-day exploits bypass defenses.\n" +
        "^How to Protect Yourself:\n- Install antivirus software.\n- Avoid untrusted downloads.\n- Keep systems updated."
    },
    new object[] {
        new string[] { "vpn" },
        "Definition: A VPN (Virtual Private Network) encrypts internet traffic to protect privacy and mask IP addresses.\n" +
        "Pros: Encrypts traffic. Hides IP addresses.\n" +
        "Cons: Free VPNs may log/sell data. Can reduce internet speed.\n" +
        "^How to Protect Yourself:\n- Use paid no-logs VPNs (e.g., NordVPN).\n- Enable on public Wi-Fi."
    },
    new object[] {
        new string[] { "encryption" },
        "Definition: Encryption converts data into unreadable code to prevent unauthorized access.\n" +
        "Pros: Protects sensitive files. Secures data if devices are stolen.\n" +
        "Cons: Losing keys = permanent data loss. Complex for non-tech users.\n" +
        "^How to Protect Yourself:\n- Encrypt drives (BitLocker/FileVault).\n- Use HTTPS.\n- Store keys securely."
    },
    new object[] {
        new string[] { "update", "patch" },
        "Definition: Updates/patches are software fixes that address vulnerabilities or improve functionality.\n" +
        "Pros: Fix security flaws. Improve stability.\n" +
        "Cons: May introduce bugs. Users often delay them.\n" +
        "^How to Protect Yourself:\n- Enable automatic updates.\n- Install critical patches immediately."
    },
    new object[] {
        new string[] { "social engineering" },
        "Definition: Social engineering manipulates people into divulging secrets (e.g., fake tech support calls).\n" +
        "Cons: Exploits human trust. Harder to detect than technical attacks.\n" +
        "^How to Protect Yourself:\n- Verify identities.\n- Be skeptical of urgent requests.\n- Train to recognize red flags."
    },
    new object[] {
        new string[] { "backup" },
        "Definition: Backups are copies of data stored separately to restore after loss/corruption.\n" +
        "Pros: Recovers data from ransomware/hardware failure. Cloud backups add redundancy.\n" +
        "Cons: Unprotected backups can be hacked. Requires maintenance.\n" +
        "^How to Protect Yourself:\n- Follow 3-2-1 backup rule.\n- Encrypt backups.\n- Test restores."
    },
    new object[] {
        new string[] { "firewall" },
        "Definition: A firewall is a network security system that monitors/controls incoming/outgoing traffic.\n" +
        "Pros: Blocks unauthorized access. Monitors connections.\n" +
        "Cons: Misconfigurations create false security. Advanced threats may bypass.\n" +
        "^How to Protect Yourself:\n- Enable OS firewall.\n- Use hardware firewalls.\n- Configure strict rules."
    },
    new object[] {
        new string[] { "public wifi" },
        "Definition: Public Wi-Fi is unsecured internet access in places like cafes/airports.\n" +
        "Cons: Hackers snoop on traffic. Fake hotspots mimic legitimate networks.\n" +
        "^How to Protect Yourself:\n- Avoid sensitive transactions.\n- Use a VPN.\n- Disable auto-connect."
    },
    new object[] {
        new string[] { "2fa", "mfa" },
        "Definition: 2FA/MFA adds extra authentication steps beyond passwords (e.g., SMS codes, authenticator apps).\n" +
        "Pros: Extra security layer. Protects compromised accounts.\n" +
        "Cons: Losing 2FA device = locked out. SMS-based 2FA is vulnerable.\n" +
        "^How to Protect Yourself:\n- Use app-based 2FA.\n- Store backup codes securely.\n- Avoid SMS for critical accounts."
    },
    new object[] {
        new string[] { "ransomware" },
        "Definition: Ransomware encrypts files and demands payment for decryption.\n" +
        "Cons: Locks access to data. Spreads via malicious attachments.\n" +
        "^How to Protect Yourself:\n- Maintain offline backups.\n- Don’t open suspicious attachments.\n- Use anti-ransomware tools."
    },
    new object[] {
        new string[] { "email" }, 
        "Definition: Email is a digital communication method vulnerable to interception/malware.\n" +
        "Pros: Encryption (e.g., PGP) secures messages. Spam filters reduce scams.\n" +
        "Cons: Unencrypted emails can be read by third parties. Attachments may contain malware.\n" +
        "^How to Protect Yourself:\n- Enable encryption.\n- Avoid unknown attachments.\n- Report phishing."
    }
};
        //chat loop with user
        static void Chat(string name)
        {
            while (true)
            {
                // User input
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"-{name}: ");
                string input = Console.ReadLine().ToLower().Trim();

                // Response action
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n------------------------------");

                // Exit condition
                if (input == "exit")
                {
                    Console.WriteLine("Goodbye! Stay safe!");
                    break;
                }

                List<string> matchedResponses = new List<string>();

                // Check all responses
                foreach (object[] item in responses)
                {
                    string[] keywords = (string[])item[0];
                    bool keywordFound = false;

                    // Check if any keyword in this response matches
                    foreach (string word in keywords)
                    {
                        if (input.Contains(word))
                        {
                            keywordFound = true;
                            break; // Stop checking keywords for this response
                        }
                    }

                    // Add the response if any keyword matched
                    if (keywordFound)
                    {
                        matchedResponses.Add((string)item[1]);
                    }
                }

                // Display results
                if (matchedResponses.Count > 0)
                {
                    Console.WriteLine(string.Join("\n\n", matchedResponses));
                }
                else
                {
                    Console.WriteLine("Sorry we do not provide information unrelated to cyber security. Try other sources like:\n- Google\n- You.com");
                }

                Console.WriteLine("------------------------------\n");
                Console.ResetColor();
            }
        }
    }
}