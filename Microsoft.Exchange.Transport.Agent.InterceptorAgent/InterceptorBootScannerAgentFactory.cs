using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000019 RID: 25
	public sealed class InterceptorBootScannerAgentFactory : StorageAgentFactory, IDiagnosable
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000065D2 File Offset: 0x000047D2
		public string GetDiagnosticComponentName()
		{
			return "InterceptorBootScannerAgent";
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000065D9 File Offset: 0x000047D9
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return InterceptorDiagnosticHelper.GetDiagnosticInfo(parameters, "InterceptorBootScannerAgent", this.filteredRuleCache);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000065EC File Offset: 0x000047EC
		internal override StorageAgent CreateAgent(SmtpServer server)
		{
			return new InterceptorBootScannerAgent(this.filteredRuleCache);
		}

		// Token: 0x040000AB RID: 171
		internal const string DiagnosticComponentName = "InterceptorBootScannerAgent";

		// Token: 0x040000AC RID: 172
		private readonly FilteredRuleCache filteredRuleCache = new FilteredRuleCache(InterceptorAgentEvent.OnLoadedMessage);
	}
}
