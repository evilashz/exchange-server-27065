using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F3 RID: 755
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderCopyFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x000501AC File Offset: 0x0004E3AC
		public FolderCopyFailedPermanentException(string folderName) : base(MrsStrings.FolderCopyFailed(folderName))
		{
			this.folderName = folderName;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000501C1 File Offset: 0x0004E3C1
		public FolderCopyFailedPermanentException(string folderName, Exception innerException) : base(MrsStrings.FolderCopyFailed(folderName), innerException)
		{
			this.folderName = folderName;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000501D7 File Offset: 0x0004E3D7
		protected FolderCopyFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderName = (string)info.GetValue("folderName", typeof(string));
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00050201 File Offset: 0x0004E401
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderName", this.folderName);
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x0005021C File Offset: 0x0004E41C
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x04000FFA RID: 4090
		private readonly string folderName;
	}
}
