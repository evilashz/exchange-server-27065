using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public struct JET_BKLOGTIME : IEquatable<JET_BKLOGTIME>, IJET_LOGTIME, INullableJetStruct
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00015E70 File Offset: 0x00014070
		internal JET_BKLOGTIME(DateTime time, bool isSnapshot)
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
				this.bFiller2 = (isSnapshot ? 1 : 0);
				this.bFiller2 |= (byte)((time.Millisecond & 896) >> 6);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00015F31 File Offset: 0x00014131
		public bool HasValue
		{
			get
			{
				return this.bMonth != 0 && 0 != this.bDays;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00015F49 File Offset: 0x00014149
		public bool fTimeIsUTC
		{
			get
			{
				return 0 != (this.bFiller1 & 1);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00015F59 File Offset: 0x00014159
		public bool fOSSnapshot
		{
			get
			{
				return 0 != (this.bFiller2 & 1);
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00015F69 File Offset: 0x00014169
		public static bool operator ==(JET_BKLOGTIME lhs, JET_BKLOGTIME rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00015F73 File Offset: 0x00014173
		public static bool operator !=(JET_BKLOGTIME lhs, JET_BKLOGTIME rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00015F80 File Offset: 0x00014180
		public DateTime? ToDateTime()
		{
			if (!this.HasValue)
			{
				return null;
			}
			return new DateTime?(new DateTime((int)this.bYear + 1900, (int)this.bMonth, (int)this.bDays, (int)this.bHours, (int)this.bMinutes, (int)this.bSeconds, (int)(this.bFiller2 & 14) << 6 | (int)((uint)(this.bFiller1 & 254) >> 1), this.fTimeIsUTC ? DateTimeKind.Utc : DateTimeKind.Local));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00015FFC File Offset: 0x000141FC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_BKLOGTIME({0}:{1}:{2}:{3}:{4}:{5}:0x{6:x}:0x{7:x})", new object[]
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

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00016090 File Offset: 0x00014290
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_BKLOGTIME)obj);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000160C0 File Offset: 0x000142C0
		public override int GetHashCode()
		{
			return this.bSeconds.GetHashCode() ^ (int)this.bMinutes << 6 ^ (int)this.bHours << 12 ^ (int)this.bDays << 17 ^ (int)this.bMonth << 22 ^ (int)this.bYear << 24 ^ (int)this.bFiller1 ^ (int)this.bFiller2 << 8;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001611C File Offset: 0x0001431C
		public bool Equals(JET_BKLOGTIME other)
		{
			return this.bSeconds == other.bSeconds && this.bMinutes == other.bMinutes && this.bHours == other.bHours && this.bDays == other.bDays && this.bMonth == other.bMonth && this.bYear == other.bYear && this.bFiller1 == other.bFiller1 && this.bFiller2 == other.bFiller2;
		}

		// Token: 0x0400045A RID: 1114
		private readonly byte bSeconds;

		// Token: 0x0400045B RID: 1115
		private readonly byte bMinutes;

		// Token: 0x0400045C RID: 1116
		private readonly byte bHours;

		// Token: 0x0400045D RID: 1117
		private readonly byte bDays;

		// Token: 0x0400045E RID: 1118
		private readonly byte bMonth;

		// Token: 0x0400045F RID: 1119
		private readonly byte bYear;

		// Token: 0x04000460 RID: 1120
		private readonly byte bFiller1;

		// Token: 0x04000461 RID: 1121
		private readonly byte bFiller2;
	}
}
