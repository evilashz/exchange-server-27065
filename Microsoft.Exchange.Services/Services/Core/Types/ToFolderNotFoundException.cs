using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A4 RID: 2212
	internal sealed class ToFolderNotFoundException : ServicePermanentException
	{
		// Token: 0x06003EFB RID: 16123 RVA: 0x000D9F91 File Offset: 0x000D8191
		public ToFolderNotFoundException() : base(CoreResources.IDs.ErrorToFolderNotFound)
		{
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x000D9FA3 File Offset: 0x000D81A3
		public ToFolderNotFoundException(Exception innerException) : base(CoreResources.IDs.ErrorToFolderNotFound, innerException)
		{
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x000D9FB6 File Offset: 0x000D81B6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
