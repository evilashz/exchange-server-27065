using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001EC RID: 492
	internal sealed class RfriGetAddressBookUrlResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x00020273 File Offset: 0x0001E473
		public RfriGetAddressBookUrlResponse(uint returnCode, string serverUrl, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.serverUrl = serverUrl;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00020284 File Offset: 0x0001E484
		public RfriGetAddressBookUrlResponse(Reader reader) : base(reader)
		{
			this.serverUrl = reader.ReadUnicodeString(StringFlags.IncludeNull);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x000202A1 File Offset: 0x0001E4A1
		public string ServerUrl
		{
			get
			{
				return this.serverUrl;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000202A9 File Offset: 0x0001E4A9
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUnicodeString(this.serverUrl, StringFlags.IncludeNull);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400048E RID: 1166
		private readonly string serverUrl;
	}
}
