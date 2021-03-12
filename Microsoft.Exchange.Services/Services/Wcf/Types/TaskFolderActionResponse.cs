using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B13 RID: 2835
	[DataContract]
	public class TaskFolderActionResponse
	{
		// Token: 0x0600507C RID: 20604 RVA: 0x00109B39 File Offset: 0x00107D39
		public TaskFolderActionResponse(TaskFolderActionError errorCode)
		{
			this.WasSuccessful = false;
			this.ErrorCode = errorCode;
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00109B4F File Offset: 0x00107D4F
		public TaskFolderActionResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x00109B5E File Offset: 0x00107D5E
		// (set) Token: 0x0600507F RID: 20607 RVA: 0x00109B66 File Offset: 0x00107D66
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x00109B6F File Offset: 0x00107D6F
		// (set) Token: 0x06005081 RID: 20609 RVA: 0x00109B77 File Offset: 0x00107D77
		[DataMember]
		public TaskFolderActionError ErrorCode { get; set; }
	}
}
