using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OnDatabaseDismountedEventArgs : EventArgs
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000ABEA File Offset: 0x00008DEA
		internal OnDatabaseDismountedEventArgs(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000AC04 File Offset: 0x00008E04
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x04000107 RID: 263
		private readonly Guid databaseGuid;
	}
}
