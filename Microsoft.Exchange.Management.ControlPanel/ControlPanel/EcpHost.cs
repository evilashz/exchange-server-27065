using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.ServiceModel;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000389 RID: 905
	internal class EcpHost : RunspaceHost
	{
		// Token: 0x06003074 RID: 12404 RVA: 0x000939B1 File Offset: 0x00091BB1
		public override void Activate()
		{
			this.runspaceActivationsTimer = EcpPerformanceData.ActiveRunspace.StartRequestTimer();
			EcpHost.activeRunspaceCounters.Increment();
			this.averageActiveRunspace.Start();
			this.webServiceOperation = OperationContext.Current;
			base.Activate();
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000939E9 File Offset: 0x00091BE9
		public override void Deactivate()
		{
			this.averageActiveRunspace.Stop();
			EcpHost.activeRunspaceCounters.Decrement();
			this.webServiceOperation = null;
			base.Deactivate();
			this.runspaceActivationsTimer.Dispose();
			this.runspaceActivationsTimer = null;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x00093A1F File Offset: 0x00091C1F
		public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			if (this.webServiceOperation != null)
			{
				return new Dictionary<string, PSObject>();
			}
			return base.Prompt(caption, message, descriptions);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x00093A38 File Offset: 0x00091C38
		public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
		{
			throw new ShouldContinueException(message);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x00093A40 File Offset: 0x00091C40
		public override void WriteWarningLine(string message)
		{
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x00093A42 File Offset: 0x00091C42
		public override string Name
		{
			get
			{
				return "Exchange Control Panel";
			}
		}

		// Token: 0x04002372 RID: 9074
		private const string RunspaceName = "Exchange Control Panel";

		// Token: 0x04002373 RID: 9075
		private static PerfCounterGroup activeRunspaceCounters = new PerfCounterGroup(EcpPerfCounters.ActiveRunspaces, EcpPerfCounters.ActiveRunspacesPeak, EcpPerfCounters.ActiveRunspacesTotal);

		// Token: 0x04002374 RID: 9076
		private OperationContext webServiceOperation;

		// Token: 0x04002375 RID: 9077
		private AverageTimePerfCounter averageActiveRunspace = new AverageTimePerfCounter(EcpPerfCounters.AverageActiveRunspace, EcpPerfCounters.AverageActiveRunspaceBase);

		// Token: 0x04002376 RID: 9078
		private IDisposable runspaceActivationsTimer;

		// Token: 0x04002377 RID: 9079
		public static readonly PSHostFactory Factory = new EcpHost.EcpHostFactory();

		// Token: 0x0200038A RID: 906
		private class EcpHostFactory : PSHostFactory
		{
			// Token: 0x0600307C RID: 12412 RVA: 0x00093A8B File Offset: 0x00091C8B
			public override PSHost CreatePSHost()
			{
				return new EcpHost();
			}
		}
	}
}
