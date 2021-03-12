using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000148 RID: 328
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WriteCommitStreamResultFactory : ResultFactory
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0001111C File Offset: 0x0000F31C
		internal WriteCommitStreamResultFactory()
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00011124 File Offset: 0x0000F324
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001112E File Offset: 0x0000F32E
		public RopResult CreateFailedResult(ErrorCode errorCode, ushort byteCount)
		{
			return new WriteCommitStreamResult(errorCode, byteCount);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00011137 File Offset: 0x0000F337
		public RopResult CreateSuccessfulResult(ushort byteCount)
		{
			return new WriteCommitStreamResult(ErrorCode.None, byteCount);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00011140 File Offset: 0x0000F340
		public override long SuccessfulResultMinimalSize
		{
			get
			{
				if (WriteCommitStreamResultFactory.successfulResultMinimalSize == 0L)
				{
					RopResult ropResult = new WriteCommitStreamResultFactory().CreateSuccessfulResult(0);
					ropResult.String8Encoding = CTSGlobals.AsciiEncoding;
					WriteCommitStreamResultFactory.successfulResultMinimalSize = RopResult.CalculateResultSize(ropResult);
				}
				return WriteCommitStreamResultFactory.successfulResultMinimalSize;
			}
		}

		// Token: 0x0400032A RID: 810
		private static long successfulResultMinimalSize;
	}
}
