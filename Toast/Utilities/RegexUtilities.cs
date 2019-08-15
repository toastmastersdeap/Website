using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Toast.Utilities
{
   public class RegexUtilities
   {
      private bool _invalid = false;

      public bool IsValidEmail(string strIn)
      {
         _invalid = false;
         if (string.IsNullOrEmpty(strIn))
            return false;

         // Use IdnMapping class to convert Unicode domain names.
         try
         {
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
               RegexOptions.None, TimeSpan.FromMilliseconds(200));
         }
         catch (RegexMatchTimeoutException)
         {
            return false;
         }

         if (_invalid)
            return false;

         // Return true if strIn is in valid e-mail format.
         try
         {
            return Regex.IsMatch(strIn,
               @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
               RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
         }
         catch (RegexMatchTimeoutException)
         {
            return false;
         }
      }

      //public static bool IsSimpleString(string str)
      //{
      //   try
      //   {
      //      return Regex.IsMatch(str, "^[a-zA-Z0-9.\\s]*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
      //   }
      //   catch (RegexMatchTimeoutException)
      //   {
      //      return false;
      //   }
      //}

      //public static string RemoveSpecialCharacters(string str)
      //{
      //   return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
      //}

      private string DomainMapper(Match match)
      {
         // IdnMapping class with default property values.
         var idn = new IdnMapping();

         var domainName = match.Groups[2].Value;
         try
         {
            domainName = idn.GetAscii(domainName);
         }
         catch (ArgumentException)
         {
            _invalid = true;
         }
         return match.Groups[1].Value + domainName;
      }
   }
}