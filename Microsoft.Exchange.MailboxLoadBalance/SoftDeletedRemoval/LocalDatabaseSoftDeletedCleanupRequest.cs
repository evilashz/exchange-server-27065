using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x020000FC RID: 252
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LocalDatabaseSoftDeletedCleanupRequest : BaseRequest
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x000153C0 File Offset: 0x000135C0
		public LocalDatabaseSoftDeletedCleanupRequest(DirectoryIdentity databaseIdentity, ByteQuantifiedSize targetSize, LoadBalanceAnchorContext context)
		{
			this.databaseIdentity = databaseIdentity;
			this.targetSize = targetSize;
			this.context = context;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000153DD File Offset: 0x000135DD
		protected override void ProcessRequest()
		{
			if (this.context.Settings.SoftDeletedCleanupEnabled)
			{
				this.context.CleanupSoftDeletedMailboxesOnDatabase(this.databaseIdentity, this.targetSize);
			}
		}

		// Token: 0x040002E9 RID: 745
		private readonly LoadBalanceAnchorContext context;

		// Token: 0x040002EA RID: 746
		private readonly DirectoryIdentity databaseIdentity;

		// Token: 0x040002EB RID: 747
		private readonly ByteQuantifiedSize targetSize;
	}
}
