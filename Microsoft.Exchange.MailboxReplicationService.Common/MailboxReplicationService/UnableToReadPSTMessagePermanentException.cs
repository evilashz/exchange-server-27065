using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000352 RID: 850
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadPSTMessagePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002655 RID: 9813 RVA: 0x00052F21 File Offset: 0x00051121
		public UnableToReadPSTMessagePermanentException(string filePath, uint messageId) : base(MrsStrings.UnableToReadPSTMessage(filePath, messageId))
		{
			this.filePath = filePath;
			this.messageId = messageId;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00052F3E File Offset: 0x0005113E
		public UnableToReadPSTMessagePermanentException(string filePath, uint messageId, Exception innerException) : base(MrsStrings.UnableToReadPSTMessage(filePath, messageId), innerException)
		{
			this.filePath = filePath;
			this.messageId = messageId;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00052F5C File Offset: 0x0005115C
		protected UnableToReadPSTMessagePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.messageId = (uint)info.GetValue("messageId", typeof(uint));
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00052FB1 File Offset: 0x000511B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("messageId", this.messageId);
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00052FDD File Offset: 0x000511DD
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00052FE5 File Offset: 0x000511E5
		public uint MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x04001056 RID: 4182
		private readonly string filePath;

		// Token: 0x04001057 RID: 4183
		private readonly uint messageId;
	}
}
