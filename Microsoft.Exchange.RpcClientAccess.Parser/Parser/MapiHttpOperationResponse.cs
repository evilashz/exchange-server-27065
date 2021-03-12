using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BC RID: 444
	internal abstract class MapiHttpOperationResponse : MapiHttpResponse
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		protected MapiHttpOperationResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(0U, auxiliaryBuffer)
		{
			this.returnCode = returnCode;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001E2F3 File Offset: 0x0001C4F3
		protected MapiHttpOperationResponse(Reader reader) : base(reader)
		{
			this.returnCode = reader.ReadUInt32();
			if (base.StatusCode != 0U)
			{
				throw new InvalidStatusCodeException("Attempted to parse a successful response with a nonzero StatusCode", base.StatusCode);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001E321 File Offset: 0x0001C521
		public uint ReturnCode
		{
			get
			{
				return this.returnCode;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001E329 File Offset: 0x0001C529
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.returnCode);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001E33E File Offset: 0x0001C53E
		public override void AppendLogString(StringBuilder stringBuilder)
		{
			base.AppendLogString(stringBuilder);
			if (this.returnCode == 0U)
			{
				stringBuilder.Append(";RC:0");
				return;
			}
			stringBuilder.Append(";RC:");
			stringBuilder.Append(this.returnCode);
		}

		// Token: 0x0400041F RID: 1055
		private readonly uint returnCode;
	}
}
