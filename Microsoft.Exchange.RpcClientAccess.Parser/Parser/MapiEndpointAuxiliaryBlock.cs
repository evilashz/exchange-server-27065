using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200001D RID: 29
	internal sealed class MapiEndpointAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000039EF File Offset: 0x00001BEF
		public MapiEndpointAuxiliaryBlock(MapiEndpointProcessType mapiEndpointProcessType, string endpointFqdn) : base((endpointFqdn == null) ? 1 : 2, AuxiliaryBlockTypes.MapiEndpoint)
		{
			this.mapiEndpointProcessType = mapiEndpointProcessType;
			this.endpointFqdn = endpointFqdn;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A0F File Offset: 0x00001C0F
		internal MapiEndpointAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.mapiEndpointProcessType = (MapiEndpointProcessType)reader.ReadByte();
			if (base.Version >= 2)
			{
				this.endpointFqdn = reader.ReadAsciiString(StringFlags.Sized);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003A3A File Offset: 0x00001C3A
		public MapiEndpointProcessType MapiEndpointProcessType
		{
			get
			{
				return this.mapiEndpointProcessType;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003A42 File Offset: 0x00001C42
		public string EndpointFqdn
		{
			get
			{
				return this.endpointFqdn;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A4A File Offset: 0x00001C4A
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.mapiEndpointProcessType);
			if (base.Version >= 2)
			{
				writer.WriteAsciiString(this.endpointFqdn, StringFlags.Sized);
			}
		}

		// Token: 0x04000084 RID: 132
		private readonly MapiEndpointProcessType mapiEndpointProcessType;

		// Token: 0x04000085 RID: 133
		private readonly string endpointFqdn;
	}
}
