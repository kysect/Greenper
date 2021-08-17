using System;
using System.Linq;

namespace Greenper.Extensions
{
    internal static class IndexExtensions
    {
        public static String TakeAfter(this String str, Char limiter, Func<Char, Boolean> predicate) =>
            new String(str.SkipWhile(c => c != limiter && predicate(c)).Skip(1).ToArray());

        public static String TakeAfter(this String str, Char limiter) => str.TakeAfter(limiter, _ => true);

        public static String TakeBefore(this String str, Char limiter, Func<Char, Boolean> predicate) =>
            new String(str.TakeWhile(c => c != limiter && predicate(c)).ToArray());

        public static String TakeBefore(this String str, Char limiter) => str.TakeBefore(limiter, _ => true);
    }
}