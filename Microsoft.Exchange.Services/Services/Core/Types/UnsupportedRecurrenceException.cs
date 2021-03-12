using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B6 RID: 2230
	internal sealed class UnsupportedRecurrenceException : ServicePermanentException
	{
		// Token: 0x06003F48 RID: 16200 RVA: 0x000DB2F3 File Offset: 0x000D94F3
		public UnsupportedRecurrenceException() : base((CoreResources.IDs)3322365201U)
		{
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000DB305 File Offset: 0x000D9505
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
