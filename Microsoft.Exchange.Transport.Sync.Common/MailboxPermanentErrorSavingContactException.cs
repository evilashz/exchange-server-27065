using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxPermanentErrorSavingContactException : ImportContactsException
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00005211 File Offset: 0x00003411
		public MailboxPermanentErrorSavingContactException(int failedContactIndex, int contactsSaved) : base(Strings.MailboxPermanentErrorSavingContact(failedContactIndex, contactsSaved))
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000522E File Offset: 0x0000342E
		public MailboxPermanentErrorSavingContactException(int failedContactIndex, int contactsSaved, Exception innerException) : base(Strings.MailboxPermanentErrorSavingContact(failedContactIndex, contactsSaved), innerException)
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000524C File Offset: 0x0000344C
		protected MailboxPermanentErrorSavingContactException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedContactIndex = (int)info.GetValue("failedContactIndex", typeof(int));
			this.contactsSaved = (int)info.GetValue("contactsSaved", typeof(int));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000052A1 File Offset: 0x000034A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failedContactIndex", this.failedContactIndex);
			info.AddValue("contactsSaved", this.contactsSaved);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000052CD File Offset: 0x000034CD
		public int FailedContactIndex
		{
			get
			{
				return this.failedContactIndex;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000052D5 File Offset: 0x000034D5
		public int ContactsSaved
		{
			get
			{
				return this.contactsSaved;
			}
		}

		// Token: 0x040000DE RID: 222
		private readonly int failedContactIndex;

		// Token: 0x040000DF RID: 223
		private readonly int contactsSaved;
	}
}
