using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000036 RID: 54
	public sealed class RuleResult : Result<bool>
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00007986 File Offset: 0x00005B86
		public RuleResult(bool value) : base(value)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000798F File Offset: 0x00005B8F
		public RuleResult(Exception exception) : base(exception)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007998 File Offset: 0x00005B98
		internal RuleResult(RuleResult toCopy, AnalysisMember source, Result parent, ExDateTime startTime, ExDateTime stopTime) : base(toCopy, source, parent, startTime, stopTime)
		{
			this.Severity = toCopy.Severity;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000079B3 File Offset: 0x00005BB3
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000079BB File Offset: 0x00005BBB
		public Severity? Severity { get; set; }
	}
}
