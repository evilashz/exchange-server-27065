using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B6 RID: 694
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientIsNotAMailboxPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		public RecipientIsNotAMailboxPermanentException(string recipient) : base(MrsStrings.RecipientIsNotAMailbox(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0004E4E6 File Offset: 0x0004C6E6
		public RecipientIsNotAMailboxPermanentException(string recipient, Exception innerException) : base(MrsStrings.RecipientIsNotAMailbox(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0004E4FC File Offset: 0x0004C6FC
		protected RecipientIsNotAMailboxPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0004E526 File Offset: 0x0004C726
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x0004E541 File Offset: 0x0004C741
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04000FC0 RID: 4032
		private readonly string recipient;
	}
}
