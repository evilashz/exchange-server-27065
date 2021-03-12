using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.SoftDeletedMailboxRemoval
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalLogEntry : ConfigurableObject
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public SoftDeletedMailboxRemovalLogEntry() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000FB7D File Offset: 0x0000DD7D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SoftDeletedMailboxRemovalLogEntrySchema>();
			}
		}
	}
}
