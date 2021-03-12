using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038D RID: 909
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasSyncCouldNotFindFolderException : MailboxReplicationTransientException
	{
		// Token: 0x0600277D RID: 10109 RVA: 0x00054B82 File Offset: 0x00052D82
		public EasSyncCouldNotFindFolderException(string folderId) : base(MrsStrings.EasSyncCouldNotFindFolder(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00054B97 File Offset: 0x00052D97
		public EasSyncCouldNotFindFolderException(string folderId, Exception innerException) : base(MrsStrings.EasSyncCouldNotFindFolder(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00054BAD File Offset: 0x00052DAD
		protected EasSyncCouldNotFindFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (string)info.GetValue("folderId", typeof(string));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00054BD7 File Offset: 0x00052DD7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x00054BF2 File Offset: 0x00052DF2
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x04001092 RID: 4242
		private readonly string folderId;
	}
}
