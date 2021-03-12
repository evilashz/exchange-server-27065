using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD5 RID: 3029
	internal sealed class BufferManager
	{
		// Token: 0x0600418F RID: 16783 RVA: 0x000AE0E8 File Offset: 0x000AC2E8
		public BufferManager(int bufferSize, int fragmentSize)
		{
			if (bufferSize % 8 != 0)
			{
				throw new ArgumentException(NetException.MultipleOfAlignmentFactor, "bufferSize");
			}
			this.bufferSize = bufferSize;
			fragmentSize = Math.Max(fragmentSize, 131072);
			this.buffersPerFragment = (fragmentSize - 4) / (8 + bufferSize) + 1;
			this.fragmentSize = 4 + this.buffersPerFragment * (8 + bufferSize);
			this.freePool = new Queue(this.buffersPerFragment);
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000AE167 File Offset: 0x000AC367
		public int MaxBufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000AE170 File Offset: 0x000AC370
		public int Alloc(out byte[] fragment, out int offset)
		{
			int num;
			lock (this.freePool)
			{
				if (this.freePool.Count == 0)
				{
					this.AllocateNewFragment();
				}
				num = (int)this.freePool.Dequeue();
			}
			this.GetBufferAndOffset(num, out fragment, out offset);
			this.CheckBuffer(fragment, offset, 252);
			this.ApplyBufferSignature(fragment, offset, 172);
			return num;
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000AE1F8 File Offset: 0x000AC3F8
		public void Free(int bufferToFree)
		{
			if (bufferToFree == -1)
			{
				throw new ArgumentOutOfRangeException("bufferToFree");
			}
			byte[] array;
			int num;
			this.GetBufferAndOffset(bufferToFree, out array, out num);
			this.CheckBuffer(array, num, 172);
			this.ApplyBufferSignature(array, num, 252);
			lock (this.freePool)
			{
				this.freePool.Enqueue(bufferToFree);
			}
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000AE278 File Offset: 0x000AC478
		public void GetBufferAndOffset(int bufferIndex, out byte[] buffer, out int offset)
		{
			buffer = this.fragmentList[bufferIndex / this.buffersPerFragment];
			int num = bufferIndex % this.buffersPerFragment;
			offset = 8 + num * (8 + this.bufferSize);
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000AE2AD File Offset: 0x000AC4AD
		public void CheckBuffer(byte[] fragment, int offsetToBuffer, byte signatureByte)
		{
			if (offsetToBuffer < 8 || offsetToBuffer > this.fragmentSize - 8 - 4 || offsetToBuffer % 8 != 0)
			{
				throw new ArgumentException(NetException.BadOffset, "offsetToBuffer");
			}
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000AE2DC File Offset: 0x000AC4DC
		private void AllocateNewFragment()
		{
			int num = this.FindNextAvailableFragment();
			BufferManager.ApplyFragmentSignature(this.fragmentList[num]);
			int num2 = 8;
			for (int i = 0; i < this.buffersPerFragment; i++)
			{
				this.freePool.Enqueue(num * this.buffersPerFragment + i);
				this.ApplyBufferSignature(this.fragmentList[num], num2, 252);
				num2 += this.bufferSize + 8;
			}
			if (this.freePool.Count == 0)
			{
				throw new InvalidOperationException(NetException.CouldNotAllocateFragment);
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000AE368 File Offset: 0x000AC568
		private int FindNextAvailableFragment()
		{
			int i;
			for (i = 0; i < this.fragmentList.Length; i++)
			{
				if (this.fragmentList[i] == null)
				{
					this.fragmentList[i] = new byte[this.fragmentSize];
					return i;
				}
			}
			byte[][] destinationArray = new byte[this.fragmentList.Length + 16][];
			Array.Copy(this.fragmentList, destinationArray, this.fragmentList.Length);
			i = this.fragmentList.Length;
			this.fragmentList = destinationArray;
			this.fragmentList[i] = new byte[this.fragmentSize];
			return i;
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000AE3F0 File Offset: 0x000AC5F0
		private static void ApplyFragmentSignature(byte[] fragment)
		{
			for (int i = 0; i < 4; i++)
			{
				fragment[i] = BufferManager.FragmentSignature[i];
			}
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000AE414 File Offset: 0x000AC614
		private void ApplyBufferSignature(byte[] buffer, int offset, byte signature)
		{
			for (int i = 0; i < 4; i++)
			{
				buffer[offset - 4 + i] = signature;
				buffer[offset + this.bufferSize + i] = signature;
			}
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000AE444 File Offset: 0x000AC644
		[Conditional("DEBUG")]
		private static void VerifyFragmentSignature(byte[] buffer)
		{
			for (int i = 0; i < 4; i++)
			{
				if (buffer[i] != BufferManager.FragmentSignature[i])
				{
					ExTraceGlobals.NetworkTracer.Information<byte, byte, int>(0L, "Fragment signature does not match. Found {0:x}, expected {1:x} at location {2}", buffer[i], BufferManager.FragmentSignature[i], i);
					throw new InvalidOperationException(NetException.SignatureDoesNotMatch);
				}
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000AE498 File Offset: 0x000AC698
		[Conditional("DEBUG")]
		private static void VerifySignature(byte[] buffer, int offset, int length, byte signature)
		{
			for (int i = 0; i < length; i++)
			{
				if (buffer[offset + i] != signature)
				{
					ExTraceGlobals.NetworkTracer.Information(0L, "Signature does not match. Found 0x{0:x}, expected 0x{1:x} at location {2}+{3}", new object[]
					{
						buffer[offset + i],
						signature,
						offset,
						i
					});
					throw new InvalidOperationException(NetException.SignatureDoesNotMatch);
				}
			}
		}

		// Token: 0x04003865 RID: 14437
		public const int InvalidBufferIndex = -1;

		// Token: 0x04003866 RID: 14438
		private const int MinFragmentSize = 131072;

		// Token: 0x04003867 RID: 14439
		private const int FragmentListGrowFactor = 16;

		// Token: 0x04003868 RID: 14440
		private const int AlignmentFactor = 8;

		// Token: 0x04003869 RID: 14441
		private const int SignatureLength = 4;

		// Token: 0x0400386A RID: 14442
		private const byte GuardByteInUse = 172;

		// Token: 0x0400386B RID: 14443
		private const byte GuardByteFree = 252;

		// Token: 0x0400386C RID: 14444
		private readonly int bufferSize;

		// Token: 0x0400386D RID: 14445
		private readonly int fragmentSize;

		// Token: 0x0400386E RID: 14446
		private readonly int buffersPerFragment;

		// Token: 0x0400386F RID: 14447
		private static readonly byte[] FragmentSignature = new byte[]
		{
			67,
			67,
			13,
			10
		};

		// Token: 0x04003870 RID: 14448
		private byte[][] fragmentList = new byte[0][];

		// Token: 0x04003871 RID: 14449
		private Queue freePool;
	}
}
