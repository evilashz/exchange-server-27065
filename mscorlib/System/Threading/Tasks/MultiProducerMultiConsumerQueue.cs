using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x02000556 RID: 1366
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06004147 RID: 16711 RVA: 0x000F28AB File Offset: 0x000F0AAB
		void IProducerConsumerQueue<!0>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000F28B4 File Offset: 0x000F0AB4
		bool IProducerConsumerQueue<!0>.TryDequeue(out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000F28BD File Offset: 0x000F0ABD
		bool IProducerConsumerQueue<!0>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x000F28C5 File Offset: 0x000F0AC5
		int IProducerConsumerQueue<!0>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000F28CD File Offset: 0x000F0ACD
		int IProducerConsumerQueue<!0>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}
	}
}
