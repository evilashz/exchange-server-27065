using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000E RID: 14
	internal sealed class ClientConnectionInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003548 File Offset: 0x00001748
		public ClientConnectionInfoAuxiliaryBlock(Guid connectionGuid, uint connectionAttempts, uint connectionFlags, string connectionContextInfo) : base(1, AuxiliaryBlockTypes.ClientConnectionInfo)
		{
			this.connectionGuid = connectionGuid;
			this.connectionAttempts = connectionAttempts;
			this.connectionFlags = connectionFlags;
			this.connectionContextInfo = connectionContextInfo;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003570 File Offset: 0x00001770
		internal ClientConnectionInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.connectionGuid = reader.ReadGuid();
			ushort offset = reader.ReadUInt16();
			reader.ReadUInt16();
			this.connectionAttempts = reader.ReadUInt32();
			this.connectionFlags = reader.ReadUInt32();
			this.connectionContextInfo = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000035C3 File Offset: 0x000017C3
		public Guid ConnectionGuid
		{
			get
			{
				return this.connectionGuid;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000035CB File Offset: 0x000017CB
		public string ConnectionContextInfo
		{
			get
			{
				return this.connectionContextInfo;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000035D3 File Offset: 0x000017D3
		public uint ConnectionAttempts
		{
			get
			{
				return this.connectionAttempts;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000035DB File Offset: 0x000017DB
		public uint ConnectionFlags
		{
			get
			{
				return this.connectionFlags;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035E4 File Offset: 0x000017E4
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteGuid(this.connectionGuid);
			long position = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16(0);
			writer.WriteUInt32(this.connectionAttempts);
			writer.WriteUInt32(this.connectionFlags);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.connectionContextInfo, position);
		}

		// Token: 0x04000061 RID: 97
		private readonly string connectionContextInfo;

		// Token: 0x04000062 RID: 98
		private readonly Guid connectionGuid;

		// Token: 0x04000063 RID: 99
		private readonly uint connectionAttempts;

		// Token: 0x04000064 RID: 100
		private readonly uint connectionFlags;
	}
}
