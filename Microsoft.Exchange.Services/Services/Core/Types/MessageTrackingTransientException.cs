using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000805 RID: 2053
	[Serializable]
	internal sealed class MessageTrackingTransientException : ServicePermanentException
	{
		// Token: 0x06003BF8 RID: 15352 RVA: 0x000D4F6D File Offset: 0x000D316D
		public MessageTrackingTransientException() : base((CoreResources.IDs)3399410586U)
		{
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000D4F7F File Offset: 0x000D317F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
