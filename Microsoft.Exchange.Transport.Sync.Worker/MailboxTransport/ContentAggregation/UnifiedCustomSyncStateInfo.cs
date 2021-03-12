using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000222 RID: 546
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UnifiedCustomSyncStateInfo : SyncStateInfo
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00042D0E File Offset: 0x00040F0E
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00042D15 File Offset: 0x00040F15
		public override string UniqueName
		{
			get
			{
				return "Microsoft.Exchange.MailboxUnified.ContentAggregation.UnifiedCustomSyncState";
			}
			set
			{
				throw new InvalidOperationException("This property is not settable.");
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00042D21 File Offset: 0x00040F21
		public override int Version
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000A61 RID: 2657
		private const string NameKey = "Microsoft.Exchange.MailboxUnified.ContentAggregation.UnifiedCustomSyncState";
	}
}
