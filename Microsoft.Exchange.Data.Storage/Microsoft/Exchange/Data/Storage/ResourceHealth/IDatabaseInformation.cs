using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B20 RID: 2848
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDatabaseInformation
	{
		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x0600674B RID: 26443
		Guid DatabaseGuid { get; }

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x0600674C RID: 26444
		string DatabaseName { get; }

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x0600674D RID: 26445
		string DatabaseVolumeName { get; }
	}
}
