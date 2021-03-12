using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public struct JET_LOGTIME : IEquatable<JET_LOGTIME>, IJET_LOGTIME, INullableJetStruct
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		internal JET_LOGTIME(ulong ui64Time)
		{
			this.bSeconds = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bMinutes = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bHours = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bDays = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bMonth = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bYear = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bFiller1 = (byte)(ui64Time & 255UL);
			ui64Time >>= 8;
			this.bFiller2 = (byte)(ui64Time & 255UL);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private JET_LOGTIME(byte year, byte month, byte day, byte hours, byte minutes, byte seconds, byte filler1, byte filler2)
		{
			this.bSeconds = seconds;
			this.bMinutes = minutes;
			this.bHours = hours;
			this.bDays = day;
			this.bMonth = month;
			this.bYear = year;
			this.bFiller1 = filler1;
			this.bFiller2 = filler2;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		internal static JET_LOGTIME CreateArbitraryJetLogtimeForTestingOnly(byte year, byte month, byte day, byte hours, byte minutes, byte seconds, byte filler1, byte filler2)
		{
			return new JET_LOGTIME(year, month, day, hours, minutes, seconds, filler1, filler2);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		internal ulong ToUint64()
		{
			ulong num = (ulong)this.bFiller2;
			num = (num << 8) + (ulong)this.bFiller1;
			num = (num << 8) + (ulong)this.bYear;
			num = (num << 8) + (ulong)this.bMonth;
			num = (num << 8) + (ulong)this.bDays;
			num = (num << 8) + (ulong)this.bHours;
			num = (num << 8) + (ulong)this.bMinutes;
			return (num << 8) + (ulong)this.bSeconds;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000CE44 File Offset: 0x0000B044
		internal JET_LOGTIME(DateTime time)
		{
			checked
			{
				this.bSeconds = (byte)time.Second;
				this.bMinutes = (byte)time.Minute;
				this.bHours = (byte)time.Hour;
				this.bDays = (byte)time.Day;
				this.bMonth = (byte)time.Month;
				this.bYear = (byte)(time.Year - 1900);
				this.bFiller1 = ((time.Kind == DateTimeKind.Utc) ? 1 : 0);
				this.bFiller1 |= (byte)((time.Millisecond & 127) << 1);
				this.bFiller2 = (byte)((time.Millisecond & 896) >> 6);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public bool HasValue
		{
			get
			{
				return this.bMonth != 0 && 0 != this.bDays;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000CF08 File Offset: 0x0000B108
		public bool fTimeIsUTC
		{
			get
			{
				return 0 != (this.bFiller1 & 1);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000CF18 File Offset: 0x0000B118
		public static bool operator ==(JET_LOGTIME lhs, JET_LOGTIME rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000CF22 File Offset: 0x0000B122
		public static bool operator !=(JET_LOGTIME lhs, JET_LOGTIME rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000CF30 File Offset: 0x0000B130
		public DateTime? ToDateTime()
		{
			if (!this.HasValue)
			{
				return null;
			}
			return new DateTime?(new DateTime((int)this.bYear + 1900, (int)this.bMonth, (int)this.bDays, (int)this.bHours, (int)this.bMinutes, (int)this.bSeconds, (int)(this.bFiller2 & 14) << 6 | (int)((uint)(this.bFiller1 & 254) >> 1), this.fTimeIsUTC ? DateTimeKind.Utc : DateTimeKind.Local));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_LOGTIME({0}:{1}:{2}:{3}:{4}:{5}:0x{6:x}:0x{7:x})", new object[]
			{
				this.bSeconds,
				this.bMinutes,
				this.bHours,
				this.bDays,
				this.bMonth,
				this.bYear,
				this.bFiller1,
				this.bFiller2
			});
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000D040 File Offset: 0x0000B240
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_LOGTIME)obj);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000D070 File Offset: 0x0000B270
		public override int GetHashCode()
		{
			return this.bSeconds.GetHashCode() ^ (int)this.bMinutes << 6 ^ (int)this.bHours << 12 ^ (int)this.bDays << 17 ^ (int)this.bMonth << 22 ^ (int)this.bYear << 24 ^ (int)this.bFiller1 ^ (int)this.bFiller2 << 8;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public bool Equals(JET_LOGTIME other)
		{
			return this.bSeconds == other.bSeconds && this.bMinutes == other.bMinutes && this.bHours == other.bHours && this.bDays == other.bDays && this.bMonth == other.bMonth && this.bYear == other.bYear && this.bFiller1 == other.bFiller1 && this.bFiller2 == other.bFiller2;
		}

		// Token: 0x040002C5 RID: 709
		private readonly byte bSeconds;

		// Token: 0x040002C6 RID: 710
		private readonly byte bMinutes;

		// Token: 0x040002C7 RID: 711
		private readonly byte bHours;

		// Token: 0x040002C8 RID: 712
		private readonly byte bDays;

		// Token: 0x040002C9 RID: 713
		private readonly byte bMonth;

		// Token: 0x040002CA RID: 714
		private readonly byte bYear;

		// Token: 0x040002CB RID: 715
		private readonly byte bFiller1;

		// Token: 0x040002CC RID: 716
		private readonly byte bFiller2;
	}
}
