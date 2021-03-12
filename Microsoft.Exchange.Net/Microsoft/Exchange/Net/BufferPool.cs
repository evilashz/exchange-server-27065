using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200000E RID: 14
	public class BufferPool
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002A19 File Offset: 0x00000C19
		public BufferPool(int bufferSize) : this(bufferSize, 20, true, false)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A26 File Offset: 0x00000C26
		public BufferPool(int bufferSize, int maxBufferCountPerProcessor) : this(bufferSize, maxBufferCountPerProcessor, true, false)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A32 File Offset: 0x00000C32
		public BufferPool(int bufferSize, bool cleanBufferOnRelease) : this(bufferSize, 20, cleanBufferOnRelease, false)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A3F File Offset: 0x00000C3F
		public BufferPool(int bufferSize, int maxBufferCountPerProcessor, bool cleanBufferOnRelease) : this(bufferSize, maxBufferCountPerProcessor, cleanBufferOnRelease, false)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A4C File Offset: 0x00000C4C
		public BufferPool(int bufferSize, int maxBufferCountPerProcessor, bool cleanBufferOnRelease, bool enablePoolSharing)
		{
			if (bufferSize > 1048576)
			{
				throw new ArgumentOutOfRangeException("bufferSize", string.Format(CultureInfo.InvariantCulture, NetException.LargeBuffer, new object[]
				{
					bufferSize,
					1048576
				}));
			}
			if (bufferSize < 256)
			{
				throw new ArgumentOutOfRangeException("bufferSize", string.Format(CultureInfo.InvariantCulture, NetException.SmallBuffer, new object[]
				{
					bufferSize,
					256
				}));
			}
			if (maxBufferCountPerProcessor < 1)
			{
				throw new ArgumentOutOfRangeException("maxBufferCountPerProcessor", string.Format(CultureInfo.InvariantCulture, NetException.InvalidMaxBufferCount, new object[0]));
			}
			this.countLimit = Math.Min(maxBufferCountPerProcessor, 4194304 / bufferSize);
			this.bufferSize = bufferSize;
			this.cleanBufferOnRelease = cleanBufferOnRelease;
			this.enablePoolSharing = (bufferSize >= 92160 || enablePoolSharing);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B5D File Offset: 0x00000D5D
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002B64 File Offset: 0x00000D64
		public static bool EnableReleaseTracking
		{
			get
			{
				return BufferPool.enableReleaseTracking;
			}
			set
			{
				BufferPool.enableReleaseTracking = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B6C File Offset: 0x00000D6C
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B74 File Offset: 0x00000D74
		public byte[] Acquire()
		{
			int currentProcessor = BufferPool.NativeMethods.CurrentProcessor;
			Stack<byte[]> stack = this.GetPool(currentProcessor);
			lock (stack)
			{
				if (stack.Count > 0)
				{
					return stack.Pop();
				}
			}
			if (this.enablePoolSharing)
			{
				for (int i = 1; i < this.pool.Length; i++)
				{
					stack = this.GetPool(currentProcessor + i);
					lock (stack)
					{
						if (stack.Count > 0)
						{
							return stack.Pop();
						}
					}
				}
			}
			return new byte[this.bufferSize];
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C40 File Offset: 0x00000E40
		public void Release(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length != this.bufferSize)
			{
				throw new ArgumentException(NetException.BufferMismatch, "buffer");
			}
			if (this.cleanBufferOnRelease)
			{
				Array.Clear(buffer, 0, this.bufferSize);
			}
			if (BufferPool.EnableReleaseTracking)
			{
				lock (this)
				{
					if (this.recentRelease == null)
					{
						this.recentRelease = new Queue<byte[]>(1024);
					}
					if (this.recentReleaseStacks == null)
					{
						this.recentReleaseStacks = new Dictionary<byte[], StackTrace>();
					}
					if (this.recentRelease.Contains(buffer))
					{
						throw new InvalidOperationException("Buffer is released twice! Originally released\n" + this.recentReleaseStacks[buffer].ToString() + "\nSecond release is\n" + new StackTrace(true).ToString());
					}
					this.recentRelease.Enqueue(buffer);
					this.recentReleaseStacks.Add(buffer, new StackTrace(true));
					if (this.recentRelease.Count <= 1000)
					{
						return;
					}
					buffer = this.recentRelease.Dequeue();
					this.recentReleaseStacks.Remove(buffer);
				}
			}
			int currentProcessor = BufferPool.NativeMethods.CurrentProcessor;
			Stack<byte[]> stack = this.GetPool(currentProcessor);
			if (stack.Count < this.countLimit)
			{
				lock (stack)
				{
					if (stack.Count < this.countLimit)
					{
						stack.Push(buffer);
						buffer = null;
					}
				}
			}
			if (buffer != null && this.enablePoolSharing)
			{
				for (int i = 1; i < this.pool.Length; i++)
				{
					stack = this.GetPool(currentProcessor + i);
					lock (stack)
					{
						if (stack.Count < this.countLimit)
						{
							stack.Push(buffer);
							buffer = null;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E48 File Offset: 0x00001048
		private Stack<byte[]> GetPool(int index)
		{
			index %= this.pool.Length;
			if (this.pool[index] == null)
			{
				lock (this.initializationLock)
				{
					if (this.pool[index] == null)
					{
						this.pool[index] = new Stack<byte[]>(this.countLimit);
					}
				}
			}
			return this.pool[index];
		}

		// Token: 0x0400001A RID: 26
		public const int MinBufferSize = 256;

		// Token: 0x0400001B RID: 27
		public const int MaxBufferSize = 1048576;

		// Token: 0x0400001C RID: 28
		public const int DefaultMaxStackDepth = 20;

		// Token: 0x0400001D RID: 29
		private const int MinBufferSizeToUseSharing = 92160;

		// Token: 0x0400001E RID: 30
		private static bool enableReleaseTracking;

		// Token: 0x0400001F RID: 31
		private readonly int countLimit;

		// Token: 0x04000020 RID: 32
		private readonly int bufferSize;

		// Token: 0x04000021 RID: 33
		private readonly bool cleanBufferOnRelease;

		// Token: 0x04000022 RID: 34
		private readonly bool enablePoolSharing;

		// Token: 0x04000023 RID: 35
		private readonly object initializationLock = new object();

		// Token: 0x04000024 RID: 36
		private Stack<byte[]>[] pool = new Stack<byte[]>[4];

		// Token: 0x04000025 RID: 37
		private Queue<byte[]> recentRelease;

		// Token: 0x04000026 RID: 38
		private Dictionary<byte[], StackTrace> recentReleaseStacks;

		// Token: 0x0200000F RID: 15
		internal static class NativeMethods
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00002EC2 File Offset: 0x000010C2
			internal static int CurrentProcessor
			{
				get
				{
					return (int)BufferPool.NativeMethods.GetCurrentProcessorNumber();
				}
			}

			// Token: 0x06000051 RID: 81
			[DllImport("kernel32.dll")]
			private static extern uint GetCurrentProcessorNumber();
		}
	}
}
