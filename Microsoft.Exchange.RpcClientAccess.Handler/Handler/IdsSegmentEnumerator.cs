using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IdsSegmentEnumerator : SegmentEnumerator
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00014AA5 File Offset: 0x00012CA5
		public IdsSegmentEnumerator(StoreObjectId[] objectIds, int segmentSize) : base(segmentSize)
		{
			Util.ThrowOnNullArgument(objectIds, "objectIds");
			this.objectIds = objectIds;
			this.idsIndex = 0;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public override StoreObjectId[] GetNextBatchIds()
		{
			if (this.idsIndex >= this.objectIds.Length)
			{
				return Array<StoreObjectId>.Empty;
			}
			if (this.idsIndex == 0 && this.objectIds.Length <= base.SegmentSize)
			{
				this.idsIndex += this.objectIds.Length;
				return this.objectIds;
			}
			int val = this.objectIds.Length - this.idsIndex;
			int num = Math.Min(val, base.SegmentSize);
			StoreObjectId[] array = Array<StoreObjectId>.New(num);
			Array.Copy(this.objectIds, this.idsIndex, array, 0, num);
			this.idsIndex += num;
			return array;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00014B66 File Offset: 0x00012D66
		public int Count
		{
			get
			{
				return this.objectIds.Length;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00014B70 File Offset: 0x00012D70
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IdsSegmentEnumerator>(this);
		}

		// Token: 0x040000CB RID: 203
		private readonly StoreObjectId[] objectIds;

		// Token: 0x040000CC RID: 204
		private int idsIndex;
	}
}
