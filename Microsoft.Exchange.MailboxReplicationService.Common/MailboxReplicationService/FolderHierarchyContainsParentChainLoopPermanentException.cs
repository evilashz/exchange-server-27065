using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C3 RID: 707
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyContainsParentChainLoopPermanentException : FolderHierarchyIsInconsistentPermanentException
	{
		// Token: 0x06002393 RID: 9107 RVA: 0x0004EC25 File Offset: 0x0004CE25
		public FolderHierarchyContainsParentChainLoopPermanentException(string folderIdStr) : base(MrsStrings.FolderHierarchyContainsParentChainLoop(folderIdStr))
		{
			this.folderIdStr = folderIdStr;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0004EC3A File Offset: 0x0004CE3A
		public FolderHierarchyContainsParentChainLoopPermanentException(string folderIdStr, Exception innerException) : base(MrsStrings.FolderHierarchyContainsParentChainLoop(folderIdStr), innerException)
		{
			this.folderIdStr = folderIdStr;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0004EC50 File Offset: 0x0004CE50
		protected FolderHierarchyContainsParentChainLoopPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderIdStr = (string)info.GetValue("folderIdStr", typeof(string));
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0004EC7A File Offset: 0x0004CE7A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderIdStr", this.folderIdStr);
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0004EC95 File Offset: 0x0004CE95
		public string FolderIdStr
		{
			get
			{
				return this.folderIdStr;
			}
		}

		// Token: 0x04000FD0 RID: 4048
		private readonly string folderIdStr;
	}
}
