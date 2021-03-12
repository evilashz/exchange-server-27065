using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C2 RID: 450
	internal class RecordContext
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00039A34 File Offset: 0x00037C34
		internal RecordContext()
		{
			this.Reset();
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00039A42 File Offset: 0x00037C42
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00039A4A File Offset: 0x00037C4A
		internal int TotalSeconds
		{
			get
			{
				return this.totalSeconds;
			}
			set
			{
				this.totalSeconds = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00039A53 File Offset: 0x00037C53
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00039A5B File Offset: 0x00037C5B
		internal int TotalFailures
		{
			get
			{
				return this.totalFailures;
			}
			set
			{
				this.totalFailures = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00039A64 File Offset: 0x00037C64
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00039A6C File Offset: 0x00037C6C
		internal bool Append
		{
			get
			{
				return this.append;
			}
			set
			{
				this.append = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00039A75 File Offset: 0x00037C75
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00039A7D File Offset: 0x00037C7D
		internal ITempWavFile Recording
		{
			get
			{
				return this.recording;
			}
			set
			{
				this.recording = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00039A86 File Offset: 0x00037C86
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00039A8E File Offset: 0x00037C8E
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00039A97 File Offset: 0x00037C97
		internal void Reset()
		{
			this.totalSeconds = 0;
			this.totalFailures = 0;
			this.append = false;
			this.recording = null;
			this.id = string.Empty;
		}

		// Token: 0x04000A6B RID: 2667
		private int totalSeconds;

		// Token: 0x04000A6C RID: 2668
		private int totalFailures;

		// Token: 0x04000A6D RID: 2669
		private bool append;

		// Token: 0x04000A6E RID: 2670
		private ITempWavFile recording;

		// Token: 0x04000A6F RID: 2671
		private string id;
	}
}
