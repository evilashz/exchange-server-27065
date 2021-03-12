using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B9 RID: 441
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregationReValidator
	{
		// Token: 0x060017F0 RID: 6128
		bool IsTypeReValidationRequired();

		// Token: 0x060017F1 RID: 6129
		IEnumerable<KeyValuePair<string, SerializableDataBase>> RevalidatedTypes();
	}
}
