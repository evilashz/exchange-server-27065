using System;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000205 RID: 517
	internal static class SimulatedWebRequestContext
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x00048690 File Offset: 0x00046890
		internal static void Execute(IMailboxContext userContext, string eventId, Action<MailboxSession, IRecipientSession, RequestDetailsLogger> action)
		{
			SimulatedWebRequestContext.Execute(userContext, eventId, delegate(RequestDetailsLogger logger)
			{
				ExchangePrincipal exchangePrincipal = userContext.ExchangePrincipal;
				if (exchangePrincipal == null)
				{
					return;
				}
				IRecipientSession arg = InstantMessageUtilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, exchangePrincipal, userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN);
				try
				{
					userContext.LockAndReconnectMailboxSession(3000);
					MailboxSession mailboxSession = userContext.MailboxSession;
					action(mailboxSession, arg, logger);
				}
				catch (OwaLockTimeoutException exception)
				{
					SimulatedWebRequestContext.ProcessException(logger, eventId, exception);
				}
				finally
				{
					if (userContext.MailboxSessionLockedByCurrentThread())
					{
						userContext.UnlockAndDisconnectMailboxSession();
					}
				}
			});
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000486F0 File Offset: 0x000468F0
		internal static void Execute(IMailboxContext userContext, string eventId, Action<RequestDetailsLogger> action)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(eventId))
			{
				throw new ArgumentException("String should not be null or empty.", "eventId");
			}
			string primarySmtpAddress = SimulatedWebRequestContext.GetPrimarySmtpAddress(userContext);
			if (primarySmtpAddress == null)
			{
				return;
			}
			ExchangeVersion value = ExchangeVersion.Current;
			bool flag;
			RequestDetailsLogger logger = SimulatedWebRequestContext.GetRequestDetailsLogger(eventId, userContext, primarySmtpAddress, out flag);
			try
			{
				ExchangeVersion.Current = ExchangeVersion.Latest;
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					action(logger);
				});
			}
			catch (GrayException ex)
			{
				SimulatedWebRequestContext.ProcessException(logger, eventId, ex.InnerException);
			}
			finally
			{
				if (flag)
				{
					logger.Commit();
				}
				ExchangeVersion.Current = value;
			}
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000487F8 File Offset: 0x000469F8
		internal static void ExecuteWithoutUserContext(string eventId, Action<RequestDetailsLogger> action)
		{
			RequestDetailsLogger logger = OwaApplication.GetRequestDetailsLogger;
			ActivityContext.ClearThreadScope();
			logger = RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger();
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					action(logger);
				});
			}
			catch (GrayException ex)
			{
				SimulatedWebRequestContext.ProcessException(logger, eventId, ex.InnerException);
			}
			finally
			{
				logger.Commit();
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000488A0 File Offset: 0x00046AA0
		internal static void LogExceptionsOnly(IMailboxContext userContext, string eventId, Action action)
		{
			ExchangeVersion value = ExchangeVersion.Current;
			try
			{
				ExchangeVersion.Current = ExchangeVersion.Latest;
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					action();
				});
			}
			catch (GrayException ex)
			{
				string primarySmtpAddress = SimulatedWebRequestContext.GetPrimarySmtpAddress(userContext);
				bool flag = false;
				RequestDetailsLogger requestDetailsLogger = SimulatedWebRequestContext.GetRequestDetailsLogger(eventId, userContext, primarySmtpAddress ?? "[user logged out]", out flag);
				SimulatedWebRequestContext.ProcessException(requestDetailsLogger, eventId, ex.InnerException);
				if (flag)
				{
					requestDetailsLogger.Commit();
				}
			}
			finally
			{
				ExchangeVersion.Current = value;
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004894C File Offset: 0x00046B4C
		private static void ProcessException(RequestDetailsLogger logger, string eventId, Exception exception)
		{
			ExTraceGlobals.CoreCallTracer.TraceError<string, Exception>(0L, "SimulatedWebRequestContext.Execute failed for event {0} with exception {1}", eventId, exception);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(logger, exception, "SimulatedWebRequestContext_" + eventId);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00048974 File Offset: 0x00046B74
		private static string GetPrimarySmtpAddress(IMailboxContext userContext)
		{
			OwaIdentity mailboxIdentity = userContext.MailboxIdentity;
			if (mailboxIdentity == null)
			{
				return null;
			}
			string text = (string)mailboxIdentity.PrimarySmtpAddress;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000489A4 File Offset: 0x00046BA4
		private static RequestDetailsLogger GetRequestDetailsLogger(string eventId, IMailboxContext userContext, string primarySmtpAddress, out bool commitLogger)
		{
			RequestDetailsLogger requestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			if (requestDetailsLogger != null && !requestDetailsLogger.IsDisposed)
			{
				commitLogger = false;
				requestDetailsLogger.AppendGenericInfo("NestedCallback", eventId);
			}
			else
			{
				commitLogger = true;
				ActivityContext.ClearThreadScope();
				requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger();
				requestDetailsLogger.ActivityScope.UserEmail = primarySmtpAddress;
				requestDetailsLogger.Set(ExtensibleLoggerMetadata.EventId, eventId);
				requestDetailsLogger.Set(OwaServerLogger.LoggerData.PrimarySmtpAddress, primarySmtpAddress);
				requestDetailsLogger.Set(OwaServerLogger.LoggerData.UserContext, userContext.Key);
			}
			return requestDetailsLogger;
		}
	}
}
