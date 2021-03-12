using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BF RID: 1727
	internal sealed class AttachmentNestLevelLimitExceededException : ServicePermanentException
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x000BF87D File Offset: 0x000BDA7D
		public AttachmentNestLevelLimitExceededException() : base(CoreResources.IDs.ErrorAttachmentNestLevelLimitExceeded)
		{
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000BF88F File Offset: 0x000BDA8F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP2;
			}
		}
	}
}
