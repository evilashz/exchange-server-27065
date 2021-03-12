using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B3 RID: 691
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600233F RID: 9023 RVA: 0x0004E310 File Offset: 0x0004C510
		public RecipientNotFoundPermanentException(Guid mailboxGuid) : base(MrsStrings.RecipientNotFound(mailboxGuid))
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0004E325 File Offset: 0x0004C525
		public RecipientNotFoundPermanentException(Guid mailboxGuid, Exception innerException) : base(MrsStrings.RecipientNotFound(mailboxGuid), innerException)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0004E33B File Offset: 0x0004C53B
		protected RecipientNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxGuid = (Guid)info.GetValue("mailboxGuid", typeof(Guid));
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0004E365 File Offset: 0x0004C565
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxGuid", this.mailboxGuid);
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0004E385 File Offset: 0x0004C585
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x04000FBC RID: 4028
		private readonly Guid mailboxGuid;
	}
}
