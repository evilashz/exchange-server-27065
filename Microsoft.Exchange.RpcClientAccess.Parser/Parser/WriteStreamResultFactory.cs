using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200014B RID: 331
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WriteStreamResultFactory : ResultFactory
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x000111F7 File Offset: 0x0000F3F7
		internal WriteStreamResultFactory()
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000111FF File Offset: 0x0000F3FF
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00011209 File Offset: 0x0000F409
		public RopResult CreateFailedResult(ErrorCode errorCode, ushort byteCount)
		{
			return new WriteStreamResult(errorCode, byteCount);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00011212 File Offset: 0x0000F412
		public RopResult CreateSuccessfulResult(ushort byteCount)
		{
			return new WriteStreamResult(ErrorCode.None, byteCount);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001121C File Offset: 0x0000F41C
		public override long SuccessfulResultMinimalSize
		{
			get
			{
				if (WriteStreamResultFactory.successfulResultMinimalSize == 0L)
				{
					RopResult ropResult = new WriteStreamResultFactory().CreateSuccessfulResult(0);
					ropResult.String8Encoding = CTSGlobals.AsciiEncoding;
					WriteStreamResultFactory.successfulResultMinimalSize = RopResult.CalculateResultSize(ropResult);
				}
				return WriteStreamResultFactory.successfulResultMinimalSize;
			}
		}

		// Token: 0x0400032C RID: 812
		private static long successfulResultMinimalSize;
	}
}
