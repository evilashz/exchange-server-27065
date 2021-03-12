using System;
using Microsoft.Exchange.LogAnalyzer.Analyzers.EventLog;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation.Monitors
{
	// Token: 0x0200029C RID: 668
	internal class SubComponentConfiguration
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x000854FC File Offset: 0x000836FC
		internal SubComponentConfiguration(StackTraceAnalysisProcessNames process, StackTraceAnalysisComponentNames subComponent, ProcessTrigger triggerType) : this(process, subComponent, null, triggerType, false)
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00085509 File Offset: 0x00083709
		internal SubComponentConfiguration(StackTraceAnalysisProcessNames process, StackTraceAnalysisComponentNames subComponent, Component escalationComponent, ProcessTrigger triggerType, bool addCorrelation)
		{
			this.Process = process;
			this.SubComponent = subComponent;
			this.EscalationComponent = escalationComponent;
			this.TriggerType = triggerType;
			this.AddCorrelation = addCorrelation;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00085536 File Offset: 0x00083736
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x0008553E File Offset: 0x0008373E
		internal bool AddCorrelation { get; set; }

		// Token: 0x04000E5F RID: 3679
		internal const string SubComponentParameter = "SubComponent";

		// Token: 0x04000E60 RID: 3680
		internal readonly StackTraceAnalysisProcessNames Process;

		// Token: 0x04000E61 RID: 3681
		internal readonly StackTraceAnalysisComponentNames SubComponent;

		// Token: 0x04000E62 RID: 3682
		internal readonly Component EscalationComponent;

		// Token: 0x04000E63 RID: 3683
		internal readonly ProcessTrigger TriggerType;
	}
}
