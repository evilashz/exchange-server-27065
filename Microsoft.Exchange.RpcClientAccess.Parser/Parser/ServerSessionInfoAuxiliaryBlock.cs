using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000037 RID: 55
	internal sealed class ServerSessionInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004939 File Offset: 0x00002B39
		public ServerSessionInfoAuxiliaryBlock(string sessionInfo) : base(1, AuxiliaryBlockTypes.ServerSessionInfo)
		{
			this.sessionInfo = sessionInfo;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000494C File Offset: 0x00002B4C
		internal ServerSessionInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			ushort offset = reader.ReadUInt16();
			this.sessionInfo = AuxiliaryBlock.ReadUnicodeStringAtPosition(reader, offset);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004974 File Offset: 0x00002B74
		public string SessionInfo
		{
			get
			{
				return this.sessionInfo;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000497C File Offset: 0x00002B7C
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			long position = writer.Position;
			writer.WriteUInt16(0);
			AuxiliaryBlock.WriteUnicodeStringAndUpdateOffset(writer, this.sessionInfo, position);
		}

		// Token: 0x040000B8 RID: 184
		private readonly string sessionInfo;
	}
}
