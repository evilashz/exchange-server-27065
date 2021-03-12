using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013E RID: 318
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TargetMailboxOutofQuotaException : Exception
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x00051D4D File Offset: 0x0004FF4D
		public TargetMailboxOutofQuotaException(string orgName, string mailboxGuid) : base(Strings.TargetMailboxOutofQuotaError(orgName, mailboxGuid))
		{
			this.orgName = orgName;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00051D6F File Offset: 0x0004FF6F
		public TargetMailboxOutofQuotaException(string orgName, string mailboxGuid, Exception innerException) : base(Strings.TargetMailboxOutofQuotaError(orgName, mailboxGuid), innerException)
		{
			this.orgName = orgName;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00051D94 File Offset: 0x0004FF94
		protected TargetMailboxOutofQuotaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgName = (string)info.GetValue("orgName", typeof(string));
			this.mailboxGuid = (string)info.GetValue("mailboxGuid", typeof(string));
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00051DE9 File Offset: 0x0004FFE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgName", this.orgName);
			info.AddValue("mailboxGuid", this.mailboxGuid);
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00051E15 File Offset: 0x00050015
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00051E1D File Offset: 0x0005001D
		public string MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x0400083B RID: 2107
		private readonly string orgName;

		// Token: 0x0400083C RID: 2108
		private readonly string mailboxGuid;
	}
}
