using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MapiFxProxyPermanentException : MapiPermanentException
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000AE47 File Offset: 0x00009047
		protected MapiFxProxyPermanentException(Exception inner) : base("MapiFxProxyPermanentException", inner.Message, inner)
		{
		}
	}
}
