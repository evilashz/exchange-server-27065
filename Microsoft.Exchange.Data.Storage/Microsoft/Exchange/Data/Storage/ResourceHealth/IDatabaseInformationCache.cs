using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B22 RID: 2850
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDatabaseInformationCache
	{
		// Token: 0x06006755 RID: 26453
		IDatabaseInformation Get(Guid key);
	}
}
