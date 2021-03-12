using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Provisioning.Agent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000006 RID: 6
	[ProvisioningAgentClassFactory]
	internal class AdminLogAgentClassFactory : IProvisioningAgent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002628 File Offset: 0x00000828
		public IEnumerable<string> GetSupportedCmdlets()
		{
			if (TaskLogger.IsSetupLogging)
			{
				if (AdminLogAgentClassFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogAgentClassFactory.Tracer.TraceDebug<AdminLogAgentClassFactory>((long)this.GetHashCode(), "{0} GetSupportedCmdlets called. Returning the empty string array since we do not want to audit cmdlets executed during setup", this);
				}
				return AdminLogAgentClassFactory.emptyCmdlets;
			}
			if (AdminLogAgentClassFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogAgentClassFactory.Tracer.TraceDebug<AdminLogAgentClassFactory, string>((long)this.GetHashCode(), "{0} GetSupportedCmdlets called. Returning '{1}'", this, AdminLogAgentClassFactory.allCmdlets[0]);
			}
			return AdminLogAgentClassFactory.allCmdlets;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002698 File Offset: 0x00000898
		public ProvisioningHandler GetCmdletHandler(string cmdletName)
		{
			if (AdminLogAgentClassFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogAgentClassFactory.Tracer.TraceDebug<AdminLogAgentClassFactory>((long)this.GetHashCode(), "{0} Return the admin audit log agent handler.", this);
			}
			if (!AdminLogAgentClassFactory.isDiagnosticsInitialized)
			{
				AdminLogAgentClassFactory.isDiagnosticsInitialized = true;
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents(AdminAuditLogHealthHandler.GetInstance());
			}
			return new AdminLogProvisioningHandler(this.configurationCache);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026EB File Offset: 0x000008EB
		public override string ToString()
		{
			return "AdminLogAgentClassFactory: ";
		}

		// Token: 0x0400001C RID: 28
		private const string toString = "AdminLogAgentClassFactory: ";

		// Token: 0x0400001D RID: 29
		private static readonly string[] allCmdlets = new string[]
		{
			"*"
		};

		// Token: 0x0400001E RID: 30
		private static readonly string[] emptyCmdlets = new string[0];

		// Token: 0x0400001F RID: 31
		private static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x04000020 RID: 32
		private readonly ConfigurationCache configurationCache = new ConfigurationCache();

		// Token: 0x04000021 RID: 33
		private static bool isDiagnosticsInitialized;
	}
}
