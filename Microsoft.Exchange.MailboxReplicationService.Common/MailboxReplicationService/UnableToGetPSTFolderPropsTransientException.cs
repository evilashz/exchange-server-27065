using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000356 RID: 854
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTFolderPropsTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600266B RID: 9835 RVA: 0x000531A9 File Offset: 0x000513A9
		public UnableToGetPSTFolderPropsTransientException(uint folderId) : base(MrsStrings.UnableToGetPSTFolderProps(folderId))
		{
			this.folderId = folderId;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000531BE File Offset: 0x000513BE
		public UnableToGetPSTFolderPropsTransientException(uint folderId, Exception innerException) : base(MrsStrings.UnableToGetPSTFolderProps(folderId), innerException)
		{
			this.folderId = folderId;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000531D4 File Offset: 0x000513D4
		protected UnableToGetPSTFolderPropsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderId = (uint)info.GetValue("folderId", typeof(uint));
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000531FE File Offset: 0x000513FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderId", this.folderId);
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x00053219 File Offset: 0x00051419
		public uint FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0400105C RID: 4188
		private readonly uint folderId;
	}
}
