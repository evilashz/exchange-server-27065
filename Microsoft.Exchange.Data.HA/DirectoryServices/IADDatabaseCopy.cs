using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADDatabaseCopy : IADObjectCommon
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000079 RID: 121
		string HostServerName { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122
		EnhancedTimeSpan ReplayLagTime { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007B RID: 123
		EnhancedTimeSpan TruncationLagTime { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007C RID: 124
		int ActivationPreference { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007D RID: 125
		ADObjectId HostServer { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126
		string DatabaseName { get; }
	}
}
