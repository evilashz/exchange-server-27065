using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IUnifiedContactStoreConfiguration
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001E0 RID: 480
		int MaxImGroups { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E1 RID: 481
		int MaxImContacts { get; }
	}
}
