using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000025 RID: 37
	internal sealed class PerfClientInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003C74 File Offset: 0x00001E74
		internal PerfClientInfoAuxiliaryBlock(uint blockAdapterSpeed, ushort blockClientId, string blockMachineName, string blockUserName, ArraySegment<byte> blockClientIp, ArraySegment<byte> blockClientIpMask, string blockAdapterName, ArraySegment<byte> blockMacAddress, ClientMode blockClientMode) : base(1, AuxiliaryBlockTypes.PerfClientInfo)
		{
			this.adapterSpeed = blockAdapterSpeed;
			this.clientId = blockClientId;
			this.machineName = blockMachineName;
			this.userName = blockUserName;
			this.clientIp = blockClientIp;
			this.clientIpMask = blockClientIpMask;
			this.adapterName = blockAdapterName;
			this.macAddress = blockMacAddress;
			this.clientMode = blockClientMode;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CD0 File Offset: 0x00001ED0
		internal PerfClientInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.adapterSpeed = reader.ReadUInt32();
			this.clientId = reader.ReadUInt16();
			ushort offset = reader.ReadUInt16();
			ushort offset2 = reader.ReadUInt16();
			ushort count = reader.ReadUInt16();
			ushort offset3 = reader.ReadUInt16();
			ushort count2 = reader.ReadUInt16();
			ushort offset4 = reader.ReadUInt16();
			ushort offset5 = reader.ReadUInt16();
			ushort count3 = reader.ReadUInt16();
			ushort offset6 = reader.ReadUInt16();
			this.clientMode = (ClientMode)reader.ReadUInt16();
			this.machineName = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset);
			this.userName = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset2);
			this.clientIp = AuxiliaryBlock.ReadBytesAtPosition(reader, offset3, count);
			this.clientIpMask = AuxiliaryBlock.ReadBytesAtPosition(reader, offset4, count2);
			this.adapterName = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset5);
			this.macAddress = AuxiliaryBlock.ReadBytesAtPosition(reader, offset6, count3);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003DA2 File Offset: 0x00001FA2
		public ClientMode ClientMode
		{
			get
			{
				return this.clientMode;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003DAA File Offset: 0x00001FAA
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003DB2 File Offset: 0x00001FB2
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003DBC File Offset: 0x00001FBC
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.adapterSpeed);
			writer.WriteUInt16(this.clientId);
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)this.clientIp.Count);
			long position3 = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)this.clientIpMask.Count);
			long position4 = writer.Position;
			writer.WriteUInt16(0);
			long position5 = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)this.macAddress.Count);
			long position6 = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16((ushort)this.clientMode);
			writer.WriteUInt16(0);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.machineName, position);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.userName, position2);
			AuxiliaryBlock.WriteBytesAndUpdateOffset(writer, this.clientIp, position3);
			AuxiliaryBlock.WriteBytesAndUpdateOffset(writer, this.clientIpMask, position4);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.adapterName, position5);
			AuxiliaryBlock.WriteBytesAndUpdateOffset(writer, this.macAddress, position6);
		}

		// Token: 0x0400008D RID: 141
		private readonly uint adapterSpeed;

		// Token: 0x0400008E RID: 142
		private readonly ushort clientId;

		// Token: 0x0400008F RID: 143
		private readonly string machineName;

		// Token: 0x04000090 RID: 144
		private readonly string userName;

		// Token: 0x04000091 RID: 145
		private readonly ArraySegment<byte> clientIp;

		// Token: 0x04000092 RID: 146
		private readonly ArraySegment<byte> clientIpMask;

		// Token: 0x04000093 RID: 147
		private readonly string adapterName;

		// Token: 0x04000094 RID: 148
		private readonly ArraySegment<byte> macAddress;

		// Token: 0x04000095 RID: 149
		private readonly ClientMode clientMode;
	}
}
