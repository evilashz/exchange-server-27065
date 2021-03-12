using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CE RID: 462
	internal sealed class NspiGetMatchesRequest : MapiHttpRequest
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public NspiGetMatchesRequest(NspiGetMatchesFlags flags, NspiState state, int[] inputEphemeralIds, uint interfaceOptions, Restriction restriction, NamedProperty propertyName, uint maxCount, PropertyTag[] columns, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.inputEphemeralIds = inputEphemeralIds;
			this.interfaceOptions = interfaceOptions;
			this.restriction = restriction;
			this.propertyName = propertyName;
			this.maxCount = maxCount;
			this.columns = columns;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public NspiGetMatchesRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiGetMatchesFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.inputEphemeralIds = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			this.interfaceOptions = reader.ReadUInt32();
			this.restriction = reader.ReadNullableRestriction(MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state));
			this.propertyName = reader.ReadNullableNamedProperty();
			this.maxCount = reader.ReadUInt32();
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001EA94 File Offset: 0x0001CC94
		public NspiGetMatchesFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
		public int[] InputEphemeralIds
		{
			get
			{
				return this.inputEphemeralIds;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001EAAC File Offset: 0x0001CCAC
		public uint InterfaceOptions
		{
			get
			{
				return this.interfaceOptions;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		public Restriction Restriction
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public NamedProperty PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		public uint MaxCount
		{
			get
			{
				return this.maxCount;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001EACC File Offset: 0x0001CCCC
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableSizeAndIntegerArray(this.inputEphemeralIds, FieldLength.DWordSize);
			writer.WriteUInt32(this.interfaceOptions);
			writer.WriteNullableRestriction(this.restriction, MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state));
			writer.WriteNullableNamedProperty(this.propertyName);
			writer.WriteUInt32(this.maxCount);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000438 RID: 1080
		private readonly NspiGetMatchesFlags flags;

		// Token: 0x04000439 RID: 1081
		private readonly NspiState state;

		// Token: 0x0400043A RID: 1082
		private readonly int[] inputEphemeralIds;

		// Token: 0x0400043B RID: 1083
		private readonly uint interfaceOptions;

		// Token: 0x0400043C RID: 1084
		private readonly Restriction restriction;

		// Token: 0x0400043D RID: 1085
		private readonly NamedProperty propertyName;

		// Token: 0x0400043E RID: 1086
		private readonly uint maxCount;

		// Token: 0x0400043F RID: 1087
		private readonly PropertyTag[] columns;
	}
}
