using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000293 RID: 659
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ValidatingTaskResult
	{
		// Token: 0x060017CD RID: 6093 RVA: 0x0006483A File Offset: 0x00062A3A
		public ValidatingTaskResult()
		{
			this.conditionDescription = null;
			this.result = ValidatingTaskResult.ResultType.NotRun;
			this.failureDetails = null;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00064857 File Offset: 0x00062A57
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0006485F File Offset: 0x00062A5F
		public string ConditionDescription
		{
			get
			{
				return this.conditionDescription;
			}
			set
			{
				this.conditionDescription = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00064868 File Offset: 0x00062A68
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00064870 File Offset: 0x00062A70
		public ValidatingTaskResult.ResultType Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00064879 File Offset: 0x00062A79
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x00064881 File Offset: 0x00062A81
		public Exception FailureDetails
		{
			get
			{
				return this.failureDetails;
			}
			set
			{
				this.failureDetails = value;
			}
		}

		// Token: 0x04000A0A RID: 2570
		private string conditionDescription;

		// Token: 0x04000A0B RID: 2571
		private ValidatingTaskResult.ResultType result;

		// Token: 0x04000A0C RID: 2572
		private Exception failureDetails;

		// Token: 0x02000294 RID: 660
		public enum ResultType
		{
			// Token: 0x04000A0E RID: 2574
			Passed,
			// Token: 0x04000A0F RID: 2575
			Failed,
			// Token: 0x04000A10 RID: 2576
			NotRun
		}
	}
}
