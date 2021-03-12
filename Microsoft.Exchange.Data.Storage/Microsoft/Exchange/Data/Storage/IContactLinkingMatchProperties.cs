using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A8 RID: 1192
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IContactLinkingMatchProperties
	{
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x060034DE RID: 13534
		HashSet<string> EmailAddresses { get; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x060034DF RID: 13535
		string IMAddress { get; }
	}
}
