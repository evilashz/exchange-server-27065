using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ExceptionTranslator
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00005110 File Offset: 0x00003310
		private static ErrorCode ErrorFromXsoException(Exception ex, out Exception translatedException)
		{
			translatedException = null;
			if (ExceptionTranslator.IsConnectionToStoreDead(ex))
			{
				translatedException = new SessionDeadException(string.Format("Connection is severed ({0}). Client needs to reconnect.", ex.GetType().Name), ex);
				return ExceptionTranslator.FindErrorCodeFromInnerExceptions(ex, ErrorCode.MdbOffline);
			}
			if (ex is InvalidRecipientsException)
			{
				return ErrorCode.InvalidRecipients;
			}
			if (ex is SaveConflictException)
			{
				return (ErrorCode)2147746057U;
			}
			if (ex is ObjectValidationException)
			{
				return (ErrorCode)2147746075U;
			}
			if (ex is ObjectNotFoundException && ex.InnerException is ADNoSuchObjectException)
			{
				return ErrorCode.ADNotFound;
			}
			if (ex is InvalidParamException)
			{
				return (ErrorCode)2147942487U;
			}
			if (ex is NonCanonicalACLException)
			{
				return ErrorCode.NonCanonicalACL;
			}
			if (ex is ACLTooBigException)
			{
				return (ErrorCode)2147746565U;
			}
			if (ex.InnerException is DataValidationException)
			{
				return ErrorCode.ADPropertyError;
			}
			if (ex.InnerException is ADTransientException)
			{
				return ErrorCode.ADUnavailable;
			}
			if (ex.InnerException is ADExternalException || ex.InnerException is ADOperationException)
			{
				return ErrorCode.ADError;
			}
			ErrorCode errorCode = ExceptionTranslator.FindErrorCodeFromInnerExceptions(ex, (ErrorCode)2147500037U);
			if (errorCode == ErrorCode.RpcServerTooBusy || errorCode == ErrorCode.MaxThreadsPerSCTExceeded || errorCode == ErrorCode.MaxThreadsPerMdbExceeded)
			{
				translatedException = new ClientBackoffException("Backend reported that it is too busy", ex);
				return ErrorCode.RpcServerTooBusy;
			}
			return errorCode;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005240 File Offset: 0x00003440
		private static ErrorCode ErrorFromXsoException(Exception ex, bool noThrow)
		{
			Exception ex2;
			ErrorCode result = ExceptionTranslator.ErrorFromXsoException(ex, out ex2);
			if (noThrow || ex2 == null)
			{
				return result;
			}
			throw ex2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000525F File Offset: 0x0000345F
		internal static ErrorCode ErrorFromXsoException(Exception ex)
		{
			return ExceptionTranslator.ErrorFromXsoException(ex, false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005268 File Offset: 0x00003468
		internal static bool IsConnectionToStoreDead(Exception ex)
		{
			return (ex is ConnectionFailedTransientException || ex is ConnectionFailedPermanentException) && !(ex.InnerException is MapiExceptionLogonFailed);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000528D File Offset: 0x0000348D
		internal static bool IsInterestingForInfoWatson(Exception ex)
		{
			return ex == null || (!ExceptionTranslator.IsConnectionToStoreDead(ex) && (!(ex is StorageTransientException) || !(ex.InnerException is MapiExceptionRpcServerTooBusy)));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000052B8 File Offset: 0x000034B8
		internal static bool IsInterestingForProtocolLogging(RopId ropId, ErrorCode errorCode)
		{
			if (errorCode <= (ErrorCode)2147746058U)
			{
				if (errorCode == ErrorCode.None)
				{
					return false;
				}
				if (errorCode != ErrorCode.SyncClientChangeNewer)
				{
					switch (errorCode)
					{
					case (ErrorCode)2147746057U:
					case (ErrorCode)2147746058U:
						break;
					default:
						return true;
					}
				}
			}
			else if (errorCode != (ErrorCode)2147746063U)
			{
				if (errorCode != (ErrorCode)2147747332U)
				{
					switch (errorCode)
					{
					case (ErrorCode)2147747841U:
					case (ErrorCode)2147747842U:
					case (ErrorCode)2147747843U:
						break;
					default:
						return true;
					}
				}
			}
			else
			{
				if (ropId != RopId.Logon)
				{
					return false;
				}
				return true;
			}
			if (ropId == RopId.ImportHierarchyChange || ropId == RopId.ImportMessageChange || ropId == RopId.ImportMessageChangePartial || ropId == RopId.ImportDelete || ropId == RopId.ImportMessageMove)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005360 File Offset: 0x00003560
		internal static bool TryExecuteCatchAndTranslateExceptions(Action protectedCode, bool noThrow, out Exception exception, out ErrorCode errorCode)
		{
			object obj;
			return ExceptionTranslator.TryExecuteCatchAndTranslateExceptions<object>(delegate()
			{
				protectedCode();
				return null;
			}, (object unused2) => ErrorCode.None, noThrow, out obj, out exception, out errorCode);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000053BC File Offset: 0x000035BC
		internal static bool TryExecuteCatchAndTranslateExceptions<TResult>(Func<TResult> protectedCode, Func<TResult, ErrorCode> errorCodeExtractor, bool noThrow, out TResult result, out Exception exception, out ErrorCode errorCode)
		{
			Func<RopExecutionException, bool, ErrorCode> func = null;
			Func<OverflowException, bool, ErrorCode> func2 = null;
			Util.ThrowOnNullArgument(protectedCode, "protectedCode");
			Util.ThrowOnNullArgument(errorCodeExtractor, "errorCodeExtractor");
			bool result2;
			try
			{
				TestInterceptor.Intercept(TestInterceptorLocation.ExceptionTranslator_TryExecuteCatchAndTranslateExceptions, new object[0]);
				result = protectedCode();
				exception = null;
				errorCode = errorCodeExtractor(result);
				result2 = true;
			}
			catch (StoragePermanentException exception2)
			{
				result2 = ExceptionTranslator.Translate<StoragePermanentException, TResult>(exception2, noThrow, new Func<StoragePermanentException, bool, ErrorCode>(ExceptionTranslator.ErrorFromXsoException), out result, out exception, out errorCode);
			}
			catch (StorageTransientException exception3)
			{
				result2 = ExceptionTranslator.Translate<StorageTransientException, TResult>(exception3, noThrow, new Func<StorageTransientException, bool, ErrorCode>(ExceptionTranslator.ErrorFromXsoException), out result, out exception, out errorCode);
			}
			catch (RopExecutionException ex)
			{
				RopExecutionException exception4 = ex;
				if (func == null)
				{
					func = ((RopExecutionException ropExecutionException, bool noThrowArgument) => ropExecutionException.ErrorCode);
				}
				result2 = ExceptionTranslator.Translate<RopExecutionException, TResult>(exception4, noThrow, func, out result, out exception, out errorCode);
			}
			catch (OverflowException ex2)
			{
				OverflowException exception5 = ex2;
				if (func2 == null)
				{
					func2 = ((OverflowException unused, bool noThrowArgument) => (ErrorCode)2147942487U);
				}
				result2 = ExceptionTranslator.Translate<OverflowException, TResult>(exception5, noThrow, func2, out result, out exception, out errorCode);
			}
			return result2;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000054D4 File Offset: 0x000036D4
		internal static bool IsWarningErrorCode(ErrorCode errorCode)
		{
			return errorCode > (ErrorCode)4000U && errorCode < (ErrorCode)2147483648U;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000054E8 File Offset: 0x000036E8
		private static ErrorCode FindErrorCodeFromInnerExceptions(Exception ex, ErrorCode defaultErrorCode)
		{
			MapiPermanentException ex2 = ex.InnerException as MapiPermanentException;
			MapiRetryableException ex3 = ex.InnerException as MapiRetryableException;
			ErrorCode result = defaultErrorCode;
			if (ex2 != null)
			{
				result = (ErrorCode)((ex2.LowLevelError != 0) ? ex2.LowLevelError : ex2.ErrorCode);
			}
			else if (ex3 != null)
			{
				result = (ErrorCode)((ex3.LowLevelError != 0) ? ex3.LowLevelError : ex3.ErrorCode);
			}
			else if (ex is ObjectNotFoundException)
			{
				result = (ErrorCode)2147746063U;
			}
			else if (ex is NoSupportException)
			{
				return (ErrorCode)2147746050U;
			}
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005566 File Offset: 0x00003766
		private static bool Translate<TException, TResult>(TException exception, bool noThrow, Func<TException, bool, ErrorCode> translator, out TResult result, out Exception outException, out ErrorCode errorCode) where TException : Exception
		{
			result = default(TResult);
			outException = exception;
			errorCode = translator(exception, noThrow);
			if (errorCode == ErrorCode.None)
			{
				errorCode = (ErrorCode)2147500037U;
			}
			return false;
		}
	}
}
