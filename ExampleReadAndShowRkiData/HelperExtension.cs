using ExampleReadAndShowRkiData.Rki;

namespace ExampleReadAndShowRkiData
{
    public static class HelperExtension
    {
        public static string BuildString(this RkiCovidApiCountryItem item)
        {
            return $"{item.name} - {item.weekIncidence:N2}";
        }
    }
}