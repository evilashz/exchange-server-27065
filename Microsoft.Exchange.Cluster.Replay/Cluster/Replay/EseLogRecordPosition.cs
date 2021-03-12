using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000EB RID: 235
	internal class EseLogRecordPosition
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002D96A File Offset: 0x0002BB6A
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0002D972 File Offset: 0x0002BB72
		public EseLogPos LogPos { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002D97B File Offset: 0x0002BB7B
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0002D983 File Offset: 0x0002BB83
		public int LogRecordLength { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002D98C File Offset: 0x0002BB8C
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0002D994 File Offset: 0x0002BB94
		public int LogSectorSize { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002D99D File Offset: 0x0002BB9D
		public int ByteOffsetToStartOfRec
		{
			get
			{
				if (this.LogPos == null)
				{
					return 0;
				}
				return this.LogPos.ToBytePos(this.LogSectorSize);
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002D9BA File Offset: 0x0002BBBA
		public override string ToString()
		{
			return string.Format("LgPos=0x{0},RecLen=0x{1:X},SectorSize=0x{2:X}", this.LogPos, this.LogRecordLength, this.LogSectorSize);
		}
	}
}
