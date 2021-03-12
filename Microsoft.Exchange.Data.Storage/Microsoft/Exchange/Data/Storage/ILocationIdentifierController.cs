using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILocationIdentifierController
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022E RID: 558
		LocationIdentifierHelper LocationIdentifierHelperInstance { get; }
	}
}
