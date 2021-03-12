using System;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200019B RID: 411
	internal interface ISession
	{
		// Token: 0x06000815 RID: 2069
		bool TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty);

		// Token: 0x06000816 RID: 2070
		bool TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag);
	}
}
