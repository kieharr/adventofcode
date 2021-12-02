using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Solutions._2020;

public class Day04: ASolution
{
    public override object Part1()
    {
        return string.Join('\n', Input).Split("\n\n")
            .Select(ParsePassport)
            .Count(x => Validator.TryValidateObject(x, new ValidationContext(x), null, false));
    }

    public override object Part2()
    {
        return string.Join('\n', Input).Split("\n\n")
            .Select(ParsePassport)
            .Count(x => Validator.TryValidateObject(x, new ValidationContext(x), null, true));
    }
    
    private Passport ParsePassport(string input)
    {
        var passport = new Passport();
        foreach (var entry in input.Split(' ', '\n').Select(x => x.Split(':')))
        {
            var propertyToSet = typeof(Passport).GetProperties().FirstOrDefault(propInfo =>
                propInfo.GetCustomAttributes(typeof(CodeAttribute), true).OfType<CodeAttribute>().Any(x => x.Value == entry[0]));
            propertyToSet?.SetValue(passport, Convert.ChangeType(entry[1], Nullable.GetUnderlyingType(propertyToSet.PropertyType) ?? propertyToSet.PropertyType), null);
        }
        return passport;
    }
    
    class Passport
    {
        [Code("byr"), Required, Range(1920, 2002)]
        public int? BirthYear { get; set; }
            
        [Code("iyr"), Required, Range(2010, 2020)]
        public int? IssueYear { get; set; }
            
        [Code("eyr"), Required, Range(2020, 2030)]
        public int? ExpirationYear { get; set; }
            
        [Code("hgt"), Required, RegularExpression("(^(1[5-8][0-9]|19[0-3])(cm)$)|(^(59|6[0-9]|7[0-6])(in)$)")]
        public string Height { get; set; }
            
        [Code("hcl"), Required, RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
        public string HairColour { get; set; }
            
        [Code("ecl"), Required, RegularExpression("^(amb|blu|brn|gry|grn|hzl|oth)$")]
        public string EyeColour { get; set; }
            
        [Code("pid"), Required, RegularExpression("^[0-9]{9}$")]
        public string PassportId { get; set; }
            
        [Code("cid")]
        public int? CountryId { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    class CodeAttribute : Attribute
    {
        public string Value { get; }
        public CodeAttribute(string value)
        {
            Value = value;
        }
    }
}