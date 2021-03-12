using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000119 RID: 281
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MapiExceptionFilter
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x000496E4 File Offset: 0x000478E4
		public static TResult TryOperation<TResult>(Func<TResult> operation, TResult resultOnException, Func<Exception, bool, bool> exceptionHandler)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			if (exceptionHandler == null)
			{
				throw new ArgumentNullException("exceptionHandler");
			}
			TResult result;
			try
			{
				result = operation();
			}
			catch (TransientException arg)
			{
				if (exceptionHandler(arg, true))
				{
					throw;
				}
				result = resultOnException;
			}
			catch (StoragePermanentException arg2)
			{
				if (exceptionHandler(arg2, false))
				{
					throw;
				}
				result = resultOnException;
			}
			catch (MapiPermanentException arg3)
			{
				if (exceptionHandler(arg3, false))
				{
					throw;
				}
				result = resultOnException;
			}
			catch (MissingSystemMailboxException arg4)
			{
				if (exceptionHandler(arg4, false))
				{
					throw;
				}
				result = resultOnException;
			}
			return result;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00049798 File Offset: 0x00047998
		public static void TryOperation(Action operation, Func<Exception, bool, bool> exceptionHandler)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			if (exceptionHandler == null)
			{
				throw new ArgumentNullException("exceptionHandler");
			}
			MapiExceptionFilter.TryOperation<object>(MapiExceptionFilter.ConvertActionToFunc(operation), null, exceptionHandler);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000497C4 File Offset: 0x000479C4
		public static void ThrowInnerIfMapiExceptionHandledbyAI(StoragePermanentException e)
		{
			if (e == null)
			{
				return;
			}
			MapiPermanentException ex = e.InnerException as MapiPermanentException;
			if (ex != null && MapiExceptionFilter.IsMapiPermanentExceptionHandledByAI(ex))
			{
				throw ex;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00049804 File Offset: 0x00047A04
		private static Func<object> ConvertActionToFunc(Action action)
		{
			return delegate()
			{
				action();
				return null;
			};
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0004982A File Offset: 0x00047A2A
		private static bool IsMapiPermanentExceptionHandledByAI(MapiPermanentException e)
		{
			return e != null && (e is MapiExceptionAmbiguousAlias || e is MapiExceptionJetErrorIndexNotFound || e is MapiExceptionJetErrorInstanceUnavailableDueToFatalLogDiskFull || e is MapiExceptionJetErrorLogDiskFull || e is MapiExceptionJetErrorReadVerifyFailure || e is MapiExceptionMdbOffline);
		}
	}
}
