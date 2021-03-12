using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IXSOMailbox : IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x0600002B RID: 43
		void Save();
	}
}
