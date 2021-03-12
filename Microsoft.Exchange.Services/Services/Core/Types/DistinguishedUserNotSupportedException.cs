using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000755 RID: 1877
	internal class DistinguishedUserNotSupportedException : ServicePermanentException
	{
		// Token: 0x06003838 RID: 14392 RVA: 0x000C7170 File Offset: 0x000C5370
		public DistinguishedUserNotSupportedException() : base((CoreResources.IDs)4170132598U)
		{
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000C7182 File Offset: 0x000C5382
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
