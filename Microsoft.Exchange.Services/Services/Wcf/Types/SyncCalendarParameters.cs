using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A39 RID: 2617
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncCalendarParameters
	{
		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x00102F0D File Offset: 0x0010110D
		// (set) Token: 0x060049DB RID: 18907 RVA: 0x00102F15 File Offset: 0x00101115
		[DataMember]
		public string SyncState { get; set; }

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x00102F1E File Offset: 0x0010111E
		// (set) Token: 0x060049DD RID: 18909 RVA: 0x00102F26 File Offset: 0x00101126
		[DataMember]
		public TargetFolderId FolderId { get; set; }

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x00102F2F File Offset: 0x0010112F
		// (set) Token: 0x060049DF RID: 18911 RVA: 0x00102F37 File Offset: 0x00101137
		[DataMember]
		public string WindowStart { get; set; }

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x00102F40 File Offset: 0x00101140
		// (set) Token: 0x060049E1 RID: 18913 RVA: 0x00102F48 File Offset: 0x00101148
		[DataMember]
		public string WindowEnd { get; set; }

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00102F51 File Offset: 0x00101151
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x00102F59 File Offset: 0x00101159
		[DataMember]
		public int MaxChangesReturned { get; set; }

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00102F62 File Offset: 0x00101162
		// (set) Token: 0x060049E5 RID: 18917 RVA: 0x00102F6A File Offset: 0x0010116A
		[DataMember]
		public bool RespondWithAdditionalData { get; set; }
	}
}
