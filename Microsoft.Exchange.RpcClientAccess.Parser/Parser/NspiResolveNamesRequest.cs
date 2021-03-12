using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E0 RID: 480
	internal sealed class NspiResolveNamesRequest : MapiHttpRequest
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x0001F83E File Offset: 0x0001DA3E
		public NspiResolveNamesRequest(NspiResolveNamesFlags flags, NspiState state, PropertyTag[] columns, string[] names, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.columns = columns;
			this.names = names;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001F868 File Offset: 0x0001DA68
		public NspiResolveNamesRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiResolveNamesFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			this.names = reader.ReadNullableCountAndUnicodeStringList(StringFlags.IncludeNull, FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001F8B6 File Offset: 0x0001DAB6
		public NspiResolveNamesFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001F8BE File Offset: 0x0001DABE
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0001F8C6 File Offset: 0x0001DAC6
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0001F8CE File Offset: 0x0001DACE
		public string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001F8D6 File Offset: 0x0001DAD6
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			writer.WriteNullableCountAndUnicodeStringList(this.names, StringFlags.IncludeNull, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400046F RID: 1135
		private readonly NspiResolveNamesFlags flags;

		// Token: 0x04000470 RID: 1136
		private readonly NspiState state;

		// Token: 0x04000471 RID: 1137
		private readonly PropertyTag[] columns;

		// Token: 0x04000472 RID: 1138
		private readonly string[] names;
	}
}
