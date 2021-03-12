using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001EE RID: 494
	internal sealed class RfriGetMailboxUrlResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x00020338 File Offset: 0x0001E538
		public RfriGetMailboxUrlResponse(uint returnCode, string serverUrl, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.serverUrl = serverUrl;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00020349 File Offset: 0x0001E549
		public RfriGetMailboxUrlResponse(Reader reader) : base(reader)
		{
			this.serverUrl = reader.ReadUnicodeString(StringFlags.IncludeNull);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00020366 File Offset: 0x0001E566
		public string ServerUrl
		{
			get
			{
				return this.serverUrl;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002036E File Offset: 0x0001E56E
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUnicodeString(this.serverUrl, StringFlags.IncludeNull);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000491 RID: 1169
		private readonly string serverUrl;
	}
}
