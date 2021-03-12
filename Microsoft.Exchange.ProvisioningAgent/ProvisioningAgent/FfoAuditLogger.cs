using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200001B RID: 27
	internal class FfoAuditLogger : IAuditLog
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005A99 File Offset: 0x00003C99
		public FfoAuditLogger(OrganizationId organization)
		{
			this.session = AdminAuditLogHelper.CreateSession(organization, null);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005AAE File Offset: 0x00003CAE
		public DateTime EstimatedLogStartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005AB5 File Offset: 0x00003CB5
		public DateTime EstimatedLogEndTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005ABC File Offset: 0x00003CBC
		public bool IsAsynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005ABF File Offset: 0x00003CBF
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			ConfigObjectId configObjectId = new ConfigObjectId(CombGuidGenerator.NewGuid().ToString());
			string asString = AuditLogParseSerialize.GetAsString(auditRecord);
			AdminAuditLogEvent adminAuditLogEvent = new AdminAuditLogEvent(new AdminAuditLogEventId(configObjectId), asString);
			this.session.Save(new AdminAuditLogEventFacade(configObjectId)
			{
				ObjectModified = adminAuditLogEvent.ObjectModified,
				ModifiedObjectResolvedName = adminAuditLogEvent.ModifiedObjectResolvedName,
				CmdletName = adminAuditLogEvent.CmdletName,
				CmdletParameters = adminAuditLogEvent.CmdletParameters,
				ModifiedProperties = adminAuditLogEvent.ModifiedProperties,
				Caller = adminAuditLogEvent.Caller,
				Succeeded = adminAuditLogEvent.Succeeded,
				Error = adminAuditLogEvent.Error,
				RunDate = adminAuditLogEvent.RunDate,
				OriginatingServer = adminAuditLogEvent.OriginatingServer
			});
			return Encoding.Unicode.GetByteCount(asString);
		}

		// Token: 0x0400007B RID: 123
		private IConfigurationSession session;
	}
}
