using System;
using System.Text;
using Microsoft.Exchange.Data.Storage.Auditing;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200000B RID: 11
	internal class AdminLogMessageData
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00004625 File Offset: 0x00002825
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000462D File Offset: 0x0000282D
		public string Subject { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004636 File Offset: 0x00002836
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000463E File Offset: 0x0000283E
		public string Body { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00004647 File Offset: 0x00002847
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000464F File Offset: 0x0000284F
		public string Caller { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00004658 File Offset: 0x00002858
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00004660 File Offset: 0x00002860
		public string ObjectModified { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x0000466C File Offset: 0x0000286C
		public AdminLogMessageData(IAuditLogRecord auditRecord)
		{
			this.Body = AuditLogParseSerialize.GetAsString(auditRecord);
			this.Subject = string.Format("{0} : {1}", auditRecord.UserId, auditRecord.Operation);
			this.Caller = string.Format("{0}{1}", auditRecord.UserId, AdminLogMessageData.LogID);
			this.ObjectModified = string.Format("{0}{1}", auditRecord.ObjectId, AdminLogMessageData.LogID);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000046E0 File Offset: 0x000028E0
		public int GetSize()
		{
			return Encoding.Unicode.GetByteCount(this.Subject) + Encoding.Unicode.GetByteCount(this.Body) + Encoding.Unicode.GetByteCount(this.Caller) + Encoding.Unicode.GetByteCount(this.ObjectModified);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004730 File Offset: 0x00002930
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Subject: {0}", this.Subject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Body: ");
			stringBuilder.Append(this.Body);
			return stringBuilder.ToString();
		}

		// Token: 0x0400003C RID: 60
		private static readonly string LogID = "audit";
	}
}
