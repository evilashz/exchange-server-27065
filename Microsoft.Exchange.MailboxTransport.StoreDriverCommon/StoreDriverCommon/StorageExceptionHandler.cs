using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000011 RID: 17
	internal static class StorageExceptionHandler
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000257F File Offset: 0x0000077F
		public static ExceptionHandler TableBasedExceptionHandler
		{
			get
			{
				return StorageExceptionHandler.tableBasedExceptionHandler;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002586 File Offset: 0x00000786
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000025B3 File Offset: 0x000007B3
		public static bool MailboxTransportTableBasedExceptionHandlerEnabled
		{
			get
			{
				if (StorageExceptionHandler.useTableBasedErrorHandling == null)
				{
					return Components.Configuration.AppConfig.RemoteDelivery.MailboxTransportTableBasedExceptionHandlerEnabled;
				}
				return StorageExceptionHandler.useTableBasedErrorHandling.Value;
			}
			set
			{
				StorageExceptionHandler.useTableBasedErrorHandling = new bool?(value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025ED File Offset: 0x000007ED
		public static TimeSpan MailboxDeliveryFastQueueRetryInterval
		{
			get
			{
				if (!(StorageExceptionHandler.mailboxDeliveryFastQueueRetryInterval == TimeSpan.Zero))
				{
					return StorageExceptionHandler.mailboxDeliveryFastQueueRetryInterval;
				}
				return Components.Configuration.AppConfig.RemoteDelivery.MailboxDeliveryFastQueueRetryInterval;
			}
			set
			{
				StorageExceptionHandler.mailboxDeliveryFastQueueRetryInterval = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025F5 File Offset: 0x000007F5
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002622 File Offset: 0x00000822
		public static TimeSpan QuarantinedMailboxRetryInterval
		{
			get
			{
				if (!(StorageExceptionHandler.quarantinedMailboxRetryInterval == TimeSpan.Zero))
				{
					return StorageExceptionHandler.quarantinedMailboxRetryInterval;
				}
				return Components.Configuration.AppConfig.RemoteDelivery.QuarantinedMailboxRetryInterval;
			}
			set
			{
				StorageExceptionHandler.quarantinedMailboxRetryInterval = value;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000262C File Offset: 0x0000082C
		public static void Init()
		{
			if (StorageExceptionHandler.initCalled)
			{
				return;
			}
			lock (StorageExceptionHandler.syncObject)
			{
				if (!StorageExceptionHandler.initCalled)
				{
					StorageExceptionHandler.tableBasedExceptionHandler = new ExceptionHandler();
					if (!StorageExceptionHandler.tableBasedExceptionHandler.Register("Delivery", MailboxTransportExceptionMapping.Delivery))
					{
						throw new InvalidOperationException("Registration of the Delivery exception handler failed.");
					}
					if (!StorageExceptionHandler.tableBasedExceptionHandler.Register("Submission", MailboxTransportExceptionMapping.Submission))
					{
						throw new InvalidOperationException("Registration of the Submission exception handler failed.");
					}
					StorageExceptionHandler.initCalled = true;
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026C8 File Offset: 0x000008C8
		public static MessageStatus RunUnderExceptionHandler(IMessageConverter converter, StoreDriverDelegate workerFunction)
		{
			if (StorageExceptionHandler.MailboxTransportTableBasedExceptionHandlerEnabled)
			{
				return StorageExceptionHandler.RunUnderTableBasedExceptionHandler(converter, workerFunction);
			}
			return StorageExceptionHandler.RunUnderExplicitExceptionHandler(converter, workerFunction);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026E0 File Offset: 0x000008E0
		internal static string GetExceptionTypeString(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			stringBuilder.Append(e.GetType().Name);
			while (e.InnerException != null)
			{
				stringBuilder.AppendFormat(".{0}", e.InnerException.GetType().Name);
				e = e.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002740 File Offset: 0x00000940
		internal static string GetExceptionDiagnosticInfo(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder((exception.InnerException == null) ? 1000 : 2000);
			stringBuilder.Append(exception.Message);
			Exception ex = (exception.InnerException == null) ? exception : exception.InnerException;
			MapiPermanentException ex2 = ex as MapiPermanentException;
			if (ex2 != null)
			{
				stringBuilder.AppendFormat(" {0}", StorageExceptionHandler.GetDiagnosticInfo(ex2));
			}
			else
			{
				MapiRetryableException ex3 = ex as MapiRetryableException;
				if (ex3 != null)
				{
					stringBuilder.AppendFormat(" {0}", StorageExceptionHandler.GetDiagnosticInfo(ex3));
				}
				else
				{
					stringBuilder.AppendFormat(" {0}: {1}", ex.GetType().Name, ex.Message);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027E8 File Offset: 0x000009E8
		internal static void LogException(IMessageConverter converter, Exception e)
		{
			TraceHelper.TracePass(StorageExceptionHandler.diag, 0L, "Exception occurred during message {0} : {1}", new object[]
			{
				converter.Description,
				e
			});
			converter.LogMessage(e);
			StorageExceptionHandler.exceptionCounter.IncrementCounter(e.GetType().FullName);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002838 File Offset: 0x00000A38
		internal static SmtpResponse GetExceptionSmtpResponse(IMessageConverter converter, Exception exception, bool isPermanent, SmtpResponse smtpResponse)
		{
			return StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, isPermanent, smtpResponse.StatusCode, smtpResponse.EnhancedStatusCode, smtpResponse.StatusText[0], true);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000285C File Offset: 0x00000A5C
		internal static SmtpResponse GetExceptionSmtpResponse(IMessageConverter converter, Exception exception, bool isPermanent, string statusCode, string enhancedStatusCode, string primaryStatusText, bool includeDiagnostic = true)
		{
			string source = string.Format(CultureInfo.InvariantCulture, "STOREDRV.{0}.Exception:{1}; Failed to process message due to a {2} exception with message {3}", new object[]
			{
				converter.Description,
				StorageExceptionHandler.GetExceptionTypeString(exception),
				isPermanent ? "permanent" : "transient",
				StorageExceptionHandler.GetExceptionDiagnosticInfo(exception)
			});
			if (string.IsNullOrEmpty(statusCode))
			{
				statusCode = (isPermanent ? "554" : "432");
			}
			if (string.IsNullOrEmpty(enhancedStatusCode))
			{
				if (isPermanent)
				{
					enhancedStatusCode = (converter.IsOutbound ? "5.6.0" : "5.2.0");
				}
				else
				{
					enhancedStatusCode = "4.2.0";
				}
			}
			return new SmtpResponse(statusCode, enhancedStatusCode, new string[]
			{
				primaryStatusText,
				includeDiagnostic ? StorageExceptionHandler.StripNonAscii(source) : string.Empty
			});
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000291B File Offset: 0x00000B1B
		internal static SmtpResponse GetExceptionSmtpResponse(IMessageConverter converter, Exception exception, bool isPermanent)
		{
			return StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, isPermanent, null, null, null, true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002929 File Offset: 0x00000B29
		internal static SmtpResponse GetExceptionSmtpResponse(IMessageConverter converter, Exception exception, bool isPermanent, string primaryStatusText)
		{
			return StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, isPermanent, null, null, primaryStatusText, true);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002938 File Offset: 0x00000B38
		internal static string GetReasonDescription(ConversionFailureReason reason)
		{
			switch (reason)
			{
			case ConversionFailureReason.ExceedsLimit:
				return "Content conversion limit(s) exceeded";
			case ConversionFailureReason.MaliciousContent:
				return "Malicious message content";
			case ConversionFailureReason.CorruptContent:
				return "Corrupt message content";
			case ConversionFailureReason.ConverterInternalFailure:
				return "Content conversion internal failure";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002980 File Offset: 0x00000B80
		private static MessageStatus RunUnderTableBasedExceptionHandler(IMessageConverter converter, StoreDriverDelegate workerFunction)
		{
			MessageStatus messageStatus = MessageStatus.Success;
			StorageExceptionHandler.diag = converter.Tracer;
			try
			{
				workerFunction();
			}
			catch (Exception exception)
			{
				messageStatus = StorageExceptionHandler.tableBasedExceptionHandler.Handle(converter.IsOutbound ? "Submission" : "Delivery", exception, converter, StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval, StorageExceptionHandler.QuarantinedMailboxRetryInterval);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			return messageStatus;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029F0 File Offset: 0x00000BF0
		private static MessageStatus RunUnderExplicitExceptionHandler(IMessageConverter converter, StoreDriverDelegate workerFunction)
		{
			MessageStatus messageStatus = MessageStatus.Success;
			StorageExceptionHandler.diag = converter.Tracer;
			try
			{
				workerFunction();
			}
			catch (StoreDriverAgentRaisedException ex)
			{
				if (ex.InnerException == null)
				{
					TraceHelper.TraceFail(StorageExceptionHandler.diag, 0L, "StoreDriverAgentRaisedException's InnerException is null. It is expected to contain the actual exception that the agent has thrown. StoreDriverAgentRaisedException.Message: {0}; StoreDriverAgentRaisedException.StackTrace: {1}.", new object[]
					{
						ex.Message,
						ex.StackTrace
					});
					throw;
				}
				if (ex.InnerException is StoreDriverAgentTransientException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (StoreDriverAgentTransientException)ex.InnerException);
				}
				else if (ex.InnerException is SmtpResponseException)
				{
					messageStatus = StorageExceptionHandler.ProcessSmtpResponseException((SmtpResponseException)ex.InnerException, converter);
				}
				else if (ex.InnerException is ADTransientException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (ADTransientException)ex.InnerException);
				}
				else if (ex.InnerException is DataValidationException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (DataValidationException)ex.InnerException);
				}
				else if (ex.InnerException is DataSourceOperationException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (DataSourceOperationException)ex.InnerException);
				}
				else if (ex.InnerException is StorageTransientException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (StorageTransientException)ex.InnerException);
				}
				else if (ex.InnerException is StoragePermanentException)
				{
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (StoragePermanentException)ex.InnerException);
				}
				else if (ex.InnerException is ExchangeDataException)
				{
					messageStatus = StorageExceptionHandler.ProcessExchangeDataException((ExchangeDataException)ex.InnerException, converter);
				}
				else
				{
					if (!(ex.InnerException is IOException))
					{
						throw;
					}
					messageStatus = StorageExceptionHandler.GetMessageStatus(converter, (IOException)ex.InnerException);
				}
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (StoreDriverAgentTransientException storeDriverAgentTransientException)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, storeDriverAgentTransientException);
			}
			catch (SmtpResponseException smtpResponseException)
			{
				messageStatus = StorageExceptionHandler.ProcessSmtpResponseException(smtpResponseException, converter);
			}
			catch (ADTransientException unavailableException)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, unavailableException);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (DataValidationException invalidDataException)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, invalidDataException);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (DataSourceOperationException dataSourceOperationException)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, dataSourceOperationException);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (StorageTransientException storageException)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, storageException);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (StoragePermanentException storageException2)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, storageException2);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (ExchangeDataException exchangeDataException)
			{
				messageStatus = StorageExceptionHandler.ProcessExchangeDataException(exchangeDataException, converter);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			catch (IOException e)
			{
				messageStatus = StorageExceptionHandler.GetMessageStatus(converter, e);
				if (messageStatus.Action == MessageAction.Throw)
				{
					throw;
				}
			}
			return messageStatus;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D5C File Offset: 0x00000F5C
		private static MessageStatus GetMessageStatus(IMessageConverter converter, ConversionFailedException exception)
		{
			StorageExceptionHandler.LogException(converter, exception);
			SmtpResponse exceptionSmtpResponse = StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, true, "554", (ConversionFailureReason.ExceedsLimit == exception.ConversionFailureReason) ? "5.3.4" : "5.6.0", StorageExceptionHandler.GetReasonDescription(exception.ConversionFailureReason), true);
			return new MessageStatus(MessageAction.NDR, exceptionSmtpResponse, exception, true);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DAC File Offset: 0x00000FAC
		private static MessageStatus GetMessageStatus(IMessageConverter converter, StoragePermanentException storageException)
		{
			StorageExceptionHandler.LogException(converter, storageException);
			ConversionFailedException ex = storageException as ConversionFailedException;
			if (ex != null)
			{
				return StorageExceptionHandler.GetMessageStatus(converter, ex);
			}
			if (storageException is MailboxCrossSiteFailoverException || storageException is MailboxInfoStaleException)
			{
				return new MessageStatus(converter.IsOutbound ? MessageAction.Skip : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			}
			if (storageException is UnableToFindServerForDatabaseException || storageException is IllegalCrossServerConnectionException)
			{
				return new MessageStatus(converter.IsOutbound ? MessageAction.Skip : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			}
			if (storageException.InnerException is CannotGetSiteInfoException)
			{
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException.InnerException, true), storageException);
			}
			if (storageException.InnerException != null)
			{
				MapiPermanentException ex2 = storageException.InnerException as MapiPermanentException;
				if (ex2 != null)
				{
					return StorageExceptionHandler.GetMessageStatus<StoragePermanentException>(converter, ex2, storageException);
				}
				MapiRetryableException ex3 = storageException.InnerException as MapiRetryableException;
				if (ex3 != null)
				{
					return StorageExceptionHandler.GetMessageStatus<StoragePermanentException>(converter, ex3, storageException);
				}
			}
			return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EA0 File Offset: 0x000010A0
		private static MessageStatus GetMessageStatus(IMessageConverter converter, StorageTransientException storageException)
		{
			StorageExceptionHandler.LogException(converter, storageException);
			if (storageException is MailboxInSiteFailoverException)
			{
				return new MessageStatus(converter.IsOutbound ? MessageAction.Skip : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			}
			if (storageException.InnerException != null)
			{
				MapiPermanentException ex = storageException.InnerException as MapiPermanentException;
				if (ex != null)
				{
					return StorageExceptionHandler.GetMessageStatus<StorageTransientException>(converter, ex, storageException);
				}
				MapiRetryableException ex2 = storageException.InnerException as MapiRetryableException;
				if (ex2 != null)
				{
					return StorageExceptionHandler.GetMessageStatus<StorageTransientException>(converter, ex2, storageException);
				}
			}
			return new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F20 File Offset: 0x00001120
		private static MessageStatus GetMessageStatus(IMessageConverter converter, DataValidationException invalidDataException)
		{
			StorageExceptionHandler.LogException(converter, invalidDataException);
			SmtpResponse exceptionSmtpResponse = StorageExceptionHandler.GetExceptionSmtpResponse(converter, invalidDataException, true, converter.IsOutbound ? AckReason.OutboundInvalidDirectoryData : AckReason.InboundInvalidDirectoryData);
			return new MessageStatus(MessageAction.NDR, exceptionSmtpResponse, invalidDataException);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F5C File Offset: 0x0000115C
		private static MessageStatus GetMessageStatus(IMessageConverter converter, DataSourceOperationException dataSourceOperationException)
		{
			if (dataSourceOperationException is ADInvalidCredentialException)
			{
				ADTransientException unavailableException = new ADTransientException(dataSourceOperationException.LocalizedString, dataSourceOperationException);
				return StorageExceptionHandler.GetMessageStatus(converter, unavailableException);
			}
			StorageExceptionHandler.LogException(converter, dataSourceOperationException);
			return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, dataSourceOperationException, true), dataSourceOperationException);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F9C File Offset: 0x0000119C
		private static MessageStatus GetMessageStatus(IMessageConverter converter, ExchangeDataException dataException)
		{
			StorageExceptionHandler.LogException(converter, dataException);
			return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, dataException, true), dataException);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FB4 File Offset: 0x000011B4
		private static MessageStatus GetMessageStatus(IMessageConverter converter, ADTransientException unavailableException)
		{
			StorageExceptionHandler.LogException(converter, unavailableException);
			SmtpResponse exceptionSmtpResponse = StorageExceptionHandler.GetExceptionSmtpResponse(converter, unavailableException, false);
			return new MessageStatus(MessageAction.Retry, exceptionSmtpResponse, unavailableException);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FDC File Offset: 0x000011DC
		private static MessageStatus GetMessageStatus<T>(IMessageConverter converter, MapiPermanentException mapiException, T storageException) where T : Exception
		{
			string name = mapiException.GetType().Name;
			string key;
			MessageStatus messageStatus;
			switch (key = name)
			{
			case "MapiExceptionJetErrorLogDiskFull":
				return new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, AckReason.MailboxDiskFull), storageException);
			case "MapiExceptionADDuplicateEntry":
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, "Duplicated AD entries found"), storageException);
			case "MapiExceptionNoMoreConnections":
				messageStatus = new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, SmtpResponse.TooManyConnectionsPerSource), storageException);
				messageStatus.RetryInterval = new TimeSpan?(StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval);
				return messageStatus;
			case "MapiExceptionUnknownUser":
				return new MessageStatus(converter.IsOutbound ? MessageAction.Retry : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			case "MapiExceptionNoReplicaHere":
			case "MapiExceptionWrongServer":
			case "MapiExceptionWrongMailbox":
				return new MessageStatus(converter.IsOutbound ? MessageAction.Skip : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			case "MapiExceptionClientVersionDisallowed":
				return new MessageStatus(MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			case "MapiExceptionDuplicateDelivery":
				return new MessageStatus(MessageAction.LogDuplicate, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			case "MapiExceptionMaxTimeExpired":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "4.4.1", "connection timed out", true), storageException);
			case "MapiExceptionUnableToComplete":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "4.4.1", "connection timed out", true), storageException);
			case "MapiExceptionMessageTooBig":
			case "MapiExceptionTooComplex":
			case "MapiExceptionTooBig":
			case "MapiExceptionMaxAttachmentExceeded":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "5.3.4", "message exceeds fixed system limits", true), storageException);
			case "MapiExceptionTooManyRecips":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "5.3.3", "too many recipients", true), storageException);
			case "MapiExceptionQuotaExceeded":
			case "MapiExceptionShutoffQuotaExceeded":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "5.2.2", "mailbox full", true), storageException);
			case "MapiExceptionNoReplicaAvailable":
			case "MapiExceptionFolderDisabled":
			case "MapiExceptionMailboxDisabled":
			case "MapiExceptionAccountDisabled":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "5.2.1", "mailbox disabled", true), storageException);
			case "MapiExceptionMdbOffline":
				return new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, AckReason.MDBOffline), storageException);
			case "MapiExceptionMaxObjsExceeded":
			case "MapiExceptionRpcBufferTooSmall":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, null, "4.3.2", "mailbox busy", true), storageException);
			case "MapiExceptionNoAccess":
				return new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MapiNoAccessFailure), storageException);
			case "MapiExceptionJetErrorPageNotInitialized":
				return new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, "Database page not initialized"), storageException);
			case "MapiExceptionOutOfMemory":
			case "MapiExceptionTooManyMountedDatabases":
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			case "MapiExceptionUnconfigured":
				return new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			}
			messageStatus = new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
			return messageStatus;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003538 File Offset: 0x00001738
		private static MessageStatus GetMessageStatus<T>(IMessageConverter converter, MapiRetryableException mapiException, T storageException) where T : Exception
		{
			string name = mapiException.GetType().Name;
			string key;
			MessageStatus messageStatus;
			switch (key = name)
			{
			case "MapiExceptionMailboxInTransit":
			case "MapiExceptionServerPaused":
				return new MessageStatus(converter.IsOutbound ? MessageAction.Skip : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			case "MapiExceptionDismountInProgress":
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryQueue : MessageAction.Reroute, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			case "MapiExceptionLogonFailed":
				return new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.LogonFailure), storageException);
			case "MapiExceptionJetErrorLogWriteFail":
			case "MapiExceptionJetErrorDiskIO":
			case "MapiExceptionJetErrorCheckpointDepthTooDeep":
				return new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxIOError), storageException);
			case "MapiExceptionJetErrorInstanceUnavailable":
				return new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxIOError), storageException);
			case "MapiExceptionNetworkError":
				messageStatus = new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxServerOffline), storageException);
				messageStatus.RetryInterval = new TimeSpan?(StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval);
				return messageStatus;
			case "MapiExceptionRpcServerTooBusy":
			case "MapiExceptionSessionLimit":
			case "MapiExceptionNotEnoughMemory":
				messageStatus = new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, name, false), storageException);
				messageStatus.RetryInterval = new TimeSpan?(StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval);
				return messageStatus;
			case "MapiExceptionMaxThreadsPerMdbExceeded":
				messageStatus = new MessageStatus(MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxServerMaxThreadsPerMdbExceeded), storageException);
				messageStatus.RetryInterval = new TimeSpan?(StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval);
				return messageStatus;
			case "MapiExceptionMaxThreadsPerSCTExceeded":
				messageStatus = new MessageStatus(converter.IsOutbound ? MessageAction.Retry : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MapiExceptionMaxThreadsPerSCTExceeded), storageException);
				messageStatus.RetryInterval = new TimeSpan?(StorageExceptionHandler.MailboxDeliveryFastQueueRetryInterval);
				return messageStatus;
			case "MapiExceptionRpcOutOfMemory":
			case "MapiExceptionRpcOutOfResources":
			case "MapiExceptionRpcServerOutOfMemory":
			case "MapiExceptionVersionStoreBusy":
				return new MessageStatus(converter.IsOutbound ? MessageAction.Retry : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			case "MapiExceptionSubsystemStopping":
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			case "MapiExceptionLowDatabaseDiskSpace":
			case "MapiExceptionLowDatabaseLogDiskSpace":
				return new MessageStatus(converter.IsOutbound ? MessageAction.Retry : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxDiskFull), storageException);
			case "MapiExceptionADNotFound":
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false, AckReason.MailboxDiskFull), storageException);
			case "MapiExceptionMailboxQuarantined":
				if (converter.IsOutbound)
				{
					return new MessageStatus(MessageAction.Skip, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true), storageException);
				}
				return new MessageStatus(MessageAction.NDR, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, true, AckReason.RecipientMailboxQuarantined), storageException);
			case "MapiExceptionRetryableImportFailure":
			case "MapiExceptionBusy":
			case "MapiExceptionDiskError":
			case "MapiExceptionJetErrorOutOfBuffers":
			case "MapiExceptionJetErrorOutOfCursors":
			case "MapiExceptionJetErrorOutOfMemory":
			case "MapiExceptionJetErrorOutOfSessions":
			case "MapiExceptionJetErrorTooManyOpenTables":
			case "MapiExceptionJetErrorTooManyOpenTablesAndCleanupTimedOut":
			case "MapiExceptionJetErrorVersionStoreOutOfMemory":
			case "MapiExceptionJetErrorVersionStoreOutOfMemoryAndCleanupTimedOut":
			case "MapiExceptionNoFreeJses":
			case "MapiExceptionNotEnoughDisk":
			case "MapiExceptionTimeout":
			case "MapiExceptionWait":
			case "MapiExceptionCollision":
				return new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			}
			messageStatus = new MessageStatus(MessageAction.Retry, StorageExceptionHandler.GetExceptionSmtpResponse(converter, storageException, false), storageException);
			return messageStatus;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003AF0 File Offset: 0x00001CF0
		private static MessageStatus GetMessageStatus(IMessageConverter converter, IOException e)
		{
			StorageExceptionHandler.LogException(converter, e);
			SmtpResponse exceptionSmtpResponse = StorageExceptionHandler.GetExceptionSmtpResponse(converter, e, true, AckReason.DiskFull);
			if (ExceptionHelper.IsDiskFullException(e))
			{
				return new MessageStatus(converter.IsOutbound ? MessageAction.RetryMailboxServer : MessageAction.RetryQueue, exceptionSmtpResponse, e);
			}
			return new MessageStatus(MessageAction.Throw, exceptionSmtpResponse, e);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B37 File Offset: 0x00001D37
		private static string GetDiagnosticInfo(MapiPermanentException e)
		{
			if (e.DiagCtx != null)
			{
				return e.DiagCtx.ToCompactString();
			}
			return string.Empty;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003B52 File Offset: 0x00001D52
		private static string GetDiagnosticInfo(MapiRetryableException e)
		{
			if (e.DiagCtx != null)
			{
				return e.DiagCtx.ToCompactString();
			}
			return string.Empty;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B70 File Offset: 0x00001D70
		private static SmtpResponse GetExceptionSmtpResponse(IMessageConverter converter, Exception exception, string exceptionName, bool isPermanent)
		{
			SmtpResponse smtpResponse = SmtpResponse.Empty;
			if (exceptionName != null)
			{
				if (!(exceptionName == "MapiExceptionRpcServerTooBusy"))
				{
					if (!(exceptionName == "MapiExceptionSessionLimit"))
					{
						if (exceptionName == "MapiExceptionNotEnoughMemory")
						{
							smtpResponse = AckReason.MailboxServerNotEnoughMemory;
						}
					}
					else
					{
						smtpResponse = AckReason.MailboxMapiSessionLimit;
					}
				}
				else
				{
					smtpResponse = AckReason.MailboxServerTooBusy;
				}
			}
			return StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, isPermanent, smtpResponse.StatusCode, smtpResponse.EnhancedStatusCode, smtpResponse.StatusText[0], true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003BF7 File Offset: 0x00001DF7
		private static string StripNonAscii(string source)
		{
			return new string((from c in source
			where c > '\0' && c < '\u0080'
			select c).ToArray<char>());
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C26 File Offset: 0x00001E26
		private static MessageStatus GetMessageStatus(IMessageConverter converter, StoreDriverAgentTransientException storeDriverAgentTransientException)
		{
			return new MessageStatus(MessageAction.Retry, AckReason.DeliverAgentTransientFailure, storeDriverAgentTransientException);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C34 File Offset: 0x00001E34
		private static MessageStatus ProcessSmtpResponseException(SmtpResponseException smtpResponseException, IMessageConverter converter)
		{
			MessageStatus status = smtpResponseException.Status;
			if (status.Action == MessageAction.NDR)
			{
				converter.LogMessage(smtpResponseException);
			}
			return status;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C5C File Offset: 0x00001E5C
		private static MessageStatus ProcessExchangeDataException(ExchangeDataException exchangeDataException, IMessageConverter converter)
		{
			MessageStatus messageStatus = StorageExceptionHandler.GetMessageStatus(converter, exchangeDataException);
			if (messageStatus.Action == MessageAction.NDR)
			{
				converter.LogMessage(exchangeDataException);
			}
			return messageStatus;
		}

		// Token: 0x04000029 RID: 41
		private const int EstimatedExceptionTypeLength = 500;

		// Token: 0x0400002A RID: 42
		private const int EstimatedExceptionDiagnosticInfoLength = 1000;

		// Token: 0x0400002B RID: 43
		private static Trace diag;

		// Token: 0x0400002C RID: 44
		private static CounterDictionary<string> exceptionCounter = new CounterDictionary<string>();

		// Token: 0x0400002D RID: 45
		private static bool? useTableBasedErrorHandling;

		// Token: 0x0400002E RID: 46
		private static TimeSpan mailboxDeliveryFastQueueRetryInterval = TimeSpan.Zero;

		// Token: 0x0400002F RID: 47
		private static TimeSpan quarantinedMailboxRetryInterval = TimeSpan.Zero;

		// Token: 0x04000030 RID: 48
		private static ExceptionHandler tableBasedExceptionHandler;

		// Token: 0x04000031 RID: 49
		private static bool initCalled = false;

		// Token: 0x04000032 RID: 50
		private static object syncObject = new object();
	}
}
