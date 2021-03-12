using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Server.Storage.BlockMode
{
	// Token: 0x02000009 RID: 9
	internal class BlockModeMessageStream
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000422C File Offset: 0x0000242C
		public BlockModeMessageStream(string dbName, int maxgenToKeep, BlockModeMessageStream.FreeIOBuffers bigWriteBuffers)
		{
			this.freeBuffers = bigWriteBuffers;
			this.DatabaseName = dbName;
			this.firstMessage = new BlockModeMessageStream.BlockModeMessageBase();
			this.lastMessage = this.firstMessage;
			this.maxGenerationsToKeep = maxgenToKeep;
			this.oldestBufferOrdinalReferencedBySenders = ulong.MaxValue;
			this.appendBuffer = this.freeBuffers.Allocate(1048576);
			this.appendBuffer.LifetimeOrdinal = (this.currentBufferOrdinal += 1UL);
			this.oldestBuffer = this.appendBuffer;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000042BD File Offset: 0x000024BD
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.BlockModeMessageStreamTracer;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000042C4 File Offset: 0x000024C4
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000042CC File Offset: 0x000024CC
		public bool FoundFirstLogBoundary { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000042D5 File Offset: 0x000024D5
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000042DD File Offset: 0x000024DD
		public string DatabaseName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000042E6 File Offset: 0x000024E6
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000042EE File Offset: 0x000024EE
		public ulong OldestBufferLifetimeReferencedBySenders
		{
			get
			{
				return this.oldestBufferOrdinalReferencedBySenders;
			}
			set
			{
				if (this.oldestBufferOrdinalReferencedBySenders != value)
				{
					this.oldestBufferOrdinalReferencedBySenders = value;
					this.FreeOldBuffers();
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00004306 File Offset: 0x00002506
		public ulong LatestBufferLifetimeOrdinal
		{
			get
			{
				return this.currentBufferOrdinal;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000430E File Offset: 0x0000250E
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00004316 File Offset: 0x00002516
		public bool CompressLogData { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00004320 File Offset: 0x00002520
		public int Append(JET_EMITDATACTX emitContext, byte[] logdata, out int compressedLengthOfLogData)
		{
			int cblogdata = checked((int)emitContext.cbLogData);
			compressedLengthOfLogData = 0;
			BlockModeMessageStream.BlockModeDataMessage blockModeDataMessage = new BlockModeMessageStream.BlockModeDataMessage(emitContext);
			if (!this.FoundFirstLogBoundary)
			{
				if (!this.endOfALogFound)
				{
					if (blockModeDataMessage.IsLastForLog)
					{
						this.endOfALogFound = true;
					}
					return 0;
				}
				if (!blockModeDataMessage.HasDataBuffersFlag)
				{
					BlockModeMessageStream.Tracer.TraceDebug((long)this.GetHashCode(), "Ignoring msg since it doesn't have data");
					return 0;
				}
				this.FoundFirstLogBoundary = true;
				this.endOfALogFound = false;
				blockModeDataMessage.IsFirstForLog = true;
				BlockModeMessageStream.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Found first boundary at gen 0x{0:X}", blockModeDataMessage.LogGeneration);
			}
			else
			{
				if (this.endOfALogFound && blockModeDataMessage.HasDataBuffersFlag)
				{
					blockModeDataMessage.IsFirstForLog = true;
					this.endOfALogFound = false;
				}
				if (blockModeDataMessage.IsLastForLog)
				{
					this.endOfALogFound = true;
				}
			}
			bool compressLogData = this.CompressLogData;
			int num;
			if (compressLogData)
			{
				num = BlockModeCompressedDataMsg.CalculateWorstLength(emitContext, cblogdata);
			}
			else
			{
				num = GranularLogDataMsg.CalculateSerializedLength(emitContext, cblogdata);
			}
			BlockModeMessageStream.IOBuffer iobuffer = null;
			BlockModeMessageStream.IOBuffer iobuffer2 = this.appendBuffer;
			if (this.appendBuffer.RemainingSpace < num)
			{
				int bufSize = Math.Max(num, 1048576);
				using (LockManager.Lock(this.bufferManagementLock, LockManager.LockType.LeafMonitorLock))
				{
					iobuffer = this.freeBuffers.Allocate(bufSize);
					iobuffer.LifetimeOrdinal = (this.currentBufferOrdinal += 1UL);
				}
				BlockModeMessageStream.Tracer.TraceDebug<ulong, int>((long)this.GetHashCode(), "Buffers in use: {0} free: {1}", this.currentBufferOrdinal - this.oldestBuffer.LifetimeOrdinal + 1UL, this.freeBuffers.FreeBufferCount);
				iobuffer2 = iobuffer;
			}
			if (compressLogData)
			{
				num = BlockModeCompressedDataMsg.SerializeToBuffer(emitContext, logdata, cblogdata, iobuffer2.Buffer, iobuffer2.AppendOffset, out compressedLengthOfLogData);
			}
			else
			{
				GranularLogDataMsg.SerializeToBuffer(num, GranularLogDataMsg.Flags.None, emitContext, logdata, cblogdata, iobuffer2.Buffer, iobuffer2.AppendOffset);
			}
			blockModeDataMessage.IOBuffer = iobuffer2;
			blockModeDataMessage.MessageStartOffset = iobuffer2.AppendOffset;
			iobuffer2.AppendOffset += num;
			if (iobuffer != null)
			{
				this.appendBuffer.NextBuffer = iobuffer;
				this.appendBuffer = iobuffer;
			}
			this.lastMessage.NextMsg = blockModeDataMessage;
			this.lastMessage = blockModeDataMessage;
			if (blockModeDataMessage.IsLastForLog)
			{
				this.TrimOldGenerations(blockModeDataMessage.LogGeneration);
				this.FreeOldBuffers();
			}
			return num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000455C File Offset: 0x0000275C
		public BlockModeMessageStream.SenderPosition Join(uint firstGenToSend)
		{
			BlockModeMessageStream.SenderPosition result;
			using (LockManager.Lock(this.bufferManagementLock, LockManager.LockType.LeafMonitorLock))
			{
				BlockModeMessageStream.BlockModeMessageBase nextMsg = this.firstMessage;
				BlockModeMessageStream.BlockModeDataMessage blockModeDataMessage;
				for (;;)
				{
					blockModeDataMessage = (nextMsg as BlockModeMessageStream.BlockModeDataMessage);
					if (blockModeDataMessage != null && blockModeDataMessage.IsFirstForLog && (long)blockModeDataMessage.LogGeneration >= (long)((ulong)firstGenToSend))
					{
						break;
					}
					if (nextMsg.NextMsg == null)
					{
						goto Block_8;
					}
					nextMsg = nextMsg.NextMsg;
				}
				if ((long)blockModeDataMessage.LogGeneration == (long)((ulong)firstGenToSend))
				{
					if (this.oldestBufferOrdinalReferencedBySenders > blockModeDataMessage.IOBuffer.LifetimeOrdinal)
					{
						this.oldestBufferOrdinalReferencedBySenders = blockModeDataMessage.IOBuffer.LifetimeOrdinal;
					}
					return new BlockModeMessageStream.SenderPosition(blockModeDataMessage);
				}
				BlockModeMessageStream.Tracer.TraceError<string, int>((long)this.GetHashCode(), "JoinQ({0}) oldest Gen is 0x{1:X}", this.DatabaseName, blockModeDataMessage.LogGeneration);
				return null;
				Block_8:
				BlockModeMessageStream.Tracer.TraceError<string, uint>((long)this.GetHashCode(), "JoinQ({0}) couldn't find start point. Generations haven't arrived at 0x{1:X}", this.DatabaseName, firstGenToSend);
				result = null;
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004650 File Offset: 0x00002850
		public void FreeOldBuffers()
		{
			bool flag = false;
			using (LockManager.Lock(this.bufferManagementLock, LockManager.LockType.LeafMonitorLock))
			{
				BlockModeMessageStream.BlockModeDataMessage blockModeDataMessage = this.firstMessage as BlockModeMessageStream.BlockModeDataMessage;
				if (blockModeDataMessage != null)
				{
					ulong num = Math.Min(this.oldestBufferOrdinalReferencedBySenders, blockModeDataMessage.IOBuffer.LifetimeOrdinal);
					Globals.AssertRetail(num >= this.oldestBuffer.LifetimeOrdinal, "oldest lifetime mismatch");
					while (num > this.oldestBuffer.LifetimeOrdinal)
					{
						Globals.AssertRetail(this.appendBuffer != this.oldestBuffer, "current lifetime mismatch");
						BlockModeMessageStream.IOBuffer iobuffer = this.oldestBuffer;
						this.oldestBuffer = iobuffer.NextBuffer;
						this.freeBuffers.Free(iobuffer);
						flag = true;
					}
					if (flag)
					{
						BlockModeMessageStream.Tracer.TraceDebug<ulong, int>((long)this.GetHashCode(), "After free: Buffers in use: {0} free: {1}", this.currentBufferOrdinal - this.oldestBuffer.LifetimeOrdinal + 1UL, this.freeBuffers.FreeBufferCount);
					}
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000475C File Offset: 0x0000295C
		private void TrimOldGenerations(int curGen)
		{
			if (curGen > this.maxGenerationsToKeep)
			{
				int num = curGen - this.maxGenerationsToKeep;
				BlockModeMessageStream.BlockModeMessageBase nextMsg = this.firstMessage;
				BlockModeMessageStream.BlockModeDataMessage blockModeDataMessage;
				for (;;)
				{
					blockModeDataMessage = (nextMsg as BlockModeMessageStream.BlockModeDataMessage);
					if (blockModeDataMessage != null && blockModeDataMessage.IsFirstForLog && blockModeDataMessage.LogGeneration >= num)
					{
						break;
					}
					if (nextMsg.NextMsg == null)
					{
						goto Block_6;
					}
					nextMsg = nextMsg.NextMsg;
				}
				if (blockModeDataMessage != this.firstMessage)
				{
					BlockModeMessageStream.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "TrimOld({0}) sets first to gen 0x{1:X}", this.DatabaseName, blockModeDataMessage.LogGeneration);
					this.firstMessage = blockModeDataMessage;
					return;
				}
				return;
				Block_6:
				BlockModeMessageStream.Tracer.TraceError<string>((long)this.GetHashCode(), "TrimOld({0}) didn't find stop point", this.DatabaseName);
				return;
			}
		}

		// Token: 0x04000037 RID: 55
		public const int IOBufferSize = 1048576;

		// Token: 0x04000038 RID: 56
		private BlockModeMessageStream.IOBuffer appendBuffer;

		// Token: 0x04000039 RID: 57
		private BlockModeMessageStream.IOBuffer oldestBuffer;

		// Token: 0x0400003A RID: 58
		private ulong oldestBufferOrdinalReferencedBySenders;

		// Token: 0x0400003B RID: 59
		private ulong currentBufferOrdinal;

		// Token: 0x0400003C RID: 60
		private object bufferManagementLock = new object();

		// Token: 0x0400003D RID: 61
		private BlockModeMessageStream.FreeIOBuffers freeBuffers;

		// Token: 0x0400003E RID: 62
		private BlockModeMessageStream.BlockModeMessageBase lastMessage;

		// Token: 0x0400003F RID: 63
		private BlockModeMessageStream.BlockModeMessageBase firstMessage;

		// Token: 0x04000040 RID: 64
		private bool endOfALogFound;

		// Token: 0x04000041 RID: 65
		private int maxGenerationsToKeep;

		// Token: 0x0200000A RID: 10
		internal class IOBuffer
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00004801 File Offset: 0x00002A01
			public IOBuffer(int size, bool preAllocated)
			{
				this.AppendOffset = 0;
				this.NextBuffer = null;
				this.Buffer = new byte[size];
				this.PreAllocated = preAllocated;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000057 RID: 87 RVA: 0x0000482A File Offset: 0x00002A2A
			// (set) Token: 0x06000058 RID: 88 RVA: 0x00004832 File Offset: 0x00002A32
			public byte[] Buffer { get; private set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000059 RID: 89 RVA: 0x0000483B File Offset: 0x00002A3B
			// (set) Token: 0x0600005A RID: 90 RVA: 0x00004843 File Offset: 0x00002A43
			public ulong LifetimeOrdinal { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600005B RID: 91 RVA: 0x0000484C File Offset: 0x00002A4C
			// (set) Token: 0x0600005C RID: 92 RVA: 0x00004856 File Offset: 0x00002A56
			public int AppendOffset
			{
				get
				{
					return this.appendOffset;
				}
				set
				{
					this.appendOffset = value;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00004861 File Offset: 0x00002A61
			// (set) Token: 0x0600005E RID: 94 RVA: 0x0000486B File Offset: 0x00002A6B
			public BlockModeMessageStream.IOBuffer NextBuffer
			{
				get
				{
					return this.nextBuffer;
				}
				set
				{
					this.nextBuffer = value;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00004876 File Offset: 0x00002A76
			public int RemainingSpace
			{
				get
				{
					return this.Buffer.Length - this.AppendOffset;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00004887 File Offset: 0x00002A87
			// (set) Token: 0x06000061 RID: 97 RVA: 0x0000488F File Offset: 0x00002A8F
			public bool PreAllocated { get; private set; }

			// Token: 0x04000045 RID: 69
			private volatile int appendOffset;

			// Token: 0x04000046 RID: 70
			private volatile BlockModeMessageStream.IOBuffer nextBuffer;
		}

		// Token: 0x0200000B RID: 11
		internal class FreeIOBuffers
		{
			// Token: 0x06000062 RID: 98 RVA: 0x00004898 File Offset: 0x00002A98
			public FreeIOBuffers(int bufSize, int numberOfBuffersToPreAllocate)
			{
				this.bufferSize = bufSize;
				for (int i = 0; i < numberOfBuffersToPreAllocate; i++)
				{
					this.Free(new BlockModeMessageStream.IOBuffer(bufSize, true));
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000063 RID: 99 RVA: 0x000048CB File Offset: 0x00002ACB
			public int FreeBufferCount
			{
				get
				{
					return this.freeBufferCount;
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x000048D4 File Offset: 0x00002AD4
			public BlockModeMessageStream.IOBuffer Allocate(int bufSize)
			{
				if (bufSize != this.bufferSize)
				{
					return new BlockModeMessageStream.IOBuffer(bufSize, false);
				}
				BlockModeMessageStream.IOBuffer iobuffer = this.firstFreeBuf;
				if (iobuffer != null)
				{
					this.firstFreeBuf = iobuffer.NextBuffer;
					iobuffer.AppendOffset = 0;
					iobuffer.NextBuffer = null;
					this.freeBufferCount--;
					return iobuffer;
				}
				return new BlockModeMessageStream.IOBuffer(bufSize, false);
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00004930 File Offset: 0x00002B30
			public void Free(BlockModeMessageStream.IOBuffer buf)
			{
				if (buf.Buffer.Length == this.bufferSize && (buf.PreAllocated || this.firstFreeBuf == null))
				{
					buf.NextBuffer = this.firstFreeBuf;
					if (buf.PreAllocated && buf.NextBuffer != null && !buf.NextBuffer.PreAllocated)
					{
						buf.NextBuffer = buf.NextBuffer.NextBuffer;
						this.freeBufferCount--;
					}
					this.firstFreeBuf = buf;
					this.freeBufferCount++;
				}
			}

			// Token: 0x0400004A RID: 74
			private readonly int bufferSize;

			// Token: 0x0400004B RID: 75
			private BlockModeMessageStream.IOBuffer firstFreeBuf;

			// Token: 0x0400004C RID: 76
			private int freeBufferCount;
		}

		// Token: 0x0200000C RID: 12
		internal class SenderPosition
		{
			// Token: 0x06000066 RID: 102 RVA: 0x000049BA File Offset: 0x00002BBA
			public SenderPosition(BlockModeMessageStream.BlockModeDataMessage startMsg)
			{
				this.CurrentBuffer = startMsg.IOBuffer;
				this.NextSendOffset = startMsg.MessageStartOffset;
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000067 RID: 103 RVA: 0x000049DA File Offset: 0x00002BDA
			// (set) Token: 0x06000068 RID: 104 RVA: 0x000049E2 File Offset: 0x00002BE2
			public BlockModeMessageStream.IOBuffer CurrentBuffer { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000069 RID: 105 RVA: 0x000049EB File Offset: 0x00002BEB
			// (set) Token: 0x0600006A RID: 106 RVA: 0x000049F3 File Offset: 0x00002BF3
			public int NextSendOffset { get; set; }
		}

		// Token: 0x0200000D RID: 13
		internal class BlockModeMessageBase
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600006B RID: 107 RVA: 0x000049FC File Offset: 0x00002BFC
			// (set) Token: 0x0600006C RID: 108 RVA: 0x00004A04 File Offset: 0x00002C04
			public BlockModeMessageStream.BlockModeMessageBase NextMsg { get; set; }
		}

		// Token: 0x0200000E RID: 14
		internal class BlockModeDataMessage : BlockModeMessageStream.BlockModeMessageBase
		{
			// Token: 0x0600006E RID: 110 RVA: 0x00004A15 File Offset: 0x00002C15
			public BlockModeDataMessage(JET_EMITDATACTX emitlogdatactx)
			{
				this.EmitContext = emitlogdatactx;
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600006F RID: 111 RVA: 0x00004A24 File Offset: 0x00002C24
			// (set) Token: 0x06000070 RID: 112 RVA: 0x00004A2C File Offset: 0x00002C2C
			public BlockModeMessageStream.IOBuffer IOBuffer { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000071 RID: 113 RVA: 0x00004A35 File Offset: 0x00002C35
			// (set) Token: 0x06000072 RID: 114 RVA: 0x00004A3D File Offset: 0x00002C3D
			public int MessageStartOffset { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00004A46 File Offset: 0x00002C46
			// (set) Token: 0x06000074 RID: 116 RVA: 0x00004A4E File Offset: 0x00002C4E
			public JET_EMITDATACTX EmitContext { get; private set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00004A57 File Offset: 0x00002C57
			public bool IsLastForLog
			{
				get
				{
					return BitMasker.IsOn((int)this.EmitContext.grbitOperationalFlags, 16);
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000076 RID: 118 RVA: 0x00004A6B File Offset: 0x00002C6B
			// (set) Token: 0x06000077 RID: 119 RVA: 0x00004A73 File Offset: 0x00002C73
			public bool IsFirstForLog { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000078 RID: 120 RVA: 0x00004A7C File Offset: 0x00002C7C
			public bool IsTerminationMessage
			{
				get
				{
					return BitMasker.IsOn((int)this.EmitContext.grbitOperationalFlags, 2);
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00004A8F File Offset: 0x00002C8F
			public bool HasDataBuffersFlag
			{
				get
				{
					return BitMasker.IsOn((int)this.EmitContext.grbitOperationalFlags, 8);
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600007A RID: 122 RVA: 0x00004AA4 File Offset: 0x00002CA4
			public int LogGeneration
			{
				get
				{
					return this.EmitContext.lgposLogData.lGeneration;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00004AC4 File Offset: 0x00002CC4
			public int LogSector
			{
				get
				{
					return this.EmitContext.lgposLogData.isec;
				}
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00004AE4 File Offset: 0x00002CE4
			public override string ToString()
			{
				return string.Format("Gen=0x{0:X} Sector=0x{1:X} JBits=0x{2:X} EmitSeq=0x{3:X} LogDataLen=0x{4:X}", new object[]
				{
					this.LogGeneration,
					this.LogSector,
					(int)this.EmitContext.grbitOperationalFlags,
					(int)this.EmitContext.qwSequenceNum,
					(int)this.EmitContext.cbLogData
				});
			}
		}
	}
}
