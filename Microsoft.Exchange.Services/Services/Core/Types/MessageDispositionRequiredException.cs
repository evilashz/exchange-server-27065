using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000801 RID: 2049
	[Serializable]
	internal sealed class MessageDispositionRequiredException : ServicePermanentException
	{
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000D4F21 File Offset: 0x000D3121
		public MessageDispositionRequiredException() : base(CoreResources.IDs.ErrorMessageDispositionRequired)
		{
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000D4F33 File Offset: 0x000D3133
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
