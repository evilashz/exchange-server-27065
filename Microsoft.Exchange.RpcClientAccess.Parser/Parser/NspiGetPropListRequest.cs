using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D0 RID: 464
	internal sealed class NspiGetPropListRequest : MapiHttpRequest
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0001ED13 File Offset: 0x0001CF13
		public NspiGetPropListRequest(NspiGetPropListFlags flags, int ephemeralId, uint codePage, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.ephemeralId = ephemeralId;
			this.codePage = codePage;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001ED32 File Offset: 0x0001CF32
		public NspiGetPropListRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiGetPropListFlags)reader.ReadUInt32();
			this.ephemeralId = reader.ReadInt32();
			this.codePage = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001ED66 File Offset: 0x0001CF66
		public NspiGetPropListFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001ED6E File Offset: 0x0001CF6E
		public int EphemeralId
		{
			get
			{
				return this.ephemeralId;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001ED76 File Offset: 0x0001CF76
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001ED7E File Offset: 0x0001CF7E
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteInt32(this.ephemeralId);
			writer.WriteUInt32(this.codePage);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000445 RID: 1093
		private readonly NspiGetPropListFlags flags;

		// Token: 0x04000446 RID: 1094
		private readonly int ephemeralId;

		// Token: 0x04000447 RID: 1095
		private readonly uint codePage;
	}
}
