using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000226 RID: 550
	internal sealed class SuccessfulCloneStreamResult : RopResult
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x000269A5 File Offset: 0x00024BA5
		internal SuccessfulCloneStreamResult(IServerObject serverObject) : base(RopId.CloneStream, ErrorCode.None, serverObject)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000269B1 File Offset: 0x00024BB1
		internal SuccessfulCloneStreamResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000269BA File Offset: 0x00024BBA
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulCloneStreamResult(reader);
		}
	}
}
