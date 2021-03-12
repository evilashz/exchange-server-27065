using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x020000FF RID: 255
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedDatabaseCleanupRequest : BaseRequest
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x00015661 File Offset: 0x00013861
		public SoftDeletedDatabaseCleanupRequest(IClientFactory clientFactory, DirectoryDatabase directoryObject, ByteQuantifiedSize targetSize)
		{
			this.clientFactory = clientFactory;
			this.directoryObject = directoryObject;
			this.targetSize = targetSize;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00015680 File Offset: 0x00013880
		protected override void ProcessRequest()
		{
			using (ILoadBalanceService loadBalanceClientForDatabase = this.clientFactory.GetLoadBalanceClientForDatabase(this.directoryObject))
			{
				loadBalanceClientForDatabase.CleanupSoftDeletedMailboxesOnDatabase(this.directoryObject.Identity, this.targetSize);
			}
		}

		// Token: 0x040002F1 RID: 753
		private readonly IClientFactory clientFactory;

		// Token: 0x040002F2 RID: 754
		private readonly DirectoryDatabase directoryObject;

		// Token: 0x040002F3 RID: 755
		private readonly ByteQuantifiedSize targetSize;
	}
}
