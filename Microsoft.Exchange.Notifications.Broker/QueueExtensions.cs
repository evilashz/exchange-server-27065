using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002D RID: 45
	internal static class QueueExtensions
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00009F48 File Offset: 0x00008148
		public static List<T> Dequeue<T>(this Queue<T> q, int numItemsRequested)
		{
			int num = Math.Min(numItemsRequested, q.Count);
			List<T> list = new List<T>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(q.Dequeue());
			}
			return list;
		}
	}
}
