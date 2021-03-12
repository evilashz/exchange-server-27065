using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024F RID: 591
	internal sealed class SuccessfulGetAttachmentTableResult : RopResult
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00027ECE File Offset: 0x000260CE
		internal SuccessfulGetAttachmentTableResult(IServerObject serverObject) : base(RopId.GetAttachmentTable, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00027EE8 File Offset: 0x000260E8
		internal SuccessfulGetAttachmentTableResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00027EF1 File Offset: 0x000260F1
		internal static SuccessfulGetAttachmentTableResult Parse(Reader reader)
		{
			return new SuccessfulGetAttachmentTableResult(reader);
		}
	}
}
