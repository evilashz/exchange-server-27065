using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000776 RID: 1910
	internal static class ExtensionMethods
	{
		// Token: 0x06003917 RID: 14615 RVA: 0x000C9FD2 File Offset: 0x000C81D2
		public static StoreObjectId GetRefreshedDefaultFolderId(this MailboxSession mailboxSession, DefaultFolderType defaultFolderType)
		{
			return mailboxSession.GetRefreshedDefaultFolderId(defaultFolderType, false);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000C9FDC File Offset: 0x000C81DC
		public static StoreObjectId GetRefreshedDefaultFolderId(this MailboxSession mailboxSession, DefaultFolderType defaultFolderType, bool unifiedSession)
		{
			CallContext callContext = CallContext.Current;
			if (callContext == null || callContext.SessionCache == null)
			{
				throw new InvalidOperationException("Method GetRefreshedDefaultFolderId requires a valid CallContext.SessionCache.");
			}
			SessionAndAuthZ sessionAndAuthZ = callContext.SessionCache.GetSessionAndAuthZ(mailboxSession.MailboxGuid, unifiedSession);
			return sessionAndAuthZ.GetRefreshedDefaultFolderId(defaultFolderType);
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000CA020 File Offset: 0x000C8220
		public static DelegateSessionHandle GetDelegateSessionHandleForEWS(this MailboxSession mailboxSession, IExchangePrincipal exchangePrincipal)
		{
			DelegateSessionHandle delegateSessionHandle;
			try
			{
				delegateSessionHandle = mailboxSession.GetDelegateSessionHandle(exchangePrincipal);
			}
			catch (NotSupportedWithServerVersionException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<NotSupportedWithServerVersionException>(0L, "mailboxSession.GetDelegateSessionHandle failed. Exception '{0}'.", ex);
				throw new WrongServerVersionDelegateException(ex);
			}
			return delegateSessionHandle;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000CA064 File Offset: 0x000C8264
		public static CalendarItemBase GetCorrelatedItemForEWS(this MeetingMessage meetingMessage)
		{
			try
			{
				return meetingMessage.GetCorrelatedItem();
			}
			catch (CorrelationFailedException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<CorrelationFailedException>(0L, "CalendarItem associated with MeetingMessage could not be found. Exception '{0}'.", ex);
				if (ex.InnerException is NotSupportedWithServerVersionException)
				{
					throw new WrongServerVersionDelegateException(ex);
				}
			}
			catch (CorruptDataException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<CorruptDataException>(0L, "CalendarItem associated with MeetingMessage could not be found. Exception '{0}'.", arg);
			}
			catch (VirusException arg2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<VirusException>(0L, "CalendarItem associated with MeetingMessage could not be found. Exception '{0}'.", arg2);
			}
			catch (RecurrenceException arg3)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<RecurrenceException>(0L, "CalendarItem associated with MeetingMessage could not be found. Exception '{0}'.", arg3);
			}
			return null;
		}
	}
}
