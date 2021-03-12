using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AD RID: 685
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct PerRpcStats
	{
		// Token: 0x040011C9 RID: 4553
		internal uint cmsecInServer;

		// Token: 0x040011CA RID: 4554
		internal uint cmsecInCPU;

		// Token: 0x040011CB RID: 4555
		internal uint ulPageRead;

		// Token: 0x040011CC RID: 4556
		internal uint ulPagePreread;

		// Token: 0x040011CD RID: 4557
		internal uint ulLogRecord;

		// Token: 0x040011CE RID: 4558
		internal uint ulcbLogRecord;

		// Token: 0x040011CF RID: 4559
		internal ulong ulLdapReads;

		// Token: 0x040011D0 RID: 4560
		internal ulong ulLdapSearches;

		// Token: 0x040011D1 RID: 4561
		internal uint avgDbLatency;

		// Token: 0x040011D2 RID: 4562
		internal uint avgServerLatency;

		// Token: 0x040011D3 RID: 4563
		internal uint currentThreads;

		// Token: 0x040011D4 RID: 4564
		internal uint totalDbOperations;

		// Token: 0x040011D5 RID: 4565
		internal uint currentDbThreads;

		// Token: 0x040011D6 RID: 4566
		internal uint currentSCTThreads;

		// Token: 0x040011D7 RID: 4567
		internal uint currentSCTSessions;

		// Token: 0x040011D8 RID: 4568
		internal uint processID;

		// Token: 0x040011D9 RID: 4569
		internal uint dataProtectionHealth;

		// Token: 0x040011DA RID: 4570
		internal uint dataAvailabilityHealth;

		// Token: 0x040011DB RID: 4571
		internal uint currentCpuUsage;
	}
}
