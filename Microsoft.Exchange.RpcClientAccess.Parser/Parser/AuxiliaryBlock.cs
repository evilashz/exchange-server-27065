using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000007 RID: 7
	internal abstract class AuxiliaryBlock
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025F7 File Offset: 0x000007F7
		internal AuxiliaryBlock(byte blockVersion, AuxiliaryBlockTypes blockType)
		{
			this.version = blockVersion;
			this.type = blockType;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000260D File Offset: 0x0000080D
		protected AuxiliaryBlock(Reader reader)
		{
			this.version = reader.ReadByte();
			this.type = (AuxiliaryBlockTypes)reader.ReadByte();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000262D File Offset: 0x0000082D
		internal byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002635 File Offset: 0x00000835
		internal AuxiliaryBlockTypes Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002640 File Offset: 0x00000840
		public static AuxiliaryBlock Parse(BufferReader reader)
		{
			long num = reader.Length - reader.Position;
			if (num < 2L)
			{
				return new CorruptAuxiliaryBlock(reader);
			}
			int num2 = (int)reader.PeekUInt16(0L);
			if ((long)num2 > num || num2 <= 2)
			{
				return new CorruptAuxiliaryBlock(reader);
			}
			AuxiliaryBlock result;
			using (Reader reader2 = reader.SubReader(num2))
			{
				try
				{
					reader2.ReadUInt16();
					result = AuxiliaryBlock.InternalParse(reader2);
				}
				catch (BufferParseException)
				{
					reader2.Position = 0L;
					result = new CorruptAuxiliaryBlock(reader2);
				}
				finally
				{
					reader.Position += (long)num2;
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026F0 File Offset: 0x000008F0
		protected internal virtual void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026F4 File Offset: 0x000008F4
		internal bool TrySerialize(BufferWriter writer)
		{
			ushort num = this.CalculateSerializedSize();
			if (writer.AvailableSpace < (uint)num)
			{
				num = (ushort)this.Truncate((int)writer.AvailableSpace, (int)num);
				if (writer.AvailableSpace < (uint)num)
				{
					return false;
				}
			}
			using (Writer writer2 = writer.SubWriter())
			{
				writer2.WriteUInt16(num);
				this.Serialize(writer2);
			}
			writer.Position += (long)((ulong)num);
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000276C File Offset: 0x0000096C
		internal ushort CalculateSerializedSize()
		{
			ushort result;
			using (CountWriter countWriter = new CountWriter())
			{
				countWriter.WriteUInt16(0);
				this.Serialize(countWriter);
				result = (ushort)countWriter.Position;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027B4 File Offset: 0x000009B4
		protected static string ReadUnicodeStringAtPosition(Reader reader, ushort offset)
		{
			if (offset == 0)
			{
				return string.Empty;
			}
			long position = reader.Position;
			reader.Position = (long)((ulong)offset);
			string result = reader.ReadUnicodeString(StringFlags.IncludeNull);
			reader.Position = position;
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027EC File Offset: 0x000009EC
		protected static string ReadAsciiStringAtPosition(Reader reader, ushort offset)
		{
			if (offset == 0)
			{
				return string.Empty;
			}
			long position = reader.Position;
			reader.Position = (long)((ulong)offset);
			string result = reader.ReadAsciiString(StringFlags.IncludeNull);
			reader.Position = position;
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002824 File Offset: 0x00000A24
		protected static ArraySegment<byte> ReadBytesAtPosition(Reader reader, ushort offset, ushort count)
		{
			if (count == 0)
			{
				return Array<byte>.EmptySegment;
			}
			long position = reader.Position;
			reader.Position = (long)((ulong)offset);
			ArraySegment<byte> result = reader.ReadArraySegment((uint)count);
			reader.Position = position;
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002859 File Offset: 0x00000A59
		protected static void WriteUnicodeStringAndUpdateOffset(Writer writer, string stringToWrite, long offsetPosition)
		{
			if (string.IsNullOrEmpty(stringToWrite))
			{
				AuxiliaryBlock.WriteOffset(writer, offsetPosition, 0L);
				return;
			}
			AuxiliaryBlock.WriteOffset(writer, offsetPosition);
			writer.WriteUnicodeString(stringToWrite, StringFlags.IncludeNull);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000287C File Offset: 0x00000A7C
		protected static void WriteAsciiStringAndUpdateOffset(Writer writer, string stringToWrite, long offsetPosition)
		{
			if (string.IsNullOrEmpty(stringToWrite))
			{
				AuxiliaryBlock.WriteOffset(writer, offsetPosition, 0L);
				return;
			}
			AuxiliaryBlock.WriteOffset(writer, offsetPosition);
			writer.WriteAsciiString(stringToWrite, StringFlags.IncludeNull);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000289F File Offset: 0x00000A9F
		protected static void WriteBytesAndUpdateOffset(Writer writer, ArraySegment<byte> bytes, long offsetPosition)
		{
			if (bytes.Count == 0)
			{
				AuxiliaryBlock.WriteOffset(writer, offsetPosition, 0L);
				return;
			}
			AuxiliaryBlock.WriteOffset(writer, offsetPosition);
			writer.WriteBytesSegment(bytes);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028C2 File Offset: 0x00000AC2
		protected virtual void Serialize(Writer writer)
		{
			writer.WriteByte(this.version);
			writer.WriteByte((byte)this.type);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028DC File Offset: 0x00000ADC
		protected virtual int Truncate(int maxSerializedSize, int currentSize)
		{
			return currentSize;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028E0 File Offset: 0x00000AE0
		private static AuxiliaryBlock InternalParse(Reader reader)
		{
			byte b = reader.PeekByte(0L);
			byte b2 = reader.PeekByte(1L);
			if (b == 1)
			{
				AuxiliaryBlockTypes auxiliaryBlockTypes = (AuxiliaryBlockTypes)b2;
				switch (auxiliaryBlockTypes)
				{
				case AuxiliaryBlockTypes.PerfRequestId:
					return new PerfRequestIdAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfClientInfo:
					return new PerfClientInfoAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfServerInfo:
				case AuxiliaryBlockTypes.PerfSessionInfo:
				case AuxiliaryBlockTypes.OsInfo:
					break;
				case AuxiliaryBlockTypes.PerfDefMdbSuccess:
					return new PerfDefMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfDefGcSuccess:
					return new PerfDefGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfMdbSuccess:
					return new PerfMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfGcSuccess:
					return new PerfGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFailure:
					return new PerfFailureAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.ClientControl:
					return new ClientControlAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfProcessInfo:
					return new PerfProcessInfoAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgDefMdbSuccess:
					return new PerfBgDefMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgDefGcSuccess:
					return new PerfBgDefGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgMdbSuccess:
					return new PerfBgMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgGcSuccess:
					return new PerfBgGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgFailure:
					return new PerfBgFailureAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgDefMdbSuccess:
					return new PerfFgDefMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgDefGcSuccess:
					return new PerfFgDefGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgMdbSuccess:
					return new PerfFgMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgGcSuccess:
					return new PerfFgGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgFailure:
					return new PerfFgFailureAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.ExOrgInfo:
					return new ExOrgInfoAuxiliaryBlock(reader);
				default:
					switch (auxiliaryBlockTypes)
					{
					case AuxiliaryBlockTypes.DiagCtxReqId:
						return new DiagCtxReqIdAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.DiagCtxCtxData:
						return new DiagCtxCtxDataAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.PerRpcStatistics:
						return new PerRpcStatisticsAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ClientSessionInfo:
						return new ClientSessionInfoAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ServerCapabilities:
						return new ServerCapabilitiesAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.DiagCtxClientId:
						return new DiagCtxClientIdAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.EndpointCapabilities:
						return new EndpointCapabilitiesAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ExceptionTrace:
						return new ExceptionTraceAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ClientConnectionInfo:
						return new ClientConnectionInfoAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ServerSessionInfo:
						return new ServerSessionInfoAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.SetMonitoringContext:
						return new SetMonitoringContextAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ClientActivity:
						return new ClientActivityAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ProtocolDeviceIdentification:
						return new ProtocolDeviceIdentificationAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.MonitoringActivity:
						return new MonitoringActivityAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.ServerInformation:
						return new ServerInformationAuxiliaryBlock(reader);
					case AuxiliaryBlockTypes.IdentityCorrelationInfo:
						return new IdentityCorrelationAuxiliaryBlock(reader);
					}
					break;
				}
			}
			else if (b == 2)
			{
				AuxiliaryBlockTypes auxiliaryBlockTypes2 = (AuxiliaryBlockTypes)b2;
				switch (auxiliaryBlockTypes2)
				{
				case AuxiliaryBlockTypes.PerfMdbSuccess:
					return new PerfMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfGcSuccess:
					return new PerfGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFailure:
					return new PerfFailureAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.ClientControl:
				case AuxiliaryBlockTypes.PerfBgDefMdbSuccess:
				case AuxiliaryBlockTypes.PerfBgDefGcSuccess:
				case AuxiliaryBlockTypes.PerfFgDefMdbSuccess:
				case AuxiliaryBlockTypes.PerfFgDefGcSuccess:
					break;
				case AuxiliaryBlockTypes.PerfProcessInfo:
					return new PerfProcessInfoAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgMdbSuccess:
					return new PerfBgMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgGcSuccess:
					return new PerfBgGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfBgFailure:
					return new PerfBgFailureAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgMdbSuccess:
					return new PerfFgMdbSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgGcSuccess:
					return new PerfFgGcSuccessAuxiliaryBlock(reader);
				case AuxiliaryBlockTypes.PerfFgFailure:
					return new PerfFgFailureAuxiliaryBlock(reader);
				default:
					if (auxiliaryBlockTypes2 == AuxiliaryBlockTypes.PerRpcStatistics)
					{
						return new PerRpcStatisticsAuxiliaryBlock(reader);
					}
					break;
				}
			}
			else if (b == 3)
			{
				AuxiliaryBlockTypes auxiliaryBlockTypes3 = (AuxiliaryBlockTypes)b2;
				if (auxiliaryBlockTypes3 == AuxiliaryBlockTypes.PerRpcStatistics)
				{
					return new PerRpcStatisticsAuxiliaryBlock(reader);
				}
			}
			else if (b == 4)
			{
				AuxiliaryBlockTypes auxiliaryBlockTypes4 = (AuxiliaryBlockTypes)b2;
				if (auxiliaryBlockTypes4 == AuxiliaryBlockTypes.PerRpcStatistics)
				{
					return new PerRpcStatisticsAuxiliaryBlock(reader);
				}
			}
			else if (b == 5)
			{
				AuxiliaryBlockTypes auxiliaryBlockTypes5 = (AuxiliaryBlockTypes)b2;
				if (auxiliaryBlockTypes5 == AuxiliaryBlockTypes.PerRpcStatistics)
				{
					return new PerRpcStatisticsAuxiliaryBlock(reader);
				}
			}
			AuxiliaryBlockTypes auxiliaryBlockTypes6 = (AuxiliaryBlockTypes)b2;
			if (auxiliaryBlockTypes6 == AuxiliaryBlockTypes.MapiEndpoint)
			{
				return new MapiEndpointAuxiliaryBlock(reader);
			}
			return new UnknownAuxiliaryBlock(reader);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private static void WriteOffset(Writer writer, long offsetPosition)
		{
			AuxiliaryBlock.WriteOffset(writer, offsetPosition, writer.Position);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private static void WriteOffset(Writer writer, long offsetPosition, long offsetValue)
		{
			long position = writer.Position;
			writer.Position = offsetPosition;
			writer.WriteUInt16((ushort)offsetValue);
			writer.Position = position;
		}

		// Token: 0x04000039 RID: 57
		private readonly byte version;

		// Token: 0x0400003A RID: 58
		private readonly AuxiliaryBlockTypes type;
	}
}
