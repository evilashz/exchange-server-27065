using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	internal class AuditLogRecord : ItemPropertiesBase, IAuditLogRecord
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000347D File Offset: 0x0000167D
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00003485 File Offset: 0x00001685
		[DataMember]
		public int RecordTypeInt { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000348E File Offset: 0x0000168E
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00003496 File Offset: 0x00001696
		[DataMember]
		public DateTime CreationTime { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000349F File Offset: 0x0000169F
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000034A7 File Offset: 0x000016A7
		[DataMember]
		public string Operation { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000034B0 File Offset: 0x000016B0
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000034B8 File Offset: 0x000016B8
		[DataMember(EmitDefaultValue = false)]
		public string ObjectId { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000034C1 File Offset: 0x000016C1
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000034C9 File Offset: 0x000016C9
		[DataMember(EmitDefaultValue = false)]
		public string UserId { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000034D2 File Offset: 0x000016D2
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000034DA File Offset: 0x000016DA
		[DataMember(EmitDefaultValue = false)]
		public Tuple<string, string>[] Details { get; set; }

		// Token: 0x0600015B RID: 347 RVA: 0x00003688 File Offset: 0x00001888
		IEnumerable<KeyValuePair<string, string>> IAuditLogRecord.GetDetails()
		{
			if (this.Details != null)
			{
				foreach (Tuple<string, string> detail in this.Details)
				{
					yield return new KeyValuePair<string, string>(detail.Item1, detail.Item2);
				}
			}
			yield break;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000036A5 File Offset: 0x000018A5
		AuditLogRecordType IAuditLogRecord.RecordType
		{
			get
			{
				return (AuditLogRecordType)this.RecordTypeInt;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000036B0 File Offset: 0x000018B0
		public override void Apply(MailboxSession session, Item item)
		{
			BodyWriteConfiguration configuration = new BodyWriteConfiguration(BodyFormat.TextPlain);
			using (TextWriter textWriter = item.Body.OpenTextWriter(configuration))
			{
				string asString = AuditLogParseSerialize.GetAsString(this);
				textWriter.Write(asString);
			}
			item.ClassName = "IPM.AuditLog";
		}
	}
}
