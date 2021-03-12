using System;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003C RID: 60
	internal interface IOrganizationName
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600025A RID: 602
		string EscapedName { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600025B RID: 603
		string UnescapedName { get; }

		// Token: 0x0600025C RID: 604
		string ToString();
	}
}
