using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D8 RID: 472
	internal sealed class NspiModLinkAttRequest : MapiHttpRequest
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0001F29E File Offset: 0x0001D49E
		public NspiModLinkAttRequest(NspiModLinkAttFlags flags, PropertyTag propertyTag, int ephemeralId, byte[][] entryIds, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.propertyTag = propertyTag;
			this.ephemeralId = ephemeralId;
			this.entryIds = entryIds;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public NspiModLinkAttRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiModLinkAttFlags)reader.ReadUInt32();
			this.propertyTag = reader.ReadPropertyTag();
			this.ephemeralId = reader.ReadInt32();
			this.entryIds = reader.ReadNullableCountAndByteArrayList(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0001F314 File Offset: 0x0001D514
		public NspiModLinkAttFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001F31C File Offset: 0x0001D51C
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0001F324 File Offset: 0x0001D524
		public int EphemeralId
		{
			get
			{
				return this.ephemeralId;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0001F32C File Offset: 0x0001D52C
		public byte[][] EntryIds
		{
			get
			{
				return this.entryIds;
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001F334 File Offset: 0x0001D534
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteInt32(this.ephemeralId);
			writer.WriteNullableByteArrayList(this.entryIds, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400045B RID: 1115
		private readonly NspiModLinkAttFlags flags;

		// Token: 0x0400045C RID: 1116
		private readonly PropertyTag propertyTag;

		// Token: 0x0400045D RID: 1117
		private readonly int ephemeralId;

		// Token: 0x0400045E RID: 1118
		private readonly byte[][] entryIds;
	}
}
