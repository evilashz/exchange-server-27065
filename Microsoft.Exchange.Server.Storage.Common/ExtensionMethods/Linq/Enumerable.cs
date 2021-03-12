using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq
{
	// Token: 0x0200004B RID: 75
	public static class Enumerable
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return Enumerable.Empty<TResult>();
		}
	}
}
