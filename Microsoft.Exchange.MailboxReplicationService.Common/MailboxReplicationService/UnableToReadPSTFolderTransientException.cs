using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000358 RID: 856
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadPSTFolderTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002675 RID: 9845 RVA: 0x00053299 File Offset: 0x00051499
		public UnableToReadPSTFolderTransientException(uint folderId) : base(MrsStrings.UnableToReadPSTFolder(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000532AE File Offset: 0x000514AE
		public UnableToReadPSTFolderTransientException(uint folderId, Exception innerException) : base(MrsStrings.UnableToReadPSTFolder(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000532C4 File Offset: 0x000514C4
		protected UnableToReadPSTFolderTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (uint)info.GetValue("folderId", typeof(uint));
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000532EE File Offset: 0x000514EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x00053309 File Offset: 0x00051509
		public uint FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0400105E RID: 4190
		private readonly uint folderId;
	}
}
