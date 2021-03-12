using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F6 RID: 758
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderAlreadyInTargetPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x000502CB File Offset: 0x0004E4CB
		public FolderAlreadyInTargetPermanentException(string folderId) : base(MrsStrings.FolderAlreadyInTarget(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000502E0 File Offset: 0x0004E4E0
		public FolderAlreadyInTargetPermanentException(string folderId, Exception innerException) : base(MrsStrings.FolderAlreadyInTarget(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000502F6 File Offset: 0x0004E4F6
		protected FolderAlreadyInTargetPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (string)info.GetValue("folderId", typeof(string));
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00050320 File Offset: 0x0004E520
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x0005033B File Offset: 0x0004E53B
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x04000FFC RID: 4092
		private readonly string folderId;
	}
}
