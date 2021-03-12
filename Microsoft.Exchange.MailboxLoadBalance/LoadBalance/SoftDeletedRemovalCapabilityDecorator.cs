using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedRemovalCapabilityDecorator : MissingCapabilityLoadBalanceClientDecorator
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0000EF0F File Offset: 0x0000D10F
		public SoftDeletedRemovalCapabilityDecorator(ILoadBalanceService service, DirectoryServer targetServer) : base(service, targetServer)
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000EF19 File Offset: 0x0000D119
		public override void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity identity, ByteQuantifiedSize targetSize)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public override SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("The target server '{0}' does not have the SoftDeletedRemoval capability so removal is not valid", new object[]
			{
				base.TargetServer.Name
			});
		}
	}
}
