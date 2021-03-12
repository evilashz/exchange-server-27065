using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorSerializable
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025D RID: 605
		PropertyDefinition[] PropertyDefinitions { get; }

		// Token: 0x0600025E RID: 606
		bool ReadFromMessageItem(IAnchorStoreObject message);

		// Token: 0x0600025F RID: 607
		void WriteToMessageItem(IAnchorStoreObject message, bool loaded);
	}
}
