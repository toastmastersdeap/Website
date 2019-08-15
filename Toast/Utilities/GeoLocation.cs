using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Toast.Utilities
{
    public static class GeoLocation
    {
        public static string GetUserIP(HttpRequestBase request)
        {
            var ip = (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                        && request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                        ? request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                        : request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }

        public static string GetCountryFromIP(string ipAddress)
        {
            string country;
            try
            {
                using (var reader = new DatabaseReader(HttpContext.Current.Server.MapPath("~/App_Data/GeoLite2-Country.mmdb")))
                {
                    var response = reader.Country(ipAddress);
                    country = response.Country.IsoCode;
                }
            }
            catch (AddressNotFoundException)
            {
                country = null;
            }
            catch
            {
                country = null;
            }

            return country;
        }

        public static string GetCityFromIP(string ipAddress)
        {
            string city;
            try
            {
                using (var reader = new DatabaseReader(HttpContext.Current.Server.MapPath("~/App_Data/GeoLite2-City.mmdb")))
                {
                    var response = reader.City(ipAddress);
                    city = response.City.Name;
                }
            }
            catch (AddressNotFoundException)
            {
                city = null;
            }
            catch
            {
                city = null;
            }

            return city;
        }

        public static List<SelectListItem> SelectListCountries(string country)
        {
            var getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var countries =
                   getCultureInfo.Select(cultureInfo => new RegionInfo(cultureInfo.LCID))
                    .Select(getRegionInfo => new SelectListItem
                    {
                        Text = getRegionInfo.EnglishName,
                        Value = getRegionInfo.TwoLetterISORegionName,
                        Selected = country == getRegionInfo.TwoLetterISORegionName
                    }).OrderBy(c => c.Text).DistinctBy(i => i.Text).ToList();
            return countries;
        }

        public static string GetCountryISO(string country)
        {
            var getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var getRegionInfo = getCultureInfo.Select(cultureInfo => new RegionInfo(cultureInfo.LCID));
            var countryISO = getRegionInfo.Where(x => x.EnglishName == country).Select(x => x.TwoLetterISORegionName).FirstOrDefault();

            //var countries =
            //       getCultureInfo.Select(cultureInfo => new RegionInfo(cultureInfo.LCID))
            //        .Select(getRegionInfo => new SelectListItem
            //        {
            //           Text     = getRegionInfo.EnglishName,
            //           Value    = getRegionInfo.TwoLetterISORegionName,
            //           Selected = country == getRegionInfo.TwoLetterISORegionName
            //        }).OrderBy(c => c.Text).DistinctBy(i => i.Text).ToList();
            return countryISO;
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}