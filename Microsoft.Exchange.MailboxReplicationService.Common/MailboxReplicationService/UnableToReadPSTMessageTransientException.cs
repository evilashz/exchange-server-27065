using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000353 RID: 851
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadPSTMessageTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600265B RID: 9819 RVA: 0x00052FED File Offset: 0x000511ED
		public UnableToReadPSTMessageTransientException(string filePath, uint messageId) : base(MrsStrings.UnableToReadPSTMessage(filePath, messageId))
		{
			this.filePath = filePath;
			this.messageId = messageId;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0005300A File Offset: 0x0005120A
		public UnableToReadPSTMessageTransientException(string filePath, uint messageId, Exception innerException) : base(MrsStrings.UnableToReadPSTMessage(filePath, messageId), innerException)
		{
			this.filePath = filePath;
			this.messageId = messageId;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00053028 File Offset: 0x00051228
		protected UnableToReadPSTMessageTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.messageId = (uint)info.GetValue("messageId", typeof(uint));
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0005307D File Offset: 0x0005127D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("messageId", this.messageId);
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000530A9 File Offset: 0x000512A9
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000530B1 File Offset: 0x000512B1
		public uint MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x04001058 RID: 4184
		private readonly string filePath;

		// Token: 0x04001059 RID: 4185
		private readonly uint messageId;
	}
}
