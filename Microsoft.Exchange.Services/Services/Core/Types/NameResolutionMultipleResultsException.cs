using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081A RID: 2074
	internal sealed class NameResolutionMultipleResultsException : ServicePermanentException
	{
		// Token: 0x06003C31 RID: 15409 RVA: 0x000D59F1 File Offset: 0x000D3BF1
		public NameResolutionMultipleResultsException() : base(CoreResources.IDs.ErrorNameResolutionMultipleResults)
		{
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000D5A03 File Offset: 0x000D3C03
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
