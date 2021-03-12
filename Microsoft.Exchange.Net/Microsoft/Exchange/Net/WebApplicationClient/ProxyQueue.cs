using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B24 RID: 2852
	internal class ProxyQueue<T>
	{
		// Token: 0x06003D8F RID: 15759 RVA: 0x000A04C1 File Offset: 0x0009E6C1
		public ProxyQueue(IEnumerable<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.items = items.ToArray<T>();
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000A04E3 File Offset: 0x0009E6E3
		public IEnumerator<T> GetEnumerator()
		{
			return new ProxyQueue<T>.Enumerator(this);
		}

		// Token: 0x0400359C RID: 13724
		private T[] items;

		// Token: 0x0400359D RID: 13725
		private int head;

		// Token: 0x02000B25 RID: 2853
		private class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003D91 RID: 15761 RVA: 0x000A04EB File Offset: 0x0009E6EB
			public Enumerator(ProxyQueue<T> queue)
			{
				this.queue = queue;
				this.Reset();
			}

			// Token: 0x17000F37 RID: 3895
			// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000A0500 File Offset: 0x0009E700
			public T Current
			{
				get
				{
					if (this.current < 0)
					{
						throw new InvalidOperationException();
					}
					return this.queue.items[this.current];
				}
			}

			// Token: 0x06003D93 RID: 15763 RVA: 0x000A0527 File Offset: 0x0009E727
			public void Reset()
			{
				this.current = -1;
				this.itemsEnumerated = 0;
			}

			// Token: 0x06003D94 RID: 15764 RVA: 0x000A0538 File Offset: 0x0009E738
			public bool MoveNext()
			{
				this.itemsEnumerated++;
				lock (this.queue)
				{
					if (this.itemsEnumerated == 1)
					{
						this.current = this.queue.head;
					}
					else if (this.current != this.queue.head)
					{
						this.current = this.queue.head;
					}
					else
					{
						this.queue.head = ++this.queue.head % this.queue.items.Length;
						this.current = this.queue.head;
					}
				}
				if (this.itemsEnumerated > this.queue.items.Length)
				{
					this.current = -1;
					return false;
				}
				return true;
			}

			// Token: 0x17000F38 RID: 3896
			// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000A0624 File Offset: 0x0009E824
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003D96 RID: 15766 RVA: 0x000A0631 File Offset: 0x0009E831
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400359E RID: 13726
			private ProxyQueue<T> queue;

			// Token: 0x0400359F RID: 13727
			private int current;

			// Token: 0x040035A0 RID: 13728
			private int itemsEnumerated;
		}
	}
}
