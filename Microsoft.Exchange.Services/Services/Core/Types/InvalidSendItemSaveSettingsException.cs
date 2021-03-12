using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D1 RID: 2001
	internal sealed class InvalidSendItemSaveSettingsException : ServicePermanentException
	{
		// Token: 0x06003B08 RID: 15112 RVA: 0x000CFB10 File Offset: 0x000CDD10
		public InvalidSendItemSaveSettingsException() : base((CoreResources.IDs)3825363766U)
		{
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000CFB22 File Offset: 0x000CDD22
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
