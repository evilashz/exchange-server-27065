using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal class DateTimeFormatProvider : IFormatProvider
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		private DateTimeFormatProvider(DateTimeFormatProvider.DateTimeFormat format)
		{
			this.format = format;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00020FC4 File Offset: 0x0001F1C4
		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(DateTimeFormatProvider.DateTimeFormat))
			{
				switch (this.format)
				{
				case DateTimeFormatProvider.DateTimeFormat.UTC:
					return "yyMMddHHmmss'Z'";
				}
				return "yyyyMMddHHmmss'.0Z'";
			}
			return null;
		}

		// Token: 0x04000666 RID: 1638
		internal static IFormatProvider UTC = new DateTimeFormatProvider(DateTimeFormatProvider.DateTimeFormat.UTC);

		// Token: 0x04000667 RID: 1639
		internal static IFormatProvider Generalized = new DateTimeFormatProvider(DateTimeFormatProvider.DateTimeFormat.Generalized);

		// Token: 0x04000668 RID: 1640
		private DateTimeFormatProvider.DateTimeFormat format;

		// Token: 0x0200012F RID: 303
		internal enum DateTimeFormat
		{
			// Token: 0x0400066A RID: 1642
			Generalized,
			// Token: 0x0400066B RID: 1643
			UTC
		}
	}
}
