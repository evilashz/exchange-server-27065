using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF0 RID: 3056
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PFAttachmentContext : PFRuleEvaluationContext
	{
		// Token: 0x06006D14 RID: 27924 RVA: 0x001D22DF File Offset: 0x001D04DF
		public PFAttachmentContext(PFMessageContext messageContext, Attachment attachment) : base(messageContext)
		{
			this.attachment = attachment;
		}

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x06006D15 RID: 27925 RVA: 0x001D22EF File Offset: 0x001D04EF
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x001D22F7 File Offset: 0x001D04F7
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04003E21 RID: 15905
		private Attachment attachment;
	}
}
