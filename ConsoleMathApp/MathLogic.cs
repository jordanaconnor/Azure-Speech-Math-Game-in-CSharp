namespace ConsoleMathApp;

public class MathLogic
{
    public static int RandomNum(string mode)
    {
        //Change numbers here for difficulty
        var rand = new Random();
        var looping = false;
        if (mode == "Hard")
        {
            var numValue = rand.Next(1, 1001);
            if (numValue <= 2) looping = true;

            while (looping)
            {
                numValue = rand.Next(1, 1001);

                if (numValue > 2) looping = false;
            }

            return numValue;
        }

        if (mode == "Medium") return rand.Next(1, 101);

        return rand.Next(1, 11);
    }

    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Subtract(int a, int b)
    {
        return a - b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static int Divide(int a, int b)
    {
        if (b != 0) return a / b;

        return -1000000000;
    }

    public static void PrintProblem(int a, int b, char operatorSymbol)
    {
        Console.WriteLine($"          {a} {operatorSymbol} {b} = ?");
    }
}