using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A5 RID: 2213
	internal sealed class TokenSerializationDeniedException : ServicePermanentException
	{
		// Token: 0x06003EFE RID: 16126 RVA: 0x000D9FBD File Offset: 0x000D81BD
		public TokenSerializationDeniedException() : base((CoreResources.IDs)3279473776U)
		{
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003EFF RID: 16127 RVA: 0x000D9FCF File Offset: 0x000D81CF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
