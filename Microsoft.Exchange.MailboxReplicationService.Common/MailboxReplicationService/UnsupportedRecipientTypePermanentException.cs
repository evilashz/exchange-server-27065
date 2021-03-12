using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B4 RID: 692
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedRecipientTypePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x0004E38D File Offset: 0x0004C58D
		public UnsupportedRecipientTypePermanentException(string recipient, string recipientType) : base(MrsStrings.UnsupportedRecipientType(recipient, recipientType))
		{
			this.recipient = recipient;
			this.recipientType = recipientType;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0004E3AA File Offset: 0x0004C5AA
		public UnsupportedRecipientTypePermanentException(string recipient, string recipientType, Exception innerException) : base(MrsStrings.UnsupportedRecipientType(recipient, recipientType), innerException)
		{
			this.recipient = recipient;
			this.recipientType = recipientType;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0004E3C8 File Offset: 0x0004C5C8
		protected UnsupportedRecipientTypePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
			this.recipientType = (string)info.GetValue("recipientType", typeof(string));
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0004E41D File Offset: 0x0004C61D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
			info.AddValue("recipientType", this.recipientType);
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x0004E449 File Offset: 0x0004C649
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0004E451 File Offset: 0x0004C651
		public string RecipientType
		{
			get
			{
				return this.recipientType;
			}
		}

		// Token: 0x04000FBD RID: 4029
		private readonly string recipient;

		// Token: 0x04000FBE RID: 4030
		private readonly string recipientType;
	}
}
