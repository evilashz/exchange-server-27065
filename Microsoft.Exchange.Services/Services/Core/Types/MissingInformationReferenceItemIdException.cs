using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000810 RID: 2064
	[Serializable]
	internal sealed class MissingInformationReferenceItemIdException : ServicePermanentException
	{
		// Token: 0x06003C1C RID: 15388 RVA: 0x000D58E4 File Offset: 0x000D3AE4
		public MissingInformationReferenceItemIdException() : base(CoreResources.IDs.ErrorMissingInformationReferenceItemId)
		{
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000D58F6 File Offset: 0x000D3AF6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
