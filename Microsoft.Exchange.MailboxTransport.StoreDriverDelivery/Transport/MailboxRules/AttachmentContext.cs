using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x0200008B RID: 139
	internal class AttachmentContext : RuleEvaluationContext
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00018F74 File Offset: 0x00017174
		public AttachmentContext(MessageContext messageContext, Attachment attachment) : base(messageContext)
		{
			this.attachment = attachment;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00018F84 File Offset: 0x00017184
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00018F8C File Offset: 0x0001718C
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x040002A6 RID: 678
		private Attachment attachment;
	}
}
