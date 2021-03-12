using System;
using System.Diagnostics.Tracing;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E2 RID: 1250
	[EventSource(Name = "Microsoft-DotNETRuntime-PinnableBufferCache")]
	internal sealed class PinnableBufferCacheEventSource : EventSource
	{
		// Token: 0x06003BBA RID: 15290 RVA: 0x000E1208 File Offset: 0x000DF408
		[Event(1, Level = EventLevel.Verbose)]
		public void DebugMessage(string message)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(1, message);
			}
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x000E121A File Offset: 0x000DF41A
		[Event(2, Level = EventLevel.Verbose)]
		public void DebugMessage1(string message, long value)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(2, message, value);
			}
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x000E122D File Offset: 0x000DF42D
		[Event(3, Level = EventLevel.Verbose)]
		public void DebugMessage2(string message, long value1, long value2)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(3, new object[]
				{
					message,
					value1,
					value2
				});
			}
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000E125A File Offset: 0x000DF45A
		[Event(18, Level = EventLevel.Verbose)]
		public void DebugMessage3(string message, long value1, long value2, long value3)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(18, new object[]
				{
					message,
					value1,
					value2,
					value3
				});
			}
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000E1292 File Offset: 0x000DF492
		[Event(4)]
		public void Create(string cacheName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(4, cacheName);
			}
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000E12A4 File Offset: 0x000DF4A4
		[Event(5, Level = EventLevel.Verbose)]
		public void AllocateBuffer(string cacheName, ulong objectId, int objectHash, int objectGen, int freeCountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(5, new object[]
				{
					cacheName,
					objectId,
					objectHash,
					objectGen,
					freeCountAfter
				});
			}
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000E12F0 File Offset: 0x000DF4F0
		[Event(6)]
		public void AllocateBufferFromNotGen2(string cacheName, int notGen2CountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(6, cacheName, notGen2CountAfter);
			}
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000E1303 File Offset: 0x000DF503
		[Event(7)]
		public void AllocateBufferCreatingNewBuffers(string cacheName, int totalBuffsBefore, int objectCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(7, cacheName, totalBuffsBefore, objectCount);
			}
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000E1317 File Offset: 0x000DF517
		[Event(8)]
		public void AllocateBufferAged(string cacheName, int agedCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, cacheName, agedCount);
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000E132A File Offset: 0x000DF52A
		[Event(9)]
		public void AllocateBufferFreeListEmpty(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000E133E File Offset: 0x000DF53E
		[Event(10, Level = EventLevel.Verbose)]
		public void FreeBuffer(string cacheName, ulong objectId, int objectHash, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[]
				{
					cacheName,
					objectId,
					objectHash,
					freeCountBefore
				});
			}
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000E1376 File Offset: 0x000DF576
		[Event(11)]
		public void FreeBufferStillTooYoung(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(11, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000E138A File Offset: 0x000DF58A
		[Event(13)]
		public void TrimCheck(string cacheName, int totalBuffs, bool neededMoreThanFreeList, int deltaMSec)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(13, new object[]
				{
					cacheName,
					totalBuffs,
					neededMoreThanFreeList,
					deltaMSec
				});
			}
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000E13C2 File Offset: 0x000DF5C2
		[Event(14)]
		public void TrimFree(string cacheName, int totalBuffs, int freeListCount, int toBeFreed)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(14, new object[]
				{
					cacheName,
					totalBuffs,
					freeListCount,
					toBeFreed
				});
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000E13FA File Offset: 0x000DF5FA
		[Event(15)]
		public void TrimExperiment(string cacheName, int totalBuffs, int freeListCount, int numTrimTrial)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(15, new object[]
				{
					cacheName,
					totalBuffs,
					freeListCount,
					numTrimTrial
				});
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000E1432 File Offset: 0x000DF632
		[Event(16)]
		public void TrimFreeSizeOK(string cacheName, int totalBuffs, int freeListCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, cacheName, totalBuffs, freeListCount);
			}
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000E1447 File Offset: 0x000DF647
		[Event(17)]
		public void TrimFlush(string cacheName, int totalBuffs, int freeListCount, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(17, new object[]
				{
					cacheName,
					totalBuffs,
					freeListCount,
					notGen2CountBefore
				});
			}
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000E147F File Offset: 0x000DF67F
		[Event(20)]
		public void AgePendingBuffersResults(string cacheName, int promotedToFreeListCount, int heldBackCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(20, cacheName, promotedToFreeListCount, heldBackCount);
			}
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x000E1494 File Offset: 0x000DF694
		[Event(21)]
		public void WalkFreeListResult(string cacheName, int freeListCount, int gen0BuffersInFreeList)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(21, cacheName, freeListCount, gen0BuffersInFreeList);
			}
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x000E14A9 File Offset: 0x000DF6A9
		[Event(22)]
		public void FreeBufferNull(string cacheName, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(22, cacheName, freeCountBefore);
			}
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000E14C0 File Offset: 0x000DF6C0
		internal static ulong AddressOf(object obj)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				return (ulong)PinnableBufferCacheEventSource.AddressOfByteArray(array);
			}
			return 0UL;
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000E14E0 File Offset: 0x000DF6E0
		[SecuritySafeCritical]
		internal unsafe static long AddressOfByteArray(byte[] array)
		{
			if (array == null)
			{
				return 0L;
			}
			fixed (byte* ptr = array)
			{
				return ptr - 2 * sizeof(void*);
			}
		}

		// Token: 0x0400191E RID: 6430
		public static readonly PinnableBufferCacheEventSource Log = new PinnableBufferCacheEventSource();
	}
}
