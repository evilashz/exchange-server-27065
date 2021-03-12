using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000386 RID: 902
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarCopyMoveException : Exception
	{
		// Token: 0x060027CE RID: 10190 RVA: 0x0009F2F9 File Offset: 0x0009D4F9
		public CalendarCopyMoveException(string message) : base(message)
		{
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0009F302 File Offset: 0x0009D502
		public CalendarCopyMoveException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
