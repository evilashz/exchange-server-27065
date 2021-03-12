using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000027 RID: 39
	public static class RoutingExtensions
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000034A2 File Offset: 0x000016A2
		public static void AddIfNotNull<T>(this IList<T> list, T objectToAdd)
		{
			if (objectToAdd != null)
			{
				list.Add(objectToAdd);
			}
		}
	}
}
