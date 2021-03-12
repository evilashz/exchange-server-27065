using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000168 RID: 360
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetMoveRequestCommand : NewMoveRequestCommandBase
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x00049757 File Offset: 0x00047957
		public SetMoveRequestCommand(ICollection<Type> ignoredExceptions) : base("Set-MoveRequest", ignoredExceptions)
		{
		}

		// Token: 0x04000608 RID: 1544
		public const string CmdletName = "Set-MoveRequest";
	}
}
