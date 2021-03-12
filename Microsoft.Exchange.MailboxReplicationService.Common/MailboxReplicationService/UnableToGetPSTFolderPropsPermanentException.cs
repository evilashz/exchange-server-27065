using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000355 RID: 853
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTFolderPropsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002666 RID: 9830 RVA: 0x00053131 File Offset: 0x00051331
		public UnableToGetPSTFolderPropsPermanentException(uint folderId) : base(MrsStrings.UnableToGetPSTFolderProps(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00053146 File Offset: 0x00051346
		public UnableToGetPSTFolderPropsPermanentException(uint folderId, Exception innerException) : base(MrsStrings.UnableToGetPSTFolderProps(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x0005315C File Offset: 0x0005135C
		protected UnableToGetPSTFolderPropsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (uint)info.GetValue("folderId", typeof(uint));
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00053186 File Offset: 0x00051386
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000531A1 File Offset: 0x000513A1
		public uint FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0400105B RID: 4187
		private readonly uint folderId;
	}
}
