using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x0200002D RID: 45
	internal class ServerId
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00007DAC File Offset: 0x00005FAC
		public ServerId(ADObjectId id)
		{
			this.id = id;
			this.legacyDN = null;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007DC2 File Offset: 0x00005FC2
		public ServerId(string legacyDN)
		{
			this.id = null;
			this.legacyDN = legacyDN;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public string Key
		{
			get
			{
				if (this.id == null)
				{
					return this.legacyDN;
				}
				return this.id.ToString();
			}
		}

		// Token: 0x04000174 RID: 372
		private ADObjectId id;

		// Token: 0x04000175 RID: 373
		private string legacyDN;
	}
}
