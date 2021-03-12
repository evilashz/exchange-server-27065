using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RecipientAdaptor : IRecipient
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x000234DF File Offset: 0x000216DF
		internal RecipientAdaptor(CoreRecipient coreRecipient, ICoreObject propertyMappingReference, Encoding string8Encoding, bool wantUnicode)
		{
			this.coreRecipient = coreRecipient;
			this.propertyMappingReference = propertyMappingReference;
			this.string8Encoding = string8Encoding;
			this.wantUnicode = wantUnicode;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00023504 File Offset: 0x00021704
		public IPropertyBag PropertyBag
		{
			get
			{
				if (this.recipientPropertyBag == null)
				{
					this.recipientPropertyBag = new RecipientPropertyBagAdaptor(this.coreRecipient.PropertyBag, this.propertyMappingReference, this.string8Encoding, this.wantUnicode);
				}
				return this.recipientPropertyBag;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002353C File Offset: 0x0002173C
		public void Save()
		{
			if (!this.coreRecipient.TryValidateRecipient())
			{
				throw new CorruptRecipientException("Failed to save recipient. The recipient does not have the required properties.", (ErrorCode)2147746075U);
			}
		}

		// Token: 0x04000219 RID: 537
		private readonly CoreRecipient coreRecipient;

		// Token: 0x0400021A RID: 538
		private readonly ICoreObject propertyMappingReference;

		// Token: 0x0400021B RID: 539
		private readonly Encoding string8Encoding;

		// Token: 0x0400021C RID: 540
		private readonly bool wantUnicode;

		// Token: 0x0400021D RID: 541
		private RecipientPropertyBagAdaptor recipientPropertyBag;
	}
}
