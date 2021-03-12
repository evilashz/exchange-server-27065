using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000957 RID: 2391
	[DataContract]
	[Serializable]
	public sealed class SplitPlanFolder
	{
		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x0016CFC8 File Offset: 0x0016B1C8
		// (set) Token: 0x060058C1 RID: 22721 RVA: 0x0016CFD0 File Offset: 0x0016B1D0
		[DataMember]
		public PublicFolderId PublicFolderId { get; set; }

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x0016CFD9 File Offset: 0x0016B1D9
		// (set) Token: 0x060058C3 RID: 22723 RVA: 0x0016CFE1 File Offset: 0x0016B1E1
		[DataMember]
		public ulong ContentSize { get; set; }
	}
}
