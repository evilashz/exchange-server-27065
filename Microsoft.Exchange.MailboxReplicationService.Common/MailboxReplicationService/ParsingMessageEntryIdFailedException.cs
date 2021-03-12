using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000373 RID: 883
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingMessageEntryIdFailedException : MailboxReplicationPermanentException
	{
		// Token: 0x060026F9 RID: 9977 RVA: 0x00053E9D File Offset: 0x0005209D
		public ParsingMessageEntryIdFailedException(string messageEntryId) : base(MrsStrings.ParsingMessageEntryIdFailed(messageEntryId))
		{
			this.messageEntryId = messageEntryId;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x00053EB2 File Offset: 0x000520B2
		public ParsingMessageEntryIdFailedException(string messageEntryId, Exception innerException) : base(MrsStrings.ParsingMessageEntryIdFailed(messageEntryId), innerException)
		{
			this.messageEntryId = messageEntryId;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00053EC8 File Offset: 0x000520C8
		protected ParsingMessageEntryIdFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageEntryId = (string)info.GetValue("messageEntryId", typeof(string));
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00053EF2 File Offset: 0x000520F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("messageEntryId", this.messageEntryId);
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00053F0D File Offset: 0x0005210D
		public string MessageEntryId
		{
			get
			{
				return this.messageEntryId;
			}
		}

		// Token: 0x04001076 RID: 4214
		private readonly string messageEntryId;
	}
}
