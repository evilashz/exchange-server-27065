using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class QuotaExceededSavingContactException : ImportContactsException
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00005076 File Offset: 0x00003276
		public QuotaExceededSavingContactException(int failedContactIndex, int contactsSaved) : base(Strings.QuotaExceededSavingContact(failedContactIndex, contactsSaved))
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005093 File Offset: 0x00003293
		public QuotaExceededSavingContactException(int failedContactIndex, int contactsSaved, Exception innerException) : base(Strings.QuotaExceededSavingContact(failedContactIndex, contactsSaved), innerException)
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000050B4 File Offset: 0x000032B4
		protected QuotaExceededSavingContactException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedContactIndex = (int)info.GetValue("failedContactIndex", typeof(int));
			this.contactsSaved = (int)info.GetValue("contactsSaved", typeof(int));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005109 File Offset: 0x00003309
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failedContactIndex", this.failedContactIndex);
			info.AddValue("contactsSaved", this.contactsSaved);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005135 File Offset: 0x00003335
		public int FailedContactIndex
		{
			get
			{
				return this.failedContactIndex;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000513D File Offset: 0x0000333D
		public int ContactsSaved
		{
			get
			{
				return this.contactsSaved;
			}
		}

		// Token: 0x040000DA RID: 218
		private readonly int failedContactIndex;

		// Token: 0x040000DB RID: 219
		private readonly int contactsSaved;
	}
}
