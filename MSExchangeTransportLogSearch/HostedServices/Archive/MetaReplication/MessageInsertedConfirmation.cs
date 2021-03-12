using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000057 RID: 87
	[MimeContent]
	public sealed class MessageInsertedConfirmation : MetaConfirmation<MessageInsertedConfirmation, MessageInsertedKey>
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		public override void Confirm(IReplicationService service)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			service.Confirm(this);
		}
	}
}
