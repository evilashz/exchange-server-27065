using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B8 RID: 440
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregatedUserConfigurationWriter
	{
		// Token: 0x060017EE RID: 6126
		void Prepare();

		// Token: 0x060017EF RID: 6127
		void Commit();
	}
}
