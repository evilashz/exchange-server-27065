using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000C RID: 12
	internal class ExchangeFlowContext
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000493F File Offset: 0x00002B3F
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004947 File Offset: 0x00002B47
		public Guid InstanceGuid { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004950 File Offset: 0x00002B50
		public string InstanceName
		{
			[DebuggerStepThrough]
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004958 File Offset: 0x00002B58
		public SearchConfig Config
		{
			get
			{
				if (this.config == null)
				{
					this.config = new SearchConfig(this.InstanceGuid);
				}
				return this.config;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004979 File Offset: 0x00002B79
		public ExEventLog EventLog
		{
			get
			{
				if (ExchangeFlowContext.eventLog == null)
				{
					ExchangeFlowContext.eventLog = new ExEventLog(ExchangeFlowContext.eventLogComponentGuid, "MSExchangeFastSearch");
				}
				return ExchangeFlowContext.eventLog;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000499B File Offset: 0x00002B9B
		public IOperatorPerfCounter FlowPerformanceCounters
		{
			get
			{
				return this.flowPerformanceCounters;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000049A4 File Offset: 0x00002BA4
		public void SetInstance(Guid instanceGuid, string instanceName)
		{
			if (instanceGuid != this.InstanceGuid || instanceName != this.instanceName)
			{
				this.InstanceGuid = instanceGuid;
				this.instanceName = (string.IsNullOrEmpty(instanceName) ? instanceGuid.ToString() : instanceName);
				this.config = null;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000049F9 File Offset: 0x00002BF9
		public void SetFlowPerformanceCounters(IOperatorPerfCounter flowPerformanceCounters)
		{
			this.flowPerformanceCounters = flowPerformanceCounters;
		}

		// Token: 0x0400004C RID: 76
		public const string Name = "ExchangeFlowContext";

		// Token: 0x0400004D RID: 77
		private const string EventLogServiceName = "MSExchangeFastSearch";

		// Token: 0x0400004E RID: 78
		private static readonly Guid eventLogComponentGuid = Guid.Parse("c87fb454-7dfe-4559-af8c-3905438e1398");

		// Token: 0x0400004F RID: 79
		private static ExEventLog eventLog;

		// Token: 0x04000050 RID: 80
		private SearchConfig config;

		// Token: 0x04000051 RID: 81
		private string instanceName = "Unknown";

		// Token: 0x04000052 RID: 82
		private IOperatorPerfCounter flowPerformanceCounters;
	}
}
