using System;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000131 RID: 305
	internal abstract class NetworkChannelMessage
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000338C9 File Offset: 0x00031AC9
		protected NetworkChannel Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000338D1 File Offset: 0x00031AD1
		internal NetworkChannelMessage.MessageType Type
		{
			get
			{
				return this.m_msgType;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000338D9 File Offset: 0x00031AD9
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000338F4 File Offset: 0x00031AF4
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x000338FC File Offset: 0x00031AFC
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

		// Token: 0x06000B9F RID: 2975 RVA: 0x00033905 File Offset: 0x00031B05
		public override string ToString()
		{
			return string.Format("NetworkChannelMessage({0})", this.Type);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003391C File Offset: 0x00031B1C
		internal static bool IsValidType(NetworkChannelMessage.MessageType msgType)
		{
			if (msgType <= NetworkChannelMessage.MessageType.CompressionRequest)
			{
				if (msgType <= NetworkChannelMessage.MessageType.SeedLogCopyRequest)
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
					else if (msgType <= NetworkChannelMessage.MessageType.EnterBlockMode)
					{
						if (msgType != NetworkChannelMessage.MessageType.GranularTermination && msgType != NetworkChannelMessage.MessageType.GranularLogData && msgType != NetworkChannelMessage.MessageType.EnterBlockMode)
						{
							return false;
						}
					}
					else if (msgType != NetworkChannelMessage.MessageType.GetE00GenerationRequest && msgType != NetworkChannelMessage.MessageType.CancelCiFileRequest && msgType != NetworkChannelMessage.MessageType.SeedLogCopyRequest)
					{
						return false;
					}
				}
				else if (msgType <= NetworkChannelMessage.MessageType.CopyLogRequest)
				{
					if (msgType <= NetworkChannelMessage.MessageType.SeedDatabaseFileRequest)
					{
						if (msgType != NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest && msgType != NetworkChannelMessage.MessageType.SeedDatabaseFileRequest)
						{
							return false;
						}
					}
					else
					{
						switch (msgType)
						{
						case NetworkChannelMessage.MessageType.ContinuousLogCopyRequest:
						case NetworkChannelMessage.MessageType.NotifyEndOfLogRequest:
							break;
						case (NetworkChannelMessage.MessageType)1363627076:
							return false;
						default:
							if (msgType != NetworkChannelMessage.MessageType.TestLogExistenceRequest && msgType != NetworkChannelMessage.MessageType.CopyLogRequest)
							{
								return false;
							}
							break;
						}
					}
				}
				else if (msgType <= NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest)
				{
					if (msgType != NetworkChannelMessage.MessageType.QueryLogRangeRequest && msgType != NetworkChannelMessage.MessageType.TestHealthRequest && msgType != NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest)
					{
						return false;
					}
				}
				else if (msgType != NetworkChannelMessage.MessageType.SeedPageReaderMultiplePageRequest && msgType != NetworkChannelMessage.MessageType.ProgressCiFileRequest && msgType != NetworkChannelMessage.MessageType.CompressionRequest)
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

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00033BBC File Offset: 0x00031DBC
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

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00033C08 File Offset: 0x00031E08
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
			if (messageType2 <= NetworkChannelMessage.MessageType.CompressionRequest)
			{
				if (messageType2 <= NetworkChannelMessage.MessageType.SeedLogCopyRequest)
				{
					if (messageType2 <= NetworkChannelMessage.MessageType.Ping)
					{
						if (messageType2 <= NetworkChannelMessage.MessageType.PassiveStatus)
						{
							if (messageType2 == NetworkChannelMessage.MessageType.TestNetwork0)
							{
								return new TestNetwork0Request(channel, array);
							}
							if (messageType2 != NetworkChannelMessage.MessageType.PassiveStatus)
							{
								goto IL_51C;
							}
							return new PassiveStatusMsg(channel, array);
						}
						else
						{
							if (messageType2 == NetworkChannelMessage.MessageType.CompressionConfig)
							{
								return new NetworkChannelCompressionConfigMsg(channel, array);
							}
							if (messageType2 != NetworkChannelMessage.MessageType.BlockModeCompressedData)
							{
								if (messageType2 != NetworkChannelMessage.MessageType.Ping)
								{
									goto IL_51C;
								}
								return new PingMessage(channel, array);
							}
						}
					}
					else if (messageType2 <= NetworkChannelMessage.MessageType.EnterBlockMode)
					{
						if (messageType2 == NetworkChannelMessage.MessageType.GranularTermination)
						{
							return new GranularTerminationMsg(channel, array);
						}
						if (messageType2 != NetworkChannelMessage.MessageType.GranularLogData)
						{
							if (messageType2 != NetworkChannelMessage.MessageType.EnterBlockMode)
							{
								goto IL_51C;
							}
							return new EnterBlockModeMsg(channel, array);
						}
					}
					else
					{
						if (messageType2 == NetworkChannelMessage.MessageType.GetE00GenerationRequest)
						{
							return new GetE00GenerationRequest(channel, array);
						}
						if (messageType2 == NetworkChannelMessage.MessageType.CancelCiFileRequest)
						{
							return new CancelCiFileRequest(channel, array);
						}
						if (messageType2 != NetworkChannelMessage.MessageType.SeedLogCopyRequest)
						{
							goto IL_51C;
						}
						return new SeedLogCopyRequest(channel, array);
					}
					throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("ReadMessage() does not support message type: {0}.", messageType));
				}
				if (messageType2 <= NetworkChannelMessage.MessageType.CopyLogRequest)
				{
					if (messageType2 <= NetworkChannelMessage.MessageType.SeedDatabaseFileRequest)
					{
						if (messageType2 == NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest)
						{
							return new PassiveSeedDatabaseFileRequest(channel, array);
						}
						if (messageType2 == NetworkChannelMessage.MessageType.SeedDatabaseFileRequest)
						{
							return new SeedDatabaseFileRequest(channel, array);
						}
					}
					else
					{
						switch (messageType2)
						{
						case NetworkChannelMessage.MessageType.ContinuousLogCopyRequest:
							return new ContinuousLogCopyRequest(channel, array);
						case (NetworkChannelMessage.MessageType)1363627076:
							break;
						case NetworkChannelMessage.MessageType.NotifyEndOfLogRequest:
							return new NotifyEndOfLogRequest(channel, array);
						default:
							if (messageType2 == NetworkChannelMessage.MessageType.TestLogExistenceRequest)
							{
								return new TestLogExistenceRequest(channel, array);
							}
							if (messageType2 == NetworkChannelMessage.MessageType.CopyLogRequest)
							{
								return new CopyLogRequest(channel, array);
							}
							break;
						}
					}
				}
				else if (messageType2 <= NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest)
				{
					if (messageType2 == NetworkChannelMessage.MessageType.QueryLogRangeRequest)
					{
						return new QueryLogRangeRequest(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.TestHealthRequest)
					{
						return new TestHealthRequest(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest)
					{
						return new SeedPageReaderRollLogFileRequest(channel, array);
					}
				}
				else
				{
					if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderMultiplePageRequest)
					{
						return new SeedPageReaderMultiplePageRequest(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.ProgressCiFileRequest)
					{
						return new ProgressCiFileRequest(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.CompressionRequest)
					{
						return new NetworkChannelCompressionRequest(channel, array);
					}
				}
			}
			else if (messageType2 <= NetworkChannelMessage.MessageType.SeedDatabaseFileReply)
			{
				if (messageType2 <= NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2)
				{
					if (messageType2 <= NetworkChannelMessage.MessageType.SeedCiFileRequest)
					{
						if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderSinglePageRequest)
						{
							return new SeedPageReaderSinglePageRequest(channel, array);
						}
						if (messageType2 == NetworkChannelMessage.MessageType.SeedCiFileRequest)
						{
							return new SeedCiFileRequest(channel, array);
						}
					}
					else
					{
						if (messageType2 == NetworkChannelMessage.MessageType.SeedCiFileRequest2)
						{
							return new SeedCiFileRequest2(channel, array);
						}
						if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderPageSizeRequest)
						{
							return new SeedPageReaderPageSizeRequest(channel, array);
						}
						if (messageType2 == NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2)
						{
							return new ContinuousLogCopyRequest2(channel, array);
						}
					}
				}
				else if (messageType2 <= NetworkChannelMessage.MessageType.GetE00GenerationReply)
				{
					if (messageType2 == NetworkChannelMessage.MessageType.LogCopyServerStatus)
					{
						return new LogCopyServerStatusMsg(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.GranularLogComplete)
					{
						return new GranularLogCompleteMsg(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.GetE00GenerationReply)
					{
						return new GetE00GenerationReply(channel, array);
					}
				}
				else
				{
					if (messageType2 == NetworkChannelMessage.MessageType.NotifyEndOfLogAsyncReply)
					{
						return new NotifyEndOfLogAsyncReply(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.CancelCiFileReply)
					{
						return new CancelCiFileReply(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.SeedDatabaseFileReply)
					{
						return new SeedDatabaseFileReply(channel, array);
					}
				}
			}
			else if (messageType2 <= NetworkChannelMessage.MessageType.TestHealthReply)
			{
				if (messageType2 <= NetworkChannelMessage.MessageType.TestLogExistenceReply)
				{
					if (messageType2 == NetworkChannelMessage.MessageType.NotifyEndOfLogReply)
					{
						return new NotifyEndOfLogReply(channel, messageType, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.TestLogExistenceReply)
					{
						return new TestLogExistenceReply(channel, array);
					}
				}
				else
				{
					if (messageType2 == NetworkChannelMessage.MessageType.CopyLogReply)
					{
						return new CopyLogReply(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.QueryLogRangeReply)
					{
						return new QueryLogRangeReply(channel, array);
					}
					if (messageType2 == NetworkChannelMessage.MessageType.TestHealthReply)
					{
						return new TestHealthReply(channel, array);
					}
				}
			}
			else if (messageType2 <= NetworkChannelMessage.MessageType.CompressionReply)
			{
				if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileReply)
				{
					return new SeedPageReaderRollLogFileReply(channel, array);
				}
				if (messageType2 == NetworkChannelMessage.MessageType.ProgressCiFileReply)
				{
					return new ProgressCiFileReply(channel, array);
				}
				if (messageType2 == NetworkChannelMessage.MessageType.CompressionReply)
				{
					return new NetworkChannelCompressionReply(channel, array);
				}
			}
			else
			{
				if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderSinglePageReply)
				{
					return new SeedPageReaderSinglePageReply(channel, array);
				}
				if (messageType2 == NetworkChannelMessage.MessageType.SeedCiFileReply)
				{
					return new SeedCiFileReply(channel, array);
				}
				if (messageType2 == NetworkChannelMessage.MessageType.SeedPageReaderPageSizeReply)
				{
					return new SeedPageReaderPageSizeReply(channel, array);
				}
			}
			IL_51C:
			throw new NetworkUnexpectedMessageException(channel.PartnerNodeName, string.Format("Unknown message type: 0x{0:X}", (int)messageType));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003414C File Offset: 0x0003234C
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

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000341D1 File Offset: 0x000323D1
		internal virtual void SendInternal()
		{
			this.m_channel.SendMessage(this.m_packet.Buffer, 0, this.m_packet.Length);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000341F5 File Offset: 0x000323F5
		protected virtual void Serialize()
		{
			this.Packet.Append((uint)this.m_msgType);
			this.Packet.Append(16U);
			this.Packet.Append(this.m_messageUtc);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00034226 File Offset: 0x00032426
		internal NetworkChannelMessage(NetworkChannel channel, NetworkChannelMessage.MessageType msgType)
		{
			this.m_channel = channel;
			this.m_msgType = msgType;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0003423C File Offset: 0x0003243C
		internal NetworkChannelMessage(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, byte[] packetContent)
		{
			this.m_channel = channel;
			this.m_msgType = msgType;
			this.m_packet = new NetworkChannelPacket(packetContent);
			this.Packet.ExtractUInt32();
			this.Packet.ExtractUInt32();
			this.MessageUtc = this.Packet.ExtractDateTime();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00034292 File Offset: 0x00032492
		internal NetworkChannelMessage(NetworkChannelMessage.MessageType msgType)
		{
			this.m_msgType = msgType;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000342A4 File Offset: 0x000324A4
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

		// Token: 0x040004DB RID: 1243
		public const int GuidSize = 16;

		// Token: 0x040004DC RID: 1244
		internal const int MessageHeaderSize = 16;

		// Token: 0x040004DD RID: 1245
		private const int OffsetOfMessageLengthField = 4;

		// Token: 0x040004DE RID: 1246
		internal const int MaxMessageSize = 1052672;

		// Token: 0x040004DF RID: 1247
		protected NetworkChannel m_channel;

		// Token: 0x040004E0 RID: 1248
		protected NetworkChannelMessage.MessageType m_msgType;

		// Token: 0x040004E1 RID: 1249
		protected NetworkChannelPacket m_packet;

		// Token: 0x040004E2 RID: 1250
		protected DateTime m_messageUtc;

		// Token: 0x02000132 RID: 306
		internal enum MessageType
		{
			// Token: 0x040004E4 RID: 1252
			Invalid,
			// Token: 0x040004E5 RID: 1253
			CopyLogRequest = 1363627852,
			// Token: 0x040004E6 RID: 1254
			CopyLogReply = 1497845580,
			// Token: 0x040004E7 RID: 1255
			NotifyEndOfLogRequest = 1363627077,
			// Token: 0x040004E8 RID: 1256
			NotifyEndOfLogReply = 1497844805,
			// Token: 0x040004E9 RID: 1257
			NotifyEndOfLogAsyncReply = 1497451589,
			// Token: 0x040004EA RID: 1258
			QueryLogRangeRequest = 1363628620,
			// Token: 0x040004EB RID: 1259
			QueryLogRangeReply = 1497846348,
			// Token: 0x040004EC RID: 1260
			GetE00GenerationRequest = 1362112581,
			// Token: 0x040004ED RID: 1261
			GetE00GenerationReply = 1496330309,
			// Token: 0x040004EE RID: 1262
			TestLogExistenceRequest = 1363627092,
			// Token: 0x040004EF RID: 1263
			TestLogExistenceReply = 1497844820,
			// Token: 0x040004F0 RID: 1264
			TestHealthRequest = 1363694164,
			// Token: 0x040004F1 RID: 1265
			TestHealthReply = 1497911892,
			// Token: 0x040004F2 RID: 1266
			CancelCiFileRequest = 1363364163,
			// Token: 0x040004F3 RID: 1267
			CancelCiFileReply = 1497581891,
			// Token: 0x040004F4 RID: 1268
			ProgressCiFileRequest = 1364216131,
			// Token: 0x040004F5 RID: 1269
			ProgressCiFileReply = 1498433859,
			// Token: 0x040004F6 RID: 1270
			SeedCiFileRequest = 1364412739,
			// Token: 0x040004F7 RID: 1271
			SeedCiFileRequest2 = 1364478275,
			// Token: 0x040004F8 RID: 1272
			SeedCiFileReply = 1498630467,
			// Token: 0x040004F9 RID: 1273
			CompressionRequest = 1364217155,
			// Token: 0x040004FA RID: 1274
			CompressionReply = 1498434883,
			// Token: 0x040004FB RID: 1275
			CompressionConfig = 1129336131,
			// Token: 0x040004FC RID: 1276
			SeedDatabaseFileRequest = 1363559507,
			// Token: 0x040004FD RID: 1277
			SeedDatabaseFileReply = 1497777235,
			// Token: 0x040004FE RID: 1278
			PassiveDatabaseFileRequest = 1363559504,
			// Token: 0x040004FF RID: 1279
			SeedLogCopyRequest = 1363364947,
			// Token: 0x04000500 RID: 1280
			SeedPageReaderSinglePageRequest = 1364349011,
			// Token: 0x04000501 RID: 1281
			SeedPageReaderSinglePageReply = 1498566739,
			// Token: 0x04000502 RID: 1282
			SeedPageReaderMultiplePageRequest = 1364021331,
			// Token: 0x04000503 RID: 1283
			SeedPageReaderPageSizeRequest = 1364870227,
			// Token: 0x04000504 RID: 1284
			SeedPageReaderPageSizeReply = 1499087955,
			// Token: 0x04000505 RID: 1285
			SeedPageReaderRollLogFileRequest = 1363956307,
			// Token: 0x04000506 RID: 1286
			SeedPageReaderRollLogFileReply = 1498174035,
			// Token: 0x04000507 RID: 1287
			ContinuousLogCopyRequest = 1363627075,
			// Token: 0x04000508 RID: 1288
			ContinuousLogCopyRequest2 = 1380139852,
			// Token: 0x04000509 RID: 1289
			LogCopyServerStatus = 1397965644,
			// Token: 0x0400050A RID: 1290
			GranularLogData = 1312903751,
			// Token: 0x0400050B RID: 1291
			GranularLogComplete = 1481527879,
			// Token: 0x0400050C RID: 1292
			GranularTermination = 1297371719,
			// Token: 0x0400050D RID: 1293
			BlockModeCompressedData = 1145261378,
			// Token: 0x0400050E RID: 1294
			Ping = 1196312912,
			// Token: 0x0400050F RID: 1295
			TestNetwork0 = 810832724,
			// Token: 0x04000510 RID: 1296
			EnterBlockMode = 1313164610,
			// Token: 0x04000511 RID: 1297
			PassiveStatus = 1096045392
		}
	}
}
