﻿using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000350 RID: 848
	internal class GranularLogDataMsg : NetworkChannelMessage
	{
		// Token: 0x06002275 RID: 8821 RVA: 0x000A0710 File Offset: 0x0009E910
		public static int CalculateSerializedLength(JET_EMITDATACTX emitContext, int cblogdata)
		{
			return 73 + cblogdata;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000A0724 File Offset: 0x0009E924
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

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000A0833 File Offset: 0x0009EA33
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x000A083B File Offset: 0x0009EA3B
		public int LogDataLength { get; private set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000A0844 File Offset: 0x0009EA44
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x000A084C File Offset: 0x0009EA4C
		public JET_EMITDATACTX EmitContext { get; private set; }

		// Token: 0x0600227B RID: 8827 RVA: 0x000A0855 File Offset: 0x0009EA55
		protected override void Serialize()
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000A0857 File Offset: 0x0009EA57
		internal override void SendInternal()
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000A0859 File Offset: 0x0009EA59
		public GranularLogDataMsg() : base(NetworkChannelMessage.MessageType.GranularLogData)
		{
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000A0868 File Offset: 0x0009EA68
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

		// Token: 0x04000E4D RID: 3661
		private const int OffsetToStartOfLogData = 52;

		// Token: 0x04000E4E RID: 3662
		public GranularLogDataMsg.Flags FlagsUsed;

		// Token: 0x04000E4F RID: 3663
		public long RequestAckCounter;

		// Token: 0x02000351 RID: 849
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E53 RID: 3667
			None = 0UL,
			// Token: 0x04000E54 RID: 3668
			AckRequested = 1UL,
			// Token: 0x04000E55 RID: 3669
			GranularStatus = 2UL,
			// Token: 0x04000E56 RID: 3670
			DebugChecksumPresent = 4UL,
			// Token: 0x04000E57 RID: 3671
			CompletionsDisabled = 8UL
		}
	}
}
