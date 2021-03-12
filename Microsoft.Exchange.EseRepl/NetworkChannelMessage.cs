using System;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200001B RID: 27
	internal abstract class NetworkChannelMessage
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003AF9 File Offset: 0x00001CF9
		protected NetworkChannel Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003B01 File Offset: 0x00001D01
		internal NetworkChannelMessage.MessageType Type
		{
			get
			{
				return this.m_msgType;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003B09 File Offset: 0x00001D09
		protected NetworkChannelPacket Packet
		{
			get
			{
				if (this.m_packet == null)
				{
					this.m_packet = new NetworkChannelPacket();
				}
				return this.m_packet;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003B24 File Offset: 0x00001D24
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003B2C File Offset: 0x00001D2C
		internal DateTime MessageUtc
		{
			get
			{
				return this.m_messageUtc;
			}
			set
			{
				this.m_messageUtc = value;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003B35 File Offset: 0x00001D35
		public override string ToString()
		{
			return string.Format("NetworkChannelMessage({0})", this.Type);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003B4C File Offset: 0x00001D4C
		internal static bool IsValidType(NetworkChannelMessage.MessageType msgType)
		{
			if (msgType <= NetworkChannelMessage.MessageType.CompressionRequest)
			{
				if (msgType <= NetworkChannelMessage.MessageType.CancelCiFileRequest)
				{
					if (msgType <= NetworkChannelMessage.MessageType.Ping)
					{
						if (msgType <= NetworkChannelMessage.MessageType.PassiveStatus)
						{
							if (msgType != NetworkChannelMessage.MessageType.TestNetwork0 && msgType != NetworkChannelMessage.MessageType.PassiveStatus)
							{
								return false;
							}
						}
						else if (msgType != NetworkChannelMessage.MessageType.CompressionConfig && msgType != NetworkChannelMessage.MessageType.BlockModeCompressedData && msgType != NetworkChannelMessage.MessageType.Ping)
						{
							return false;
						}
					}
					else if (msgType <= NetworkChannelMessage.MessageType.GranularLogData)
					{
						if (msgType != NetworkChannelMessage.MessageType.GranularTermination && msgType != NetworkChannelMessage.MessageType.GranularLogData)
						{
							return false;
						}
					}
					else if (msgType != NetworkChannelMessage.MessageType.EnterBlockMode && msgType != NetworkChannelMessage.MessageType.GetE00GenerationRequest && msgType != NetworkChannelMessage.MessageType.CancelCiFileRequest)
					{
						return false;
					}
				}
				else if (msgType <= NetworkChannelMessage.MessageType.TestLogExistenceRequest)
				{
					if (msgType <= NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest)
					{
						if (msgType != NetworkChannelMessage.MessageType.SeedLogCopyRequest && msgType != NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest)
						{
							return false;
						}
					}
					else if (msgType != NetworkChannelMessage.MessageType.SeedDatabaseFileRequest)
					{
						switch (msgType)
						{
						case NetworkChannelMessage.MessageType.ContinuousLogCopyRequest:
						case NetworkChannelMessage.MessageType.NotifyEndOfLogRequest:
							break;
						case (NetworkChannelMessage.MessageType)1363627076:
							return false;
						default:
							if (msgType != NetworkChannelMessage.MessageType.TestLogExistenceRequest)
							{
								return false;
							}
							break;
						}
					}
				}
				else if (msgType <= NetworkChannelMessage.MessageType.TestHealthRequest)
				{
					if (msgType != NetworkChannelMessage.MessageType.CopyLogRequest && msgType != NetworkChannelMessage.MessageType.QueryLogRangeRequest && msgType != NetworkChannelMessage.MessageType.TestHealthRequest)
					{
						return false;
					}
				}
				else if (msgType != NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest && msgType != NetworkChannelMessage.MessageType.ProgressCiFileRequest && msgType != NetworkChannelMessage.MessageType.CompressionRequest)
				{
					return false;
				}
			}
			else if (msgType <= NetworkChannelMessage.MessageType.SeedDatabaseFileReply)
			{
				if (msgType <= NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2)
				{
					if (msgType <= NetworkChannelMessage.MessageType.SeedCiFileRequest)
					{
						if (msgType != NetworkChannelMessage.MessageType.SeedPageReaderSinglePageRequest && msgType != NetworkChannelMessage.MessageType.SeedCiFileRequest)
						{
							return false;
						}
					}
					else if (msgType != NetworkChannelMessage.MessageType.SeedCiFileRequest2 && msgType != NetworkChannelMessage.MessageType.SeedPageReaderPageSizeRequest && msgType != NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2)
					{
						return false;
					}
				}
				else if (msgType <= NetworkChannelMessage.MessageType.GetE00GenerationReply)
				{
					if (msgType != NetworkChannelMessage.MessageType.LogCopyServerStatus && msgType != NetworkChannelMessage.MessageType.GranularLogComplete && msgType != NetworkChannelMessage.MessageType.GetE00GenerationReply)
					{
						return false;
					}
				}
				else if (msgType != NetworkChannelMessage.MessageType.NotifyEndOfLogAsyncReply && msgType != NetworkChannelMessage.MessageType.CancelCiFileReply && msgType != NetworkChannelMessage.MessageType.SeedDatabaseFileReply)
				{
					return false;
				}
			}
			else if (msgType <= NetworkChannelMessage.MessageType.TestHealthReply)
			{
				if (msgType <= NetworkChannelMessage.MessageType.TestLogExistenceReply)
				{
					if (msgType != NetworkChannelMessage.MessageType.NotifyEndOfLogReply && msgType != NetworkChannelMessage.MessageType.TestLogExistenceReply)
					{
						return false;
					}
				}
				else if (msgType != NetworkChannelMessage.MessageType.CopyLogReply && msgType != NetworkChannelMessage.MessageType.QueryLogRangeReply && msgType != NetworkChannelMessage.MessageType.TestHealthReply)
				{
					return false;
				}
			}
			else if (msgType <= NetworkChannelMessage.MessageType.CompressionReply)
			{
				if (msgType != NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileReply && msgType != NetworkChannelMessage.MessageType.ProgressCiFileReply && msgType != NetworkChannelMessage.MessageType.CompressionReply)
				{
					return false;
				}
			}
			else if (msgType != NetworkChannelMessage.MessageType.SeedPageReaderSinglePageReply && msgType != NetworkChannelMessage.MessageType.SeedCiFileReply && msgType != NetworkChannelMessage.MessageType.SeedPageReaderPageSizeReply)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public static NetworkChannelMessageHeader ReadHeaderFromNet(NetworkChannel netChan, byte[] workingBuf, int startOffset)
		{
			netChan.Read(workingBuf, startOffset, 16);
			BufDeserializer bufDeserializer = new BufDeserializer(workingBuf, 0);
			NetworkChannelMessageHeader result;
			result.MessageType = (NetworkChannelMessage.MessageType)bufDeserializer.ExtractUInt32();
			result.MessageLength = bufDeserializer.ExtractInt32();
			result.MessageUtc = bufDeserializer.ExtractDateTime();
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E2C File Offset: 0x0000202C
		internal static NetworkChannelMessage ReadMessage(NetworkChannel channel, byte[] headerBuf)
		{
			int num = 0;
			NetworkChannelMessage.MessageType messageType = (NetworkChannelMessage.MessageType)Serialization.DeserializeUInt32(headerBuf, ref num);
			if (!NetworkChannelMessage.IsValidType(messageType))
			{
				throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("Unknown Type{0}", messageType));
			}
			int num2 = (int)Serialization.DeserializeUInt32(headerBuf, ref num);
			if (num2 < 16 || num2 > 1052672)
			{
				throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("Invalid msgLen: {0}", num2));
			}
			ExTraceGlobals.NetworkChannelTracer.TraceDebug<NetworkChannelMessage.MessageType, string, string>((long)channel.GetHashCode(), "ReadMessage: Received {0} from {1} on {2}", messageType, channel.RemoteEndPointString, channel.LocalEndPointString);
			byte[] array = new byte[num2];
			Array.Copy(headerBuf, 0, array, 0, 16);
			int len = num2 - 16;
			channel.Read(array, 16, len);
			NetworkChannelMessage.MessageType messageType2 = messageType;
			if (messageType2 <= NetworkChannelMessage.MessageType.BlockModeCompressedData)
			{
				if (messageType2 == NetworkChannelMessage.MessageType.PassiveStatus)
				{
					return new PassiveStatusMsg(channel, array);
				}
				if (messageType2 != NetworkChannelMessage.MessageType.BlockModeCompressedData)
				{
					goto IL_118;
				}
			}
			else
			{
				if (messageType2 == NetworkChannelMessage.MessageType.Ping)
				{
					return new PingMessage(channel, array);
				}
				if (messageType2 != NetworkChannelMessage.MessageType.GranularLogData)
				{
					if (messageType2 != NetworkChannelMessage.MessageType.EnterBlockMode)
					{
						goto IL_118;
					}
					return new EnterBlockModeMsg(channel, array);
				}
			}
			throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("ReadMessage() does not support message type: {0}.", messageType));
			IL_118:
			throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("Unknown message type: 0x{0:X}", (int)messageType));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003F6C File Offset: 0x0000216C
		internal virtual void Send()
		{
			this.MessageUtc = DateTime.UtcNow;
			this.Packet.PrepareToWrite();
			this.Serialize();
			int num = 4;
			Serialization.SerializeUInt32(this.Packet.Buffer, ref num, (uint)this.Packet.Length);
			ExTraceGlobals.NetworkChannelTracer.TraceDebug<NetworkChannelMessage.MessageType, string, string>((long)this.m_channel.GetHashCode(), "SendingMessage: {0} to {1} on {2}", this.Type, this.m_channel.RemoteEndPointString, this.m_channel.LocalEndPointString);
			this.SendInternal();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003FF1 File Offset: 0x000021F1
		internal virtual void SendInternal()
		{
			this.m_channel.SendMessage(this.m_packet.Buffer, 0, this.m_packet.Length);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004015 File Offset: 0x00002215
		protected virtual void Serialize()
		{
			this.Packet.Append((uint)this.m_msgType);
			this.Packet.Append(16U);
			this.Packet.Append(this.m_messageUtc);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004046 File Offset: 0x00002246
		internal NetworkChannelMessage(NetworkChannel channel, NetworkChannelMessage.MessageType msgType)
		{
			this.m_channel = channel;
			this.m_msgType = msgType;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000405C File Offset: 0x0000225C
		internal NetworkChannelMessage(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, byte[] packetContent)
		{
			this.m_channel = channel;
			this.m_msgType = msgType;
			this.m_packet = new NetworkChannelPacket(packetContent);
			this.Packet.ExtractUInt32();
			this.Packet.ExtractUInt32();
			this.MessageUtc = this.Packet.ExtractDateTime();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000040B2 File Offset: 0x000022B2
		internal NetworkChannelMessage(NetworkChannelMessage.MessageType msgType)
		{
			this.m_msgType = msgType;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000040C4 File Offset: 0x000022C4
		public static void SerializeHeaderToBuffer(NetworkChannelMessage.MessageType msgType, int totalSize, byte[] targetBuffer, int targetBufferOffsetToStart)
		{
			NetworkChannelPacket networkChannelPacket = new NetworkChannelPacket(targetBuffer, targetBufferOffsetToStart);
			networkChannelPacket.GrowthDisabled = true;
			networkChannelPacket.Append(1);
			int val = totalSize - 5;
			networkChannelPacket.Append(val);
			networkChannelPacket.Append((int)msgType);
			networkChannelPacket.Append(val);
			networkChannelPacket.Append(DateTime.UtcNow);
		}

		// Token: 0x04000051 RID: 81
		public const int GuidSize = 16;

		// Token: 0x04000052 RID: 82
		internal const int MessageHeaderSize = 16;

		// Token: 0x04000053 RID: 83
		private const int OffsetOfMessageLengthField = 4;

		// Token: 0x04000054 RID: 84
		internal const int MaxMessageSize = 1052672;

		// Token: 0x04000055 RID: 85
		protected NetworkChannel m_channel;

		// Token: 0x04000056 RID: 86
		protected NetworkChannelMessage.MessageType m_msgType;

		// Token: 0x04000057 RID: 87
		protected NetworkChannelPacket m_packet;

		// Token: 0x04000058 RID: 88
		protected DateTime m_messageUtc;

		// Token: 0x0200001C RID: 28
		internal enum MessageType
		{
			// Token: 0x0400005A RID: 90
			Invalid,
			// Token: 0x0400005B RID: 91
			CopyLogRequest = 1363627852,
			// Token: 0x0400005C RID: 92
			CopyLogReply = 1497845580,
			// Token: 0x0400005D RID: 93
			NotifyEndOfLogRequest = 1363627077,
			// Token: 0x0400005E RID: 94
			NotifyEndOfLogReply = 1497844805,
			// Token: 0x0400005F RID: 95
			NotifyEndOfLogAsyncReply = 1497451589,
			// Token: 0x04000060 RID: 96
			QueryLogRangeRequest = 1363628620,
			// Token: 0x04000061 RID: 97
			QueryLogRangeReply = 1497846348,
			// Token: 0x04000062 RID: 98
			GetE00GenerationRequest = 1362112581,
			// Token: 0x04000063 RID: 99
			GetE00GenerationReply = 1496330309,
			// Token: 0x04000064 RID: 100
			TestLogExistenceRequest = 1363627092,
			// Token: 0x04000065 RID: 101
			TestLogExistenceReply = 1497844820,
			// Token: 0x04000066 RID: 102
			TestHealthRequest = 1363694164,
			// Token: 0x04000067 RID: 103
			TestHealthReply = 1497911892,
			// Token: 0x04000068 RID: 104
			CancelCiFileRequest = 1363364163,
			// Token: 0x04000069 RID: 105
			CancelCiFileReply = 1497581891,
			// Token: 0x0400006A RID: 106
			ProgressCiFileRequest = 1364216131,
			// Token: 0x0400006B RID: 107
			ProgressCiFileReply = 1498433859,
			// Token: 0x0400006C RID: 108
			SeedCiFileRequest = 1364412739,
			// Token: 0x0400006D RID: 109
			SeedCiFileRequest2 = 1364478275,
			// Token: 0x0400006E RID: 110
			SeedCiFileReply = 1498630467,
			// Token: 0x0400006F RID: 111
			CompressionRequest = 1364217155,
			// Token: 0x04000070 RID: 112
			CompressionReply = 1498434883,
			// Token: 0x04000071 RID: 113
			CompressionConfig = 1129336131,
			// Token: 0x04000072 RID: 114
			SeedDatabaseFileRequest = 1363559507,
			// Token: 0x04000073 RID: 115
			SeedDatabaseFileReply = 1497777235,
			// Token: 0x04000074 RID: 116
			PassiveDatabaseFileRequest = 1363559504,
			// Token: 0x04000075 RID: 117
			SeedLogCopyRequest = 1363364947,
			// Token: 0x04000076 RID: 118
			SeedPageReaderSinglePageRequest = 1364349011,
			// Token: 0x04000077 RID: 119
			SeedPageReaderSinglePageReply = 1498566739,
			// Token: 0x04000078 RID: 120
			SeedPageReaderPageSizeRequest = 1364870227,
			// Token: 0x04000079 RID: 121
			SeedPageReaderPageSizeReply = 1499087955,
			// Token: 0x0400007A RID: 122
			SeedPageReaderRollLogFileRequest = 1363956307,
			// Token: 0x0400007B RID: 123
			SeedPageReaderRollLogFileReply = 1498174035,
			// Token: 0x0400007C RID: 124
			ContinuousLogCopyRequest = 1363627075,
			// Token: 0x0400007D RID: 125
			ContinuousLogCopyRequest2 = 1380139852,
			// Token: 0x0400007E RID: 126
			LogCopyServerStatus = 1397965644,
			// Token: 0x0400007F RID: 127
			GranularLogData = 1312903751,
			// Token: 0x04000080 RID: 128
			GranularLogComplete = 1481527879,
			// Token: 0x04000081 RID: 129
			GranularTermination = 1297371719,
			// Token: 0x04000082 RID: 130
			BlockModeCompressedData = 1145261378,
			// Token: 0x04000083 RID: 131
			Ping = 1196312912,
			// Token: 0x04000084 RID: 132
			TestNetwork0 = 810832724,
			// Token: 0x04000085 RID: 133
			EnterBlockMode = 1313164610,
			// Token: 0x04000086 RID: 134
			PassiveStatus = 1096045392
		}
	}
}
