using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C3 RID: 1731
	internal sealed class AttachmentSizeLimitExceededException : ServicePermanentException
	{
		// Token: 0x06003567 RID: 13671 RVA: 0x000BFD74 File Offset: 0x000BDF74
		public AttachmentSizeLimitExceededException() : base(CoreResources.IDs.ErrorAttachmentSizeLimitExceeded)
		{
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000BFD86 File Offset: 0x000BDF86
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x04001DE9 RID: 7657
		internal const int AttachmentDataSizeLimit = 2147483647;
	}
}
