using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200027C RID: 636
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct StorageGlobals
	{
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0007C926 File Offset: 0x0007AB26
		internal static ExEventLog EventLogger
		{
			get
			{
				if (StorageGlobals.eventLogger == null)
				{
					StorageGlobals.eventLogger = new ExEventLog(new Guid("{8E4F12B2-E72A-42b4-816C-30462241203A}"), "MSExchange Mid-Tier Storage");
				}
				return StorageGlobals.eventLogger;
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0007C950 File Offset: 0x0007AB50
		public static LocalizedException TranslateMapiException(LocalizedString exceptionMessage, LocalizedException exception, StoreSession session, object thisObject, string traceMessage, params object[] traceMessageParameters)
		{
			if (ExTraceGlobals.MapiConnectivityTracer.IsTraceEnabled(TraceType.ErrorTrace) && (exception is MapiExceptionBackupInProgress || exception is MapiExceptionNetworkError || exception is MapiExceptionEndOfSession || exception is MapiExceptionLogonFailed || exception is MapiExceptionExiting || exception is MapiExceptionRpcServerTooBusy || exception is MapiExceptionBusy))
			{
				Guid guid = (session == null) ? Guid.Empty : session.MdbGuid;
				ExTraceGlobals.MapiConnectivityTracer.TraceError((long)((thisObject != null) ? thisObject.GetHashCode() : 0), "MAPI exception: {0}\n\rClient: {1}\n\rServer: {2}\n\rMDB: {3}", new object[]
				{
					exception.ToString(),
					(session != null) ? session.ClientInfoString : null,
					(session != null && !session.IsRemote) ? session.ServerFullyQualifiedDomainName : null,
					guid
				});
			}
			LocalizedException ex = null;
			if (session != null && session.IsRemote)
			{
				if (exception is MapiExceptionBackupInProgress || exception is MapiExceptionNetworkError || exception is MapiExceptionEndOfSession || exception is MapiExceptionLogonFailed || exception is MapiExceptionExiting || exception is MapiExceptionRpcServerTooBusy || exception is MapiExceptionBusy || exception is MapiExceptionMailboxInTransit || exception is MapiExceptionNotEnoughDisk || exception is MapiExceptionNotEnoughResources || exception is MapiExceptionMdbOffline || exception is MapiExceptionServerPaused || exception is MapiExceptionMailboxDisabled || exception is MapiExceptionAccountDisabled || exception is MapiExceptionWrongMailbox || exception is MapiExceptionCorruptStore || exception is MapiExceptionNoAccess || exception is MapiExceptionNoSupport || exception is MapiExceptionNotAuthorized || exception is MapiExceptionPasswordChangeRequired || exception is MapiExceptionPasswordExpired || exception is MapiExceptionNoMoreConnections || exception is MapiExceptionWrongServer || exception is MapiExceptionSessionLimit || exception is MapiExceptionUnconfigured || exception is MapiExceptionUnknownUser || exception is MapiExceptionCallFailed)
				{
					MailboxSession mailboxSession = session as MailboxSession;
					string text = mailboxSession.MailboxOwner.MailboxInfo.RemoteIdentity.Value.ToString();
					string text2 = exception.ToString();
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_XtcMapiError, text, new object[]
					{
						text,
						text2
					});
					ExTraceGlobals.XtcTracer.TraceError<string, string>(0L, "The remote connection threw exception for user {0}. Exception: {1}", text, text2);
				}
			}
			else
			{
				ex = StorageGlobals.CheckHAState(exceptionMessage, exception, session);
			}
			if (ex == null)
			{
				if (exception is MapiExceptionBackupInProgress || exception is MapiExceptionNetworkError || exception is MapiExceptionEndOfSession || exception is MapiExceptionLogonFailed || exception is MapiExceptionExiting)
				{
					ex = new ConnectionFailedTransientException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMailboxInTransit)
				{
					ex = new MailboxInTransitException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNotEnoughDisk || exception is MapiExceptionNotEnoughResources || exception is MapiExceptionBusy)
				{
					ex = new ResourcesException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionObjectChanged)
				{
					ex = new SaveConflictException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionCanNotComplete)
				{
					ex = new CannotCompleteOperationException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionJetErrorCheckpointDepthTooDeep)
				{
					ex = new CheckpointTooDeepException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMdbOffline)
				{
					ex = new MailboxOfflineException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionVirusScanInProgress)
				{
					ex = new VirusScanInProgressException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionServerPaused)
				{
					ex = new ServerPausedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionSearchEvaluationInProgress)
				{
					ex = new QueryInProgressException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionGlobalCounterRangeExceeded)
				{
					ex = new GlobalCounterRangeExceededException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionCorruptMidsetDeleted)
				{
					ex = new CorruptMidsetDeletedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMailboxDisabled || exception is MapiExceptionAccountDisabled || exception is MapiExceptionMailboxSoftDeleted)
				{
					ex = new AccountDisabledException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionWrongMailbox || exception is MapiExceptionCorruptStore)
				{
					ex = new ConnectionFailedPermanentException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNoAccess || exception is MapiExceptionNotAuthorized || exception is MapiExceptionPasswordChangeRequired || exception is MapiExceptionPasswordExpired || exception is MapiExceptionNoCreateRight || exception is MapiExceptionNoCreateSubfolderRight)
				{
					ex = new AccessDeniedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionCorruptData)
				{
					ex = new CorruptDataException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionQuotaExceeded || exception is MapiExceptionNamedPropsQuotaExceeded || exception is MapiExceptionShutoffQuotaExceeded)
				{
					ex = new QuotaExceededException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionCollision)
				{
					ex = new ObjectExistedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNotFound || exception is MapiExceptionInvalidEntryId || exception is MapiExceptionJetErrorRecordDeleted || exception is MapiExceptionObjectDeleted)
				{
					ex = new ObjectNotFoundException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNoMoreConnections)
				{
					ex = new NoMoreConnectionsException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionPartialCompletion)
				{
					ex = new PartialCompletionException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionFolderCycle)
				{
					ex = new FolderCycleException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionWrongServer)
				{
					ex = new WrongServerException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionIllegalCrossServerConnection)
				{
					ex = new IllegalCrossServerConnectionException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionInvalidRecipients)
				{
					ex = new InvalidRecipientsException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMessageTooBig)
				{
					ex = new MessageTooBigException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMaxSubmissionExceeded)
				{
					ex = new MessageSubmissionExceededException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionMaxAttachmentExceeded)
				{
					ex = new AttachmentExceededException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionVirusDetected)
				{
					ex = new VirusDetectedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionVirusMessageDeleted)
				{
					ex = new VirusMessageDeletedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNotInitialized)
				{
					ex = new ObjectNotInitializedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionSendAsDenied)
				{
					ex = new SendAsDeniedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionSessionLimit)
				{
					ex = new TooManyObjectsOpenedException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut)
				{
					ex = new ServerCleanupTimedOutException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionJetErrorInvalidLanguageId)
				{
					ex = new InvalidFolderLanguageIdException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionEventsDeleted)
				{
					ex = new EventNotFoundException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionUnconfigured)
				{
					ex = new MailboxUnavailableException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionUnknownUser)
				{
					ex = new MailboxUnavailableException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNoReplicaHere)
				{
					ex = new NoReplicaHereException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNoReplicaAvailable)
				{
					ex = new NoReplicaException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionConditionViolation)
				{
					ex = new FolderSaveConditionViolationException(exceptionMessage, exception);
				}
				else if (exception is MapiExceptionNoSupport)
				{
					ex = new NoSupportException(exceptionMessage, exception);
				}
				else if (exception is MapiPermanentException)
				{
					ex = new StoragePermanentException(exceptionMessage, exception);
				}
				else if (exception is MapiRetryableException)
				{
					ex = new StorageTransientException(exceptionMessage, exception);
				}
				else
				{
					if (!(exception is MapiExceptionCallFailed))
					{
						throw new ArgumentException("Exception is not of type MapiException");
					}
					ex = new StoragePermanentException(exceptionMessage, exception);
				}
			}
			if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				string arg = string.Format(traceMessage, traceMessageParameters);
				ExTraceGlobals.StorageTracer.TraceError<string, LocalizedException>((long)((thisObject != null) ? thisObject.GetHashCode() : 0), "{0}. Throwing exception: {1}.", arg, ex);
			}
			return ex;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0007D020 File Offset: 0x0007B220
		internal static bool IsDiskFullException(IOException e)
		{
			uint hrforException = (uint)Marshal.GetHRForException(e);
			return hrforException == 2147942512U;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0007D03C File Offset: 0x0007B23C
		internal static void SetCallMapiTestHook(StorageGlobals.MapiTestHook beforeCall, StorageGlobals.MapiTestHook afterCall)
		{
			StorageGlobals.MapiTestHookBeforeCall = beforeCall;
			StorageGlobals.MapiTestHookAfterCall = afterCall;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0007D04A File Offset: 0x0007B24A
		static StorageGlobals()
		{
			StorageGlobals.buildVersion = StorageGlobals.ObtainBuildVersion();
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0007D068 File Offset: 0x0007B268
		internal static TResult ProtectedADCall<TResult>(Func<TResult> activeDirectoryCall)
		{
			TResult result;
			try
			{
				result = activeDirectoryCall();
			}
			catch (DataSourceOperationException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "StorageGlobals::{0} failed due to directory exception {1}.", new object[]
				{
					activeDirectoryCall,
					ex
				});
			}
			catch (DataSourceTransientException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "StorageGlobals::{0} failed due to directory exception {1}.", new object[]
				{
					activeDirectoryCall,
					ex2
				});
			}
			catch (DataValidationException ex3)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex3, null, "StorageGlobals::{0} failed due to directory exception {1}.", new object[]
				{
					activeDirectoryCall,
					ex3
				});
			}
			return result;
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0007D11C File Offset: 0x0007B31C
		internal static long BuildVersion
		{
			get
			{
				return StorageGlobals.buildVersion;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0007D123 File Offset: 0x0007B323
		internal static string BuildVersionString
		{
			get
			{
				return "15.00.1497.010";
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0007D12C File Offset: 0x0007B32C
		internal static string ExchangeVersion
		{
			get
			{
				if (StorageGlobals.exchangeVersion == null)
				{
					StorageGlobals.exchangeVersion = ((ushort)(StorageGlobals.BuildVersion >> 48)).ToString(NumberFormatInfo.InvariantInfo) + "." + ((ushort)(StorageGlobals.BuildVersion >> 32)).ToString(NumberFormatInfo.InvariantInfo) + "Ex";
				}
				return StorageGlobals.exchangeVersion;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0007D188 File Offset: 0x0007B388
		private static long ObtainBuildVersion()
		{
			return 4222124748767242L;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0007D1A0 File Offset: 0x0007B3A0
		public static void TraceConstructIDisposable(object obj)
		{
			if (ExTraceGlobals.DisposeTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int hashCode = obj.GetHashCode();
				ExTraceGlobals.DisposeTracer.TraceDebug<string, int>((long)hashCode, "{0}::Constructor. Hashcode = {1}.", obj.GetType().Name, hashCode);
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0007D1E0 File Offset: 0x0007B3E0
		public static void TraceDispose(object obj, bool isDisposed, bool disposing)
		{
			if (ExTraceGlobals.DisposeTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int hashCode = obj.GetHashCode();
				ExTraceGlobals.DisposeTracer.TraceDebug((long)hashCode, "{0}::Dispose. Hashcode = {1}. IsDisposed = {2}. Disposing = {3}.", new object[]
				{
					obj.GetType().Name,
					hashCode,
					isDisposed,
					disposing
				});
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0007D244 File Offset: 0x0007B444
		public static void TraceFailedCheckDisposed(object obj, string methodName)
		{
			if (ExTraceGlobals.DisposeTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int hashCode = obj.GetHashCode();
				ExTraceGlobals.DisposeTracer.TraceDebug<string, string, int>((long)hashCode, "{0}::{1}. Attempted to use disposed object. Hashcode = {2}.", obj.GetType().Name, methodName, hashCode);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0007D284 File Offset: 0x0007B484
		public static void TraceFailedCheckDead(object obj, string methodName)
		{
			if (ExTraceGlobals.DisposeTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int hashCode = obj.GetHashCode();
				ExTraceGlobals.DisposeTracer.TraceDebug<string, string, int>((long)hashCode, "{0}::{1}. Attempted to use object on dead session. Hashcode = {2}.", obj.GetType().Name, methodName, hashCode);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0007D2C3 File Offset: 0x0007B4C3
		private static Stack<StorageGlobals.TraceContextDescriptor> TraceContextStack
		{
			get
			{
				if (StorageGlobals.traceContextStack == null)
				{
					StorageGlobals.traceContextStack = new Stack<StorageGlobals.TraceContextDescriptor>();
				}
				return StorageGlobals.traceContextStack;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0007D2DB File Offset: 0x0007B4DB
		private static Random TraceContextRandom
		{
			get
			{
				if (StorageGlobals.traceContextRandom == null)
				{
					StorageGlobals.traceContextRandom = new Random();
				}
				return StorageGlobals.traceContextRandom;
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0007D2F4 File Offset: 0x0007B4F4
		private static int GetTraceContextCode(bool isTraceEnabled)
		{
			if (StorageGlobals.TraceContextStack.Count > 0)
			{
				StorageGlobals.TraceContextDescriptor traceContextDescriptor = StorageGlobals.TraceContextStack.Peek();
				return traceContextDescriptor.GetContextCode(isTraceEnabled);
			}
			return 0;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0007D322 File Offset: 0x0007B522
		public static IDisposable SetTraceContext(object context)
		{
			return new StorageGlobals.TraceContextDescriptor(context);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0007D32C File Offset: 0x0007B52C
		public static void ContextTracePfd(Trace tracer, string message)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.PfdTrace);
			tracer.TracePfd((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), message);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0007D350 File Offset: 0x0007B550
		public static void ContextTracePfd(Trace tracer, string formatString, params object[] args)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.PfdTrace);
			tracer.TracePfd((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, args);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0007D374 File Offset: 0x0007B574
		public static void ContextTracePfd<T>(Trace tracer, string formatString, T arg0)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.PfdTrace);
			tracer.TracePfd<T>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0007D398 File Offset: 0x0007B598
		public static void ContextTracePfd<T0, T1>(Trace tracer, string formatString, T0 arg0, T1 arg1)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.PfdTrace);
			tracer.TracePfd<T0, T1>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0007D3C0 File Offset: 0x0007B5C0
		public static void ContextTracePfd<T0, T1, T2>(Trace tracer, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.PfdTrace);
			tracer.TracePfd<T0, T1, T2>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1, arg2);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0007D3E8 File Offset: 0x0007B5E8
		public static void ContextTraceInformation(Trace tracer, string message)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.InfoTrace);
			tracer.Information((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), message);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0007D40C File Offset: 0x0007B60C
		public static void ContextTraceInformation(Trace tracer, string formatString, params object[] args)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.InfoTrace);
			tracer.Information((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, args);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0007D430 File Offset: 0x0007B630
		public static void ContextTraceInformation<T>(Trace tracer, string formatString, T arg0)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.InfoTrace);
			tracer.Information<T>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0007D454 File Offset: 0x0007B654
		public static void ContextTraceInformation<T0, T1>(Trace tracer, string formatString, T0 arg0, T1 arg1)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.InfoTrace);
			tracer.Information<T0, T1>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0007D47C File Offset: 0x0007B67C
		public static void ContextTraceInformation<T0, T1, T2>(Trace tracer, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.InfoTrace);
			tracer.Information<T0, T1, T2>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1, arg2);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0007D4A4 File Offset: 0x0007B6A4
		public static void ContextTraceDebug(Trace tracer, string message)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.DebugTrace);
			tracer.TraceDebug((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), message);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0007D4C8 File Offset: 0x0007B6C8
		public static void ContextTraceDebug(Trace tracer, string formatString, params object[] args)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.DebugTrace);
			tracer.TraceDebug((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, args);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0007D4EC File Offset: 0x0007B6EC
		public static void ContextTraceDebug<T>(Trace tracer, string formatString, T arg0)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.DebugTrace);
			tracer.TraceDebug<T>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0007D510 File Offset: 0x0007B710
		public static void ContextTraceDebug<T0, T1>(Trace tracer, string formatString, T0 arg0, T1 arg1)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.DebugTrace);
			tracer.TraceDebug<T0, T1>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0007D538 File Offset: 0x0007B738
		public static void ContextTraceDebug<T0, T1, T2>(Trace tracer, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.DebugTrace);
			tracer.TraceDebug<T0, T1, T2>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1, arg2);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0007D560 File Offset: 0x0007B760
		public static void ContextTraceError(Trace tracer, string message)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.ErrorTrace);
			tracer.TraceError((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), message);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0007D584 File Offset: 0x0007B784
		public static void ContextTraceError(Trace tracer, string formatString, params object[] args)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.ErrorTrace);
			tracer.TraceError((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, args);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		public static void ContextTraceError<T0>(Trace tracer, string formatString, T0 arg0)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.ErrorTrace);
			tracer.TraceError<T0>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0007D5CC File Offset: 0x0007B7CC
		public static void ContextTraceError<T0, T1>(Trace tracer, string formatString, T0 arg0, T1 arg1)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.ErrorTrace);
			tracer.TraceError<T0, T1>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0007D5F4 File Offset: 0x0007B7F4
		public static void ContextTraceError<T0, T1, T2>(Trace tracer, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			bool isTraceEnabled = tracer.IsTraceEnabled(TraceType.ErrorTrace);
			tracer.TraceError<T0, T1, T2>((long)StorageGlobals.GetTraceContextCode(isTraceEnabled), formatString, arg0, arg1, arg2);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0007D61C File Offset: 0x0007B81C
		private static LocalizedException CheckHAState(LocalizedString exceptionMessage, LocalizedException mapiException, StoreSession session)
		{
			if (session != null && (mapiException is MapiExceptionBackupInProgress || mapiException is MapiExceptionEndOfSession || mapiException is MapiExceptionLogonFailed || mapiException is MapiExceptionExiting || mapiException is MapiExceptionMailboxInTransit || mapiException is MapiExceptionCanNotComplete || mapiException is MapiExceptionMdbOffline || mapiException is MapiExceptionWrongServer || mapiException is MapiExceptionUnconfigured || mapiException is MapiExceptionUnknownUser || mapiException is MapiExceptionConditionViolation || mapiException is MapiExceptionServerPaused || mapiException is MapiExceptionCallFailed))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<LocalizedException>((long)session.GetHashCode(), "StorageGlobals::CheckHAState. Translating exception {0}.", mapiException);
				DatabaseLocationInfo databaseLocationInfoOnOperationFailure = session.GetDatabaseLocationInfoOnOperationFailure();
				ExTraceGlobals.SessionTracer.TraceDebug<DatabaseLocationInfoResult>((long)session.GetHashCode(), "StorageGlobals::CheckHAState. AM result {0}.", databaseLocationInfoOnOperationFailure.RequestResult);
				switch (databaseLocationInfoOnOperationFailure.RequestResult)
				{
				case DatabaseLocationInfoResult.Success:
				{
					IExchangePrincipal mailboxOwner = session.MailboxOwner;
					if (mailboxOwner != null)
					{
						IMailboxInfo mailboxInfo = mailboxOwner.MailboxInfo;
						if (mailboxInfo != null)
						{
							IMailboxLocation location = mailboxInfo.Location;
							if (location != null)
							{
								if (string.Equals(location.ServerFqdn, databaseLocationInfoOnOperationFailure.ServerFqdn, StringComparison.OrdinalIgnoreCase))
								{
									if (mapiException is MapiExceptionWrongServer)
									{
										return new WrongServerException(exceptionMessage, mapiException);
									}
									return null;
								}
								else
								{
									if (location.ServerSite == null || databaseLocationInfoOnOperationFailure.ServerSite == null)
									{
										return new MailboxInSiteFailoverException(exceptionMessage, mapiException);
									}
									if (ADObjectId.Equals(location.ServerSite, databaseLocationInfoOnOperationFailure.ServerSite))
									{
										return new MailboxInSiteFailoverException(exceptionMessage, mapiException);
									}
									return new MailboxCrossSiteFailoverException(exceptionMessage, mapiException, databaseLocationInfoOnOperationFailure);
								}
							}
						}
					}
					break;
				}
				case DatabaseLocationInfoResult.Unknown:
					return new MailboxOfflineException(exceptionMessage, mapiException);
				case DatabaseLocationInfoResult.InTransitSameSite:
					return new MailboxInSiteFailoverException(exceptionMessage, mapiException);
				case DatabaseLocationInfoResult.InTransitCrossSite:
					return new MailboxCrossSiteFailoverException(exceptionMessage, mapiException, databaseLocationInfoOnOperationFailure);
				case DatabaseLocationInfoResult.SiteViolation:
					return new WrongServerException(exceptionMessage, mapiException);
				default:
					throw new NotSupportedException(string.Format("DatabaseLocationInfoResult.{0} is not supported", databaseLocationInfoOnOperationFailure));
				}
			}
			return null;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0007D7B4 File Offset: 0x0007B9B4
		internal static LocalizedException TranslateDirectoryException(LocalizedString exceptionMessage, LocalizedException exception, object thisObject, string traceMessage = "", params object[] traceMessageParameters)
		{
			LocalizedException ex;
			if (exception is ADNoSuchObjectException)
			{
				ex = new ObjectNotFoundException(exceptionMessage, exception);
			}
			else if (exception is DataValidationException || exception is DataConversionException || exception is NonUniqueRecipientException)
			{
				ex = new StoragePermanentException(exceptionMessage, exception);
			}
			else if (exception is ADTransientException || exception is ComputerNameNotCurrentlyAvailableException || exception is ADPossibleOperationException || exception is RusServerUnavailableException || exception is DataSourceTransientException)
			{
				ex = new StorageTransientException(exceptionMessage, exception);
			}
			else if (exception is ADOperationException || exception is ADObjectAlreadyExistsException || exception is ADFilterException || exception is ServerRoleOperationException || exception is RusOperationException || exception is NameConversionException || exception is GenerateUniqueLegacyDnException)
			{
				ex = new StoragePermanentException(exceptionMessage, exception);
			}
			else
			{
				if (!(exception is ADExternalException) && !(exception is CannotGetComputerNameException) && !(exception is CannotGetDomainInfoException) && !(exception is CannotGetSiteInfoException) && !(exception is CannotGetForestInfoException) && !(exception is LocalServerNotFoundException) && !(exception is DataSourceOperationException))
				{
					throw new ArgumentException("exception is not a directory exception");
				}
				ex = new StoragePermanentException(exceptionMessage, exception);
			}
			if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				string arg = string.Format(CultureInfo.InvariantCulture, traceMessage, traceMessageParameters);
				ExTraceGlobals.StorageTracer.TraceError<string, LocalizedException>((long)((thisObject != null) ? thisObject.GetHashCode() : 0), "{0}. Throwing exception: {1}.", arg, ex);
			}
			return ex;
		}

		// Token: 0x040012B7 RID: 4791
		private static ExEventLog eventLogger;

		// Token: 0x040012B8 RID: 4792
		private static long buildVersion;

		// Token: 0x040012B9 RID: 4793
		private static string exchangeVersion;

		// Token: 0x040012BA RID: 4794
		[ThreadStatic]
		public static bool RPCBudgetStarted = false;

		// Token: 0x040012BB RID: 4795
		[ThreadStatic]
		public static StorageGlobals.MapiTestHook MapiTestHookBeforeCall = null;

		// Token: 0x040012BC RID: 4796
		public static StorageGlobals.MapiTestHook MapiTestHookAfterCall = null;

		// Token: 0x040012BD RID: 4797
		[ThreadStatic]
		private static Stack<StorageGlobals.TraceContextDescriptor> traceContextStack;

		// Token: 0x040012BE RID: 4798
		[ThreadStatic]
		private static Random traceContextRandom;

		// Token: 0x0200027D RID: 637
		// (Invoke) Token: 0x06001AB1 RID: 6833
		internal delegate void MapiCall();

		// Token: 0x0200027E RID: 638
		// (Invoke) Token: 0x06001AB5 RID: 6837
		internal delegate T MapiCallWithReturnValue<T>();

		// Token: 0x0200027F RID: 639
		// (Invoke) Token: 0x06001AB9 RID: 6841
		internal delegate void MapiTestHook(object caller);

		// Token: 0x02000280 RID: 640
		private class TraceContextDescriptor : IDisposable
		{
			// Token: 0x06001ABC RID: 6844 RVA: 0x0007D8FB File Offset: 0x0007BAFB
			public TraceContextDescriptor(object context)
			{
				this.context = context;
				this.isWritten = false;
				this.isWrittenShadow = false;
				this.isDisposed = false;
				this.contextCode = StorageGlobals.TraceContextRandom.Next();
				StorageGlobals.TraceContextStack.Push(this);
			}

			// Token: 0x06001ABD RID: 6845 RVA: 0x0007D93C File Offset: 0x0007BB3C
			private int GetParentCode(bool isTraceEnabled)
			{
				StorageGlobals.TraceContextStack.Pop();
				int traceContextCode = StorageGlobals.GetTraceContextCode(isTraceEnabled);
				StorageGlobals.TraceContextStack.Push(this);
				return traceContextCode;
			}

			// Token: 0x06001ABE RID: 6846 RVA: 0x0007D968 File Offset: 0x0007BB68
			public int GetContextCode(bool isTraceEnabled)
			{
				bool flag = ExTraceGlobals.ContextTracer.IsTraceEnabled(TraceType.InfoTrace);
				bool flag2 = ExTraceGlobals.ContextShadowTracer.IsTraceEnabled(TraceType.InfoTrace);
				if (isTraceEnabled && flag)
				{
					if (!this.isWritten)
					{
						int parentCode = this.GetParentCode(isTraceEnabled);
						ExTraceGlobals.ContextTracer.Information<int, int, object>((long)parentCode, "Code: {0}, Parent: {1}, \n{2}", this.contextCode, parentCode, this.context);
						this.isWritten = true;
						this.isWrittenShadow = true;
					}
				}
				else if (flag2 && !this.isWrittenShadow)
				{
					int parentCode2 = this.GetParentCode(isTraceEnabled);
					ExTraceGlobals.ContextShadowTracer.Information<int, int, object>((long)parentCode2, "Code: {0}, Parent: {1}, \n{2}", this.contextCode, parentCode2, this.context);
					this.isWrittenShadow = true;
				}
				return this.contextCode;
			}

			// Token: 0x06001ABF RID: 6847 RVA: 0x0007DA0F File Offset: 0x0007BC0F
			protected void Dispose(bool disposing)
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.InternalDispose(disposing);
				}
			}

			// Token: 0x06001AC0 RID: 6848 RVA: 0x0007DA27 File Offset: 0x0007BC27
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06001AC1 RID: 6849 RVA: 0x0007DA36 File Offset: 0x0007BC36
			private void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					StorageGlobals.TraceContextStack.Pop();
				}
			}

			// Token: 0x040012BF RID: 4799
			private object context;

			// Token: 0x040012C0 RID: 4800
			private bool isWritten;

			// Token: 0x040012C1 RID: 4801
			private bool isWrittenShadow;

			// Token: 0x040012C2 RID: 4802
			private int contextCode;

			// Token: 0x040012C3 RID: 4803
			private bool isDisposed;
		}
	}
}
