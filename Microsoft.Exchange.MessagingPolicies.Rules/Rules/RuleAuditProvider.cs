using System;
using System.ComponentModel;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000025 RID: 37
	internal sealed class RuleAuditProvider : AuditProvider
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00007797 File Offset: 0x00005997
		private RuleAuditProvider() : base(RuleAuditProvider.EtrEventSourceName)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000077A4 File Offset: 0x000059A4
		private static void Init()
		{
			if (!RuleAuditProvider.init)
			{
				lock (RuleAuditProvider.lockVar)
				{
					if (!RuleAuditProvider.init)
					{
						try
						{
							RuleAuditProvider.provider = new RuleAuditProvider();
							RuleAuditProvider.skip = false;
						}
						catch (UnauthorizedAccessException)
						{
							RuleAuditProvider.skip = true;
						}
						catch (PrivilegeNotHeldException)
						{
							RuleAuditProvider.skip = true;
						}
						RuleAuditProvider.init = true;
					}
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007830 File Offset: 0x00005A30
		public static void LogSuccess(string ruleInfo)
		{
			try
			{
				RuleAuditProvider.Init();
				if (!RuleAuditProvider.skip)
				{
					RuleAuditProvider.provider.ReportAudit(1, true, new object[]
					{
						ruleInfo
					});
				}
			}
			catch (Win32Exception)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, "audit logging for a success rule loading failed");
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007888 File Offset: 0x00005A88
		public static void LogFailure(string ruleInfo)
		{
			try
			{
				RuleAuditProvider.Init();
				if (!RuleAuditProvider.skip)
				{
					RuleAuditProvider.provider.ReportAudit(2, false, new object[]
					{
						ruleInfo
					});
				}
			}
			catch (Win32Exception)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, "audit logging for a failed rule loading failed");
			}
		}

		// Token: 0x0400010A RID: 266
		internal static readonly string EtrEventSourceName = "MSExchange Messaging Policies";

		// Token: 0x0400010B RID: 267
		private static RuleAuditProvider provider;

		// Token: 0x0400010C RID: 268
		private static object lockVar = new object();

		// Token: 0x0400010D RID: 269
		private static bool init;

		// Token: 0x0400010E RID: 270
		private static bool skip;
	}
}
