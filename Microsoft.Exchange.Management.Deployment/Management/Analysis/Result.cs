using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000043 RID: 67
	internal abstract class Result : IResultAccessor
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000075E6 File Offset: 0x000057E6
		public Result() : this(null)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000075F0 File Offset: 0x000057F0
		public Result(Exception exception)
		{
			this.Source = null;
			this.Parent = null;
			this.Exception = exception;
			this.StartTime = default(ExDateTime);
			this.StopTime = default(ExDateTime);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007636 File Offset: 0x00005836
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000763E File Offset: 0x0000583E
		public AnalysisMember Source { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007647 File Offset: 0x00005847
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000764F File Offset: 0x0000584F
		public Result Parent { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007658 File Offset: 0x00005858
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00007660 File Offset: 0x00005860
		public Exception Exception { get; protected set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001CA RID: 458
		public abstract object ValueAsObject { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007669 File Offset: 0x00005869
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00007671 File Offset: 0x00005871
		public ExDateTime StartTime { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000767A File Offset: 0x0000587A
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007682 File Offset: 0x00005882
		public ExDateTime StopTime { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000768B File Offset: 0x0000588B
		public bool HasException
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000778C File Offset: 0x0000598C
		public IEnumerable<Result> AncestorsAndSelf()
		{
			for (Result current = this; current != null; current = current.Parent)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000077C4 File Offset: 0x000059C4
		public Result<T> AncestorOfType<T>(AnalysisMember<T> ancestor)
		{
			Result result = (from x in this.AncestorsAndSelf()
			where x.Source == ancestor
			select x).FirstOrDefault<Result>();
			if (ancestor != null)
			{
				return (Result<T>)result;
			}
			throw new AnalysisException(this.Source, Strings.ResultAncestorNotFound(ancestor.Name));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007838 File Offset: 0x00005A38
		public Results<T> DescendantsOfType<T>(AnalysisMember<T> analysisMemeber)
		{
			return new Results<T>(analysisMemeber, from x in analysisMemeber.Results
			where x.AncestorsAndSelf().Contains(this)
			select x);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007857 File Offset: 0x00005A57
		void IResultAccessor.SetSource(AnalysisMember source)
		{
			this.Source = source;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007860 File Offset: 0x00005A60
		void IResultAccessor.SetParent(Result parent)
		{
			this.Parent = parent;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007869 File Offset: 0x00005A69
		void IResultAccessor.SetStartTime(ExDateTime startTime)
		{
			this.StartTime = startTime;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007872 File Offset: 0x00005A72
		void IResultAccessor.SetStopTime(ExDateTime stopTime)
		{
			this.StopTime = stopTime;
		}
	}
}
