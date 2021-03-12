using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E1C RID: 3612
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderSyncStateInfo : SyncStateInfo
	{
		// Token: 0x06007CF3 RID: 31987 RVA: 0x00228A37 File Offset: 0x00226C37
		public FolderSyncStateInfo() : this(null)
		{
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x00228A40 File Offset: 0x00226C40
		public FolderSyncStateInfo(string uniqueName)
		{
			this.uniqueName = uniqueName;
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06007CF5 RID: 31989 RVA: 0x00228A4F File Offset: 0x00226C4F
		public override int Version
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06007CF6 RID: 31990 RVA: 0x00228A52 File Offset: 0x00226C52
		// (set) Token: 0x06007CF7 RID: 31991 RVA: 0x00228A5A File Offset: 0x00226C5A
		public override string UniqueName
		{
			get
			{
				return this.uniqueName;
			}
			set
			{
				this.uniqueName = value;
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06007CF8 RID: 31992 RVA: 0x00228A63 File Offset: 0x00226C63
		public override SaveMode SaveModeForSyncState
		{
			get
			{
				return SaveMode.FailOnAnyConflict;
			}
		}

		// Token: 0x04005568 RID: 21864
		private string uniqueName;
	}
}
