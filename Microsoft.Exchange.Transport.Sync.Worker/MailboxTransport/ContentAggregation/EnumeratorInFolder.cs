using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001FE RID: 510
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EnumeratorInFolder<TValue, UValue> : DisposeTrackableBase, IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x00038648 File Offset: 0x00036848
		internal EnumeratorInFolder(IEnumerator<UValue> underlyingEnumerator, object filter)
		{
			if (underlyingEnumerator == null)
			{
				throw new ArgumentNullException("underlyingEnumerator");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.filterHash = filter.GetHashCode();
			this.underlyingEnumerator = underlyingEnumerator;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0003867F File Offset: 0x0003687F
		public TValue Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00038687 File Offset: 0x00036887
		object IEnumerator.Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00038694 File Offset: 0x00036894
		public bool MoveNext()
		{
			while (this.underlyingEnumerator.MoveNext())
			{
				UValue item = this.underlyingEnumerator.Current;
				if (!this.SkipCurrent(item))
				{
					TValue currentFolder = this.GetCurrentFolder(item);
					if (this.filterHash == currentFolder.GetHashCode())
					{
						this.current = this.GetCurrent(item);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000386F0 File Offset: 0x000368F0
		public void Reset()
		{
			this.underlyingEnumerator.Reset();
			this.current = default(TValue);
		}

		// Token: 0x06001126 RID: 4390
		protected abstract bool SkipCurrent(UValue item);

		// Token: 0x06001127 RID: 4391
		protected abstract TValue GetCurrent(UValue item);

		// Token: 0x06001128 RID: 4392
		protected abstract TValue GetCurrentFolder(UValue item);

		// Token: 0x06001129 RID: 4393 RVA: 0x00038709 File Offset: 0x00036909
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.underlyingEnumerator != null)
				{
					this.underlyingEnumerator.Dispose();
					this.underlyingEnumerator = null;
				}
				this.current = default(TValue);
			}
		}

		// Token: 0x04000992 RID: 2450
		private readonly int filterHash;

		// Token: 0x04000993 RID: 2451
		private IEnumerator<UValue> underlyingEnumerator;

		// Token: 0x04000994 RID: 2452
		private TValue current;
	}
}
