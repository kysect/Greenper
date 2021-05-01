using System.Collections.Generic;

namespace OopSubmitStatistic.Models
{
    public record StudentRow(
        string Name,
        string Group,
        double Lab1,
        double Lab2,
        double Lab3,
        double Lab4,
        double Lab5,
        double Lab6,
        double Theory,
        double Lk,
        double Exam,
        double Sum)
    {
        public static bool Parse(IList<object> data, out StudentRow studentRow)
        {
            studentRow = null;
            if (data[0] is not string s || string.IsNullOrEmpty(s))
                return false;

            if (data[4] is null || !double.TryParse(data[4].ToString(), out double l1))
                l1 = 0;
            if (data[7] is null || !double.TryParse(data[7].ToString(), out double l2))
                l2 = 0;
            if (data[10] is null || !double.TryParse(data[10].ToString(), out double l3))
                l3 = 0;
            if (data[13] is null || !double.TryParse(data[13].ToString(), out double l4))
                l4 = 0;
            if (data[16] is null || !double.TryParse(data[16].ToString(), out double l5))
                l5 = 0;
            if (data[19] is null || !double.TryParse(data[19].ToString(), out double l6))
                l6 = 0;

            if (data[20] is null || !double.TryParse(data[20].ToString(), out double th))
                th = 0;
            if (data[21] is null || !double.TryParse(data[21].ToString(), out double lk1))
                lk1 = 0;
            if (data[22] is null || !double.TryParse(data[22].ToString(), out double lk2))
                lk2 = 0;
            if (data[23] is null || !double.TryParse(data[23].ToString(), out double ex))
                ex = 0;
            if (data[24] is null || !double.TryParse(data[24].ToString(), out double sum))
                sum = 0;

            studentRow = new StudentRow(
                data[0].ToString(),
                data[1].ToString().Substring(7, 2),
                l1,
                l2,
                l3,
                l4,
                l5,
                l6,
                th,
                lk1 + lk2,
                ex,
                sum);

            return true;
        }
    }
}