using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000033 RID: 51
	internal sealed class ProtocolDeviceIdentificationAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000472F File Offset: 0x0000292F
		internal ProtocolDeviceIdentificationAuxiliaryBlock(string manufacturer, string model, string serialNumber, string deviceVersion, string firmwareVersion) : base(1, AuxiliaryBlockTypes.ProtocolDeviceIdentification)
		{
			this.manufacturer = manufacturer;
			this.model = model;
			this.serialNumber = serialNumber;
			this.deviceVersion = deviceVersion;
			this.firmwareVersion = firmwareVersion;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004760 File Offset: 0x00002960
		internal ProtocolDeviceIdentificationAuxiliaryBlock(Reader reader) : base(reader)
		{
			ushort offset = reader.ReadUInt16();
			ushort offset2 = reader.ReadUInt16();
			ushort offset3 = reader.ReadUInt16();
			ushort offset4 = reader.ReadUInt16();
			ushort offset5 = reader.ReadUInt16();
			this.manufacturer = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset);
			this.model = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset2);
			this.serialNumber = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset3);
			this.deviceVersion = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset4);
			this.firmwareVersion = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset5);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000047DA File Offset: 0x000029DA
		public string Manufacturer
		{
			get
			{
				return this.manufacturer;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000047E2 File Offset: 0x000029E2
		public string Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000047EA File Offset: 0x000029EA
		public string SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000047F2 File Offset: 0x000029F2
		public string DeviceVersion
		{
			get
			{
				return this.deviceVersion;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000047FA File Offset: 0x000029FA
		public string FirmwareVersion
		{
			get
			{
				return this.firmwareVersion;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004804 File Offset: 0x00002A04
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			writer.WriteUInt16(0);
			long position3 = writer.Position;
			writer.WriteUInt16(0);
			long position4 = writer.Position;
			writer.WriteUInt16(0);
			long position5 = writer.Position;
			writer.WriteUInt16(0);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.manufacturer, position);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.model, position2);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.serialNumber, position3);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.deviceVersion, position4);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.firmwareVersion, position5);
		}

		// Token: 0x040000AE RID: 174
		private readonly string manufacturer;

		// Token: 0x040000AF RID: 175
		private readonly string model;

		// Token: 0x040000B0 RID: 176
		private readonly string serialNumber;

		// Token: 0x040000B1 RID: 177
		private readonly string deviceVersion;

		// Token: 0x040000B2 RID: 178
		private readonly string firmwareVersion;
	}
}
