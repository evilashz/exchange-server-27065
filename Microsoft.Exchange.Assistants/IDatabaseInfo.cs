using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002B RID: 43
	internal interface IDatabaseInfo
	{
		// Token: 0x0600014A RID: 330
		IEnumerable<IMailboxInformation> GetMailboxTable(ClientType clientType, PropertyTagPropertyDefinition[] properties);

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014B RID: 331
		string DatabaseName { get; }
	}
}
