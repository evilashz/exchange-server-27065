using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogUploaderProxy;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000046 RID: 70
	internal class AuditLogDataBatch : LogDataBatch
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000BCF6 File Offset: 0x00009EF6
		public AuditLogDataBatch(int batchSizeInBytes, long beginOffSet, string fullLogName, string logPrefix) : base(batchSizeInBytes, beginOffSet, fullLogName, logPrefix)
		{
			this.records = new List<AuditRecord>();
			this.recordSerializer = new RecordSerializer();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000BD1C File Offset: 0x00009F1C
		public RecordSerializer RecordSerializer
		{
			get
			{
				RecordSerializer result;
				if ((result = this.recordSerializer) == null)
				{
					result = (this.recordSerializer = new RecordSerializer());
				}
				return result;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000BD41 File Offset: 0x00009F41
		public List<AuditRecord> GetRecords()
		{
			if (this.recordSerializer != null)
			{
				this.recordSerializer.Dispose();
				this.recordSerializer = null;
			}
			return this.records;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BD64 File Offset: 0x00009F64
		protected override bool ShouldProcessLogLine(ParsedReadOnlyRow parsedRow)
		{
			object field = parsedRow.UnParsedRow.GetField<object>(3);
			return field != null;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BD88 File Offset: 0x00009F88
		protected override void ProcessRowData(ParsedReadOnlyRow rowData)
		{
			ReadOnlyRow unParsedRow = rowData.UnParsedRow;
			AuditLogRecordType field = unParsedRow.GetField<AuditLogRecordType>(3);
			string field2 = unParsedRow.GetField<string>(4);
			AuditRecord auditRecord = this.RecordSerializer.Read<AuditRecord>(field2, field);
			switch (AuditUploaderConfig.GetAction(auditRecord.RecordType.ToString(), auditRecord.OrganizationName, auditRecord.UserId, auditRecord.Operation))
			{
			case Actions.LetThrough:
				this.records.Add(auditRecord);
				break;
			case Actions.Skip:
			case Actions.SkipAndLogEvent:
				break;
			default:
				return;
			}
		}

		// Token: 0x0400016D RID: 365
		private readonly List<AuditRecord> records;

		// Token: 0x0400016E RID: 366
		private RecordSerializer recordSerializer;
	}
}
