using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x02000041 RID: 65
	internal class FifoCache<TObject> where TObject : class
	{
		// Token: 0x0600019D RID: 413 RVA: 0x000056A8 File Offset: 0x000038A8
		public FifoCache(int maximumSize = 10000, IEqualityComparer<TObject> comparer = null) : this(maximumSize, null, null, comparer)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000056B4 File Offset: 0x000038B4
		protected FifoCache(int maximumSize = 10000, HashSet<TObject> hashSet = null, Queue<TObject> creationOrder = null, IEqualityComparer<TObject> comparer = null)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("maximumSize", maximumSize, 1, int.MaxValue);
			this.maxNumberOfElements = maximumSize;
			this.existingInstances = (hashSet ?? new HashSet<TObject>(comparer));
			this.creationOrder = (creationOrder ?? new Queue<TObject>(maximumSize));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005704 File Offset: 0x00003904
		public bool Add(TObject property)
		{
			bool result = false;
			if (this.creationOrder.Count >= this.maxNumberOfElements && !this.existingInstances.Contains(property))
			{
				TObject item = this.creationOrder.Dequeue();
				this.existingInstances.Remove(item);
				result = true;
			}
			if (this.existingInstances.Add(property))
			{
				this.creationOrder.Enqueue(property);
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000576A File Offset: 0x0000396A
		public bool Contains(TObject property)
		{
			return this.existingInstances.Contains(property);
		}

		// Token: 0x0400008E RID: 142
		public const int DefaultMaximumSize = 10000;

		// Token: 0x0400008F RID: 143
		private readonly HashSet<TObject> existingInstances;

		// Token: 0x04000090 RID: 144
		private readonly Queue<TObject> creationOrder;

		// Token: 0x04000091 RID: 145
		private readonly int maxNumberOfElements;
	}
}
