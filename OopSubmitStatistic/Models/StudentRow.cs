using System.Collections.Generic;

namespace OopSubmitStatistic.Models
{
    public record StudentRow(
        string Name,
        string Group,
        int Lab1,
        int Lab2,
        int Lab3,
        int Lab4,
        int Lab5,
        int Lab6,
        int Theory,
        int Lk,
        int Exam,
        int Sum)
    {
        public static bool Parse(IList<object> data, out StudentRow studentRow)
        {
            studentRow = null;
            if (data[0] is not string s || string.IsNullOrEmpty(s))
                return false;

            if (data[4] is null || !int.TryParse(data[4].ToString(), out int l1))
                l1 = 0;
            if (data[7] is null || !int.TryParse(data[7].ToString(), out int l2))
                l2 = 0;
            if (data[10] is null || !int.TryParse(data[10].ToString(), out int l3))
                l3 = 0;
            if (data[13] is null || !int.TryParse(data[13].ToString(), out int l4))
                l4 = 0;
            if (data[16] is null || !int.TryParse(data[16].ToString(), out int l5))
                l5 = 0;
            if (data[19] is null || !int.TryParse(data[19].ToString(), out int l6))
                l6 = 0;

            if (data[20] is null || !int.TryParse(data[20].ToString(), out int th))
                th = 0;
            if (data[21] is null || !int.TryParse(data[21].ToString(), out int lk1))
                lk1 = 0;
            if (data[22] is null || !int.TryParse(data[22].ToString(), out int lk2))
                lk2 = 0;
            if (data[23] is null || !int.TryParse(data[23].ToString(), out int ex))
                ex = 0;
            if (data[24] is null || !int.TryParse(data[24].ToString(), out int sum))
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