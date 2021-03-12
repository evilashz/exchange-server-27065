using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D4 RID: 468
	internal sealed class NspiGetSpecialTableRequest : MapiHttpRequest
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x0001EF5A File Offset: 0x0001D15A
		public NspiGetSpecialTableRequest(NspiGetHierarchyInfoFlags flags, NspiState state, uint? version, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.version = version;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001EF79 File Offset: 0x0001D179
		public NspiGetSpecialTableRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiGetHierarchyInfoFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.version = reader.ReadNullableUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001EFAD File Offset: 0x0001D1AD
		public NspiGetHierarchyInfoFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		public uint? Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001EFC5 File Offset: 0x0001D1C5
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableUInt32(this.version);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400044E RID: 1102
		private readonly NspiGetHierarchyInfoFlags flags;

		// Token: 0x0400044F RID: 1103
		private readonly NspiState state;

		// Token: 0x04000450 RID: 1104
		private readonly uint? version;
	}
}
