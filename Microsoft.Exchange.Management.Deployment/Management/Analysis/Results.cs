using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000045 RID: 69
	internal class Results<T> : IEnumerable<Result<T>>, IEnumerable
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00007972 File Offset: 0x00005B72
		public Results(AnalysisMember source, IEnumerable<Result<T>> results)
		{
			this.Source = source;
			this.results = results;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007988 File Offset: 0x00005B88
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007990 File Offset: 0x00005B90
		public AnalysisMember Source { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000079A4 File Offset: 0x00005BA4
		public int Count
		{
			get
			{
				return (from x in this.results
				where !x.HasException
				select x).Count<Result<T>>();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000079D4 File Offset: 0x00005BD4
		public Result<T> Result
		{
			get
			{
				if (this.results.Skip(1).Any<Result<T>>())
				{
					throw new MultipleResultsException(this.Source);
				}
				Result<T> result = this.results.FirstOrDefault<Result<T>>();
				if (result == null)
				{
					throw new EmptyResultsException(this.Source);
				}
				return result;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007A1C File Offset: 0x00005C1C
		public T Value
		{
			get
			{
				return this.Result.Value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007A2C File Offset: 0x00005C2C
		public T ValueOrDefault
		{
			get
			{
				T result;
				try
				{
					result = this.Result.ValueOrDefault;
				}
				catch
				{
					result = default(T);
				}
				return result;
			}
		}

		// Token: 0x17000077 RID: 119
		public Result<T> this[int index]
		{
			get
			{
				return (from x in this.results
				where !x.HasException
				select x).Skip(index).First<Result<T>>();
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public IEnumerator<Result<T>> GetEnumerator()
		{
			return this.results.GetEnumerator();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007AB5 File Offset: 0x00005CB5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.results.GetEnumerator();
		}

		// Token: 0x0400012E RID: 302
		private IEnumerable<Result<T>> results;
	}
}
