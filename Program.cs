Random random = new Random();

Console.WriteLine("Välkommen att spela Lotto!" +
    "\n\nI det här programmet spelar du en rad lotto per vecka. " +
    "Programmet räknar sedan ut hur lång tid det tar för dig att " +
    "vinna LOTTO samt hur mycket pengar du vunnit/spenderat. Du får även " +
    "en uträkning på hur mycket pengar du fått på börsen istället." +
    "\n\nVänligen lämna in dina nummer mellan 1-35 nedan:");

SpelaLotto();

Console.ReadKey();

void SpelaLotto()
{

    int[] currentUserLotto = LämnaLottoRad();
    int enGångRätt = 0;
    int tvåGångRätt = 0;
    int treGångRätt = 0;
    int fyraGångRätt = 0;
    int femGångRätt = 0;
    int sexGångRätt = 0;
    int sjuGångRätt = 0;

    Console.Write("\nDin inmämnade lotto rad är: ");
    Console.WriteLine(string.Join(" ", currentUserLotto));

    Console.WriteLine("\nTryck på en knapp för att spela!");
    Console.ReadKey();

    bool vunnitLotto = false;
    int antalOmgångar = 0;

    double årligInsats = 152; // 52 veckor x 3 kr
    double utdelningIProcent = 0.05;
    double summa = 0;

    while (vunnitLotto == false)
    {
        int[] currentRoundLotto = GenereraLottoRad();
        int antalRätt = 0;
        for (int i = 0; i < currentUserLotto.Length; i++)
        {
            int currentNumber = currentUserLotto[i];
            for (int j = 0; j < currentRoundLotto.Length; j++)
            {
                int otherCurrentNumber = currentRoundLotto[j];
                if (otherCurrentNumber == currentNumber)
                {
                    antalRätt++;
                    break;
                }
            }
        }

        switch (antalRätt)
        {
            case 1: enGångRätt++; break;
            case 2: tvåGångRätt++; break;
            case 3: treGångRätt++; break;
            case 4: fyraGångRätt++; break;
            case 5: femGångRätt++; break;
            case 6: sexGångRätt++; break;
            case 7: sjuGångRätt++; break;
        }

        antalOmgångar++;

        // Följande kod används för att räkna ut vad samma insats skulle generera på börsen med en utdelning på 5%. 
 

        // Utdelningen räknas var 52 omgång. Dvs var 52 vecka. Dvs varje år.
        if (antalOmgångar % 52  == 0)
        {
            double utdelning = summa * utdelningIProcent;
            summa += utdelning + årligInsats;
        }

        Console.WriteLine($"\nANTAL OMGÅNGAR: {antalOmgångar}" +
            $"\n\nAntal 1 rätt: {enGångRätt}" +
            $"\nAntal 2 rätt: {tvåGångRätt}" +
            $"\nAntal 3 rätt: {treGångRätt}" +
            $"\nAntal 4 rätt: {fyraGångRätt}" +
            $"\nAntal 5 rätt: {femGångRätt}" +
            $"\nAntal 6 rätt: {sexGångRätt}" +
            $"\nAntal LOTTO: {sjuGångRätt}" +
            $"\n\n Utdelning på börsen: {summa}");

        
        if (antalRätt == 7)
        {
            vunnitLotto = true;
        }
    }

    //Räknar ut hur många år det tar innan spelaren vinner Lotto. En omgång är en vecka. 52 omgångar är 1 år.
    double antalÅr = ((double)antalOmgångar) / 52;

    // Räknar ut totala kostnaden, där varje omgång kostar 3 kr. 36% av den totala insatsen på Lotto går tillbaka till spelarna. Enligt uppgifter på Svenska spels hemsida.
    double kostnad = antalOmgångar * 3;
    double vinstUtdelning = ((double)kostnad) * 0.36;
    double vinst = vinstUtdelning - kostnad;

    //Följande formel räknar ut vad utdelningen hade blivit om spelaren istället hade investerat pengarna med en genomsnittlig utdelning på 5%.

    double årligInsatsEllerKostnad = (52 * 3);
    double årligUtdelning = 0.05;
    double totalSumma = årligInsatsEllerKostnad;


    for (int year = 1; year <= antalÅr; year++)
    {
        double utdelning = totalSumma * årligUtdelning;
        totalSumma += utdelning + årligInsatsEllerKostnad;
    }

       

    Console.WriteLine($"Du vann LOTTO! Det tog dig bara {antalÅr:#} år! Grattis!!! Din totala utdelning efter vunnit lotto är: {vinst:#.#} kr.");
    Console.WriteLine($"Om du hade investerat samma summa som du köpte lotter för så skulle du idagsläget haft {totalSumma:#} kr, räknat med en ränta på 5%.");
}



int[] LämnaLottoRad()
{
    int[] dinaLottoNummer = new int[7];
    int correctAppliedNumbers = 0;
    while (correctAppliedNumbers < 7)
    {
        Console.Write($"Nummer {correctAppliedNumbers + 1}: ");
        int userInputNumber = int.Parse(Console.ReadLine());

        if (userInputNumber < 1 || userInputNumber > 35)
        {
            Console.WriteLine("Ditt nummer måste vara mellan 1 och 35. \n");
            continue;
        }

        bool numberAlreadyExist = false;

        for (int i = 0; i < correctAppliedNumbers; i++)
        {
            if (userInputNumber == dinaLottoNummer[i])
            {
                Console.WriteLine("Du har redan angett det numret!");
                numberAlreadyExist = true;
                break;
            }
        }

        if (!numberAlreadyExist)
        {
            dinaLottoNummer[correctAppliedNumbers] = userInputNumber;
            correctAppliedNumbers++;
            Console.WriteLine("Numret är inlagt!");
        }
    }
    Array.Sort(dinaLottoNummer);
    return dinaLottoNummer;
}
int[] GenereraLottoRad()
{
    int[] omgångensLottoNummer = new int[7];
    int addedNumber = 0;
    while (addedNumber < omgångensLottoNummer.Length)
    {
        int randomGeneratedNumberBetween1and35 = random.Next(1, 36);
        for (int i = 0; i < omgångensLottoNummer.Length; i++)
        {
            if (randomGeneratedNumberBetween1and35 == omgångensLottoNummer[i])
            {
                break;
            }
            else
            {
                omgångensLottoNummer[addedNumber] = randomGeneratedNumberBetween1and35;
                addedNumber++;
                break;
            }
        }
    }
    return omgångensLottoNummer;
}

