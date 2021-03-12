using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000814 RID: 2068
	internal sealed class MissingRecipientsException : ServicePermanentException
	{
		// Token: 0x06003C24 RID: 15396 RVA: 0x000D5948 File Offset: 0x000D3B48
		public MissingRecipientsException() : base((CoreResources.IDs)2985674644U)
		{
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000D595A File Offset: 0x000D3B5A
		public MissingRecipientsException(Exception innerException) : base((CoreResources.IDs)2985674644U, innerException)
		{
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000D596D File Offset: 0x000D3B6D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
