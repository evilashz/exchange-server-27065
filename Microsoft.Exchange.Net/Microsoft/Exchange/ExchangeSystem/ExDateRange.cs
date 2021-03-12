using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200005D RID: 93
	public class ExDateRange : IComparable, IComparable<ExDateRange>
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000CA2E File Offset: 0x0000AC2E
		public ExDateRange(ExDateTime start, ExDateTime end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public ExDateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public ExDateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public static ExDateRange Intersection(ExDateRange a, ExDateRange b)
		{
			if (a == null && b != null)
			{
				return new ExDateRange(b.Start, b.End);
			}
			if (a != null && b == null)
			{
				return new ExDateRange(a.Start, a.End);
			}
			ExDateTime t = (ExDateTime.Compare(a.Start, b.Start) > 0) ? a.Start : b.Start;
			ExDateTime t2 = (ExDateTime.Compare(a.End, b.End) < 0) ? a.End : b.End;
			if (!(t <= t2))
			{
				return null;
			}
			return new ExDateRange(t, t2);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000CAE9 File Offset: 0x0000ACE9
		public static bool AreEqual(ExDateRange a, ExDateRange b)
		{
			if (a != null)
			{
				return a.Equals(b);
			}
			return b == null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		public static bool AreOverlapping(ExDateRange a, ExDateRange b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			return ExDateRange.Intersection(a, b) != null;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000CB25 File Offset: 0x0000AD25
		public bool Equals(ExDateRange otherRange)
		{
			return otherRange != null && ExDateTime.Compare(this.Start, otherRange.Start) == 0 && ExDateTime.Compare(this.End, otherRange.End) == 0;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public bool ContainsDate(ExDateTime dateToTest, bool startInclusive, bool endInclusive)
		{
			bool flag;
			if (startInclusive)
			{
				flag = (ExDateTime.Compare(this.Start, dateToTest) <= 0);
			}
			else
			{
				flag = (ExDateTime.Compare(this.Start, dateToTest) < 0);
			}
			if (endInclusive)
			{
				return flag && ExDateTime.Compare(this.End, dateToTest) >= 0;
			}
			return flag && ExDateTime.Compare(this.End, dateToTest) > 0;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public override string ToString()
		{
			return this.start.ToString() + "-" + this.end.ToString();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public int CompareTo(object obj)
		{
			if (obj is ExDateRange)
			{
				return this.CompareTo((ExDateRange)obj);
			}
			throw new ArgumentException("Invalid comparison of ExDateRange value to a different type");
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public int CompareTo(ExDateRange other)
		{
			if (this.Equals(other))
			{
				return 0;
			}
			if (this.Start.CompareTo(other.start) == 0)
			{
				return this.End.CompareTo(other.End);
			}
			return this.Start.CompareTo(other.Start);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public static List<ExDateRange> SubtractRanges(ExDateRange sourceRange, List<ExDateRange> rangesToRemove)
		{
			List<ExDateRange> list = new List<ExDateRange>();
			list.Add(sourceRange);
			rangesToRemove.Sort();
			foreach (ExDateRange rangeToRemove in rangesToRemove)
			{
				list = ExDateRange.Subtract(list, rangeToRemove);
			}
			return list;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		public static List<ExDateRange> Subtract(ExDateRange sourceRange, ExDateRange rangeToRemove)
		{
			List<ExDateRange> list = new List<ExDateRange>();
			ExDateRange exDateRange = ExDateRange.Intersection(sourceRange, rangeToRemove);
			if (exDateRange == null || exDateRange.Start.Equals(exDateRange.End))
			{
				list.Add(sourceRange);
			}
			else if (!sourceRange.Equals(exDateRange))
			{
				if (sourceRange.Start < exDateRange.Start)
				{
					list.Add(new ExDateRange(sourceRange.Start, exDateRange.Start));
				}
				if (exDateRange.End < sourceRange.End)
				{
					list.Add(new ExDateRange(exDateRange.End, sourceRange.End));
				}
			}
			return list;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static List<ExDateRange> Subtract(List<ExDateRange> sourceRanges, ExDateRange rangeToRemove)
		{
			List<ExDateRange> list = new List<ExDateRange>();
			foreach (ExDateRange exDateRange in sourceRanges)
			{
				if (ExDateRange.Intersection(exDateRange, rangeToRemove) == null)
				{
					list.Add(exDateRange);
				}
				else
				{
					list.AddRange(ExDateRange.Subtract(exDateRange, rangeToRemove));
				}
			}
			return list;
		}

		// Token: 0x04000191 RID: 401
		private readonly ExDateTime start;

		// Token: 0x04000192 RID: 402
		private readonly ExDateTime end;
	}
}
