using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000021 RID: 33
	public sealed class InterceptorSmtpAgentFactory : SmtpReceiveAgentFactory, IDiagnosable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000740B File Offset: 0x0000560B
		public InterceptorSmtpAgentFactory()
		{
			Archiver.CreateArchiver(InterceptorAgentSettings.ArchivePath);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000742A File Offset: 0x0000562A
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new InterceptorSmtpAgent(this.filteredRuleCache);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007437 File Offset: 0x00005637
		public string GetDiagnosticComponentName()
		{
			return "InterceptorSmtpAgent";
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000743E File Offset: 0x0000563E
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return InterceptorDiagnosticHelper.GetDiagnosticInfo(parameters, "InterceptorSmtpAgent", this.filteredRuleCache);
		}

		// Token: 0x040000BD RID: 189
		internal const string DiagnosticComponentName = "InterceptorSmtpAgent";

		// Token: 0x040000BE RID: 190
		private readonly FilteredRuleCache filteredRuleCache = new FilteredRuleCache(InterceptorAgentEvent.OnMailFrom | InterceptorAgentEvent.OnRcptTo | InterceptorAgentEvent.OnEndOfHeaders | InterceptorAgentEvent.OnEndOfData);
	}
}
