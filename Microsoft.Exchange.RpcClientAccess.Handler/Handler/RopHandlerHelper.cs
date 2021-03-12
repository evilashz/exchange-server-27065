using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RopHandlerHelper
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000FC78 File Offset: 0x0000DE78
		internal static RopResult CallHandler(IRopHandler ropHandler, Func<RopResult> codeCallingIntoXso, IResultFactory resultFactory, Func<IResultFactory, ErrorCode, Exception, RopResult> createFailedResult)
		{
			return RopHandlerHelper.CallHandler<RopResult>(codeCallingIntoXso, (RopResult ropResult) => ropResult.ErrorCode, resultFactory, createFailedResult, (RopResult ropResult) => ropResult.RopId);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		internal static T CallHandler<T>(NotificationQueue notificationQueue, Func<T> codeCallingIntoXso)
		{
			return RopHandlerHelper.CallHandler<T>(codeCallingIntoXso, (T result) => ErrorCode.None, null, (IResultFactory factory, ErrorCode errorCode, Exception exception) => default(T), (T ropResultOrNull) => RopId.Notify);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000FD14 File Offset: 0x0000DF14
		internal static RopResult CreateAndTraceFailedRopResult(IResultFactory resultFactory, Func<IResultFactory, ErrorCode, Exception, RopResult> createFailedResult, Exception exception, ErrorCode errorCode)
		{
			RopResult ropResult = createFailedResult(resultFactory, errorCode, exception);
			RopHandlerHelper.TraceRopResult(ropResult.RopId, exception, errorCode);
			return ropResult;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000FD39 File Offset: 0x0000DF39
		internal static void TraceRopResult(RopId ropId, Exception exception, ErrorCode errorCode)
		{
			if (errorCode != ErrorCode.None)
			{
				ProtocolLog.LogRopFailure(ropId, ExceptionTranslator.IsWarningErrorCode(errorCode), ExceptionTranslator.IsInterestingForProtocolLogging(ropId, errorCode), errorCode, exception);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000FD54 File Offset: 0x0000DF54
		private static TResult CallHandler<TResult>(Func<TResult> codeCallingIntoXso, Func<TResult, ErrorCode> errorCodeExtractor, IResultFactory resultFactory, Func<IResultFactory, ErrorCode, Exception, TResult> createFailedResult, Func<TResult, RopId> getRopIdForTracing)
		{
			TResult tresult;
			Exception ex;
			ErrorCode errorCode;
			if (!ExceptionTranslator.TryExecuteCatchAndTranslateExceptions<TResult>(codeCallingIntoXso, errorCodeExtractor, false, out tresult, out ex, out errorCode))
			{
				tresult = createFailedResult(resultFactory, errorCode, ex);
			}
			RopHandlerHelper.TraceRopResult(getRopIdForTracing(tresult), ex, errorCode);
			return tresult;
		}

		// Token: 0x04000078 RID: 120
		internal static readonly FastTransferCopyMessagesFlag FastTransferCopyMessagesClientOnlyFlags = (FastTransferCopyMessagesFlag)6;

		// Token: 0x04000079 RID: 121
		internal static readonly FastTransferCopyPropertiesFlag FastTransferCopyPropertiesClientOnlyFlags = (FastTransferCopyPropertiesFlag)30;

		// Token: 0x0400007A RID: 122
		internal static readonly FastTransferCopyFlag FastTransferCopyClientOnlyFlags = (FastTransferCopyFlag)14U;
	}
}
