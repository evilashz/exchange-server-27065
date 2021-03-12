using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200044B RID: 1099
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ComputedElementCollection<TSource1, TSource2, TResult> : ReadOnlyDelegatingCollection<TResult>
	{
		// Token: 0x06003104 RID: 12548 RVA: 0x000C8F7A File Offset: 0x000C717A
		public ComputedElementCollection(Func<TSource1, TSource2, TResult> computeElementDelegate, IEnumerable<TSource1> source1Enumerable, IEnumerable<TSource2> source2Enumerable, int elementCount)
		{
			this.elementCount = elementCount;
			this.source1Enumerable = source1Enumerable;
			this.source2Enumerable = source2Enumerable;
			this.computeElementDelegate = computeElementDelegate;
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000C8F9F File Offset: 0x000C719F
		public override int Count
		{
			get
			{
				return this.elementCount;
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000C8FA7 File Offset: 0x000C71A7
		public override IEnumerator<TResult> GetEnumerator()
		{
			return new ComputedElementCollection<TSource1, TSource2, TResult>.Enumerator<TSource1, TSource2, TResult>(this.computeElementDelegate, this.source1Enumerable, this.source2Enumerable);
		}

		// Token: 0x04001A95 RID: 6805
		private readonly int elementCount;

		// Token: 0x04001A96 RID: 6806
		private readonly IEnumerable<TSource1> source1Enumerable;

		// Token: 0x04001A97 RID: 6807
		private readonly IEnumerable<TSource2> source2Enumerable;

		// Token: 0x04001A98 RID: 6808
		private readonly Func<TSource1, TSource2, TResult> computeElementDelegate;

		// Token: 0x0200044C RID: 1100
		private struct Enumerator<EnumTSource1, EnumTSource2, EnumTResult> : IEnumerator<EnumTResult>, IDisposable, IEnumerator
		{
			// Token: 0x06003107 RID: 12551 RVA: 0x000C8FC5 File Offset: 0x000C71C5
			internal Enumerator(Func<EnumTSource1, EnumTSource2, EnumTResult> computeElementDelegate, IEnumerable<EnumTSource1> source1Enumerable, IEnumerable<EnumTSource2> source2Enumerable)
			{
				this.source1Enumerator = source1Enumerable.GetEnumerator();
				this.source2Enumerator = source2Enumerable.GetEnumerator();
				this.computeElementDelegate = computeElementDelegate;
			}

			// Token: 0x17000F5F RID: 3935
			// (get) Token: 0x06003108 RID: 12552 RVA: 0x000C8FE6 File Offset: 0x000C71E6
			public EnumTResult Current
			{
				get
				{
					return this.computeElementDelegate(this.source1Enumerator.Current, this.source2Enumerator.Current);
				}
			}

			// Token: 0x06003109 RID: 12553 RVA: 0x000C9009 File Offset: 0x000C7209
			public void Dispose()
			{
				Util.DisposeIfPresent(this.source1Enumerator);
				Util.DisposeIfPresent(this.source2Enumerator);
			}

			// Token: 0x17000F60 RID: 3936
			// (get) Token: 0x0600310A RID: 12554 RVA: 0x000C9021 File Offset: 0x000C7221
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600310B RID: 12555 RVA: 0x000C9030 File Offset: 0x000C7230
			public bool MoveNext()
			{
				bool result = this.source1Enumerator.MoveNext();
				this.source2Enumerator.MoveNext();
				return result;
			}

			// Token: 0x0600310C RID: 12556 RVA: 0x000C9056 File Offset: 0x000C7256
			public void Reset()
			{
				this.source1Enumerator.Reset();
				this.source2Enumerator.Reset();
			}

			// Token: 0x04001A99 RID: 6809
			private readonly IEnumerator<EnumTSource1> source1Enumerator;

			// Token: 0x04001A9A RID: 6810
			private readonly IEnumerator<EnumTSource2> source2Enumerator;

			// Token: 0x04001A9B RID: 6811
			private readonly Func<EnumTSource1, EnumTSource2, EnumTResult> computeElementDelegate;
		}
	}
}
