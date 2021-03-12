using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B15 RID: 2837
	[DataContract]
	public class TaskFolderActionFolderIdResponse : TaskFolderActionResponse
	{
		// Token: 0x06005085 RID: 20613 RVA: 0x00109B99 File Offset: 0x00107D99
		public TaskFolderActionFolderIdResponse(TaskFolderActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00109BA2 File Offset: 0x00107DA2
		public TaskFolderActionFolderIdResponse(FolderId folderId, ItemId taskFolderEntryId)
		{
			this.NewFolderId = folderId;
			this.NewTaskFolderEntryId = taskFolderEntryId;
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x00109BB8 File Offset: 0x00107DB8
		// (set) Token: 0x06005088 RID: 20616 RVA: 0x00109BC0 File Offset: 0x00107DC0
		[DataMember]
		public FolderId NewFolderId { get; set; }

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x00109BC9 File Offset: 0x00107DC9
		// (set) Token: 0x0600508A RID: 20618 RVA: 0x00109BD1 File Offset: 0x00107DD1
		[DataMember]
		public ItemId NewTaskFolderEntryId { get; set; }
	}
}
