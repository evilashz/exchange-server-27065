using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MapiFxProxyRetryableException : MapiRetryableException
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000AFDB File Offset: 0x000091DB
		protected MapiFxProxyRetryableException(Exception inner) : base("MapiFxProxyTransientException", inner.Message, inner)
		{
		}
	}
}
