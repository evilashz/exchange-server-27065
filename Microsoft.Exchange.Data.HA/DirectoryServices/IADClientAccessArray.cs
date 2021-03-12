using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADClientAccessArray : IADObjectCommon
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000AA RID: 170
		string ExchangeLegacyDN { get; }
	}
}
