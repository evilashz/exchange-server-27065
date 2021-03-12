using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A6 RID: 166
	internal class AdminAuditFolderStrategy : AuditFolderStrategy
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x00030595 File Offset: 0x0002E795
		public AdminAuditFolderStrategy(MailboxDataForTags mailboxDataForTags, Trace tracer) : base(mailboxDataForTags, tracer)
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0003059F File Offset: 0x0002E79F
		public override DefaultFolderType DefaultFolderType
		{
			get
			{
				return DefaultFolderType.AdminAuditLogs;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000305A3 File Offset: 0x0002E7A3
		public override StoreObjectId GetFolderId(MailboxSession session)
		{
			return session.GetAdminAuditLogsFolderId();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000305AC File Offset: 0x0002E7AC
		public override EnhancedTimeSpan AuditRecordAgeLimit
		{
			get
			{
				OrganizationId organizationId = base.MailboxDataForTags.ElcUserTagInformation.ADUser.OrganizationId;
				EnhancedTimeSpan maxValue = EnhancedTimeSpan.MaxValue;
				EnhancedTimeSpan result = maxValue.Subtract(EnhancedTimeSpan.FromDays(1.0));
				Exception ex = null;
				try
				{
					ADSessionSettings sessionSettings = (organizationId != null) ? ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId) : ADSessionSettings.FromRootOrgScopeSet();
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 703, "AuditRecordAgeLimit", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\SysCleanupAssistant\\AuditExpirationEnforcer.cs");
					tenantOrTopologyConfigurationSession.SessionSettings.IncludeCNFObject = false;
					AdminAuditLogConfig[] array = tenantOrTopologyConfigurationSession.Find<AdminAuditLogConfig>(null, QueryScope.SubTree, null, null, 1);
					if (array == null || array.Length == 0)
					{
						if (base.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							base.Tracer.TraceError<AdminAuditFolderStrategy, string>((long)this.GetHashCode(), "{0}: Failed to find AdminAuditLogConfig for organization {1}.", this, (organizationId == null) ? "null" : organizationId.ToString());
						}
						return result;
					}
					return array[0].AdminAuditLogAgeLimit;
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (ADExternalException ex3)
				{
					ex = ex3;
				}
				catch (ADOperationException ex4)
				{
					ex = ex4;
				}
				if (ex != null && base.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					base.Tracer.TraceError<AdminAuditFolderStrategy, string, Exception>((long)this.GetHashCode(), "{0}: Failed to read AdminAuditLogConfig for organization {1}. Exception: {2}", this, (organizationId == null) ? "null" : organizationId.ToString(), ex);
				}
				return maxValue;
			}
		}

		// Token: 0x040004A1 RID: 1185
		public static readonly double AdminAuditsWarningPercentage = 0.8;
	}
}
