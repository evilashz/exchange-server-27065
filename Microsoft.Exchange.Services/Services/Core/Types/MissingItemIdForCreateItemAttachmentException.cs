using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000813 RID: 2067
	internal sealed class MissingItemIdForCreateItemAttachmentException : ServicePermanentException
	{
		// Token: 0x06003C22 RID: 15394 RVA: 0x000D592F File Offset: 0x000D3B2F
		public MissingItemIdForCreateItemAttachmentException() : base(CoreResources.IDs.ErrorMissingItemForCreateItemAttachment)
		{
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x000D5941 File Offset: 0x000D3B41
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
