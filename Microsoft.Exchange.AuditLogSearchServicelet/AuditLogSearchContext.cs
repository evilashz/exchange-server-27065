using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.AuditLogSearchServicelet;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000002 RID: 2
	internal static class AuditLogSearchContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D7 File Offset: 0x000002D7
		internal static ITopologyConfigurationSession ConfigurationSession { get; private set; } = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 61, ".cctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuditLogSearch\\Program\\AuditLogSearchContext.cs");

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000216C File Offset: 0x0000036C
		internal static Server Localhost
		{
			get
			{
				if (AuditLogSearchContext.localhost == null)
				{
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						AuditLogSearchContext.localhost = AuditLogSearchContext.ConfigurationSession.FindLocalServer();
						if (!AuditLogSearchContext.localhost.IsMailboxServer)
						{
							ExTraceGlobals.ServiceletTracer.TraceError<Server>(83371L, "server {0} does not have mailbox role, not running log search service", AuditLogSearchContext.localhost);
							AuditLogSearchContext.localhost = null;
							throw new InvalidOperationException("Only mailbox servers should be running service");
						}
					});
					if (!adoperationResult.Succeeded)
					{
						ExTraceGlobals.ServiceletTracer.TraceError(67552L, "Could not query ad for local server");
						throw (adoperationResult.Exception is LocalServerNotFoundException) ? adoperationResult.Exception : new InvalidOperationException("Failed to find local server information.", adoperationResult.Exception);
					}
				}
				return AuditLogSearchContext.localhost;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021EB File Offset: 0x000003EB
		internal static MicrosoftExchangeRecipient Sender
		{
			get
			{
				if (AuditLogSearchContext.sender == null)
				{
					AuditLogSearchContext.sender = AuditLogSearchContext.ConfigurationSession.FindMicrosoftExchangeRecipient();
				}
				return AuditLogSearchContext.sender;
			}
		}

		// Token: 0x04000001 RID: 1
		public const string AuditLogSearchServiceFullName = "MSExchange AuditLogSearch";

		// Token: 0x04000002 RID: 2
		internal static readonly ExEventLog EventLogger = new ExEventLog(AuditLogSearchItemSchema.BasePropertyGuid, "MSExchange AuditLogSearch");

		// Token: 0x04000003 RID: 3
		private static Server localhost;

		// Token: 0x04000004 RID: 4
		private static MicrosoftExchangeRecipient sender;
	}
}
