using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000815 RID: 2069
	internal class MissingUserIdInformationException : ServicePermanentException
	{
		// Token: 0x06003C27 RID: 15399 RVA: 0x000D5974 File Offset: 0x000D3B74
		public MissingUserIdInformationException() : base(CoreResources.IDs.ErrorMissingUserIdInformation)
		{
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000D5986 File Offset: 0x000D3B86
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
