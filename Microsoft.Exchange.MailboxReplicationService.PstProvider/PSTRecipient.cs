using System;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	internal class PSTRecipient : IRecipient
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000065FC File Offset: 0x000047FC
		public PSTRecipient(int recipientNumber, PSTPropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
			PropertyValue property = this.propertyBag.GetProperty(PropertyTag.RowId);
			if (property.IsError)
			{
				property = new PropertyValue(PropertyTag.RowId, recipientNumber);
			}
			this.propertyBag.SetProperty(property);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000664E File Offset: 0x0000484E
		public IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006656 File Offset: 0x00004856
		public void Save()
		{
		}

		// Token: 0x04000042 RID: 66
		private readonly PSTPropertyBag propertyBag;
	}
}
