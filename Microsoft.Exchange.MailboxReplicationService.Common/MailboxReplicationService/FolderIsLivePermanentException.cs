using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C8 RID: 712
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderIsLivePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x0004EED1 File Offset: 0x0004D0D1
		public FolderIsLivePermanentException(string folderName) : base(MrsStrings.FolderIsLive(folderName))
		{
			this.folderName = folderName;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0004EEE6 File Offset: 0x0004D0E6
		public FolderIsLivePermanentException(string folderName, Exception innerException) : base(MrsStrings.FolderIsLive(folderName), innerException)
		{
			this.folderName = folderName;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0004EEFC File Offset: 0x0004D0FC
		protected FolderIsLivePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderName = (string)info.GetValue("folderName", typeof(string));
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x0004EF26 File Offset: 0x0004D126
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderName", this.folderName);
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x0004EF41 File Offset: 0x0004D141
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x04000FD6 RID: 4054
		private readonly string folderName;
	}
}
