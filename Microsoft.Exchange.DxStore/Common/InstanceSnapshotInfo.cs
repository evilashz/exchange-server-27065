using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class InstanceSnapshotInfo
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000398A File Offset: 0x00001B8A
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003992 File Offset: 0x00001B92
		[DataMember]
		public bool IsCompressed { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000399B File Offset: 0x00001B9B
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000039A3 File Offset: 0x00001BA3
		[DataMember]
		public string Snapshot { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000039AC File Offset: 0x00001BAC
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000039B4 File Offset: 0x00001BB4
		[DataMember]
		public string FullKeyName { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000039BD File Offset: 0x00001BBD
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000039C5 File Offset: 0x00001BC5
		[DataMember]
		public int LastInstanceExecuted { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000039CE File Offset: 0x00001BCE
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000039D6 File Offset: 0x00001BD6
		[DataMember]
		public DateTimeOffset RetrievalStartTime { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000039DF File Offset: 0x00001BDF
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000039E7 File Offset: 0x00001BE7
		[DataMember]
		public DateTimeOffset RetrievalFinishTime { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000039F0 File Offset: 0x00001BF0
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000039F8 File Offset: 0x00001BF8
		[DataMember]
		public bool IsStale { get; set; }

		// Token: 0x060001EF RID: 495 RVA: 0x00003A04 File Offset: 0x00001C04
		public bool Compress()
		{
			lock (this.locker)
			{
				if (!this.IsCompressed)
				{
					this.Snapshot = CompressHelper.ZipToBase64String(this.Snapshot);
					this.IsCompressed = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00003A68 File Offset: 0x00001C68
		public bool Decompress()
		{
			lock (this.locker)
			{
				if (this.IsCompressed)
				{
					this.Snapshot = CompressHelper.UnzipFromBase64String(this.Snapshot);
					this.IsCompressed = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400012E RID: 302
		private readonly object locker = new object();
	}
}
