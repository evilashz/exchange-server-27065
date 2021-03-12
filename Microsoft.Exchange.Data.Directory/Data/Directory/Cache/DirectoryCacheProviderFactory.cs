using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A1 RID: 161
	internal sealed class DirectoryCacheProviderFactory
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00027EEB File Offset: 0x000260EB
		private DirectoryCacheProviderFactory()
		{
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00027EF3 File Offset: 0x000260F3
		internal static DirectoryCacheProviderFactory Default
		{
			get
			{
				return DirectoryCacheProviderFactory.instance;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00027EFA File Offset: 0x000260FA
		internal IDirectoryCacheProvider CreateNewDirectoryCacheProvider()
		{
			return new ExchangeDirectoryCacheProvider();
		}

		// Token: 0x040002ED RID: 749
		private static readonly DirectoryCacheProviderFactory instance = new DirectoryCacheProviderFactory();
	}
}
