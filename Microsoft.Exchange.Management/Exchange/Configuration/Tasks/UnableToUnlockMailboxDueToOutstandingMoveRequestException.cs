using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200114A RID: 4426
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToUnlockMailboxDueToOutstandingMoveRequestException : LocalizedException
	{
		// Token: 0x0600B557 RID: 46423 RVA: 0x0029E0C5 File Offset: 0x0029C2C5
		public UnableToUnlockMailboxDueToOutstandingMoveRequestException(string mailboxId, string moveStatus) : base(Strings.UnableToUnlockMailboxDueToOutstandingMoveRequest(mailboxId, moveStatus))
		{
			this.mailboxId = mailboxId;
			this.moveStatus = moveStatus;
		}

		// Token: 0x0600B558 RID: 46424 RVA: 0x0029E0E2 File Offset: 0x0029C2E2
		public UnableToUnlockMailboxDueToOutstandingMoveRequestException(string mailboxId, string moveStatus, Exception innerException) : base(Strings.UnableToUnlockMailboxDueToOutstandingMoveRequest(mailboxId, moveStatus), innerException)
		{
			this.mailboxId = mailboxId;
			this.moveStatus = moveStatus;
		}

		// Token: 0x0600B559 RID: 46425 RVA: 0x0029E100 File Offset: 0x0029C300
		protected UnableToUnlockMailboxDueToOutstandingMoveRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (string)info.GetValue("mailboxId", typeof(string));
			this.moveStatus = (string)info.GetValue("moveStatus", typeof(string));
		}

		// Token: 0x0600B55A RID: 46426 RVA: 0x0029E155 File Offset: 0x0029C355
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
			info.AddValue("moveStatus", this.moveStatus);
		}

		// Token: 0x1700394C RID: 14668
		// (get) Token: 0x0600B55B RID: 46427 RVA: 0x0029E181 File Offset: 0x0029C381
		public string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x1700394D RID: 14669
		// (get) Token: 0x0600B55C RID: 46428 RVA: 0x0029E189 File Offset: 0x0029C389
		public string MoveStatus
		{
			get
			{
				return this.moveStatus;
			}
		}

		// Token: 0x040062B2 RID: 25266
		private readonly string mailboxId;

		// Token: 0x040062B3 RID: 25267
		private readonly string moveStatus;
	}
}
