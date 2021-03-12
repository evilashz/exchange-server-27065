using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200000F RID: 15
	internal static class AuditLoggerFactory
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000049B3 File Offset: 0x00002BB3
		public static IAuditLog CreateForFFO(OrganizationId orgId)
		{
			return new FfoAuditLogger(orgId);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000049BC File Offset: 0x00002BBC
		public static IAuditLog Create(ExchangePrincipal principal, ArbitrationMailboxStatus status)
		{
			if (status == ArbitrationMailboxStatus.R5 || status == ArbitrationMailboxStatus.UnableToKnow)
			{
				if (AuditLoggerFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditLoggerFactory.Tracer.TraceDebug<string>(0L, "The log mailbox is on the local server. Hence writing mail directly to mailbox {0}.", principal.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				return new XsoAuditLogger(principal, AuditLoggerFactory.CanUsePartitionedLogs(status));
			}
			if (AuditFeatureManager.IsAdminAuditLocalQueueEnabled(principal))
			{
				if (AuditLoggerFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditLoggerFactory.Tracer.TraceDebug<int, string>(0L, "Tenant arbitration mailbox version is {0}. Write audit record to the local queue. Mailbox {1}.", principal.MailboxInfo.Location.ServerVersion, principal.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				OrganizationId organizationId = principal.MailboxInfo.OrganizationId;
				string organizationId2 = (organizationId == null || organizationId.OrganizationalUnit == null || organizationId.ConfigurationUnit == null) ? null : Convert.ToBase64String(organizationId.GetBytes(Encoding.UTF8));
				return new UnifiedAdminAuditLog(organizationId2, AuditRecordFactory.GetOrgNameFromOrgId(organizationId));
			}
			if (AuditLoggerFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AuditLoggerFactory.Tracer.TraceDebug<int, string>(0L, "Tenant arbitration mailbox version is {0}. Use EWS to send CreateItem in mailbox {1}.", principal.MailboxInfo.Location.ServerVersion, principal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			return new EwsAuditLogger(principal);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004B04 File Offset: 0x00002D04
		public static IAuditLog CreateAsync(ExchangePrincipal principal, ArbitrationMailboxStatus status)
		{
			if (AuditLoggerFactory.AsyncReceiver == null)
			{
				lock (AuditLoggerFactory.AsyncReceiverSyncroot)
				{
					if (AuditLoggerFactory.AsyncReceiver == null)
					{
						if (AuditLoggerFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							AuditLoggerFactory.Tracer.TraceDebug(0L, "Starting background async receiver thread");
						}
						AuditLoggerFactory.AsyncReceiver = new AsyncAuditReceiver();
						AuditLoggerFactory.AsyncReceiver.Start();
					}
				}
			}
			if (AuditLoggerFactory.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AuditLoggerFactory.Tracer.TraceDebug<string>(0L, "Returning async logger for mailbox {0}", principal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			return new AsyncAuditLogger(AuditLoggerFactory.Create(principal, status));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private static bool IsMailboxOnCurrentServer(ExchangePrincipal mailbox)
		{
			return 0 == string.Compare(LocalServerCache.LocalServerFqdn, mailbox.MailboxInfo.Location.ServerFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004BE0 File Offset: 0x00002DE0
		private static bool CanUsePartitionedLogs(ArbitrationMailboxStatus status)
		{
			return status == ArbitrationMailboxStatus.E15;
		}

		// Token: 0x04000047 RID: 71
		private static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x04000048 RID: 72
		internal static AsyncAuditReceiver AsyncReceiver = null;

		// Token: 0x04000049 RID: 73
		private static object AsyncReceiverSyncroot = new object();
	}
}
