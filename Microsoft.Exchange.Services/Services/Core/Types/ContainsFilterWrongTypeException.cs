using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072C RID: 1836
	[Serializable]
	internal sealed class ContainsFilterWrongTypeException : ServicePermanentException
	{
		// Token: 0x0600379D RID: 14237 RVA: 0x000C5B40 File Offset: 0x000C3D40
		public ContainsFilterWrongTypeException() : base((CoreResources.IDs)3836413508U)
		{
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000C5B52 File Offset: 0x000C3D52
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
