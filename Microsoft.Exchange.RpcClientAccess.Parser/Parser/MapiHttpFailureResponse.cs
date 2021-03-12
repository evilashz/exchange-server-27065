using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C6 RID: 454
	internal sealed class MapiHttpFailureResponse : MapiHttpResponse
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0001E69B File Offset: 0x0001C89B
		public MapiHttpFailureResponse(uint statusCode, ArraySegment<byte> auxiliaryBuffer) : base(statusCode, auxiliaryBuffer)
		{
			if (base.StatusCode == 0U)
			{
				throw new ArgumentException("StatusCode must be nonzero in a failure response.");
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		public MapiHttpFailureResponse(Reader reader) : base(reader)
		{
			if (base.StatusCode == 0U)
			{
				throw new BufferParseException("StatusCode must be nonzero in a failure response.");
			}
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001E6DB File Offset: 0x0001C8DB
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400042C RID: 1068
		private const string StatusCodeExceptionMessage = "StatusCode must be nonzero in a failure response.";
	}
}
