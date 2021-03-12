using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C5 RID: 709
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderAliasIsInvalidPermanentException : FolderFilterPermanentException
	{
		// Token: 0x0600239D RID: 9117 RVA: 0x0004ED15 File Offset: 0x0004CF15
		public FolderAliasIsInvalidPermanentException(string folderAlias) : base(MrsStrings.FolderAliasIsInvalid(folderAlias))
		{
			this.folderAlias = folderAlias;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0004ED2A File Offset: 0x0004CF2A
		public FolderAliasIsInvalidPermanentException(string folderAlias, Exception innerException) : base(MrsStrings.FolderAliasIsInvalid(folderAlias), innerException)
		{
			this.folderAlias = folderAlias;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0004ED40 File Offset: 0x0004CF40
		protected FolderAliasIsInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderAlias = (string)info.GetValue("folderAlias", typeof(string));
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0004ED6A File Offset: 0x0004CF6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderAlias", this.folderAlias);
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0004ED85 File Offset: 0x0004CF85
		public string FolderAlias
		{
			get
			{
				return this.folderAlias;
			}
		}

		// Token: 0x04000FD2 RID: 4050
		private readonly string folderAlias;
	}
}
