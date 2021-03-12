using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ResultFactory : IResultFactory
	{
		// Token: 0x0600041B RID: 1051
		public abstract RopResult CreateStandardFailedResult(ErrorCode errorCode);

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		public virtual long SuccessfulResultMinimalSize
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		protected static RopResult ParseResultOrProgress(RopId ropId, Reader reader, Func<Reader, RopResult> resultFunc)
		{
			return ResultFactory.ParseResultOrProgress(ropId, reader, resultFunc, resultFunc);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000E700 File Offset: 0x0000C900
		protected static RopResult ParseResultOrProgress(RopId ropId, Reader reader, Func<Reader, RopResult> successfulResultFunc, Func<Reader, RopResult> failedResultFunc)
		{
			RopId ropId2 = (RopId)reader.PeekByte(0L);
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			if (ropId2 == RopId.Progress)
			{
				if (errorCode == ErrorCode.None)
				{
					return new SuccessfulProgressResult(reader);
				}
				throw new BufferParseException("Unexpected failed progress");
			}
			else
			{
				if (ropId2 != ropId)
				{
					throw new BufferParseException(string.Format("Unexpected result RopId: {0}", ropId2));
				}
				if (errorCode == ErrorCode.None)
				{
					return successfulResultFunc(reader);
				}
				return failedResultFunc(reader);
			}
		}
	}
}
