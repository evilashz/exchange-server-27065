using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAC RID: 3500
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PublishedFolder : IDisposeTrackable, IDisposable
	{
		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x00212924 File Offset: 0x00210B24
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06007833 RID: 30771 RVA: 0x0021292C File Offset: 0x00210B2C
		public string DisplayName
		{
			get
			{
				this.CheckDisposed("DisplayName::get");
				if (string.IsNullOrEmpty(this.displayName))
				{
					using (Folder folder = Folder.Bind(this.MailboxSession, this.FolderId))
					{
						this.displayName = folder.DisplayName;
					}
				}
				return this.displayName;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06007834 RID: 30772 RVA: 0x00212994 File Offset: 0x00210B94
		public string OwnerDisplayName
		{
			get
			{
				this.CheckDisposed("OwnerDisplayName::get");
				return this.MailboxSession.MailboxOwner.MailboxInfo.DisplayName;
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06007835 RID: 30773
		public abstract string BrowseUrl { get; }

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x002129B6 File Offset: 0x00210BB6
		protected MailboxSession MailboxSession
		{
			get
			{
				if (this.mailboxSession == null)
				{
					this.mailboxSession = this.CreateMailboxSession();
					this.disposeMailboxSession = true;
				}
				return this.mailboxSession;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x002129D9 File Offset: 0x00210BD9
		protected IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					this.recipientSession = this.CreateRecipientSession();
				}
				return this.recipientSession;
			}
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x002129F5 File Offset: 0x00210BF5
		private PublishedFolder(StoreObjectId folderId)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			this.folderId = folderId;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x00212A1C File Offset: 0x00210C1C
		protected PublishedFolder(string domain, SecurityIdentifier sid, StoreObjectId folderId) : this(folderId)
		{
			Util.ThrowOnNullArgument(sid, "user");
			this.sid = sid;
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.AcquireBudgetAndStartTiming();
				disposeGuard.Success();
			}
			try
			{
				this.sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domain);
			}
			catch (CannotResolveTenantNameException arg)
			{
				ExTraceGlobals.SharingTracer.TraceError<string, CannotResolveTenantNameException>((long)this.GetHashCode(), "ObscureUrlKey.Lookup(): Cannot resolve tenant name {0}.Error: {1}", domain, arg);
			}
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x00212AAC File Offset: 0x00210CAC
		protected PublishedFolder(MailboxSession mailboxSession, StoreObjectId folderId) : this(folderId)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			this.mailboxSession = mailboxSession;
			this.sid = mailboxSession.MailboxOwner.Sid;
			this.disposeMailboxSession = false;
			this.sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId ?? OrganizationId.ForestWideOrgId);
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x00212B10 File Offset: 0x00210D10
		public static PublishedFolder Create(PublishingUrl publishingUrl)
		{
			Util.ThrowOnNullArgument(publishingUrl, "publishingUrl");
			PublishedFolder.SleepIfNecessary();
			SharingAnonymousIdentityCache instance = SharingAnonymousIdentityCache.Instance;
			SharingAnonymousIdentityCacheKey key = publishingUrl.CreateKey();
			SharingAnonymousIdentityCacheValue sharingAnonymousIdentityCacheValue = instance.Get(key);
			if (!sharingAnonymousIdentityCacheValue.IsAccessAllowed)
			{
				ExTraceGlobals.SharingTracer.TraceError<PublishingUrl, Type>(0L, "PublishedFolder.Create(PublishingUrl): Cannot find access allowed folder from the request url: path = {0}, type = {1}.", publishingUrl, publishingUrl.GetType());
				throw new PublishedFolderAccessDeniedException();
			}
			ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier, string, string>(0L, "PublishedFolder.Create(PublishingUrl): User {0} has Sharing Anonymous identity {1}. The corresponding folder identity is {2}.", sharingAnonymousIdentityCacheValue.Sid, publishingUrl.Identity, sharingAnonymousIdentityCacheValue.FolderId);
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = StoreObjectId.Deserialize(sharingAnonymousIdentityCacheValue.FolderId);
			}
			catch (CorruptDataException innerException)
			{
				ExTraceGlobals.SharingTracer.TraceError<string>(0L, "PublishedFolder.Create(PublishingUrl): The folder identity '{0}' is invalid.", sharingAnonymousIdentityCacheValue.FolderId);
				throw new PublishedFolderAccessDeniedException(innerException);
			}
			if (publishingUrl.DataType == SharingDataType.ReachCalendar || publishingUrl.DataType == SharingDataType.Calendar)
			{
				ObscureUrl obscureUrl = publishingUrl as ObscureUrl;
				return new PublishedCalendar(publishingUrl.Domain, sharingAnonymousIdentityCacheValue.Sid, storeObjectId, (obscureUrl == null) ? null : new ObscureKind?(obscureUrl.ObscureKind), (obscureUrl == null) ? null : obscureUrl.ReachUserSid);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x00212C34 File Offset: 0x00210E34
		internal static PublishedFolder Create(Folder folder)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			MailboxSession mailboxSession = folder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new ArgumentException("folder must be in a mailbox");
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(folder.ClassName, "IPF.Appointment"))
			{
				return new PublishedCalendar(mailboxSession, folder.StoreObjectId);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x00212C90 File Offset: 0x00210E90
		private static bool SleepIfNecessary()
		{
			uint value = (uint)PublishedFolder.PublishedFolderSlowdownCpuThreshold.Value;
			int arg;
			float arg2;
			if (CPUBasedSleeper.SleepIfNecessary(value, out arg, out arg2))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<int, float>(0L, "PublishedFolder.SleepIfNecessary(): SleepTime = {0}, CpuPercent = {1}", arg, arg2);
				return true;
			}
			return false;
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x00212CCA File Offset: 0x00210ECA
		public ExchangePrincipal CreateExchangePrincipal()
		{
			this.CheckDisposed("CreateExchangePrincipal");
			return ExchangePrincipal.FromUserSid(this.RecipientSession, this.sid, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x00212CE9 File Offset: 0x00210EE9
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x00212D08 File Offset: 0x00210F08
		private MailboxSession CreateMailboxSession()
		{
			ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Create mailbox session as SystemService for sid {0}.", this.sid);
			MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(ExchangePrincipal.FromUserSid(this.RecipientSession, this.sid), Thread.CurrentThread.CurrentCulture, "Client=AS;Action=PublishedFolder");
			if (this.budget != null)
			{
				mailboxSession.AccountingObject = this.budget;
			}
			mailboxSession.ExTimeZone = (TimeZoneHelper.GetUserTimeZone(mailboxSession) ?? ExTimeZone.CurrentTimeZone);
			return mailboxSession;
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x00212D84 File Offset: 0x00210F84
		private IRecipientSession CreateRecipientSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, this.sessionSettings, 392, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Sharing\\PublishedFolder.cs");
			if (this.budget != null)
			{
				tenantOrRootOrgRecipientSession.SessionSettings.AccountingObject = this.budget;
			}
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x00212DCC File Offset: 0x00210FCC
		private void AcquireBudgetAndStartTiming()
		{
			ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Acquiring and check the budget for sid {0}.", this.sid);
			this.budget = StandardBudget.Acquire(this.sid, BudgetType.Anonymous, ADSessionSettings.FromRootOrgScopeSet());
			this.budget.CheckOverBudget();
			ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Start timing for sid {0}.", this.sid);
			PublishedFolder.SleepIfNecessary();
			string callerInfo = "PublishedFolder.AcquireBudgetAndStartTiming";
			this.budget.StartConnection(callerInfo);
			this.budget.StartLocal(callerInfo, default(TimeSpan));
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x00212E61 File Offset: 0x00211061
		private void ReleaseBudgetAndStopTiming()
		{
			if (this.budget != null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Release the budget and stop timing for sid {0}.", this.sid);
				this.budget.Dispose();
				this.budget = null;
			}
		}

		// Token: 0x06007844 RID: 30788 RVA: 0x00212E99 File Offset: 0x00211099
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06007845 RID: 30789 RVA: 0x00212EA2 File Offset: 0x002110A2
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.InternalDispose(disposing);
				this.isDisposed = true;
			}
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x00212EC8 File Offset: 0x002110C8
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
				if (this.mailboxSession != null && this.disposeMailboxSession)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Release the mailbox session sid {0}.", this.sid);
					this.mailboxSession.Dispose();
				}
				this.ReleaseBudgetAndStopTiming();
			}
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x00212F28 File Offset: 0x00211128
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublishedFolder>(this);
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x00212F30 File Offset: 0x00211130
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x04005335 RID: 21301
		private readonly SecurityIdentifier sid;

		// Token: 0x04005336 RID: 21302
		private readonly StoreObjectId folderId;

		// Token: 0x04005337 RID: 21303
		private static readonly IntAppSettingsEntry PublishedFolderSlowdownCpuThreshold = new IntAppSettingsEntry("PublishedFolderSlowdownCpuThreshold", 25, ExTraceGlobals.SharingTracer);

		// Token: 0x04005338 RID: 21304
		private MailboxSession mailboxSession;

		// Token: 0x04005339 RID: 21305
		private IRecipientSession recipientSession;

		// Token: 0x0400533A RID: 21306
		private DisposeTracker disposeTracker;

		// Token: 0x0400533B RID: 21307
		private IStandardBudget budget;

		// Token: 0x0400533C RID: 21308
		private string displayName;

		// Token: 0x0400533D RID: 21309
		private bool disposeMailboxSession;

		// Token: 0x0400533E RID: 21310
		private bool isDisposed;

		// Token: 0x0400533F RID: 21311
		private ADSessionSettings sessionSettings;
	}
}
