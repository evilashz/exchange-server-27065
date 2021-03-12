using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000273 RID: 627
	internal sealed class SuccessfulOpenAttachmentResult : RopResult
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x00029101 File Offset: 0x00027301
		internal SuccessfulOpenAttachmentResult(IServerObject serverObject) : base(RopId.OpenAttachment, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002911B File Offset: 0x0002731B
		internal SuccessfulOpenAttachmentResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00029124 File Offset: 0x00027324
		internal static SuccessfulOpenAttachmentResult Parse(Reader reader)
		{
			return new SuccessfulOpenAttachmentResult(reader);
		}
	}
}
