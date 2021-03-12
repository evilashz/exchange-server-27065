using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPropertyFilter
	{
		// Token: 0x06000629 RID: 1577
		bool IncludeProperty(PropertyTag propertyTag);
	}
}
