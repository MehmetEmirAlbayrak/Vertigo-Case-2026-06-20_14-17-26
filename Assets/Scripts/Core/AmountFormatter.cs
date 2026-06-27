using System.Globalization;

public static class AmountFormatter
{
    public static string Format(int amount)
    {
        if (amount >= 1000000)
            return (amount / 1000000f).ToString("0.#", CultureInfo.InvariantCulture) + "M";

        if (amount >= 1000)
            return (amount / 1000f).ToString("0.#", CultureInfo.InvariantCulture) + "K";

        return amount.ToString();
    }
}
