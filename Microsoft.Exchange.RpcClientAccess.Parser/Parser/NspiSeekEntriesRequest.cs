using System;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E4 RID: 484
	internal sealed class NspiSeekEntriesRequest : MapiHttpRequest
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0001FBF5 File Offset: 0x0001DDF5
		public NspiSeekEntriesRequest(NspiSeekEntriesFlags flags, NspiState state, PropertyValue? target, int[] explicitTable, PropertyTag[] columns, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.target = target;
			this.explicitTable = explicitTable;
			this.columns = columns;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001FC24 File Offset: 0x0001DE24
		public NspiSeekEntriesRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiSeekEntriesFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			this.target = reader.ReadNullablePropertyValue(WireFormatStyle.Nspi);
			if (this.target != null)
			{
				this.target.Value.ResolveString8Values(stateEncodingOrDefault);
			}
			this.explicitTable = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001FCAB File Offset: 0x0001DEAB
		public NspiSeekEntriesFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001FCB3 File Offset: 0x0001DEB3
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001FCBB File Offset: 0x0001DEBB
		public PropertyValue? Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0001FCC3 File Offset: 0x0001DEC3
		public int[] ExplicitTable
		{
			get
			{
				return this.explicitTable;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001FCCB File Offset: 0x0001DECB
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		public override void Serialize(Writer writer)
		{
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullablePropertyValue(this.target, stateEncodingOrDefault, WireFormatStyle.Nspi);
			writer.WriteNullableSizeAndIntegerArray(this.explicitTable, FieldLength.DWordSize);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400047D RID: 1149
		private readonly NspiSeekEntriesFlags flags;

		// Token: 0x0400047E RID: 1150
		private readonly NspiState state;

		// Token: 0x0400047F RID: 1151
		private readonly PropertyValue? target;

		// Token: 0x04000480 RID: 1152
		private readonly int[] explicitTable;

		// Token: 0x04000481 RID: 1153
		private readonly PropertyTag[] columns;
	}
}
