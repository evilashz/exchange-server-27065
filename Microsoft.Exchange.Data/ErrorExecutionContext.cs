using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000220 RID: 544
	internal class ErrorExecutionContext : IErrorExecutionContext
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x0003A30A File Offset: 0x0003850A
		public ErrorExecutionContext(string executionContext)
		{
			this.executionContext = executionContext;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0003A319 File Offset: 0x00038519
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0003A321 File Offset: 0x00038521
		public string ExecutionHost
		{
			get
			{
				return this.executionContext;
			}
			set
			{
				this.executionContext = value;
			}
		}

		// Token: 0x04000B40 RID: 2880
		private string executionContext;
	}
}
