using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001EF RID: 495
	internal sealed class ConditionalEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001116 RID: 4374 RVA: 0x00033E35 File Offset: 0x00032035
		public ConditionalEnumerator(IEnumerable<T> conditionalEnumerable, IEnumerable<T> secondEnumerable)
		{
			this.conditionalEnumerable = conditionalEnumerable;
			this.secondEnumerable = secondEnumerable;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00033E57 File Offset: 0x00032057
		public T Current
		{
			get
			{
				if (this.mainEnum == null)
				{
					throw new InvalidOperationException("Cannot call Current without calling MoveNext first.");
				}
				return this.mainEnum.Current;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00033E77 File Offset: 0x00032077
		object IEnumerator.Current
		{
			get
			{
				if (this.mainEnum == null)
				{
					throw new InvalidOperationException("Cannot call Current without calling MoveNext first.");
				}
				return this.mainEnum.Current;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00033E9C File Offset: 0x0003209C
		public bool MoveNext()
		{
			if (this.mainEnum == null)
			{
				EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(this.conditionalEnumerable);
				if (wrapper.HasElements())
				{
					this.mainEnum = wrapper.GetEnumerator();
					this.secondEnumerable = null;
				}
				else
				{
					this.mainEnum = this.secondEnumerable.GetEnumerator();
					this.conditionalEnumerable = null;
				}
			}
			return this.mainEnum.MoveNext();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00033EFD File Offset: 0x000320FD
		public void Reset()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00033F09 File Offset: 0x00032109
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00033F18 File Offset: 0x00032118
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				if (this.mainEnum != null)
				{
					this.mainEnum.Dispose();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			this.disposed = true;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00033F52 File Offset: 0x00032152
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConditionalEnumerator<T>>(this);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00033F5A File Offset: 0x0003215A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x04000A8D RID: 2701
		private IEnumerable<T> conditionalEnumerable;

		// Token: 0x04000A8E RID: 2702
		private IEnumerable<T> secondEnumerable;

		// Token: 0x04000A8F RID: 2703
		private IEnumerator<T> mainEnum;

		// Token: 0x04000A90 RID: 2704
		private bool disposed;

		// Token: 0x04000A91 RID: 2705
		private DisposeTracker disposeTracker;
	}
}
