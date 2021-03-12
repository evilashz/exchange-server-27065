using System;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DA RID: 474
	internal sealed class NspiModPropsRequest : MapiHttpRequest
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0001F398 File Offset: 0x0001D598
		public NspiModPropsRequest(NspiModPropsFlags flags, NspiState state, PropertyTag[] propertiesToRemove, PropertyValue[] modificationValues, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.propertiesToRemove = propertiesToRemove;
			this.modificationValues = modificationValues;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public NspiModPropsRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiModPropsFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			this.propertiesToRemove = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			this.modificationValues = reader.ReadNullableCountAndPropertyValueList(stateEncodingOrDefault, WireFormatStyle.Nspi);
			if (this.modificationValues != null)
			{
				for (int i = 0; i < this.modificationValues.Length; i++)
				{
					this.modificationValues[i].ResolveString8Values(stateEncodingOrDefault);
				}
			}
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001F447 File Offset: 0x0001D647
		public NspiModPropsFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001F44F File Offset: 0x0001D64F
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001F457 File Offset: 0x0001D657
		public PropertyTag[] PropertiesToRemove
		{
			get
			{
				return this.propertiesToRemove;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001F45F File Offset: 0x0001D65F
		public PropertyValue[] ModificationValues
		{
			get
			{
				return this.modificationValues;
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001F468 File Offset: 0x0001D668
		public override void Serialize(Writer writer)
		{
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableCountAndPropertyTagArray(this.propertiesToRemove, FieldLength.DWordSize);
			writer.WriteNullableCountAndPropertyValueList(this.modificationValues, stateEncodingOrDefault, WireFormatStyle.Nspi);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400045F RID: 1119
		private readonly NspiModPropsFlags flags;

		// Token: 0x04000460 RID: 1120
		private readonly NspiState state;

		// Token: 0x04000461 RID: 1121
		private readonly PropertyTag[] propertiesToRemove;

		// Token: 0x04000462 RID: 1122
		private readonly PropertyValue[] modificationValues;
	}
}
