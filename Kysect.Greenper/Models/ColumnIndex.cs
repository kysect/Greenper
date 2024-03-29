﻿using System;
using System.Linq;

namespace Kysect.Greenper.Models
{
    internal class ColumnIndex
    {
        public String RawColumn { get; }

        public ColumnIndex(String rawColumn)
        {
            RawColumn = rawColumn;
        }

        public String ColumnName => new String(RawColumn.TakeWhile(Char.IsLetter).ToArray());
    }
}