using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxTransientExceptionSavingContactException : ImportContactsException
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005145 File Offset: 0x00003345
		public MailboxTransientExceptionSavingContactException(int failedContactIndex, int contactsSaved) : base(Strings.MailboxTransientExceptionSavingContact(failedContactIndex, contactsSaved))
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005162 File Offset: 0x00003362
		public MailboxTransientExceptionSavingContactException(int failedContactIndex, int contactsSaved, Exception innerException) : base(Strings.MailboxTransientExceptionSavingContact(failedContactIndex, contactsSaved), innerException)
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005180 File Offset: 0x00003380
		protected MailboxTransientExceptionSavingContactException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedContactIndex = (int)info.GetValue("failedContactIndex", typeof(int));
			this.contactsSaved = (int)info.GetValue("contactsSaved", typeof(int));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000051D5 File Offset: 0x000033D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failedContactIndex", this.failedContactIndex);
			info.AddValue("contactsSaved", this.contactsSaved);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005201 File Offset: 0x00003401
		public int FailedContactIndex
		{
			get
			{
				return this.failedContactIndex;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005209 File Offset: 0x00003409
		public int ContactsSaved
		{
			get
			{
				return this.contactsSaved;
			}
		}

		// Token: 0x040000DC RID: 220
		private readonly int failedContactIndex;

		// Token: 0x040000DD RID: 221
		private readonly int contactsSaved;
	}
}
