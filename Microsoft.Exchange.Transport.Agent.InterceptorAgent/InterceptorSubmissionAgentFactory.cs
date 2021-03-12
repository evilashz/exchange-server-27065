using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000025 RID: 37
	internal sealed class InterceptorSubmissionAgentFactory : StoreDriverAgentFactory, IDiagnosable
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000076A3 File Offset: 0x000058A3
		public InterceptorSubmissionAgentFactory()
		{
			Archiver.CreateArchiver(InterceptorAgentSettings.ArchivePath);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000076C5 File Offset: 0x000058C5
		public override StoreDriverAgent CreateAgent(SmtpServer server)
		{
			return new InterceptorSubmissionAgent(this.filteredRuleCache);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000076D2 File Offset: 0x000058D2
		public string GetDiagnosticComponentName()
		{
			return "InterceptorSubmissionAgent";
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000076D9 File Offset: 0x000058D9
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return InterceptorDiagnosticHelper.GetDiagnosticInfo(parameters, "InterceptorSubmissionAgent", this.filteredRuleCache);
		}

		// Token: 0x040000C7 RID: 199
		internal const string DiagnosticComponentName = "InterceptorSubmissionAgent";

		// Token: 0x040000C8 RID: 200
		private readonly FilteredRuleCache filteredRuleCache = new FilteredRuleCache(InterceptorAgentEvent.OnDemotedMessage);
	}
}
