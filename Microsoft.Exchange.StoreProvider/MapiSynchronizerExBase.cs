using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001CB RID: 459
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MapiSynchronizerExBase : MapiUnk
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x00019380 File Offset: 0x00017580
		internal MapiSynchronizerExBase(SafeExInterfaceHandle iExchangeInterfaceHandle, MapiStore mapiStore) : base(iExchangeInterfaceHandle, null, mapiStore)
		{
			this.dataBlocks = new Queue<FastTransferBlock>();
			this.done = false;
			this.error = false;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000193A4 File Offset: 0x000175A4
		public FastTransferBlock GetBuffer(out int residualCacheSize, out bool doneInCache)
		{
			base.CheckDisposed();
			base.LockStore();
			residualCacheSize = 0;
			doneInCache = false;
			FastTransferBlock result;
			try
			{
				FastTransferBlock fastTransferBlock;
				while (!this.TryGetDataBlock(out fastTransferBlock))
				{
					if (this.done)
					{
						doneInCache = true;
						return FastTransferBlock.Done;
					}
					if (this.error)
					{
						doneInCache = true;
						return FastTransferBlock.Error;
					}
					this.ReadMoreBlocks();
				}
				if (fastTransferBlock.State == FastTransferState.Done)
				{
					this.done = true;
				}
				else if (fastTransferBlock.State == FastTransferState.Error)
				{
					this.error = true;
				}
				this.GetCacheInfo(out residualCacheSize, out doneInCache);
				result = fastTransferBlock;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x060006D0 RID: 1744
		protected abstract int GetBlocks(out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks);

		// Token: 0x060006D1 RID: 1745 RVA: 0x00019444 File Offset: 0x00017644
		private void ReadMoreBlocks()
		{
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int cBlocks = 0;
			try
			{
				int blocks = this.GetBlocks(out safeExLinkedMemoryHandle, out cBlocks);
				if (blocks != 0 && blocks != 264224)
				{
					base.ThrowIfError("Synchronization failure.", blocks);
				}
				if (blocks == 0)
				{
					this.done = true;
				}
				FastTransferBlock[] array = safeExLinkedMemoryHandle.ReadFastTransferBlockArray(cBlocks);
				foreach (FastTransferBlock item in array)
				{
					this.dataBlocks.Enqueue(item);
				}
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000194D8 File Offset: 0x000176D8
		private bool TryGetDataBlock(out FastTransferBlock fastTransferBlock)
		{
			if (this.dataBlocks.Count > 0)
			{
				fastTransferBlock = this.dataBlocks.Dequeue();
				return true;
			}
			fastTransferBlock = FastTransferBlock.Partial;
			return false;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00019508 File Offset: 0x00017708
		private void GetCacheInfo(out int cacheSize, out bool doneInCache)
		{
			cacheSize = 0;
			doneInCache = false;
			if (this.dataBlocks.Count > 0)
			{
				using (Queue<FastTransferBlock>.Enumerator enumerator = this.dataBlocks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FastTransferBlock fastTransferBlock = enumerator.Current;
						if (fastTransferBlock.Buffer != null)
						{
							cacheSize += fastTransferBlock.Buffer.Length;
						}
						if (fastTransferBlock.State == FastTransferState.Done || fastTransferBlock.State == FastTransferState.Error)
						{
							doneInCache = true;
						}
					}
					return;
				}
			}
			cacheSize = 0;
			doneInCache = (this.done || this.error);
		}

		// Token: 0x0400061E RID: 1566
		private readonly Queue<FastTransferBlock> dataBlocks;

		// Token: 0x0400061F RID: 1567
		private bool done;

		// Token: 0x04000620 RID: 1568
		private bool error;
	}
}
