using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADMiniClientAccessServerOrArrayWrapper : ADObjectWrapperBase, IADMiniClientAccessServerOrArray, IADObjectCommon
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000041B0 File Offset: 0x000023B0
		private ADMiniClientAccessServerOrArrayWrapper(MiniClientAccessServerOrArray caServerOrArray) : base(caServerOrArray)
		{
			this.Fqdn = (string)caServerOrArray[MiniClientAccessServerOrArraySchema.Fqdn];
			this.ExchangeLegacyDN = (string)caServerOrArray[MiniClientAccessServerOrArraySchema.ExchangeLegacyDN];
			this.ServerSite = (ADObjectId)caServerOrArray[MiniClientAccessServerOrArraySchema.Site];
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004206 File Offset: 0x00002406
		public static ADMiniClientAccessServerOrArrayWrapper CreateWrapper(MiniClientAccessServerOrArray caServerOrArray)
		{
			if (caServerOrArray == null)
			{
				return null;
			}
			return new ADMiniClientAccessServerOrArrayWrapper(caServerOrArray);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004213 File Offset: 0x00002413
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000421B File Offset: 0x0000241B
		public string Fqdn { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004224 File Offset: 0x00002424
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000422C File Offset: 0x0000242C
		public string ExchangeLegacyDN { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004235 File Offset: 0x00002435
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000423D File Offset: 0x0000243D
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x04000071 RID: 113
		public static readonly ADPropertyDefinition[] PropertiesNeededForCas = new ADPropertyDefinition[]
		{
			MiniClientAccessServerOrArraySchema.Fqdn,
			MiniClientAccessServerOrArraySchema.Site,
			MiniClientAccessServerOrArraySchema.ExchangeLegacyDN
		};
	}
}
