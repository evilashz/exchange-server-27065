using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002D RID: 45
	internal abstract class SegmentEnumerator : BaseObject
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00014A5F File Offset: 0x00012C5F
		protected SegmentEnumerator(int segmentSize)
		{
			if (segmentSize < 0)
			{
				throw new ArgumentOutOfRangeException("segmentSize");
			}
			this.segmentSize = segmentSize;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00014A7D File Offset: 0x00012C7D
		public static int MessageSegmentSize
		{
			get
			{
				return SegmentEnumerator.messageSegmentSize;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00014A84 File Offset: 0x00012C84
		protected int SegmentSize
		{
			get
			{
				return this.segmentSize;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00014A8C File Offset: 0x00012C8C
		public static int SetMessageSegmentSize(int messageSegmentSize)
		{
			return Interlocked.Exchange(ref SegmentEnumerator.messageSegmentSize, messageSegmentSize);
		}

		// Token: 0x0600022D RID: 557
		public abstract StoreObjectId[] GetNextBatchIds();

		// Token: 0x040000C8 RID: 200
		public const int FolderSegmentSize = 10;

		// Token: 0x040000C9 RID: 201
		private static int messageSegmentSize = 1000;

		// Token: 0x040000CA RID: 202
		private readonly int segmentSize;
	}
}
