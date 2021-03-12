using System;
using System.Globalization;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000007 RID: 7
	internal static class DateTimeExtensionMethods
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002593 File Offset: 0x00000793
		public static string ToIso8061(this DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x0400001A RID: 26
		private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
	}
}
