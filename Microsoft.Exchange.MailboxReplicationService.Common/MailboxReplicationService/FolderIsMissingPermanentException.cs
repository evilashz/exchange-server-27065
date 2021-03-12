using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C7 RID: 711
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderIsMissingPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x0004EE59 File Offset: 0x0004D059
		public FolderIsMissingPermanentException(string folderPath) : base(MrsStrings.FolderIsMissing(folderPath))
		{
			this.folderPath = folderPath;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0004EE6E File Offset: 0x0004D06E
		public FolderIsMissingPermanentException(string folderPath, Exception innerException) : base(MrsStrings.FolderIsMissing(folderPath), innerException)
		{
			this.folderPath = folderPath;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0004EE84 File Offset: 0x0004D084
		protected FolderIsMissingPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderPath = (string)info.GetValue("folderPath", typeof(string));
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0004EEAE File Offset: 0x0004D0AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderPath", this.folderPath);
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0004EEC9 File Offset: 0x0004D0C9
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x04000FD5 RID: 4053
		private readonly string folderPath;
	}
}
