using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Storage.FolderTask
{
	// Token: 0x02000961 RID: 2401
	[KnownType(typeof(FolderInfo))]
	[DataContract]
	[Serializable]
	public sealed class FolderMove : IFolderMove
	{
		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x060058F0 RID: 22768 RVA: 0x0016DD28 File Offset: 0x0016BF28
		// (set) Token: 0x060058F1 RID: 22769 RVA: 0x0016DD30 File Offset: 0x0016BF30
		[DataMember]
		public List<FolderInfo> CandidateFolders { get; private set; }

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x0016DD39 File Offset: 0x0016BF39
		// (set) Token: 0x060058F3 RID: 22771 RVA: 0x0016DD41 File Offset: 0x0016BF41
		public ulong TotalSizeOccupiedByTarget { get; private set; }

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x0016DD4A File Offset: 0x0016BF4A
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x0016DD52 File Offset: 0x0016BF52
		[DataMember]
		public ulong TotalSizeToMove { get; private set; }

		// Token: 0x060058F6 RID: 22774 RVA: 0x0016DD5B File Offset: 0x0016BF5B
		public FolderMove(List<FolderInfo> candidateFolders, ulong totalSizeOccupiedByTarget, ulong totalSizeToMove)
		{
			this.CandidateFolders = candidateFolders;
			this.TotalSizeOccupiedByTarget = totalSizeOccupiedByTarget;
			this.TotalSizeToMove = totalSizeToMove;
		}
	}
}
