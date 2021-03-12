using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000376 RID: 886
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MessageEnumerationFailedException : MailboxReplicationTransientException
	{
		// Token: 0x06002709 RID: 9993 RVA: 0x00054059 File Offset: 0x00052259
		public MessageEnumerationFailedException(int exists, int messagesEnumeratedCount) : base(MrsStrings.MessageEnumerationFailed(exists, messagesEnumeratedCount))
		{
			this.exists = exists;
			this.messagesEnumeratedCount = messagesEnumeratedCount;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00054076 File Offset: 0x00052276
		public MessageEnumerationFailedException(int exists, int messagesEnumeratedCount, Exception innerException) : base(MrsStrings.MessageEnumerationFailed(exists, messagesEnumeratedCount), innerException)
		{
			this.exists = exists;
			this.messagesEnumeratedCount = messagesEnumeratedCount;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00054094 File Offset: 0x00052294
		protected MessageEnumerationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exists = (int)info.GetValue("exists", typeof(int));
			this.messagesEnumeratedCount = (int)info.GetValue("messagesEnumeratedCount", typeof(int));
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000540E9 File Offset: 0x000522E9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exists", this.exists);
			info.AddValue("messagesEnumeratedCount", this.messagesEnumeratedCount);
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x00054115 File Offset: 0x00052315
		public int Exists
		{
			get
			{
				return this.exists;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x0005411D File Offset: 0x0005231D
		public int MessagesEnumeratedCount
		{
			get
			{
				return this.messagesEnumeratedCount;
			}
		}

		// Token: 0x0400107A RID: 4218
		private readonly int exists;

		// Token: 0x0400107B RID: 4219
		private readonly int messagesEnumeratedCount;
	}
}
