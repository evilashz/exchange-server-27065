using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPeopleConnectApplicationConfigCache
	{
		// Token: 0x06000EFD RID: 3837
		bool TryGetValue(string key, out IPeopleConnectApplicationConfig value);

		// Token: 0x06000EFE RID: 3838
		void Add(string key, IPeopleConnectApplicationConfig value);
	}
}
