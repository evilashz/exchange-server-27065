using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000897 RID: 2199
	[Serializable]
	public sealed class DatabaseSeedStatus
	{
		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x0013FDEB File Offset: 0x0013DFEB
		// (set) Token: 0x06004D2E RID: 19758 RVA: 0x0013FDF3 File Offset: 0x0013DFF3
		public int ProgressPercentage { get; private set; }

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x0013FDFC File Offset: 0x0013DFFC
		// (set) Token: 0x06004D30 RID: 19760 RVA: 0x0013FE04 File Offset: 0x0013E004
		public ByteQuantifiedSize BytesRead { get; private set; }

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06004D31 RID: 19761 RVA: 0x0013FE0D File Offset: 0x0013E00D
		// (set) Token: 0x06004D32 RID: 19762 RVA: 0x0013FE15 File Offset: 0x0013E015
		public ByteQuantifiedSize BytesWritten { get; private set; }

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06004D33 RID: 19763 RVA: 0x0013FE1E File Offset: 0x0013E01E
		// (set) Token: 0x06004D34 RID: 19764 RVA: 0x0013FE26 File Offset: 0x0013E026
		public ByteQuantifiedSize BytesReadPerSec { get; private set; }

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06004D35 RID: 19765 RVA: 0x0013FE2F File Offset: 0x0013E02F
		// (set) Token: 0x06004D36 RID: 19766 RVA: 0x0013FE37 File Offset: 0x0013E037
		public ByteQuantifiedSize BytesWrittenPerSec { get; private set; }

		// Token: 0x06004D37 RID: 19767 RVA: 0x0013FE40 File Offset: 0x0013E040
		internal DatabaseSeedStatus(int percent, long kbytesRead, long kbytesWritten, float kbytesReadPerSec, float kbytesWrittenPerSec)
		{
			this.ProgressPercentage = percent;
			this.BytesRead = ByteQuantifiedSize.FromKB((ulong)kbytesRead);
			this.BytesWritten = ByteQuantifiedSize.FromKB((ulong)kbytesWritten);
			this.BytesReadPerSec = ByteQuantifiedSize.FromKB((ulong)kbytesReadPerSec);
			this.BytesWrittenPerSec = ByteQuantifiedSize.FromKB((ulong)kbytesWrittenPerSec);
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0013FE90 File Offset: 0x0013E090
		public override string ToString()
		{
			return string.Format("{0}:{1}; {2}:{3}; {4}:{5}; {6}:{7}; {8}:{9}", new object[]
			{
				Strings.DatabaseSeedStatusLabelPercentage,
				this.ProgressPercentage,
				Strings.DatabaseSeedStatusLabelRead,
				this.BytesRead.ToString("a"),
				Strings.DatabaseSeedStatusLabelWritten,
				this.BytesWritten.ToString("a"),
				Strings.DatabaseSeedStatusLabelReadPerSec,
				this.BytesReadPerSec.ToString("a"),
				Strings.DatabaseSeedStatusLabelWrittenPerSec,
				this.BytesWrittenPerSec.ToString("a")
			});
		}

		// Token: 0x04002E1C RID: 11804
		private const string ToStringFormatStr = "{0}:{1}; {2}:{3}; {4}:{5}; {6}:{7}; {8}:{9}";
	}
}
