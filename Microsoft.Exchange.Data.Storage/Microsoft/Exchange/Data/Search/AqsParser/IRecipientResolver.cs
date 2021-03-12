using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000D01 RID: 3329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecipientResolver
	{
		// Token: 0x06007293 RID: 29331
		string[] Resolve(string identity);
	}
}
