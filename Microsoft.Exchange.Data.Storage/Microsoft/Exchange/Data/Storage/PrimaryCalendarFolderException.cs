using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000388 RID: 904
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PrimaryCalendarFolderException : CalendarCopyMoveException
	{
		// Token: 0x060027D1 RID: 10193 RVA: 0x0009F315 File Offset: 0x0009D515
		public PrimaryCalendarFolderException(string message) : base(message)
		{
		}
	}
}
