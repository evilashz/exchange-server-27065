using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089F RID: 2207
	internal sealed class SyncFolderNotFoundException : ServicePermanentException
	{
		// Token: 0x06003EEB RID: 16107 RVA: 0x000D9D92 File Offset: 0x000D7F92
		public SyncFolderNotFoundException() : base(CoreResources.IDs.ErrorSyncFolderNotFound)
		{
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000D9DA4 File Offset: 0x000D7FA4
		public SyncFolderNotFoundException(Exception innerException) : base(CoreResources.IDs.ErrorSyncFolderNotFound, innerException)
		{
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000D9DB7 File Offset: 0x000D7FB7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
