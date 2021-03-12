using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema;
using Microsoft.Office.Compliance.Audit.Schema.Admin;
using Microsoft.Office.Compliance.Audit.Schema.Mailbox;
using Microsoft.Office.Compliance.Audit.Schema.Monitoring;
using Microsoft.Office.Compliance.Audit.Schema.SharePoint;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct AuditLogRecord : IAuditLogRecord
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001B617 File Offset: 0x00019817
		public AuditLogRecord(AuditRecord record, Trace trace)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			if (trace == null)
			{
				throw new ArgumentNullException("trace");
			}
			this.record = record;
			this.trace = trace;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001B644 File Offset: 0x00019844
		public AuditLogRecordType RecordType
		{
			get
			{
				switch (this.record.RecordType)
				{
				case 1:
					return AuditLogRecordType.AdminAudit;
				case 2:
				case 3:
					return AuditLogRecordType.MailboxAudit;
				default:
					throw new NotSupportedException(this.record.RecordType.ToString());
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001B691 File Offset: 0x00019891
		public DateTime CreationTime
		{
			get
			{
				return this.record.CreationTime;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001B69E File Offset: 0x0001989E
		public string Operation
		{
			get
			{
				return this.record.Operation;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001B6AB File Offset: 0x000198AB
		public string ObjectId
		{
			get
			{
				return this.record.ObjectId;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001B6B8 File Offset: 0x000198B8
		public string UserId
		{
			get
			{
				return this.record.UserId;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public IEnumerable<KeyValuePair<string, string>> GetDetails()
		{
			AuditLogRecord.RecordVisitor recordVisitor = new AuditLogRecord.RecordVisitor(this.trace);
			this.record.Visit(recordVisitor);
			IAuditLogRecord result = recordVisitor.Result;
			return result.GetDetails();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001B6FC File Offset: 0x000198FC
		public static IAuditLogRecord FillAdminRecordDetails(ExchangeAdminAuditRecord auditRecord, Trace trace)
		{
			AdminAuditLogRecord adminAuditLogRecord = new AdminAuditLogRecord(trace)
			{
				Verbose = true,
				Cmdlet = auditRecord.Operation,
				ExternalAccess = auditRecord.ExternalAccess,
				ObjectModified = auditRecord.ObjectId,
				UserId = auditRecord.UserId,
				RunDate = auditRecord.CreationTime,
				Succeeded = (auditRecord.Succeeded ?? false),
				Error = auditRecord.Error,
				ModifiedObjectResolvedName = auditRecord.ModifiedObjectResolvedName,
				Parameters = new Hashtable(),
				ModifiedPropertyValues = new Hashtable(),
				OriginalPropertyValues = new Hashtable()
			};
			if (auditRecord.Parameters != null)
			{
				foreach (NameValuePair nameValuePair in auditRecord.Parameters)
				{
					adminAuditLogRecord.Parameters.Add(nameValuePair.Name, nameValuePair.Value);
				}
			}
			if (auditRecord.ModifiedProperties != null)
			{
				foreach (ModifiedProperty modifiedProperty in auditRecord.ModifiedProperties)
				{
					adminAuditLogRecord.ModifiedPropertyValues.Add(modifiedProperty.Name, modifiedProperty.NewValue);
					adminAuditLogRecord.OriginalPropertyValues.Add(modifiedProperty.Name, modifiedProperty.OldValue);
				}
			}
			return adminAuditLogRecord;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001B884 File Offset: 0x00019A84
		public static IAuditLogRecord FillMailboxAuditRecordDetails(ExchangeMailboxAuditRecord auditRecord, Trace trace)
		{
			ItemOperationAuditEventRecordAdapter itemOperationAuditEventRecordAdapter = new ItemOperationAuditEventRecordAdapter(auditRecord, auditRecord.OrganizationId);
			return itemOperationAuditEventRecordAdapter.GetLogRecord();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		public static IAuditLogRecord FillMailboxAuditGroupRecordDetails(ExchangeMailboxAuditGroupRecord auditRecord, Trace trace)
		{
			GroupOperationAuditEventRecordAdapter groupOperationAuditEventRecordAdapter = new GroupOperationAuditEventRecordAdapter(auditRecord, auditRecord.OrganizationId);
			return groupOperationAuditEventRecordAdapter.GetLogRecord();
		}

		// Token: 0x04000324 RID: 804
		private readonly AuditRecord record;

		// Token: 0x04000325 RID: 805
		private readonly Trace trace;

		// Token: 0x020000A6 RID: 166
		private class RecordVisitor : IAuditRecordVisitor
		{
			// Token: 0x06000714 RID: 1812 RVA: 0x0001B8C4 File Offset: 0x00019AC4
			public RecordVisitor(Trace trace)
			{
				this.trace = trace;
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001B8D3 File Offset: 0x00019AD3
			// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001B8DB File Offset: 0x00019ADB
			public IAuditLogRecord Result { get; private set; }

			// Token: 0x06000717 RID: 1815 RVA: 0x0001B8E4 File Offset: 0x00019AE4
			public void Visit(ExchangeAdminAuditRecord record)
			{
				this.Result = AuditLogRecord.FillAdminRecordDetails(record, this.trace);
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0001B8F8 File Offset: 0x00019AF8
			public void Visit(ExchangeMailboxAuditRecord record)
			{
				this.Result = AuditLogRecord.FillMailboxAuditRecordDetails(record, this.trace);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x0001B90C File Offset: 0x00019B0C
			public void Visit(ExchangeMailboxAuditGroupRecord record)
			{
				this.Result = AuditLogRecord.FillMailboxAuditGroupRecordDetails(record, this.trace);
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0001B920 File Offset: 0x00019B20
			public void Visit(SharePointAuditRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x0001B927 File Offset: 0x00019B27
			public void Visit(SyntheticProbeAuditRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000326 RID: 806
			private readonly Trace trace;
		}
	}
}
