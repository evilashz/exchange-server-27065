using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200041D RID: 1053
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct LocalizedMonth : IFormattable
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x000C2F48 File Offset: 0x000C1148
		public LocalizedMonth(int month)
		{
			this.month = month;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000C2F51 File Offset: 0x000C1151
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return LocalizedDayOfWeek.GetDateTimeFormatInfo(formatProvider).MonthNames[this.month - 1];
		}

		// Token: 0x040019E3 RID: 6627
		private int month;
	}
}
