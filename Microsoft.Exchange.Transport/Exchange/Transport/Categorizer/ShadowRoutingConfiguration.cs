using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000269 RID: 617
	internal class ShadowRoutingConfiguration
	{
		// Token: 0x06001B0F RID: 6927 RVA: 0x0006F0B5 File Offset: 0x0006D2B5
		public ShadowRoutingConfiguration(ShadowMessagePreference shadowPreference, int remoteShadowCount, int localShadowCount)
		{
			this.shadowMessagePreference = shadowPreference;
			this.remoteShadowCount = remoteShadowCount;
			this.localShadowCount = localShadowCount;
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0006F0D2 File Offset: 0x0006D2D2
		public ShadowMessagePreference ShadowMessagePreference
		{
			get
			{
				return this.shadowMessagePreference;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0006F0DA File Offset: 0x0006D2DA
		public int RemoteShadowCount
		{
			get
			{
				return this.remoteShadowCount;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006F0E2 File Offset: 0x0006D2E2
		public int LocalShadowCount
		{
			get
			{
				return this.localShadowCount;
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private readonly ShadowMessagePreference shadowMessagePreference;

		// Token: 0x04000CC9 RID: 3273
		private readonly int remoteShadowCount;

		// Token: 0x04000CCA RID: 3274
		private readonly int localShadowCount;
	}
}
