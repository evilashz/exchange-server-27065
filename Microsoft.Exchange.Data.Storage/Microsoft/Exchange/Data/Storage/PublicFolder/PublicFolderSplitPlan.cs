using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200094E RID: 2382
	[KnownType(typeof(SplitPlanFolder))]
	[DataContract]
	[Serializable]
	public sealed class PublicFolderSplitPlan : IPublicFolderSplitPlan, IExtensibleDataObject
	{
		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x0016CA78 File Offset: 0x0016AC78
		// (set) Token: 0x0600589E RID: 22686 RVA: 0x0016CA80 File Offset: 0x0016AC80
		[DataMember]
		public List<SplitPlanFolder> FoldersToSplit { get; set; }

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x0016CA89 File Offset: 0x0016AC89
		// (set) Token: 0x060058A0 RID: 22688 RVA: 0x0016CA91 File Offset: 0x0016AC91
		[DataMember]
		public ulong TotalSizeOccupied { get; set; }

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0016CA9A File Offset: 0x0016AC9A
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x0016CAA2 File Offset: 0x0016ACA2
		[DataMember]
		public ulong TotalSizeToSplit { get; set; }

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0016CAAB File Offset: 0x0016ACAB
		// (set) Token: 0x060058A4 RID: 22692 RVA: 0x0016CAB3 File Offset: 0x0016ACB3
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
