using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B7 RID: 695
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetRecipientIsNotAnMEUPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x0004E549 File Offset: 0x0004C749
		public TargetRecipientIsNotAnMEUPermanentException(string recipient) : base(MrsStrings.TargetRecipientIsNotAnMEU(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0004E55E File Offset: 0x0004C75E
		public TargetRecipientIsNotAnMEUPermanentException(string recipient, Exception innerException) : base(MrsStrings.TargetRecipientIsNotAnMEU(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0004E574 File Offset: 0x0004C774
		protected TargetRecipientIsNotAnMEUPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0004E59E File Offset: 0x0004C79E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x0004E5B9 File Offset: 0x0004C7B9
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04000FC1 RID: 4033
		private readonly string recipient;
	}
}
