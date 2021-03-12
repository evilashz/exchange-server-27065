using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000019 RID: 25
	internal static class MapiUtil
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000065CC File Offset: 0x000047CC
		internal static TReturnValue TranslateMapiExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, LocalizedString errorString, Func<TReturnValue> mapiCall)
		{
			TReturnValue result = default(TReturnValue);
			MapiUtil.TranslateMapiExceptions(tracer, errorString, delegate
			{
				result = mapiCall();
			});
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000660C File Offset: 0x0000480C
		internal static void TranslateMapiExceptions(IDiagnosticsSession tracer, LocalizedString errorString, Action mapiCall)
		{
			try
			{
				mapiCall();
			}
			catch (MapiRetryableException ex)
			{
				tracer.TraceError<LocalizedString, MapiRetryableException>("Got exception from MAPI: {0}, {1}.", errorString, ex);
				throw new OperationFailedException(errorString, ex);
			}
			catch (MapiPermanentException ex2)
			{
				tracer.TraceError<LocalizedString, MapiPermanentException>("Got exception from MAPI: {0}, {1}.", errorString, ex2);
				throw new ComponentFailedPermanentException(errorString, ex2);
			}
		}
	}
}
