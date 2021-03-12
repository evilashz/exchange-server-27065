using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200018E RID: 398
	internal abstract class Iterator<TSource> : IEnumerable<!0>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x000502D6 File Offset: 0x0004E4D6
		public Iterator()
		{
			this.threadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x000502EE File Offset: 0x0004E4EE
		public TSource Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06001891 RID: 6289
		protected abstract Iterator<TSource> Clone();

		// Token: 0x06001892 RID: 6290 RVA: 0x000502F6 File Offset: 0x0004E4F6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00050305 File Offset: 0x0004E505
		protected virtual void Dispose(bool disposing)
		{
			this.current = default(TSource);
			this.state = -1;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0005031C File Offset: 0x0004E51C
		public IEnumerator<TSource> GetEnumerator()
		{
			if (this.threadId == Thread.CurrentThread.ManagedThreadId && this.state == 0)
			{
				this.state = 1;
				return this;
			}
			Iterator<TSource> iterator = this.Clone();
			iterator.state = 1;
			return iterator;
		}

		// Token: 0x06001895 RID: 6293
		public abstract bool MoveNext();

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0005035B File Offset: 0x0004E55B
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00050368 File Offset: 0x0004E568
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00050370 File Offset: 0x0004E570
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000885 RID: 2181
		private int threadId;

		// Token: 0x04000886 RID: 2182
		internal int state;

		// Token: 0x04000887 RID: 2183
		internal TSource current;
	}
}
