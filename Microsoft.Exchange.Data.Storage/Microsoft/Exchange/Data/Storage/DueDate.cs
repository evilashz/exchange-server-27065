using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDC RID: 3292
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class DueDate : TaskDate
	{
		// Token: 0x060071FD RID: 29181 RVA: 0x001F8D1A File Offset: 0x001F6F1A
		internal DueDate() : base("DueDate", InternalSchema.UtcDueDate, InternalSchema.LocalDueDate)
		{
		}
	}
}
