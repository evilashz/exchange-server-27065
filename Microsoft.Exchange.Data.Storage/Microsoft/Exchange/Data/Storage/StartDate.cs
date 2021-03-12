using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDB RID: 3291
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class StartDate : TaskDate
	{
		// Token: 0x060071FC RID: 29180 RVA: 0x001F8D03 File Offset: 0x001F6F03
		internal StartDate() : base("StartDate", InternalSchema.UtcStartDate, InternalSchema.LocalStartDate)
		{
		}
	}
}
