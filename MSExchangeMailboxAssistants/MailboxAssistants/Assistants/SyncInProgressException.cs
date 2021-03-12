using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013D RID: 317
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncInProgressException : TransientException
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x00051C81 File Offset: 0x0004FE81
		public SyncInProgressException(string orgName, string mailboxGuid) : base(Strings.ProgressCheckTimeoutError(orgName, mailboxGuid))
		{
			this.orgName = orgName;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00051C9E File Offset: 0x0004FE9E
		public SyncInProgressException(string orgName, string mailboxGuid, Exception innerException) : base(Strings.ProgressCheckTimeoutError(orgName, mailboxGuid), innerException)
		{
			this.orgName = orgName;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00051CBC File Offset: 0x0004FEBC
		protected SyncInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgName = (string)info.GetValue("orgName", typeof(string));
			this.mailboxGuid = (string)info.GetValue("mailboxGuid", typeof(string));
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00051D11 File Offset: 0x0004FF11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgName", this.orgName);
			info.AddValue("mailboxGuid", this.mailboxGuid);
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00051D3D File Offset: 0x0004FF3D
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00051D45 File Offset: 0x0004FF45
		public string MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x04000839 RID: 2105
		private readonly string orgName;

		// Token: 0x0400083A RID: 2106
		private readonly string mailboxGuid;
	}
}
