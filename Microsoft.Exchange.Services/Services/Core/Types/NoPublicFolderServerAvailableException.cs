using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000828 RID: 2088
	internal sealed class NoPublicFolderServerAvailableException : ServicePermanentException
	{
		// Token: 0x06003C67 RID: 15463 RVA: 0x000D5C87 File Offset: 0x000D3E87
		public NoPublicFolderServerAvailableException() : base(ResponseCodeType.ErrorNoPublicFolderReplicaAvailable, (CoreResources.IDs)2356362688U)
		{
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x000D5C9E File Offset: 0x000D3E9E
		public NoPublicFolderServerAvailableException(Exception innerException) : base(ResponseCodeType.ErrorNoPublicFolderReplicaAvailable, (CoreResources.IDs)2356362688U, innerException)
		{
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000D5CB6 File Offset: 0x000D3EB6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
