using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADMiniClientAccessServerOrArray : IADObjectCommon
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000AB RID: 171
		string Fqdn { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000AC RID: 172
		string ExchangeLegacyDN { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000AD RID: 173
		ADObjectId ServerSite { get; }
	}
}
