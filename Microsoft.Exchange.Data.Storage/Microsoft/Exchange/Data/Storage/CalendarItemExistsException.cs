using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000389 RID: 905
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemExistsException : CalendarCopyMoveException
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x0009F31E File Offset: 0x0009D51E
		public CalendarItemExistsException(string message) : base(message)
		{
		}
	}
}
