using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000030 RID: 48
	internal sealed class PerfProcessInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004155 File Offset: 0x00002355
		internal PerfProcessInfoAuxiliaryBlock(byte blockVersion, ushort blockSessionId, Guid blockProcessGuid, string blockProcessName) : base(blockVersion, AuxiliaryBlockTypes.PerfProcessInfo)
		{
			if (blockVersion != 1 && blockVersion != 2)
			{
				throw new ArgumentException("Version must 1 or 2", "blockVersion");
			}
			this.processId = blockSessionId;
			this.processGuid = blockProcessGuid;
			this.processName = blockProcessName;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004190 File Offset: 0x00002390
		internal PerfProcessInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.processId = reader.ReadUInt16();
			reader.ReadUInt16();
			this.processGuid = reader.ReadGuid();
			ushort offset = reader.ReadUInt16();
			this.processName = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000041D7 File Offset: 0x000023D7
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000041E0 File Offset: 0x000023E0
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.processId);
			writer.WriteUInt16(0);
			writer.WriteGuid(this.processGuid);
			long position = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16(0);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.processName, position);
		}

		// Token: 0x04000096 RID: 150
		private readonly ushort processId;

		// Token: 0x04000097 RID: 151
		private readonly Guid processGuid;

		// Token: 0x04000098 RID: 152
		private readonly string processName;
	}
}
