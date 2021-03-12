using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000819 RID: 2073
	[Serializable]
	internal sealed class MultiLegacyMailboxAccessException : ServicePermanentException
	{
		// Token: 0x06003C2F RID: 15407 RVA: 0x000D59D8 File Offset: 0x000D3BD8
		public MultiLegacyMailboxAccessException() : base(CoreResources.IDs.ErrorMultiLegacyMailboxAccess)
		{
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x000D59EA File Offset: 0x000D3BEA
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
