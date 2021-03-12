using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InternalErrorSavingContactException : ImportContactsException
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000052DD File Offset: 0x000034DD
		public InternalErrorSavingContactException(int failedContactIndex, int contactsSaved) : base(Strings.InternalErrorSavingContact(failedContactIndex, contactsSaved))
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000052FA File Offset: 0x000034FA
		public InternalErrorSavingContactException(int failedContactIndex, int contactsSaved, Exception innerException) : base(Strings.InternalErrorSavingContact(failedContactIndex, contactsSaved), innerException)
		{
			this.failedContactIndex = failedContactIndex;
			this.contactsSaved = contactsSaved;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005318 File Offset: 0x00003518
		protected InternalErrorSavingContactException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedContactIndex = (int)info.GetValue("failedContactIndex", typeof(int));
			this.contactsSaved = (int)info.GetValue("contactsSaved", typeof(int));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000536D File Offset: 0x0000356D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failedContactIndex", this.failedContactIndex);
			info.AddValue("contactsSaved", this.contactsSaved);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005399 File Offset: 0x00003599
		public int FailedContactIndex
		{
			get
			{
				return this.failedContactIndex;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000053A1 File Offset: 0x000035A1
		public int ContactsSaved
		{
			get
			{
				return this.contactsSaved;
			}
		}

		// Token: 0x040000E0 RID: 224
		private readonly int failedContactIndex;

		// Token: 0x040000E1 RID: 225
		private readonly int contactsSaved;
	}
}
