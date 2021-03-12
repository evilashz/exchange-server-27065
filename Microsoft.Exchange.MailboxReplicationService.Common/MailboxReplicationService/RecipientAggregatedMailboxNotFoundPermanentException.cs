using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B9 RID: 697
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientAggregatedMailboxNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x0004E6F2 File Offset: 0x0004C8F2
		public RecipientAggregatedMailboxNotFoundPermanentException(string recipient, string recipientAggregatedMailboxGuidsAsString, Guid targetAggregatedMailboxGuid) : base(MrsStrings.RecipientAggregatedMailboxNotFound(recipient, recipientAggregatedMailboxGuidsAsString, targetAggregatedMailboxGuid))
		{
			this.recipient = recipient;
			this.recipientAggregatedMailboxGuidsAsString = recipientAggregatedMailboxGuidsAsString;
			this.targetAggregatedMailboxGuid = targetAggregatedMailboxGuid;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0004E717 File Offset: 0x0004C917
		public RecipientAggregatedMailboxNotFoundPermanentException(string recipient, string recipientAggregatedMailboxGuidsAsString, Guid targetAggregatedMailboxGuid, Exception innerException) : base(MrsStrings.RecipientAggregatedMailboxNotFound(recipient, recipientAggregatedMailboxGuidsAsString, targetAggregatedMailboxGuid), innerException)
		{
			this.recipient = recipient;
			this.recipientAggregatedMailboxGuidsAsString = recipientAggregatedMailboxGuidsAsString;
			this.targetAggregatedMailboxGuid = targetAggregatedMailboxGuid;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0004E740 File Offset: 0x0004C940
		protected RecipientAggregatedMailboxNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
			this.recipientAggregatedMailboxGuidsAsString = (string)info.GetValue("recipientAggregatedMailboxGuidsAsString", typeof(string));
			this.targetAggregatedMailboxGuid = (Guid)info.GetValue("targetAggregatedMailboxGuid", typeof(Guid));
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
			info.AddValue("recipientAggregatedMailboxGuidsAsString", this.recipientAggregatedMailboxGuidsAsString);
			info.AddValue("targetAggregatedMailboxGuid", this.targetAggregatedMailboxGuid);
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x0004E805 File Offset: 0x0004CA05
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x0004E80D File Offset: 0x0004CA0D
		public string RecipientAggregatedMailboxGuidsAsString
		{
			get
			{
				return this.recipientAggregatedMailboxGuidsAsString;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x0004E815 File Offset: 0x0004CA15
		public Guid TargetAggregatedMailboxGuid
		{
			get
			{
				return this.targetAggregatedMailboxGuid;
			}
		}

		// Token: 0x04000FC5 RID: 4037
		private readonly string recipient;

		// Token: 0x04000FC6 RID: 4038
		private readonly string recipientAggregatedMailboxGuidsAsString;

		// Token: 0x04000FC7 RID: 4039
		private readonly Guid targetAggregatedMailboxGuid;
	}
}
