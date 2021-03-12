using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000D RID: 13
	internal sealed class ClientActivityAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000033F1 File Offset: 0x000015F1
		public ClientActivityAuxiliaryBlock(Guid activityId, uint testCaseId, string componentName, string protocolName, string actionString) : base(1, AuxiliaryBlockTypes.ClientActivity)
		{
			this.activityId = activityId;
			this.testCaseId = testCaseId;
			this.componentName = componentName;
			this.protocolName = protocolName;
			this.actionString = actionString;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003424 File Offset: 0x00001624
		internal ClientActivityAuxiliaryBlock(Reader reader) : base(reader)
		{
			if (base.Version >= 1)
			{
				this.activityId = reader.ReadGuid();
				this.testCaseId = reader.ReadUInt32();
				ushort offset = reader.ReadUInt16();
				ushort offset2 = reader.ReadUInt16();
				ushort offset3 = reader.ReadUInt16();
				reader.ReadUInt16();
				this.componentName = AuxiliaryBlock.ReadAsciiStringAtPosition(reader, offset);
				this.protocolName = AuxiliaryBlock.ReadAsciiStringAtPosition(reader, offset2);
				this.actionString = AuxiliaryBlock.ReadAsciiStringAtPosition(reader, offset3);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000349C File Offset: 0x0000169C
		public Guid ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000034A4 File Offset: 0x000016A4
		public uint TestCaseId
		{
			get
			{
				return this.testCaseId;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000034AC File Offset: 0x000016AC
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000034B4 File Offset: 0x000016B4
		public string ProtocolName
		{
			get
			{
				return this.protocolName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000034BC File Offset: 0x000016BC
		public string ActionString
		{
			get
			{
				return this.actionString;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000034C4 File Offset: 0x000016C4
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteGuid(this.ActivityId);
			writer.WriteUInt32(this.TestCaseId);
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			writer.WriteUInt16(0);
			long position3 = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt16(0);
			AuxiliaryBlock.WriteAsciiStringAndUpdateOffset(writer, this.ComponentName, position);
			AuxiliaryBlock.WriteAsciiStringAndUpdateOffset(writer, this.ProtocolName, position2);
			AuxiliaryBlock.WriteAsciiStringAndUpdateOffset(writer, this.ActionString, position3);
		}

		// Token: 0x0400005A RID: 90
		private const byte BlockVersion1 = 1;

		// Token: 0x0400005B RID: 91
		private const byte CurrentBlockVersion = 1;

		// Token: 0x0400005C RID: 92
		private readonly Guid activityId;

		// Token: 0x0400005D RID: 93
		private readonly uint testCaseId;

		// Token: 0x0400005E RID: 94
		private readonly string componentName;

		// Token: 0x0400005F RID: 95
		private readonly string protocolName;

		// Token: 0x04000060 RID: 96
		private readonly string actionString;
	}
}
