using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CalendarParticipant : IDisposable
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005514 File Offset: 0x00003714
		protected CalendarParticipant(UserObject userObject, ExDateTime validateFrom, ExDateTime validateUntil)
		{
			if (userObject == null)
			{
				throw new ArgumentNullException("userObject");
			}
			this.UserObject = userObject;
			this.ValidateFrom = validateFrom;
			this.ValidateUntil = validateUntil;
			this.ItemList = new Dictionary<GlobalObjectId, CalendarInstanceContext>();
			this.inquiredMasterGoids = new List<GlobalObjectId>();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005560 File Offset: 0x00003760
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005568 File Offset: 0x00003768
		internal UserObject UserObject { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005571 File Offset: 0x00003771
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005579 File Offset: 0x00003779
		internal ExDateTime ValidateFrom { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005582 File Offset: 0x00003782
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000558A File Offset: 0x0000378A
		internal ExDateTime ValidateUntil { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005593 File Offset: 0x00003793
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000559B File Offset: 0x0000379B
		internal Dictionary<GlobalObjectId, CalendarInstanceContext> ItemList { get; private set; }

		// Token: 0x060000D8 RID: 216 RVA: 0x000055A4 File Offset: 0x000037A4
		internal static CalendarParticipant Create(UserObject userObject, ExDateTime validateFrom, ExDateTime validateUntil, SessionManager sessionManager)
		{
			ExchangePrincipal exchangePrincipal = userObject.ExchangePrincipal;
			if (exchangePrincipal == null)
			{
				return new CalendarExternalParticipant(userObject, validateFrom, validateUntil);
			}
			try
			{
				if (exchangePrincipal.MailboxInfo.Location.ServerVersion < Globals.E15Version)
				{
					Globals.ConsistencyChecksTracer.TraceDebug(0L, "Remote mailbox is on a pre-E15 server, using XSO to access it.");
					return new CalendarLocalParticipant(userObject, validateFrom, validateUntil, sessionManager);
				}
				bool flag = StringComparer.OrdinalIgnoreCase.Equals(LocalServerCache.LocalServerFqdn, exchangePrincipal.MailboxInfo.Location.ServerFqdn);
				if (flag && !Configuration.CalendarRepairForceEwsUsage)
				{
					Globals.ConsistencyChecksTracer.TraceDebug(0L, "Local server is a best fit to service the remote mailbox.");
					return new CalendarLocalParticipant(userObject, validateFrom, validateUntil, sessionManager);
				}
				if (Configuration.CalendarRepairOppositeMailboxEwsEnabled)
				{
					Uri backEndWebServicesUrl = CalendarParticipant.GetBackEndWebServicesUrl(exchangePrincipal.MailboxInfo);
					if (backEndWebServicesUrl != null)
					{
						Globals.ConsistencyChecksTracer.TraceDebug<Uri>(0L, "Using {0} to access the remote mailbox.", backEndWebServicesUrl);
						if (CalendarItemBase.IsTenantToBeFixed(sessionManager.PrimarySession.Session))
						{
							return new CalendarRemoteParticipant2(userObject, validateFrom, validateUntil, sessionManager.PrimarySession.Session, backEndWebServicesUrl);
						}
						return new CalendarRemoteParticipant(userObject, validateFrom, validateUntil, sessionManager.PrimarySession.Session, backEndWebServicesUrl);
					}
				}
				else
				{
					Globals.ConsistencyChecksTracer.TraceDebug<ExchangePrincipal>(0L, "Unable to access the remote mailbox {0}.", exchangePrincipal);
				}
			}
			catch (WrongServerException exception)
			{
				CalendarParticipant.HandleException(exception, exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005710 File Offset: 0x00003910
		internal static bool InternalShouldProcessMailbox(ExchangePrincipal principal)
		{
			return principal.RecipientType == RecipientType.UserMailbox;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000571C File Offset: 0x0000391C
		private static Uri GetBackEndWebServicesUrl(IMailboxInfo mailbox)
		{
			Uri result = null;
			Exception ex = null;
			try
			{
				result = BackEndLocator.GetBackEndWebServicesUrl(mailbox);
			}
			catch (BackEndLocatorException ex2)
			{
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				ex = ex3;
			}
			catch (DataSourceOperationException ex4)
			{
				ex = ex4;
			}
			catch (DataValidationException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (ex != null)
				{
					Globals.ConsistencyChecksTracer.TraceError<IMailboxInfo, Exception>(0L, "Unable to find a server to process the request for {0} - {1}", mailbox, ex);
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000057A8 File Offset: 0x000039A8
		private static void HandleException(Exception exception, string mailboxSmtpAddress)
		{
			Globals.ConsistencyChecksTracer.TraceError<Exception, string>(0L, "{0}: Could not access remote server to open mailbox {1}.", exception, mailboxSmtpAddress);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000057C0 File Offset: 0x000039C0
		public virtual void Dispose()
		{
			if (this.ItemList != null)
			{
				foreach (CalendarInstanceContext calendarInstanceContext in this.ItemList.Values)
				{
					calendarInstanceContext.Dispose();
				}
				this.ItemList.Clear();
				this.ItemList = null;
			}
		}

		// Token: 0x060000DD RID: 221
		internal abstract void ValidateMeetings(ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent, Action<long> onItemRepaired);

		// Token: 0x060000DE RID: 222 RVA: 0x00005834 File Offset: 0x00003A34
		internal void ValidateInstance(CalendarInstanceContext instanceContext, Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent, Action<long> onItemRepaired)
		{
			GlobalObjectId item = new GlobalObjectId(instanceContext.ValidationContext.BaseItem.CleanGlobalObjectId);
			if (!this.inquiredMasterGoids.Contains(item))
			{
				try
				{
					instanceContext.Validate(organizerRumsSent, this.inquiredMasterGoids, onItemRepaired);
					return;
				}
				catch (AttachmentExceededException arg)
				{
					Globals.ConsistencyChecksTracer.TraceError<AttachmentExceededException>((long)this.GetHashCode(), "{0}: Unable to save RUM", arg);
					return;
				}
			}
			Globals.ConsistencyChecksTracer.TraceDebug<GlobalObjectId>((long)this.GetHashCode(), "Skipping meeting validation because its master instance already sent an inquiry message. Meeting GOID: {0}", instanceContext.ValidationContext.BaseItem.GlobalObjectId);
		}

		// Token: 0x04000042 RID: 66
		private List<GlobalObjectId> inquiredMasterGoids;
	}
}
