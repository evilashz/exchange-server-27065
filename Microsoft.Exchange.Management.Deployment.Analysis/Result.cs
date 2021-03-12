using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200002A RID: 42
	public abstract class Result
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006260 File Offset: 0x00004460
		protected Result(AnalysisMember source, Result parent, Exception exception, ExDateTime startTime, ExDateTime stopTime)
		{
			this.source = source;
			this.parent = parent;
			this.exception = exception;
			this.startTime = startTime;
			this.stopTime = stopTime;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000628D File Offset: 0x0000448D
		public AnalysisMember Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006295 File Offset: 0x00004495
		public Result Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000629D File Offset: 0x0000449D
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000062A5 File Offset: 0x000044A5
		public ExDateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000062AD File Offset: 0x000044AD
		public ExDateTime StopTime
		{
			get
			{
				return this.stopTime;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013A RID: 314
		public abstract object ValueAsObject { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000062B5 File Offset: 0x000044B5
		public bool HasException
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000063B4 File Offset: 0x000045B4
		public IEnumerable<Result> AncestorsAndSelf()
		{
			for (Result current = this; current != null; current = current.Parent)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000063EC File Offset: 0x000045EC
		public Result<T> AncestorOfType<T>(AnalysisMember<T> ancestor)
		{
			Result result = this.AncestorsAndSelf().FirstOrDefault((Result x) => x.Source == ancestor);
			if (ancestor != null)
			{
				return (Result<T>)result;
			}
			throw new AnalysisException(this.Source, Strings.ResultAncestorNotFound(ancestor.Name));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000645B File Offset: 0x0000465B
		public Results<T> DescendantsOfType<T>(AnalysisMember<T> analysisMemeber)
		{
			return new Results<T>(analysisMemeber, from x in analysisMemeber.Results
			where x.AncestorsAndSelf().Contains(this)
			select x);
		}

		// Token: 0x0400006C RID: 108
		private readonly AnalysisMember source;

		// Token: 0x0400006D RID: 109
		private readonly Result parent;

		// Token: 0x0400006E RID: 110
		private readonly Exception exception;

		// Token: 0x0400006F RID: 111
		private readonly ExDateTime startTime;

		// Token: 0x04000070 RID: 112
		private readonly ExDateTime stopTime;
	}
}
