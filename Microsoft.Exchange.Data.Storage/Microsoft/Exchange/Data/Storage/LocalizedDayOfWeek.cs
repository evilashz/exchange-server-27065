using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000419 RID: 1049
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct LocalizedDayOfWeek : IFormattable
	{
		// Token: 0x06002F2A RID: 12074 RVA: 0x000C27E6 File Offset: 0x000C09E6
		public LocalizedDayOfWeek(DayOfWeek dayOfWeek)
		{
			this.dayOfWeek = dayOfWeek;
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000C27EF File Offset: 0x000C09EF
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000C27F7 File Offset: 0x000C09F7
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return LocalizedDayOfWeek.GetDateTimeFormatInfo(formatProvider).DayNames[(int)this.dayOfWeek];
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000C280B File Offset: 0x000C0A0B
		public override string ToString()
		{
			return this.ToString(string.Empty, null);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000C281C File Offset: 0x000C0A1C
		internal static DateTimeFormatInfo GetDateTimeFormatInfo(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			DateTimeFormatInfo dateTimeFormatInfo = (cultureInfo != null) ? cultureInfo.DateTimeFormat : null;
			if (dateTimeFormatInfo == null)
			{
				dateTimeFormatInfo = (formatProvider as DateTimeFormatInfo);
			}
			if (dateTimeFormatInfo == null)
			{
				dateTimeFormatInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
			}
			return dateTimeFormatInfo;
		}

		// Token: 0x040019D9 RID: 6617
		private DayOfWeek dayOfWeek;
	}
}
