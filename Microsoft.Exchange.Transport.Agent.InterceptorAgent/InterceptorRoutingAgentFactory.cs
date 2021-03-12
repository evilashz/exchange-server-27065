using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200001D RID: 29
	public sealed class InterceptorRoutingAgentFactory : RoutingAgentFactory, IDiagnosable
	{
		// Token: 0x06000130 RID: 304 RVA: 0x000070EC File Offset: 0x000052EC
		public InterceptorRoutingAgentFactory()
		{
			Archiver.CreateArchiver(InterceptorAgentSettings.ArchivePath);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000710E File Offset: 0x0000530E
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new InterceptorRoutingAgent(this.filteredRuleCache);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000711B File Offset: 0x0000531B
		public string GetDiagnosticComponentName()
		{
			return "InterceptorRoutingAgent";
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007122 File Offset: 0x00005322
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return InterceptorDiagnosticHelper.GetDiagnosticInfo(parameters, "InterceptorRoutingAgent", this.filteredRuleCache);
		}

		// Token: 0x040000BA RID: 186
		internal const string DiagnosticComponentName = "InterceptorRoutingAgent";

		// Token: 0x040000BB RID: 187
		private readonly FilteredRuleCache filteredRuleCache = new FilteredRuleCache(InterceptorAgentEvent.OnSubmittedMessage | InterceptorAgentEvent.OnResolvedMessage | InterceptorAgentEvent.OnRoutedMessage | InterceptorAgentEvent.OnCategorizedMessage);
	}
}
