using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000047 RID: 71
	public class ReadWriteConstraints
	{
		// Token: 0x0600024F RID: 591 RVA: 0x000044FD File Offset: 0x000026FD
		public ReadWriteConstraints(WriteOptions writeOptions = null, ReadOptions readOptions = null)
		{
			this.WriteOptions = writeOptions;
			this.ReadOptions = readOptions;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00004513 File Offset: 0x00002713
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000451A File Offset: 0x0000271A
		public static ReadWriteConstraints NullConstraints { get; private set; } = new ReadWriteConstraints(null, null);

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00004522 File Offset: 0x00002722
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000452A File Offset: 0x0000272A
		public WriteOptions WriteOptions { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00004533 File Offset: 0x00002733
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000453B File Offset: 0x0000273B
		public ReadOptions ReadOptions { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00004544 File Offset: 0x00002744
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000454C File Offset: 0x0000274C
		public WriteResult WriteResult { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00004555 File Offset: 0x00002755
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000455D File Offset: 0x0000275D
		public ReadResult ReadResult { get; set; }

		// Token: 0x0600025A RID: 602 RVA: 0x00004568 File Offset: 0x00002768
		public static ReadWriteConstraints Copy(ReadWriteConstraints source)
		{
			if (source != null)
			{
				return new ReadWriteConstraints(null, null)
				{
					WriteOptions = source.WriteOptions,
					ReadOptions = source.ReadOptions
				};
			}
			return null;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000459B File Offset: 0x0000279B
		public static ReadWriteConstraints NullCheck(ReadWriteConstraints constraints)
		{
			if (constraints != null)
			{
				return constraints;
			}
			return ReadWriteConstraints.NullConstraints;
		}
	}
}
