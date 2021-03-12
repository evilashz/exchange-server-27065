using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000374 RID: 884
	internal sealed class WriteCommitStreamResult : RopResult
	{
		// Token: 0x0600158E RID: 5518 RVA: 0x00037BA7 File Offset: 0x00035DA7
		internal WriteCommitStreamResult(ErrorCode errorCode, ushort byteCount) : base(RopId.WriteCommitStream, errorCode, null)
		{
			this.byteCount = byteCount;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00037BBD File Offset: 0x00035DBD
		internal WriteCommitStreamResult(Reader reader) : base(reader)
		{
			this.byteCount = reader.ReadUInt16();
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00037BD2 File Offset: 0x00035DD2
		internal ushort ByteCount
		{
			get
			{
				return this.byteCount;
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00037BDA File Offset: 0x00035DDA
		internal static RopResult Parse(Reader reader)
		{
			return new WriteCommitStreamResult(reader);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00037BE2 File Offset: 0x00035DE2
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.byteCount);
		}

		// Token: 0x04000B39 RID: 2873
		private readonly ushort byteCount;
	}
}
