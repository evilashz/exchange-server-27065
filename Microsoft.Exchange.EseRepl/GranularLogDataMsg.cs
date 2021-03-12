using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200001D RID: 29
	internal class GranularLogDataMsg : NetworkChannelMessage
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000410C File Offset: 0x0000230C
		public static int CalculateSerializedLength(JET_EMITDATACTX emitContext, int cblogdata)
		{
			return 73 + cblogdata;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004120 File Offset: 0x00002320
		public static void SerializeToBuffer(int expectedTotalSize, GranularLogDataMsg.Flags msgFlags, JET_EMITDATACTX emitContext, byte[] logdata, int cblogdata, byte[] targetBuffer, int targetBufferOffsetToStart)
		{
			NetworkChannelPacket networkChannelPacket = new NetworkChannelPacket(targetBuffer, targetBufferOffsetToStart);
			networkChannelPacket.GrowthDisabled = true;
			networkChannelPacket.Append(1);
			int val = expectedTotalSize - 5;
			networkChannelPacket.Append(val);
			val = 1312903751;
			networkChannelPacket.Append(val);
			val = expectedTotalSize - 5;
			networkChannelPacket.Append(val);
			DateTime utcNow = DateTime.UtcNow;
			networkChannelPacket.Append(utcNow);
			networkChannelPacket.Append((long)msgFlags);
			long val2 = Win32StopWatch.GetSystemPerformanceCounter();
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
			if (cblogdata > 0)
			{
				networkChannelPacket.Append(logdata, 0, cblogdata);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000422F File Offset: 0x0000242F
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004237 File Offset: 0x00002437
		public int LogDataLength { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004248 File Offset: 0x00002448
		public JET_EMITDATACTX EmitContext { get; private set; }

		// Token: 0x060000CB RID: 203 RVA: 0x00004251 File Offset: 0x00002451
		protected override void Serialize()
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004253 File Offset: 0x00002453
		internal override void SendInternal()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004255 File Offset: 0x00002455
		public GranularLogDataMsg() : base(NetworkChannelMessage.MessageType.GranularLogData)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004264 File Offset: 0x00002464
		internal static GranularLogDataMsg ReadFromNet(NetworkChannel ch, byte[] workingBuf, int startOffset)
		{
			int len = 52;
			ch.Read(workingBuf, startOffset, len);
			GranularLogDataMsg granularLogDataMsg = new GranularLogDataMsg();
			BufDeserializer bufDeserializer = new BufDeserializer(workingBuf, startOffset);
			granularLogDataMsg.FlagsUsed = (GranularLogDataMsg.Flags)bufDeserializer.ExtractInt64();
			granularLogDataMsg.RequestAckCounter = bufDeserializer.ExtractInt64();
			granularLogDataMsg.LogDataLength = bufDeserializer.ExtractInt32();
			if (granularLogDataMsg.LogDataLength > 1048576)
			{
				throw new NetworkCorruptDataException(ch.PartnerNodeName);
			}
			granularLogDataMsg.EmitContext = new JET_EMITDATACTX();
			granularLogDataMsg.EmitContext.cbLogData = (long)granularLogDataMsg.LogDataLength;
			granularLogDataMsg.EmitContext.dwVersion = bufDeserializer.ExtractInt32();
			granularLogDataMsg.EmitContext.qwSequenceNum = bufDeserializer.ExtractUInt64();
			granularLogDataMsg.EmitContext.grbitOperationalFlags = (ShadowLogEmitGrbit)bufDeserializer.ExtractUInt32();
			granularLogDataMsg.EmitContext.logtimeEmit = bufDeserializer.ExtractDateTime();
			JET_LGPOS lgposLogData = default(JET_LGPOS);
			lgposLogData.lGeneration = bufDeserializer.ExtractInt32();
			lgposLogData.isec = (int)bufDeserializer.ExtractUInt16();
			lgposLogData.ib = (int)bufDeserializer.ExtractUInt16();
			granularLogDataMsg.EmitContext.lgposLogData = lgposLogData;
			return granularLogDataMsg;
		}

		// Token: 0x04000087 RID: 135
		private const int OffsetToStartOfLogData = 52;

		// Token: 0x04000088 RID: 136
		public GranularLogDataMsg.Flags FlagsUsed;

		// Token: 0x04000089 RID: 137
		public long RequestAckCounter;

		// Token: 0x0200001E RID: 30
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x0400008D RID: 141
			None = 0UL,
			// Token: 0x0400008E RID: 142
			AckRequested = 1UL,
			// Token: 0x0400008F RID: 143
			GranularStatus = 2UL,
			// Token: 0x04000090 RID: 144
			DebugChecksumPresent = 4UL,
			// Token: 0x04000091 RID: 145
			CompletionsDisabled = 8UL
		}
	}
}
