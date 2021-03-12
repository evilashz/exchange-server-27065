using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000017 RID: 23
	internal sealed class EndpointCapabilitiesAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003821 File Offset: 0x00001A21
		public EndpointCapabilitiesAuxiliaryBlock(EndpointCapabilityFlag endpointCapabilityFlags) : base(1, AuxiliaryBlockTypes.EndpointCapabilities)
		{
			this.endpointCapabilityFlags = endpointCapabilityFlags;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003833 File Offset: 0x00001A33
		internal EndpointCapabilitiesAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.endpointCapabilityFlags = (EndpointCapabilityFlag)reader.ReadUInt32();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003848 File Offset: 0x00001A48
		public EndpointCapabilityFlag EndpointCapabilityFlags
		{
			get
			{
				return this.endpointCapabilityFlags;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003850 File Offset: 0x00001A50
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.endpointCapabilityFlags);
		}

		// Token: 0x04000075 RID: 117
		private readonly EndpointCapabilityFlag endpointCapabilityFlags;
	}
}
