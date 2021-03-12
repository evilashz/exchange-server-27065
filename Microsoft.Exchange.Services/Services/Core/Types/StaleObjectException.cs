using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000890 RID: 2192
	internal sealed class StaleObjectException : ServicePermanentException
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000D929A File Offset: 0x000D749A
		public StaleObjectException() : base((CoreResources.IDs)3943872330U)
		{
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x000D92AC File Offset: 0x000D74AC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
