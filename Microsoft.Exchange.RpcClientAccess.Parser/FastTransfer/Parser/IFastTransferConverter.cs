using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IFastTransferConverter<TContext> where TContext : BaseObject
	{
		// Token: 0x06000661 RID: 1633
		PropertyValue Convert(TContext context, PropertyValue propertyValue);
	}
}
