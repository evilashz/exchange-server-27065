using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000811 RID: 2065
	[Serializable]
	internal sealed class MissingInformationSharingFolderIdException : ServicePermanentException
	{
		// Token: 0x06003C1E RID: 15390 RVA: 0x000D58FD File Offset: 0x000D3AFD
		public MissingInformationSharingFolderIdException() : base((CoreResources.IDs)2938284467U)
		{
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000D590F File Offset: 0x000D3B0F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
