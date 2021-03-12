using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B1 RID: 1969
	internal sealed class InvalidGetSharingFolderRequestException : ServicePermanentException
	{
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000CF427 File Offset: 0x000CD627
		public InvalidGetSharingFolderRequestException() : base(CoreResources.IDs.ErrorInvalidGetSharingFolderRequest)
		{
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x000CF439 File Offset: 0x000CD639
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
