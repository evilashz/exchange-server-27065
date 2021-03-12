using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B71 RID: 2929
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Action : ActionBase
	{
		// Token: 0x06006A07 RID: 27143 RVA: 0x001C59BD File Offset: 0x001C3BBD
		protected Action(ActionType actionType, Rule rule) : base(actionType, rule)
		{
		}
	}
}
