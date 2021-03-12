using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200001F RID: 31
	internal class BlockModeCompressedDataMsg : NetworkChannelMessage
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004370 File Offset: 0x00002570
		internal static int CalculateBlockCount(int cblogdata)
		{
			int num = cblogdata / 65536;
			if (cblogdata % 65536 > 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004394 File Offset: 0x00002594
		internal static int GetOffsetToCompressedData(int uncompressedSize)
		{
			return 73 + 4 * BlockModeCompressedDataMsg.CalculateBlockCount(uncompressedSize);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000043A4 File Offset: 0x000025A4
		public static int CalculateWorstLength(JET_EMITDATACTX emitContext, int cblogdata)
		{
			int num = BlockModeCompressedDataMsg.CalculateBlockCount(cblogdata);
			return 73 + 4 * num + cblogdata;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000043C4 File Offset: 0x000025C4
		public static int SerializeToBuffer(JET_EMITDATACTX emitContext, byte[] logdata, int cblogdata, byte[] targetBuffer, int targetBufferOffsetToStart, out int totalCompressedSize)
		{
			NetworkChannelPacket networkChannelPacket = new NetworkChannelPacket(targetBuffer, targetBufferOffsetToStart);
			networkChannelPacket.GrowthDisabled = true;
			int num = BlockModeCompressedDataMsg.CalculateBlockCount(cblogdata);
			int[] array = new int[num];
			int num2 = 73 + 4 * num;
			int num3 = num2 + targetBufferOffsetToStart;
			int num4 = 0;
			totalCompressedSize = 0;
			int num5 = cblogdata;
			for (int i = 0; i < num; i++)
			{
				int num6 = Math.Min(num5, 65536);
				Xpress.Compress(logdata, num4, num6, targetBuffer, num3, num6, out array[i]);
				num4 += num6;
				num3 += array[i];
				totalCompressedSize += array[i];
				num5 -= num6;
			}
			networkChannelPacket.Append(1);
			int val = num2 - 5 + totalCompressedSize;
			networkChannelPacket.Append(val);
			val = 1145261378;
			networkChannelPacket.Append(val);
			val = num2 - 5;
			networkChannelPacket.Append(val);
			DateTime utcNow = DateTime.UtcNow;
			networkChannelPacket.Append(utcNow);
			long val2 = 0L;
			networkChannelPacket.Append(val2);
			val2 = Win32StopWatch.GetSystemPerformanceCounter();
			networkChannelPacket.Append(val2);
			networkChannelPacket.Append(cblogdata);
			val = emitContext.dwVersion;
			networkChannelPacket.Append(val);
			val2 = (long)emitContext.qwSequenceNum;
			networkChannelPacket.Append(val2);
			val = (int)emitContext.grbitOperationalFlags;
			networkChannelPacket.Append(val);
			DateTime time = DateTime.SpecifyKind(emitContext.logtimeEmit, DateTimeKind.Utc);
			networkChannelPacket.Append(time);
			val = emitContext.lgposLogData.lGeneration;
			networkChannelPacket.Append(val);
			ushort val3 = (ushort)emitContext.lgposLogData.isec;
			networkChannelPacket.Append(val3);
			val3 = (ushort)emitContext.lgposLogData.ib;
			networkChannelPacket.Append(val3);
			for (int j = 0; j < num; j++)
			{
				networkChannelPacket.Append(array[j]);
			}
			return num2 + totalCompressedSize;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004578 File Offset: 0x00002778
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004580 File Offset: 0x00002780
		public int LogDataLength { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004589 File Offset: 0x00002789
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004591 File Offset: 0x00002791
		public int[] CompressedLengths { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000459A File Offset: 0x0000279A
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000045A2 File Offset: 0x000027A2
		public JET_EMITDATACTX EmitContext { get; private set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x000045AB File Offset: 0x000027AB
		protected override void Serialize()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000045AD File Offset: 0x000027AD
		internal override void SendInternal()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000045B0 File Offset: 0x000027B0
		internal BlockModeCompressedDataMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.BlockModeCompressedData, packetContent)
		{
			this.FlagsUsed = (BlockModeCompressedDataMsg.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.LogDataLength = base.Packet.ExtractInt32();
			if (this.LogDataLength > 1048576)
			{
				throw new NetworkCorruptDataException(channel.PartnerNodeName);
			}
			this.EmitContext = new JET_EMITDATACTX();
			this.EmitContext.cbLogData = (long)this.LogDataLength;
			this.EmitContext.dwVersion = base.Packet.ExtractInt32();
			this.EmitContext.qwSequenceNum = base.Packet.ExtractUInt64();
			this.EmitContext.grbitOperationalFlags = (ShadowLogEmitGrbit)base.Packet.ExtractUInt32();
			this.EmitContext.logtimeEmit = base.Packet.ExtractDateTime();
			JET_LGPOS lgposLogData = default(JET_LGPOS);
			lgposLogData.lGeneration = base.Packet.ExtractInt32();
			lgposLogData.isec = (int)base.Packet.ExtractUInt16();
			lgposLogData.ib = (int)base.Packet.ExtractUInt16();
			this.EmitContext.lgposLogData = lgposLogData;
			if (this.LogDataLength > 0)
			{
				int num = BlockModeCompressedDataMsg.CalculateBlockCount(this.LogDataLength);
				this.CompressedLengths = new int[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = base.Packet.ExtractInt32();
					if (num2 <= 0 || num2 > 65536)
					{
						throw new NetworkCorruptDataException(channel.PartnerNodeName);
					}
					this.CompressedLengths[i] = num2;
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000472F File Offset: 0x0000292F
		public BlockModeCompressedDataMsg() : base(NetworkChannelMessage.MessageType.BlockModeCompressedData)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000473C File Offset: 0x0000293C
		internal static BlockModeCompressedDataMsg ReadFromNet(NetworkChannel ch, byte[] workingBuf, int startOffset)
		{
			int len = 52;
			ch.Read(workingBuf, startOffset, len);
			BlockModeCompressedDataMsg blockModeCompressedDataMsg = new BlockModeCompressedDataMsg();
			BufDeserializer bufDeserializer = new BufDeserializer(workingBuf, startOffset);
			blockModeCompressedDataMsg.FlagsUsed = (BlockModeCompressedDataMsg.Flags)bufDeserializer.ExtractInt64();
			blockModeCompressedDataMsg.RequestAckCounter = bufDeserializer.ExtractInt64();
			blockModeCompressedDataMsg.LogDataLength = bufDeserializer.ExtractInt32();
			if (blockModeCompressedDataMsg.LogDataLength > 1048576)
			{
				throw new NetworkCorruptDataException(ch.PartnerNodeName);
			}
			blockModeCompressedDataMsg.EmitContext = new JET_EMITDATACTX();
			blockModeCompressedDataMsg.EmitContext.cbLogData = (long)blockModeCompressedDataMsg.LogDataLength;
			blockModeCompressedDataMsg.EmitContext.dwVersion = bufDeserializer.ExtractInt32();
			blockModeCompressedDataMsg.EmitContext.qwSequenceNum = bufDeserializer.ExtractUInt64();
			blockModeCompressedDataMsg.EmitContext.grbitOperationalFlags = (ShadowLogEmitGrbit)bufDeserializer.ExtractUInt32();
			blockModeCompressedDataMsg.EmitContext.logtimeEmit = bufDeserializer.ExtractDateTime();
			JET_LGPOS lgposLogData = default(JET_LGPOS);
			lgposLogData.lGeneration = bufDeserializer.ExtractInt32();
			lgposLogData.isec = (int)bufDeserializer.ExtractUInt16();
			lgposLogData.ib = (int)bufDeserializer.ExtractUInt16();
			blockModeCompressedDataMsg.EmitContext.lgposLogData = lgposLogData;
			if (blockModeCompressedDataMsg.LogDataLength > 0)
			{
				int num = BlockModeCompressedDataMsg.CalculateBlockCount(blockModeCompressedDataMsg.LogDataLength);
				blockModeCompressedDataMsg.CompressedLengths = new int[num];
				len = num * 4;
				ch.Read(workingBuf, startOffset, len);
				bufDeserializer.Reset(workingBuf, startOffset);
				for (int i = 0; i < num; i++)
				{
					int num2 = bufDeserializer.ExtractInt32();
					if (num2 <= 0 || num2 > 65536)
					{
						throw new NetworkCorruptDataException(ch.PartnerNodeName);
					}
					blockModeCompressedDataMsg.CompressedLengths[i] = num2;
				}
			}
			return blockModeCompressedDataMsg;
		}

		// Token: 0x04000092 RID: 146
		private const int OffsetToStartOfBlockLengths = 52;

		// Token: 0x04000093 RID: 147
		internal const int BlockingSize = 65536;

		// Token: 0x04000094 RID: 148
		internal const int MaxDataLength = 1048576;

		// Token: 0x04000095 RID: 149
		public BlockModeCompressedDataMsg.Flags FlagsUsed;

		// Token: 0x04000096 RID: 150
		public long RequestAckCounter;

		// Token: 0x02000020 RID: 32
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x0400009B RID: 155
			None = 0UL
		}
	}
}
