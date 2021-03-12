using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000093 RID: 147
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class QueuedDictionary<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x00015C7C File Offset: 0x00013E7C
		public QueuedDictionary()
		{
			this.lookupAssistor = new Dictionary<T, object>();
			this.internalQueue = new Queue<T>();
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00015CA8 File Offset: 0x00013EA8
		public int Count
		{
			get
			{
				int count;
				lock (this.syncObject)
				{
					count = this.lookupAssistor.Count;
				}
				return count;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00015CF0 File Offset: 0x00013EF0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00015CF8 File Offset: 0x00013EF8
		public IEnumerator<T> GetEnumerator()
		{
			IEnumerator<T> result;
			lock (this.syncObject)
			{
				result = this.internalQueue.GetEnumerator();
			}
			return result;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00015D44 File Offset: 0x00013F44
		public void Clear()
		{
			lock (this.syncObject)
			{
				this.lookupAssistor.Clear();
				this.internalQueue.Clear();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00015D94 File Offset: 0x00013F94
		public bool Contains(T entry)
		{
			bool result;
			lock (this.syncObject)
			{
				result = this.lookupAssistor.ContainsKey(entry);
			}
			return result;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00015DDC File Offset: 0x00013FDC
		public void Enqueue(T entry)
		{
			lock (this.syncObject)
			{
				this.lookupAssistor.Add(entry, null);
				this.internalQueue.Enqueue(entry);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00015E30 File Offset: 0x00014030
		public T Dequeue()
		{
			T result;
			lock (this.syncObject)
			{
				T t = this.internalQueue.Dequeue();
				this.lookupAssistor.Remove(t);
				result = t;
			}
			return result;
		}

		// Token: 0x040001EF RID: 495
		private readonly object syncObject = new object();

		// Token: 0x040001F0 RID: 496
		private readonly Dictionary<T, object> lookupAssistor;

		// Token: 0x040001F1 RID: 497
		private readonly Queue<T> internalQueue;
	}
}
