using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000022 RID: 34
	internal sealed class InterceptorStoreDriverDeliveryAgentFactory : StoreDriverDeliveryAgentFactory, IDiagnosable
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00007451 File Offset: 0x00005651
		public InterceptorStoreDriverDeliveryAgentFactory()
		{
			Archiver.CreateArchiver(InterceptorAgentSettings.ArchivePath);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007473 File Offset: 0x00005673
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new InterceptorStoreDriverDeliveryAgent(this.filteredRuleCache);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007480 File Offset: 0x00005680
		public string GetDiagnosticComponentName()
		{
			return "InterceptorDeliveryAgent";
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007487 File Offset: 0x00005687
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return InterceptorDiagnosticHelper.GetDiagnosticInfo(parameters, "InterceptorDeliveryAgent", this.filteredRuleCache);
		}

		// Token: 0x040000BF RID: 191
		internal const string DiagnosticComponentName = "InterceptorDeliveryAgent";

		// Token: 0x040000C0 RID: 192
		private readonly FilteredRuleCache filteredRuleCache = new FilteredRuleCache(InterceptorAgentEvent.OnInitMsg | InterceptorAgentEvent.OnPromotedMessage | InterceptorAgentEvent.OnCreatedMessage);
	}
}
