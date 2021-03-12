using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WriteStreamExtendedResultFactory : ResultFactory
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00011191 File Offset: 0x0000F391
		internal WriteStreamExtendedResultFactory()
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00011199 File Offset: 0x0000F399
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0U);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000111A3 File Offset: 0x0000F3A3
		public RopResult CreateFailedResult(ErrorCode errorCode, uint byteCount)
		{
			return new WriteStreamExtendedResult(errorCode, byteCount);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000111AC File Offset: 0x0000F3AC
		public RopResult CreateSuccessfulResult(uint byteCount)
		{
			return new WriteStreamExtendedResult(ErrorCode.None, byteCount);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public override long SuccessfulResultMinimalSize
		{
			get
			{
				if (WriteStreamExtendedResultFactory.successfulResultMinimalSize == 0L)
				{
					RopResult ropResult = new WriteStreamExtendedResultFactory().CreateSuccessfulResult(0U);
					ropResult.String8Encoding = CTSGlobals.AsciiEncoding;
					WriteStreamExtendedResultFactory.successfulResultMinimalSize = RopResult.CalculateResultSize(ropResult);
				}
				return WriteStreamExtendedResultFactory.successfulResultMinimalSize;
			}
		}

		// Token: 0x0400032B RID: 811
		private static long successfulResultMinimalSize;
	}
}
