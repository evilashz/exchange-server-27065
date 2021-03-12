using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADSiteWrapper : ADObjectWrapperBase, IADSite, IADObjectCommon
	{
		// Token: 0x06000173 RID: 371 RVA: 0x000045CD File Offset: 0x000027CD
		private ADSiteWrapper(ADSite adSite) : base(adSite)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000045D6 File Offset: 0x000027D6
		public static ADSiteWrapper CreateWrapper(ADSite adSite)
		{
			if (adSite == null)
			{
				return null;
			}
			return new ADSiteWrapper(adSite);
		}
	}
}
