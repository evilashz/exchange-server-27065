using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000357 RID: 855
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadPSTFolderPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x00053221 File Offset: 0x00051421
		public UnableToReadPSTFolderPermanentException(uint folderId) : base(MrsStrings.UnableToReadPSTFolder(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00053236 File Offset: 0x00051436
		public UnableToReadPSTFolderPermanentException(uint folderId, Exception innerException) : base(MrsStrings.UnableToReadPSTFolder(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0005324C File Offset: 0x0005144C
		protected UnableToReadPSTFolderPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (uint)info.GetValue("folderId", typeof(uint));
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00053276 File Offset: 0x00051476
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x00053291 File Offset: 0x00051491
		public uint FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0400105D RID: 4189
		private readonly uint folderId;
	}
}
