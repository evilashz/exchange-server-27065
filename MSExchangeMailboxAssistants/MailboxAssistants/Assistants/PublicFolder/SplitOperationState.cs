using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000181 RID: 385
	[DataContract]
	internal sealed class SplitOperationState : ISplitOperationState, IExtensibleDataObject
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0005BA2D File Offset: 0x00059C2D
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0005BA35 File Offset: 0x00059C35
		[DataMember]
		public DateTime StartTime { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0005BA3E File Offset: 0x00059C3E
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0005BA46 File Offset: 0x00059C46
		[DataMember]
		public DateTime CompletedTime { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0005BA4F File Offset: 0x00059C4F
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0005BA57 File Offset: 0x00059C57
		[DataMember]
		public Exception Error { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0005BA60 File Offset: 0x00059C60
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0005BA68 File Offset: 0x00059C68
		[DataMember]
		public string ErrorDetails { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0005BA71 File Offset: 0x00059C71
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0005BA79 File Offset: 0x00059C79
		[DataMember]
		public bool PartialStep { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0005BA82 File Offset: 0x00059C82
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0005BA8A File Offset: 0x00059C8A
		[DataMember]
		public byte PartialStepCount { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0005BA93 File Offset: 0x00059C93
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x0005BA9B File Offset: 0x00059C9B
		[DataMember]
		public byte RetryCount { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0005BAA4 File Offset: 0x00059CA4
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x0005BAAC File Offset: 0x00059CAC
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
