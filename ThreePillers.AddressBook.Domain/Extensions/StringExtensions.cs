namespace ThreePillers.AddressBook.Domain.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);
    }

    public static bool IsValidEmail(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var basicValidation = new EmailAddressAttribute().IsValid(value);

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        return basicValidation && regex.IsMatch(value);
    }

    public static bool IsValidPhone(this string phone)
    {
        try
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(phone, null);
            return phoneNumberUtil.IsValidNumber(phoneNumber);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
