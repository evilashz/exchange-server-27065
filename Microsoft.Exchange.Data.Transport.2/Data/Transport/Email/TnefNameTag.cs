using System;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000DB RID: 219
	internal struct TnefNameTag
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0000CCE2 File Offset: 0x0000AEE2
		public TnefNameTag(TnefNameId id, TnefPropertyType type)
		{
			this.id = id;
			this.type = type;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		public TnefNameId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		public TnefPropertyType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000320 RID: 800
		private TnefNameId id;

		// Token: 0x04000321 RID: 801
		private TnefPropertyType type;
	}
}
