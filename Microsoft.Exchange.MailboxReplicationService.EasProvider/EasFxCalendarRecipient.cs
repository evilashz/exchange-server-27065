using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000013 RID: 19
	internal class EasFxCalendarRecipient : IRecipient
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000076B2 File Offset: 0x000058B2
		public EasFxCalendarRecipient(IPropertyBag propertyBag)
		{
			this.PropertyBag = propertyBag;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000076C1 File Offset: 0x000058C1
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000076C9 File Offset: 0x000058C9
		public IPropertyBag PropertyBag { get; private set; }

		// Token: 0x0600016E RID: 366 RVA: 0x000076D2 File Offset: 0x000058D2
		public void Save()
		{
		}
	}
}
