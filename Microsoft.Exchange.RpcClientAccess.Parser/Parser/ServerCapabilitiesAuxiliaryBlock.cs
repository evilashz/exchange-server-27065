using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000035 RID: 53
	internal sealed class ServerCapabilitiesAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000048A1 File Offset: 0x00002AA1
		public ServerCapabilitiesAuxiliaryBlock(ServerCapabilityFlag serverCapabilityFlags) : base(1, AuxiliaryBlockTypes.ServerCapabilities)
		{
			this.serverCapabilityFlags = serverCapabilityFlags;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000048B3 File Offset: 0x00002AB3
		internal ServerCapabilitiesAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.serverCapabilityFlags = (ServerCapabilityFlag)reader.ReadUInt32();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000048C8 File Offset: 0x00002AC8
		public ServerCapabilityFlag ServerCapabilityFlags
		{
			get
			{
				return this.serverCapabilityFlags;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000048D0 File Offset: 0x00002AD0
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.serverCapabilityFlags);
		}

		// Token: 0x040000B6 RID: 182
		private readonly ServerCapabilityFlag serverCapabilityFlags;
	}
}
