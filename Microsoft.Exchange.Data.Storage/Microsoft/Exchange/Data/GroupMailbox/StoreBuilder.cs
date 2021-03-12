using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200080E RID: 2062
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StoreBuilder : IStoreBuilder
	{
		// Token: 0x06004CEA RID: 19690 RVA: 0x0013F665 File Offset: 0x0013D865
		public StoreBuilder(IMailboxSession currentUser, IXSOFactory xsoFactory, IExtensibleLogger logger, string clientInfoString)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.currentStoreSession = currentUser;
			this.xsoFactory = xsoFactory;
			this.logger = logger;
			this.clientInfoString = clientInfoString;
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0013F6A0 File Offset: 0x0013D8A0
		public IAssociationStore Create(IMailboxLocator targetMailbox, IMailboxAssociationPerformanceTracker performanceTracker)
		{
			StoreBuilder.Tracer.TraceDebug<IMailboxLocator>((long)this.GetHashCode(), "StoreBuilder.Create: Instantiating Store Provider for mailbox {0}", targetMailbox);
			ADUser aduser = targetMailbox.FindAdUser();
			MailboxSession mailboxSession = this.currentStoreSession as MailboxSession;
			if (mailboxSession != null)
			{
				StoreBuilder.Tracer.TraceDebug<Guid, Guid, LogonType>((long)this.GetHashCode(), "StoreBuilder.Create: Target MailboxGuid {0}. CurrentStoreSession MailboxGuid {1}. CurrentStoreSession LogonType {2}", aduser.ExchangeGuid, mailboxSession.MailboxGuid, mailboxSession.LogonType);
				if (mailboxSession.LogonType == LogonType.Owner || mailboxSession.LogonType == LogonType.Admin || mailboxSession.LogonType == LogonType.Transport || mailboxSession.LogonType == LogonType.SystemService)
				{
					if (mailboxSession.MailboxGuid == aduser.ExchangeGuid)
					{
						StoreBuilder.Tracer.TraceDebug((long)this.GetHashCode(), "StoreBuilder.Create: Returning CurrentStoreSession");
						performanceTracker.SetMailboxSessionToTrack(mailboxSession);
						return new LocalAssociationStore(targetMailbox, mailboxSession, false, this.xsoFactory, performanceTracker, this.logger);
					}
				}
				else
				{
					StoreBuilder.Tracer.TraceDebug((long)this.GetHashCode(), "StoreBuilder.Create: Can't use CurrentStoreSession as LogonType is unknown");
				}
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			performanceTracker.SetNewSessionRequired(true);
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
			StoreBuilder.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "StoreBuilder.Create: TargetMailbox location {0}. Current Server Name: {1}.", exchangePrincipal.MailboxInfo.Location.ServerFqdn, LocalServerCache.LocalServerFqdn);
			if (exchangePrincipal.MailboxInfo.Location.ServerFqdn.Equals(LocalServerCache.LocalServerFqdn, StringComparison.OrdinalIgnoreCase))
			{
				MailboxSession mailboxSession2 = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, this.clientInfoString);
				StoreBuilder.Tracer.TraceDebug((long)this.GetHashCode(), "StoreBuilder.Create: Returning new Admin session");
				stopwatch.Stop();
				performanceTracker.SetNewSessionLatency(stopwatch.ElapsedMilliseconds);
				performanceTracker.SetMailboxSessionToTrack(mailboxSession2);
				return new LocalAssociationStore(targetMailbox, mailboxSession2, true, this.xsoFactory, performanceTracker, this.logger);
			}
			stopwatch.Stop();
			performanceTracker.SetNewSessionLatency(stopwatch.ElapsedMilliseconds);
			performanceTracker.SetNewSessionWrongServer(true);
			throw new WrongServerException(ServerStrings.IncorrectServerError(aduser.PrimarySmtpAddress, exchangePrincipal.MailboxInfo.Location.ServerFqdn), aduser.ExchangeGuid, exchangePrincipal.MailboxInfo.Location.ServerFqdn, exchangePrincipal.MailboxInfo.Location.ServerVersion, null);
		}

		// Token: 0x040029EF RID: 10735
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x040029F0 RID: 10736
		private readonly IMailboxSession currentStoreSession;

		// Token: 0x040029F1 RID: 10737
		private readonly string clientInfoString;

		// Token: 0x040029F2 RID: 10738
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040029F3 RID: 10739
		private readonly IExtensibleLogger logger;
	}
}
