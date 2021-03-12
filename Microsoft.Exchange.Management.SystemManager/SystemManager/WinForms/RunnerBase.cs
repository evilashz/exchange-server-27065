using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000057 RID: 87
	public abstract class RunnerBase
	{
		// Token: 0x0600038E RID: 910
		public abstract void Run(object interactionHandler, DataRow row, DataObjectStore store);

		// Token: 0x0600038F RID: 911
		public abstract void Cancel();

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000D03A File Offset: 0x0000B23A
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000D042 File Offset: 0x0000B242
		[DefaultValue(null)]
		public IRunnable RunnableTester { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000D04B File Offset: 0x0000B24B
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000D053 File Offset: 0x0000B253
		[DDIValidLambdaExpression]
		[DefaultValue(null)]
		public string RunnableLambdaExpression { get; set; }

		// Token: 0x06000394 RID: 916 RVA: 0x0000D05C File Offset: 0x0000B25C
		public virtual bool IsRunnable(DataRow row, DataObjectStore store)
		{
			if (!string.IsNullOrEmpty(this.RunnableLambdaExpression))
			{
				return (bool)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.RunnableLambdaExpression), typeof(bool), row, null);
			}
			return this.RunnableTester == null || this.RunnableTester.IsRunnable(row);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D0AE File Offset: 0x0000B2AE
		public virtual void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000396 RID: 918 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		// (remove) Token: 0x06000397 RID: 919 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		internal event EventHandler<ProgressReportEventArgs> ProgressReport;

		// Token: 0x06000398 RID: 920 RVA: 0x0000D11D File Offset: 0x0000B31D
		internal void OnProgressReport(object sender, ProgressReportEventArgs e)
		{
			if (this.ProgressReport != null)
			{
				this.ProgressReport(sender, e);
			}
		}
	}
}
