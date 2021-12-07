using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovAnalytica.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GetRandomHexColor(this string s)
        {
            var _chars = "0123456789ABCDEF".ToCharArray();
            var _r = $"{_chars[new Random().Next(0,_chars.Length-1)]}{_chars[new Random().Next(0, _chars.Length - 1)]}";
            var _g = $"{_chars[new Random().Next(0, _chars.Length - 1)]}{_chars[new Random().Next(0, _chars.Length - 1)]}";
            var _b = $"{_chars[new Random().Next(0, _chars.Length - 1)]}{_chars[new Random().Next(0, _chars.Length - 1)]}";
            return $"#{_r}{_g}{_b}";
        }
    }
}
