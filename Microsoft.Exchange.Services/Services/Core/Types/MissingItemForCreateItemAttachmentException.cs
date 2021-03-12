using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000812 RID: 2066
	internal sealed class MissingItemForCreateItemAttachmentException : ServicePermanentException
	{
		// Token: 0x06003C20 RID: 15392 RVA: 0x000D5916 File Offset: 0x000D3B16
		public MissingItemForCreateItemAttachmentException() : base(CoreResources.IDs.ErrorMissingItemForCreateItemAttachment)
		{
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000D5928 File Offset: 0x000D3B28
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
