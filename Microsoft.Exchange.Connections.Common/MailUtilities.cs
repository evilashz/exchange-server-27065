using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MailUtilities
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002610 File Offset: 0x00000810
		public static DateTime ToDateTime(string dateTimeString)
		{
			return new DateHeader("<empty>", DateTime.UtcNow)
			{
				Value = dateTimeString
			}.UtcDateTime;
		}

		// Token: 0x04000047 RID: 71
		private const string EmptyDateHeader = "<empty>";
	}
}
