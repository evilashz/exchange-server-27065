using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000387 RID: 903
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FutureMeetingException : CalendarCopyMoveException
	{
		// Token: 0x060027D0 RID: 10192 RVA: 0x0009F30C File Offset: 0x0009D50C
		public FutureMeetingException(string message) : base(message)
		{
		}
	}
}
