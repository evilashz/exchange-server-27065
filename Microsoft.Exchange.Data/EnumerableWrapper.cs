using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200021E RID: 542
	internal sealed class EnumerableWrapper<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x00039D86 File Offset: 0x00037F86
		private EnumerableWrapper(IList<IEnumerable<T>> enumerables, IList<IEnumerableFilter<T>> filters)
		{
			if (enumerables != null)
			{
				this.enumerables = new List<IEnumerable<T>>(enumerables);
			}
			if (filters != null)
			{
				this.filters = new List<IEnumerableFilter<T>>(filters);
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00039DAC File Offset: 0x00037FAC
		private static IList<TData> GetArrayIfNotNull<TData>(TData obj)
		{
			if (obj == null)
			{
				return null;
			}
			return new TData[]
			{
				obj
			};
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00039DD3 File Offset: 0x00037FD3
		public static EnumerableWrapper<T> GetWrapper(IEnumerable<T> enumerable)
		{
			return EnumerableWrapper<T>.GetWrapper(EnumerableWrapper<T>.GetArrayIfNotNull<IEnumerable<T>>(enumerable), null);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00039DE1 File Offset: 0x00037FE1
		public static EnumerableWrapper<T> GetWrapper(IEnumerable<T> enumerable, IEnumerableFilter<T> filter)
		{
			return EnumerableWrapper<T>.GetWrapper(EnumerableWrapper<T>.GetArrayIfNotNull<IEnumerable<T>>(enumerable), EnumerableWrapper<T>.GetArrayIfNotNull<IEnumerableFilter<T>>(filter));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00039DF4 File Offset: 0x00037FF4
		public static EnumerableWrapper<T> GetWrapper(IList<IEnumerable<T>> enumerables)
		{
			return EnumerableWrapper<T>.GetWrapper(enumerables, null);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00039DFD File Offset: 0x00037FFD
		public static EnumerableWrapper<T> GetWrapper(IList<IEnumerable<T>> enumerables, IEnumerableFilter<T> filter)
		{
			return EnumerableWrapper<T>.GetWrapper(enumerables, EnumerableWrapper<T>.GetArrayIfNotNull<IEnumerableFilter<T>>(filter));
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00039E0B File Offset: 0x0003800B
		public static EnumerableWrapper<T> GetWrapper(IEnumerable<T> enumerable, IList<IEnumerableFilter<T>> filters)
		{
			return EnumerableWrapper<T>.GetWrapper(EnumerableWrapper<T>.GetArrayIfNotNull<IEnumerable<T>>(enumerable), filters);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00039E1C File Offset: 0x0003801C
		public static EnumerableWrapper<T> GetWrapper(IList<IEnumerable<T>> enumerables, IList<IEnumerableFilter<T>> filters)
		{
			if (enumerables == null || enumerables.Count == 0)
			{
				return EnumerableWrapper<T>.Empty;
			}
			if (enumerables.Count == 1)
			{
				EnumerableWrapper<T> enumerableWrapper = enumerables[0] as EnumerableWrapper<T>;
				if (enumerableWrapper != null)
				{
					if (filters == null || !enumerableWrapper.HasElements())
					{
						return enumerableWrapper;
					}
					if (enumerableWrapper.filters != null)
					{
						HashSet<IEnumerableFilter<T>> hashSet = new HashSet<IEnumerableFilter<T>>(enumerableWrapper.filters);
						HashSet<IEnumerableFilter<T>> other = new HashSet<IEnumerableFilter<T>>(filters);
						if (hashSet.IsSupersetOf(other))
						{
							return enumerableWrapper;
						}
					}
				}
				else
				{
					ICollection<T> collection = enumerables[0] as ICollection<T>;
					if (collection != null && collection.Count == 0)
					{
						return EnumerableWrapper<T>.Empty;
					}
				}
			}
			return new EnumerableWrapper<T>(enumerables, filters);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00039EAB File Offset: 0x000380AB
		public IEnumerator<T> GetEnumerator()
		{
			return this.ResolveEnumerator();
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00039EB3 File Offset: 0x000380B3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ResolveEnumerator();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00039EBB File Offset: 0x000380BB
		private IEnumerator<T> ResolveEnumerator()
		{
			if (this.enumerator == null && this.consumedEnumerator && !this.hasElements)
			{
				return EnumerableWrapper<T>.EmptyList.GetEnumerator();
			}
			return this.Enumerator;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00039EEC File Offset: 0x000380EC
		private EnumerableWrapper<T>.EnumeratorWrapper<T> Enumerator
		{
			get
			{
				if (this.enumerator == null)
				{
					if (this.enumerables != null)
					{
						this.enumerator = new EnumerableWrapper<T>.EnumeratorWrapper<T>(this.enumerables.GetEnumerator(), this.filters);
					}
					else
					{
						this.enumerator = new EnumerableWrapper<T>.EnumeratorWrapper<T>(null, null);
					}
				}
				return this.enumerator;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00039F3F File Offset: 0x0003813F
		internal bool IsEnumerableAlreadyConsumed()
		{
			return this.consumedEnumerator;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00039F47 File Offset: 0x00038147
		public bool HasElements()
		{
			this.InitHasElements();
			return this.hasElements;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00039F55 File Offset: 0x00038155
		public bool HasUnfilteredElements()
		{
			this.InitHasElements();
			return this.hasUnfilteredElements;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00039F64 File Offset: 0x00038164
		private void InitHasElements()
		{
			if (!this.consumedEnumerator)
			{
				try
				{
					EnumerableWrapper<T>.EnumeratorWrapper<T> enumeratorWrapper = this.Enumerator;
					this.hasElements = enumeratorWrapper.HasElements();
					this.hasUnfilteredElements = enumeratorWrapper.HasUnfilteredElements();
				}
				finally
				{
					if (!this.hasElements)
					{
						this.DisposeEnumerator();
					}
					this.consumedEnumerator = true;
				}
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00039FC0 File Offset: 0x000381C0
		private void DisposeEnumerator()
		{
			if (this.enumerator != null)
			{
				this.enumerator.Dispose();
			}
			this.enumerator = null;
		}

		// Token: 0x04000B2E RID: 2862
		private List<IEnumerable<T>> enumerables;

		// Token: 0x04000B2F RID: 2863
		private List<IEnumerableFilter<T>> filters;

		// Token: 0x04000B30 RID: 2864
		private EnumerableWrapper<T>.EnumeratorWrapper<T> enumerator;

		// Token: 0x04000B31 RID: 2865
		private bool consumedEnumerator;

		// Token: 0x04000B32 RID: 2866
		private bool hasElements;

		// Token: 0x04000B33 RID: 2867
		private bool hasUnfilteredElements;

		// Token: 0x04000B34 RID: 2868
		private static List<T> EmptyList = new List<T>();

		// Token: 0x04000B35 RID: 2869
		internal static EnumerableWrapper<T> Empty = new EnumerableWrapper<T>(null, null);

		// Token: 0x0200021F RID: 543
		private sealed class EnumeratorWrapper<TData> : IEnumerator<!1>, IEnumerator, IDisposeTrackable, IDisposable
		{
			// Token: 0x060012FF RID: 4863 RVA: 0x00039FF4 File Offset: 0x000381F4
			internal EnumeratorWrapper(IEnumerator<IEnumerable<TData>> elements, IList<IEnumerableFilter<TData>> filters)
			{
				this.enumerables = elements;
				if (filters != null && filters.Count > 0)
				{
					this.filterEnabled = true;
					if (filters.Count == 1)
					{
						this.filter = filters[0];
					}
					else
					{
						this.filters = filters;
					}
				}
				this.disposeTracker = this.GetDisposeTracker();
			}

			// Token: 0x06001300 RID: 4864 RVA: 0x0003A04C File Offset: 0x0003824C
			public DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<EnumerableWrapper<T>.EnumeratorWrapper<TData>>(this);
			}

			// Token: 0x06001301 RID: 4865 RVA: 0x0003A054 File Offset: 0x00038254
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06001302 RID: 4866 RVA: 0x0003A06C File Offset: 0x0003826C
			private bool MoveNextElement()
			{
				if (this.firstRead)
				{
					this.firstRead = false;
					return this.hasElements.Value;
				}
				if (this.enumerator != null)
				{
					if (this.enumerator.MoveNext())
					{
						this.hasElements = new bool?(true);
						return true;
					}
					this.DisposeCurrentEnumerator();
				}
				if (this.enumerables != null)
				{
					while (this.enumerables.MoveNext())
					{
						EnumerableWrapper<T> enumerableWrapper = this.enumerables.Current as EnumerableWrapper<T>;
						if (enumerableWrapper != null)
						{
							this.hasUnfilteredElements |= enumerableWrapper.HasUnfilteredElements();
						}
						this.enumerator = this.enumerables.Current.GetEnumerator();
						if (this.enumerator.MoveNext())
						{
							this.hasElements = new bool?(true);
							return true;
						}
						this.DisposeCurrentEnumerator();
					}
				}
				if (this.enumerator != null)
				{
					this.DisposeCurrentEnumerator();
				}
				return false;
			}

			// Token: 0x06001303 RID: 4867 RVA: 0x0003A141 File Offset: 0x00038341
			public bool MoveNext()
			{
				while (this.MoveNextElement())
				{
					this.hasUnfilteredElements = true;
					if (!this.filterEnabled || this.AcceptCurrent(this.Current))
					{
						this.hasElements = new bool?(true);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001304 RID: 4868 RVA: 0x0003A17C File Offset: 0x0003837C
			private bool AcceptCurrent(TData element)
			{
				if (this.filter != null)
				{
					return this.filter.AcceptElement(element);
				}
				foreach (IEnumerableFilter<TData> enumerableFilter in this.filters)
				{
					if (!enumerableFilter.AcceptElement(element))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001305 RID: 4869 RVA: 0x0003A1E8 File Offset: 0x000383E8
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001306 RID: 4870 RVA: 0x0003A1EF File Offset: 0x000383EF
			public TData Current
			{
				get
				{
					return this.GetCurrent();
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001307 RID: 4871 RVA: 0x0003A1F7 File Offset: 0x000383F7
			object IEnumerator.Current
			{
				get
				{
					return this.GetCurrent();
				}
			}

			// Token: 0x06001308 RID: 4872 RVA: 0x0003A204 File Offset: 0x00038404
			private TData GetCurrent()
			{
				if (this.enumerator != null)
				{
					return this.enumerator.Current;
				}
				throw new InvalidOperationException(DataStrings.InvalidOperationCurrentProperty);
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x0003A229 File Offset: 0x00038429
			public bool HasElements()
			{
				this.InitHasMethods();
				return this.hasElements.Value;
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x0003A23C File Offset: 0x0003843C
			public bool HasUnfilteredElements()
			{
				this.InitHasMethods();
				return this.hasUnfilteredElements;
			}

			// Token: 0x0600130B RID: 4875 RVA: 0x0003A24A File Offset: 0x0003844A
			private void InitHasMethods()
			{
				if (this.hasElements == null)
				{
					this.hasElements = new bool?(this.MoveNext());
					this.firstRead = true;
				}
			}

			// Token: 0x0600130C RID: 4876 RVA: 0x0003A271 File Offset: 0x00038471
			private void DisposeCurrentEnumerator()
			{
				this.enumerator.Dispose();
				this.enumerator = null;
			}

			// Token: 0x0600130D RID: 4877 RVA: 0x0003A285 File Offset: 0x00038485
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600130E RID: 4878 RVA: 0x0003A294 File Offset: 0x00038494
			private void Dispose(bool disposing)
			{
				if (!this.disposed && disposing)
				{
					if (this.enumerator != null)
					{
						this.enumerator.Dispose();
					}
					while (this.enumerables != null && this.enumerables.MoveNext())
					{
						IEnumerator<TData> enumerator = this.enumerables.Current.GetEnumerator();
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
				this.disposed = true;
			}

			// Token: 0x04000B36 RID: 2870
			private IEnumerator<TData> enumerator;

			// Token: 0x04000B37 RID: 2871
			private IEnumerator<IEnumerable<TData>> enumerables;

			// Token: 0x04000B38 RID: 2872
			private bool? hasElements;

			// Token: 0x04000B39 RID: 2873
			private bool hasUnfilteredElements;

			// Token: 0x04000B3A RID: 2874
			private bool firstRead;

			// Token: 0x04000B3B RID: 2875
			private bool filterEnabled;

			// Token: 0x04000B3C RID: 2876
			private IList<IEnumerableFilter<TData>> filters;

			// Token: 0x04000B3D RID: 2877
			private IEnumerableFilter<TData> filter;

			// Token: 0x04000B3E RID: 2878
			private DisposeTracker disposeTracker;

			// Token: 0x04000B3F RID: 2879
			private bool disposed;
		}
	}
}
