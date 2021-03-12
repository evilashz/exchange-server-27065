using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D2 RID: 466
	internal sealed class NspiGetPropsRequest : MapiHttpRequest
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x0001EDFE File Offset: 0x0001CFFE
		public NspiGetPropsRequest(NspiGetPropsFlags flags, NspiState state, PropertyTag[] propertyTags, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.propertyTags = propertyTags;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001EE1D File Offset: 0x0001D01D
		public NspiGetPropsRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiGetPropsFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.propertyTags = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0001EE52 File Offset: 0x0001D052
		public NspiGetPropsFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001EE5A File Offset: 0x0001D05A
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001EE62 File Offset: 0x0001D062
		public PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001EE6A File Offset: 0x0001D06A
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableCountAndPropertyTagArray(this.propertyTags, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000449 RID: 1097
		private readonly NspiGetPropsFlags flags;

		// Token: 0x0400044A RID: 1098
		private readonly NspiState state;

		// Token: 0x0400044B RID: 1099
		private readonly PropertyTag[] propertyTags;
	}
}
