using System;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000299 RID: 665
	internal sealed class ScopeNodeProgressProvider : IProgressProvider
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x0007A2B0 File Offset: 0x000784B0
		public ScopeNodeProgressProvider(ScopeNode scopeNode)
		{
			this.scopeNode = scopeNode;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0007A2C0 File Offset: 0x000784C0
		public IProgress CreateProgress(string operationName)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<string, string>(0L, "-->ScopeNodeProgressProvider.CreateProgress: {0}, {1}", this.scopeNode.LanguageIndependentName, operationName);
			CustomStatus customStatus = new CustomStatus();
			IProgress result = new StatusProgress(customStatus, this.scopeNode.SnapIn);
			customStatus.Title = ExchangeUserControl.RemoveAccelerator(operationName);
			customStatus.Start(this.scopeNode);
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<string, string>(0L, "<--ScopeNodeProgressProvider.CreateProgress: {0}, {1}", this.scopeNode.LanguageIndependentName, operationName);
			return result;
		}

		// Token: 0x04000A79 RID: 2681
		private ScopeNode scopeNode;
	}
}
