using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C4 RID: 708
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderPathIsInvalidPermanentException : FolderFilterPermanentException
	{
		// Token: 0x06002398 RID: 9112 RVA: 0x0004EC9D File Offset: 0x0004CE9D
		public FolderPathIsInvalidPermanentException(string folderPath) : base(MrsStrings.FolderPathIsInvalid(folderPath))
		{
			this.folderPath = folderPath;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0004ECB2 File Offset: 0x0004CEB2
		public FolderPathIsInvalidPermanentException(string folderPath, Exception innerException) : base(MrsStrings.FolderPathIsInvalid(folderPath), innerException)
		{
			this.folderPath = folderPath;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0004ECC8 File Offset: 0x0004CEC8
		protected FolderPathIsInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderPath = (string)info.GetValue("folderPath", typeof(string));
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0004ECF2 File Offset: 0x0004CEF2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderPath", this.folderPath);
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0004ED0D File Offset: 0x0004CF0D
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x04000FD1 RID: 4049
		private readonly string folderPath;
	}
}
