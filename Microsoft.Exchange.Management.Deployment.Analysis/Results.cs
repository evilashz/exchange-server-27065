using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000034 RID: 52
	public class Results<T> : IEnumerable<Result<!0>>, IEnumerable
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000077BF File Offset: 0x000059BF
		public Results(AnalysisMember source, IEnumerable<Result<T>> results)
		{
			this.Source = source;
			this.results = results;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000077D5 File Offset: 0x000059D5
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000077DD File Offset: 0x000059DD
		public AnalysisMember Source { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000077F1 File Offset: 0x000059F1
		public int Count
		{
			get
			{
				return this.results.Count((Result<T> x) => !x.HasException);
			}
		}

		// Token: 0x17000076 RID: 118
		public Result<T> this[int index]
		{
			get
			{
				return (from x in this.results
				where !x.HasException
				select x).Skip(index).First<Result<T>>();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000785B File Offset: 0x00005A5B
		public IEnumerator<Result<T>> GetEnumerator()
		{
			return this.results.GetEnumerator();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007868 File Offset: 0x00005A68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.results.GetEnumerator();
		}

		// Token: 0x04000081 RID: 129
		private IEnumerable<Result<T>> results;
	}
}
