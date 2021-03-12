using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AD RID: 685
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestoringConnectedMailboxPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002321 RID: 8993 RVA: 0x0004E02F File Offset: 0x0004C22F
		public RestoringConnectedMailboxPermanentException(Guid mailboxGuid) : base(MrsStrings.RestoringConnectedMailboxError(mailboxGuid))
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0004E044 File Offset: 0x0004C244
		public RestoringConnectedMailboxPermanentException(Guid mailboxGuid, Exception innerException) : base(MrsStrings.RestoringConnectedMailboxError(mailboxGuid), innerException)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0004E05A File Offset: 0x0004C25A
		protected RestoringConnectedMailboxPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxGuid = (Guid)info.GetValue("mailboxGuid", typeof(Guid));
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0004E084 File Offset: 0x0004C284
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxGuid", this.mailboxGuid);
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x04000FB6 RID: 4022
		private readonly Guid mailboxGuid;
	}
}
