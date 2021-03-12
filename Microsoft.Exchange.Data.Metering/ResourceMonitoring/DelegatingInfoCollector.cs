using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200002A RID: 42
	internal class DelegatingInfoCollector : IExecutionInfo
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000810D File Offset: 0x0000630D
		public DelegatingInfoCollector(IEnumerable<IExecutionInfo> executionInfos)
		{
			ArgumentValidator.ThrowIfNull("executionInfos", executionInfos);
			this.executionInfos = executionInfos;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00008127 File Offset: 0x00006327
		public IEnumerable<IExecutionInfo> ExecutionInfos
		{
			get
			{
				return this.executionInfos;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008130 File Offset: 0x00006330
		public void OnStart()
		{
			foreach (IExecutionInfo executionInfo in this.executionInfos)
			{
				executionInfo.OnStart();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000817C File Offset: 0x0000637C
		public void OnException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			foreach (IExecutionInfo executionInfo in this.executionInfos)
			{
				executionInfo.OnException(ex);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000081D4 File Offset: 0x000063D4
		public void OnFinish()
		{
			foreach (IExecutionInfo executionInfo in this.executionInfos)
			{
				executionInfo.OnFinish();
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly IEnumerable<IExecutionInfo> executionInfos;
	}
}
