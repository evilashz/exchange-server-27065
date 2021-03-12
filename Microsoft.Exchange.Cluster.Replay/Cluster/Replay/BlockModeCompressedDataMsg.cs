using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000352 RID: 850
	internal class BlockModeCompressedDataMsg : NetworkChannelMessage
	{
		// Token: 0x0600227F RID: 8831 RVA: 0x000A0974 File Offset: 0x0009EB74
		internal static int CalculateBlockCount(int cblogdata)
		{
			int num = cblogdata / 65536;
			if (cblogdata % 65536 > 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000A0998 File Offset: 0x0009EB98
		internal static int GetOffsetToCompressedData(int uncompressedSize)
		{
			return 73 + 4 * BlockModeCompressedDataMsg.CalculateBlockCount(uncompressedSize);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000A09A8 File Offset: 0x0009EBA8
		public static int CalculateWorstLength(JET_EMITDATACTX emitContext, int cblogdata)
		{
			int num = BlockModeCompressedDataMsg.CalculateBlockCount(cblogdata);
			return 73 + 4 * num + cblogdata;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000A09C8 File Offset: 0x0009EBC8
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

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x000A0B7C File Offset: 0x0009ED7C
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x000A0B84 File Offset: 0x0009ED84
		public int LogDataLength { get; private set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000A0B8D File Offset: 0x0009ED8D
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x000A0B95 File Offset: 0x0009ED95
		public int[] CompressedLengths { get; private set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000A0B9E File Offset: 0x0009ED9E
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x000A0BA6 File Offset: 0x0009EDA6
		public JET_EMITDATACTX EmitContext { get; private set; }

		// Token: 0x06002289 RID: 8841 RVA: 0x000A0BAF File Offset: 0x0009EDAF
		protected override void Serialize()
		{
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000A0BB1 File Offset: 0x0009EDB1
		internal override void SendInternal()
		{
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A0BB4 File Offset: 0x0009EDB4
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

		// Token: 0x0600228C RID: 8844 RVA: 0x000A0D33 File Offset: 0x0009EF33
		public BlockModeCompressedDataMsg() : base(NetworkChannelMessage.MessageType.BlockModeCompressedData)
		{
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000A0D40 File Offset: 0x0009EF40
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

		// Token: 0x04000E58 RID: 3672
		private const int OffsetToStartOfBlockLengths = 52;

		// Token: 0x04000E59 RID: 3673
		internal const int BlockingSize = 65536;

		// Token: 0x04000E5A RID: 3674
		internal const int MaxDataLength = 1048576;

		// Token: 0x04000E5B RID: 3675
		public BlockModeCompressedDataMsg.Flags FlagsUsed;

		// Token: 0x04000E5C RID: 3676
		public long RequestAckCounter;

		// Token: 0x02000353 RID: 851
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E61 RID: 3681
			None = 0UL
		}
	}
}
