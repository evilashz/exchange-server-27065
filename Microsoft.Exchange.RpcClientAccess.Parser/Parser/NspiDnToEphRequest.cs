using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CC RID: 460
	internal sealed class NspiDnToEphRequest : MapiHttpRequest
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		public NspiDnToEphRequest(NspiDNToEphFlags flags, string[] distinguishedNames, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.distinguishedNames = distinguishedNames;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001E8FB File Offset: 0x0001CAFB
		public NspiDnToEphRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiDNToEphFlags)reader.ReadUInt32();
			this.distinguishedNames = reader.ReadNullableCountAndString8List(CTSGlobals.AsciiEncoding, StringFlags.IncludeNull, FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001E92A File Offset: 0x0001CB2A
		public NspiDNToEphFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001E932 File Offset: 0x0001CB32
		public string[] DistinguishedNames
		{
			get
			{
				return this.distinguishedNames;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001E93A File Offset: 0x0001CB3A
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNullableCountAndString8List(this.distinguishedNames, CTSGlobals.AsciiEncoding, StringFlags.IncludeNull, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000435 RID: 1077
		private readonly NspiDNToEphFlags flags;

		// Token: 0x04000436 RID: 1078
		private readonly string[] distinguishedNames;
	}
}
