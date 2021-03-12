using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200009B RID: 155
	internal abstract class MailboxContextBase : DisposeTrackableBase, IMailboxContext, IDisposable, IEWSPartnerRequestContext
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x000112F4 File Offset: 0x0000F4F4
		internal MailboxContextBase(UserContextKey key, string userAgent)
		{
			ExAssert.RetailAssert(key != null, "[MailboxContextBase::ctor] key is null");
			ExAssert.RetailAssert(!string.IsNullOrEmpty(userAgent), "[MailboxContextBase::ctor] userAgent is null");
			this.key = key;
			this.UserAgent = userAgent;
			this.syncRoot = new object();
			if (!Globals.Owa2ServerUnitTestsHook && !Globals.DisableBreadcrumbs)
			{
				this.breadcrumbBuffer = new BreadcrumbBuffer(Globals.MaxBreadcrumbs);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00011393 File Offset: 0x0000F593
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0001139B File Offset: 0x0000F59B
		public string UserAgent { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000113A4 File Offset: 0x0000F5A4
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x000113AC File Offset: 0x0000F5AC
		public UserContextState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000113B5 File Offset: 0x0000F5B5
		public UserContextKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000113BD File Offset: 0x0000F5BD
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x000113C5 File Offset: 0x0000F5C5
		public ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.exchangePrincipal;
			}
			set
			{
				this.exchangePrincipal = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000113CE File Offset: 0x0000F5CE
		public MailboxSession MailboxSession
		{
			get
			{
				if (!this.MailboxSessionLockedByCurrentThread())
				{
					throw new OwaInvalidOperationException("Operation is not allowed if mailbox lock is not held");
				}
				this.CreateMailboxSessionIfNeeded();
				return this.mailboxSession;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000113EF File Offset: 0x0000F5EF
		public string UserPrincipalName
		{
			get
			{
				if (this.mailboxIdentity != null)
				{
					this.userPrincipalName = this.mailboxIdentity.GetOWAMiniRecipient().UserPrincipalName;
				}
				return this.userPrincipalName;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00011415 File Offset: 0x0000F615
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				if (this.mailboxIdentity != null)
				{
					return this.mailboxIdentity.GetOWAMiniRecipient().PrimarySmtpAddress;
				}
				return SmtpAddress.Empty;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00011435 File Offset: 0x0000F635
		public OwaIdentity LogonIdentity
		{
			get
			{
				return this.logonIdentity;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001143D File Offset: 0x0000F63D
		public OwaIdentity MailboxIdentity
		{
			get
			{
				return this.mailboxIdentity;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00011448 File Offset: 0x0000F648
		public virtual AuthZClientInfo CallerClientInfo
		{
			get
			{
				SidBasedIdentity sidBasedIdentity = HttpContext.Current.User.Identity as SidBasedIdentity;
				ExAssert.RetailAssert(sidBasedIdentity != null, "identity is null");
				AuthZClientInfo result = null;
				if (this.callerClientInfo == null)
				{
					try
					{
						this.callerClientInfo = CallContextUtilities.GetCallerClientInfo();
					}
					catch (AuthzException ex)
					{
						ExTraceGlobals.CoreTracer.TraceError<string, string, AuthzException>(0L, "MailboxContextBase.CallerClientInfo could not be fetched for Name={0} AuthenticationType={1}. Exception: {2}", sidBasedIdentity.Name, sidBasedIdentity.AuthenticationType, ex);
						if (ex.InnerException is Win32Exception)
						{
							throw new OwaIdentityException("There was a problem getting the caller AuthZClientInfo", ex);
						}
						throw;
					}
				}
				if (this.callerClientInfo != null && this.callerClientInfo.UserIdentity.Sid.ToString() == sidBasedIdentity.Sid.ToString())
				{
					result = this.callerClientInfo;
				}
				return result;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00011514 File Offset: 0x0000F714
		public virtual SessionDataCache SessionDataCache
		{
			get
			{
				throw new NotImplementedException("Session data cache is only supported on the primary user context");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00011520 File Offset: 0x0000F720
		public INotificationManager NotificationManager
		{
			get
			{
				return this.notificationManager;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00011528 File Offset: 0x0000F728
		public PendingRequestManager PendingRequestManager
		{
			get
			{
				return this.pendingRequestManager;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00011530 File Offset: 0x0000F730
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00011538 File Offset: 0x0000F738
		public CacheItemRemovedReason AbandonedReason
		{
			get
			{
				return this.abandonedReason;
			}
			set
			{
				this.abandonedReason = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00011541 File Offset: 0x0000F741
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00011549 File Offset: 0x0000F749
		public UserContextTerminationStatus TerminationStatus { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00011552 File Offset: 0x0000F752
		public bool IsExplicitLogon
		{
			get
			{
				return this.isExplicitLogon;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001155A File Offset: 0x0000F75A
		public bool IsMailboxSessionCreated
		{
			get
			{
				return this.isMailboxSessionCreated;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00011562 File Offset: 0x0000F762
		protected StringBuilder UserContextDiposeGraph
		{
			get
			{
				return this.userContextDiposeGraph;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001156A File Offset: 0x0000F76A
		public bool LockAndReconnectMailboxSession(int timeout)
		{
			if (this.mailboxSessionLock.LockWriterElastic(timeout))
			{
				return UserContextUtilities.ReconnectStoreSession(this.MailboxSession, this);
			}
			throw new OwaLockTimeoutException("User context could not acquire the mailbox session writer lock", null, this);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00011594 File Offset: 0x0000F794
		public void UnlockAndDisconnectMailboxSession()
		{
			if (this.mailboxSessionLock.IsWriterLockHeld)
			{
				try
				{
					if (this.mailboxSession != null)
					{
						UserContextUtilities.DisconnectStoreSession(this.mailboxSession);
					}
				}
				finally
				{
					this.mailboxSessionLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public bool MailboxSessionLockedByCurrentThread()
		{
			return this.mailboxSessionLock.IsWriterLockHeld;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public void DisconnectMailboxSession()
		{
			if (this.mailboxSession != null)
			{
				try
				{
					if (this.mailboxSessionLock.LockWriterElastic(3000))
					{
						UserContextUtilities.DisconnectStoreSession(this.mailboxSession);
					}
				}
				finally
				{
					if (this.mailboxSessionLock.IsWriterLockHeld)
					{
						this.mailboxSessionLock.ReleaseWriterLock();
					}
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00011650 File Offset: 0x0000F850
		public override string ToString()
		{
			return this.GetHashCode().ToString();
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001166C File Offset: 0x0000F86C
		public void Load(OwaIdentity logonIdentity, OwaIdentity mailboxIdentity, UserContextStatistics stats)
		{
			this.LogTrace("UserContextBase.Load", "starting");
			lock (this.syncRoot)
			{
				this.DoLoad(logonIdentity, mailboxIdentity, stats);
				this.AfterLoad();
			}
			this.LogTrace("UserContextBase.Load", "method finished");
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000116D8 File Offset: 0x0000F8D8
		protected virtual void DoLoad(OwaIdentity logonIdentity, OwaIdentity mailboxIdentity, UserContextStatistics stats)
		{
			if (logonIdentity == null)
			{
				throw new ArgumentNullException("logonIdentity");
			}
			this.logonIdentity = logonIdentity;
			if (mailboxIdentity != null)
			{
				this.isExplicitLogon = true;
				this.mailboxIdentity = mailboxIdentity;
			}
			else
			{
				this.mailboxIdentity = logonIdentity;
			}
			if (this.IsExplicitLogon)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Created partial mailbox identity from SMTP address={0}", mailboxIdentity.SafeGetRenderableName());
				OwaMiniRecipientIdentity owaMiniRecipientIdentity = this.mailboxIdentity as OwaMiniRecipientIdentity;
				try
				{
					owaMiniRecipientIdentity.UpgradePartialIdentity();
				}
				catch (DataValidationException ex)
				{
					PropertyValidationError propertyValidationError = ex.Error as PropertyValidationError;
					if (propertyValidationError == null || propertyValidationError.PropertyDefinition != MiniRecipientSchema.Languages)
					{
						throw;
					}
					OWAMiniRecipient owaminiRecipient = this.MailboxIdentity.FixCorruptOWAMiniRecipientCultureEntry();
					if (owaminiRecipient != null)
					{
						this.mailboxIdentity = OwaMiniRecipientIdentity.CreateFromOWAMiniRecipient(owaminiRecipient);
					}
				}
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.exchangePrincipal = this.mailboxIdentity.CreateExchangePrincipal();
			stats.ExchangePrincipalCreationTime = (int)stopwatch.ElapsedMilliseconds;
			this.LogTrace("UserContextBase.Load", "CreateExchangePrincipal finished");
			this.pendingRequestManager = new PendingRequestManager(this, ListenerChannelsManager.Instance);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000117DC File Offset: 0x0000F9DC
		private void AfterLoad()
		{
			this.notificationManager = this.GetNotificationManager(this);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000117EB File Offset: 0x0000F9EB
		protected virtual INotificationManager GetNotificationManager(MailboxContextBase mailboxContext)
		{
			return new OwaMapiNotificationManager(mailboxContext);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000117F3 File Offset: 0x0000F9F3
		public void LogBreadcrumb(string message)
		{
			if (Globals.DisableBreadcrumbs)
			{
				return;
			}
			if (this.breadcrumbBuffer == null)
			{
				return;
			}
			this.breadcrumbBuffer.Add(new Breadcrumb(ExDateTime.UtcNow, (message != null) ? message : "<null>"));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00011828 File Offset: 0x0000FA28
		public string DumpBreadcrumbs()
		{
			if (Globals.DisableBreadcrumbs)
			{
				return string.Empty;
			}
			if (this.breadcrumbBuffer == null)
			{
				return "<Breadcrumb buffer is null>";
			}
			StringBuilder stringBuilder = new StringBuilder(Globals.MaxBreadcrumbs * 128);
			stringBuilder.Append("OWA breadcrumbs:\r\n");
			this.breadcrumbBuffer.DumpTo(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001187F File Offset: 0x0000FA7F
		internal bool GetPendingGetManagerLock()
		{
			if (this.pendingGetManagerLock.IsWriterLockHeld)
			{
				throw new OwaInvalidOperationException("GetPendingGetManagerLock lock is already held by this thread");
			}
			return this.pendingGetManagerLock.LockWriterElastic(3000);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000118A9 File Offset: 0x0000FAA9
		internal void ReleasePendingGetManagerLock()
		{
			if (this.pendingGetManagerLock.IsWriterLockHeld)
			{
				this.pendingGetManagerLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public MailboxSession CloneMailboxSession(string mailboxKey, ExchangePrincipal exchangePrincipal, IADOrgPerson person, ClientSecurityContext clientSecurityContext, GenericIdentity genericIdentity, bool unifiedLogon)
		{
			MailboxSession result = null;
			if (!string.IsNullOrEmpty(mailboxKey) && !base.IsDisposed && this.mailboxSession != null)
			{
				string text = AccessingPrincipalTiedCache.BuildKeyCacheKey(this.clonedMailboxProperties.MailboxGuid, this.clonedMailboxProperties.MailboxCulture, this.clonedMailboxProperties.LogonType);
				if (mailboxKey.Equals(text, StringComparison.InvariantCulture))
				{
					if (this.ExchangePrincipal != null && object.Equals(this.ExchangePrincipal.ObjectId, exchangePrincipal.ObjectId))
					{
						result = this.mailboxSession.CloneWithBestAccess(exchangePrincipal, person, clientSecurityContext, this.clonedMailboxProperties.ClientInfoString, genericIdentity, unifiedLogon);
					}
					else
					{
						ExTraceGlobals.UserContextCallTracer.TraceWarning(0L, string.Format("CloneMailboxSession: Mailbox owner not same, mailboxkey:{0}", text));
					}
				}
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554
		public abstract void ValidateLogonPermissionIfNecessary();

		// Token: 0x06000613 RID: 1555 RVA: 0x0001197E File Offset: 0x0000FB7E
		protected void LogTrace(string methodName, string message)
		{
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("UserContext", this, methodName, message));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00011994 File Offset: 0x0000FB94
		protected override void InternalDispose(bool isDisposing)
		{
			this.LogBreadcrumb("Entering InternalDispose");
			this.UserContextDiposeGraph.Append("1");
			this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.UserContextDipose1, this.UserContextDiposeGraph.ToString());
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "UserContext.InternalDispose Start");
			if (this.isInProcessOfDisposing)
			{
				this.UserContextDiposeGraph.Append(".2");
				return;
			}
			this.isInProcessOfDisposing = true;
			this.UserContextDiposeGraph.Append(".3");
			this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.UserContextDipose2, this.UserContextDiposeGraph.ToString());
			lock (this.syncRoot)
			{
				this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.MailboxSessionDuration, this.mailboxSessionCreationTime.ToString() + "--" + DateTime.UtcNow.ToString());
				this.UserContextDiposeGraph.Append(".4");
				ExTraceGlobals.UserContextCallTracer.TraceDebug<bool, MailboxContextBase>(0L, "UserContext.Dispose. IsDisposing: {0}, User context instance={1}", isDisposing, this);
				if (isDisposing && !this.isDisposed)
				{
					this.UserContextDiposeGraph.Append(".5");
					try
					{
						try
						{
							RemoteNotificationManager.Instance.CleanUpSubscriptions(this.Key.ToString());
							Stopwatch stopwatch = Stopwatch.StartNew();
							this.mailboxSessionLock.LockWriterElastic(15000);
							stopwatch.Stop();
							this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.MailboxSessionLockTime, stopwatch.ElapsedMilliseconds.ToString());
							this.UserContextDiposeGraph.Append(".6");
							this.DisposeMailboxSessionReferencingObjects();
						}
						catch (StoragePermanentException ex)
						{
							this.UserContextDiposeGraph.Append(".7");
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose notification objects.  exception {0}", ex.Message);
						}
						catch (StorageTransientException ex2)
						{
							this.UserContextDiposeGraph.Append(".8");
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose notification objects.  exception {0}", ex2.Message);
						}
						catch (Exception ex3)
						{
							this.UserContextDiposeGraph.Append(".9");
							ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "Unable to dispose notification objects.  exception {0}", ex3.Message);
							throw;
						}
						finally
						{
							try
							{
								if (this.mailboxSession != null)
								{
									this.UserContextDiposeGraph.Append(".10");
									if (this.MailboxSessionLockedByCurrentThread() || this.mailboxSessionLock.LockWriterElastic(15000))
									{
										this.UserContextDiposeGraph.Append(".11");
										this.DisposeMailboxSession();
									}
									else
									{
										this.UserContextDiposeGraph.Append(".12");
										ExTraceGlobals.UserContextTracer.TraceError(0L, "Outlook Web App failed to obtain mailbox session lock to dispose of the mailbox session.");
									}
								}
							}
							finally
							{
								if (this.mailboxSessionLock.IsWriterLockHeld)
								{
									this.UserContextDiposeGraph.Append(".13");
									this.mailboxSessionLock.ReleaseWriterLock();
								}
							}
						}
					}
					finally
					{
						this.UserContextDiposeGraph.Append(".14");
						this.isDisposed = true;
						this.DisposeNonMailboxSessionReferencingObjects();
						this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.UserContextDipose3, this.UserContextDiposeGraph.ToString());
						this.UserContextDiposeGraph.Clear();
					}
				}
			}
			this.LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData.UserContextDipose4, this.UserContextDiposeGraph.ToString());
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "UserContext.InternalDispose End");
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00011D64 File Offset: 0x0000FF64
		protected virtual void DisposeMailboxSessionReferencingObjects()
		{
			if (this.notificationManager != null)
			{
				this.UserContextDiposeGraph.Append(".c1");
				this.notificationManager.Dispose();
				this.notificationManager = null;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00011D94 File Offset: 0x0000FF94
		protected virtual void DisposeNonMailboxSessionReferencingObjects()
		{
			this.UserContextDiposeGraph.Append(".b1");
			try
			{
				if (this.GetPendingGetManagerLock())
				{
					this.UserContextDiposeGraph.Append(".b2");
					if (this.pendingRequestManager != null)
					{
						this.UserContextDiposeGraph.Append(".b3");
						this.pendingRequestManager.Dispose();
						this.pendingRequestManager = null;
					}
				}
				else
				{
					this.UserContextDiposeGraph.Append(".b4");
					this.pendingRequestManager.ShouldDispose = true;
				}
			}
			finally
			{
				this.ReleasePendingGetManagerLock();
			}
			if (this.logonIdentity != null)
			{
				this.UserContextDiposeGraph.Append(".b5");
				this.logonIdentity.Dispose();
				this.logonIdentity = null;
			}
			if (this.mailboxIdentity != null)
			{
				this.UserContextDiposeGraph.Append(".b6");
				this.mailboxIdentity.Dispose();
				this.mailboxIdentity = null;
			}
			if (this.callerClientInfo != null)
			{
				this.UserContextDiposeGraph.Append(".b7");
				this.callerClientInfo.Dispose();
				this.callerClientInfo = null;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00011EB0 File Offset: 0x000100B0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxContextBase>(this);
		}

		// Token: 0x06000618 RID: 1560
		protected abstract MailboxSession CreateMailboxSession();

		// Token: 0x06000619 RID: 1561 RVA: 0x00011EB8 File Offset: 0x000100B8
		protected void InternalRetireMailboxSession()
		{
			ExTraceGlobals.UserContextTracer.TraceDebug((long)this.GetHashCode(), "InternalRetireMailboxSession: Start");
			if (this.mailboxSessionLock.LockWriterElastic(3000))
			{
				try
				{
					try
					{
						if (this.mailboxSession != null)
						{
							this.mailboxSession.Dispose();
							this.mailboxSession = null;
							this.isMailboxSessionCreated = false;
							this.mailboxSessionCreationTime = DateTime.MinValue;
							if (this.notificationManager != null)
							{
								this.notificationManager.HandleConnectionDroppedNotification();
							}
							ExTraceGlobals.UserContextTracer.TraceDebug((long)this.GetHashCode(), "InternalRetireMailboxSession: Disposed mailbox session");
						}
					}
					catch (Exception ex)
					{
						ExTraceGlobals.UserContextTracer.TraceError<string>((long)this.GetHashCode(), "InternalRetireMailboxSession: Failed to retire mailbox. exception {0}", ex.Message);
						throw;
					}
					return;
				}
				finally
				{
					this.mailboxSessionLock.ReleaseWriterLock();
				}
			}
			throw new OwaLockTimeoutException("UserContext::InternalRetireMailboxSession could not acquire the mailbox session writer lock", null, this);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00011FA0 File Offset: 0x000101A0
		protected void CreateMailboxSessionIfNeeded()
		{
			if (this.isMailboxSessionCreated)
			{
				return;
			}
			bool flag = false;
			try
			{
				if (!this.mailboxSessionLock.IsWriterLockHeld)
				{
					flag = this.mailboxSessionLock.LockWriterElastic(3000);
					if (!flag)
					{
						throw new OwaLockTimeoutException("UserContext::CreateMailboxSessionIfNeeded could not acquire the mailbox session writer lock", null, this);
					}
				}
				if (!this.isMailboxSessionCreated)
				{
					this.mailboxSession = this.CreateMailboxSession();
					this.isMailboxSessionCreated = true;
					this.mailboxSessionCreationTime = DateTime.UtcNow;
					this.clonedMailboxProperties = new MailboxContextBase.ClonedMailboxProperties(this.mailboxSession.LogonType, this.mailboxSession.MailboxGuid, this.mailboxSession.Culture, this.mailboxSession.ClientInfoString);
				}
			}
			finally
			{
				if (flag)
				{
					this.mailboxSessionLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00012064 File Offset: 0x00010264
		protected void DisposeMailboxSession()
		{
			this.UserContextDiposeGraph.Append(".a1");
			if (this.mailboxSession != null)
			{
				this.UserContextDiposeGraph.Append(".a2");
				try
				{
					UserContextUtilities.DisconnectStoreSession(this.mailboxSession);
					this.mailboxSession.Dispose();
				}
				catch (StoragePermanentException ex)
				{
					this.UserContextDiposeGraph.Append(".a3");
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to disconnect mailbox session.  exception {0}", ex.Message);
				}
				catch (StorageTransientException ex2)
				{
					this.UserContextDiposeGraph.Append(".a4");
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to disconnect mailbox session.  exception {0}", ex2.Message);
				}
				catch (Exception ex3)
				{
					this.UserContextDiposeGraph.Append(".a5");
					ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "Unable to to disconnect mailbox session.  exception {0}", ex3.Message);
					throw;
				}
				finally
				{
					this.UserContextDiposeGraph.Append(".a6");
					this.mailboxSession = null;
				}
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001218C File Offset: 0x0001038C
		protected void LogMailboxContextDisposeTrace(OwaServerLogger.LoggerData traceId, string trace)
		{
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("MailboxContextDispose-V1", this, null, traceId + "=" + trace));
		}

		// Token: 0x0400035A RID: 858
		internal const string UserContextEventId = "UserContext";

		// Token: 0x0400035B RID: 859
		protected object syncRoot;

		// Token: 0x0400035C RID: 860
		protected MailboxSession mailboxSession;

		// Token: 0x0400035D RID: 861
		protected OwaRWLockWrapper mailboxSessionLock = new OwaRWLockWrapper();

		// Token: 0x0400035E RID: 862
		protected bool isMailboxSessionCreated;

		// Token: 0x0400035F RID: 863
		protected bool isInProcessOfDisposing;

		// Token: 0x04000360 RID: 864
		private bool isDisposed;

		// Token: 0x04000361 RID: 865
		private UserContextState state;

		// Token: 0x04000362 RID: 866
		private CacheItemRemovedReason abandonedReason;

		// Token: 0x04000363 RID: 867
		private OwaRWLockWrapper pendingGetManagerLock = new OwaRWLockWrapper();

		// Token: 0x04000364 RID: 868
		private UserContextKey key;

		// Token: 0x04000365 RID: 869
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x04000366 RID: 870
		private INotificationManager notificationManager;

		// Token: 0x04000367 RID: 871
		private PendingRequestManager pendingRequestManager;

		// Token: 0x04000368 RID: 872
		private OwaIdentity logonIdentity;

		// Token: 0x04000369 RID: 873
		private OwaIdentity mailboxIdentity;

		// Token: 0x0400036A RID: 874
		private string userPrincipalName = string.Empty;

		// Token: 0x0400036B RID: 875
		private bool isExplicitLogon;

		// Token: 0x0400036C RID: 876
		private DateTime mailboxSessionCreationTime;

		// Token: 0x0400036D RID: 877
		private StringBuilder userContextDiposeGraph = new StringBuilder(256);

		// Token: 0x0400036E RID: 878
		private BreadcrumbBuffer breadcrumbBuffer;

		// Token: 0x0400036F RID: 879
		private AuthZClientInfo callerClientInfo;

		// Token: 0x04000370 RID: 880
		private MailboxContextBase.ClonedMailboxProperties clonedMailboxProperties;

		// Token: 0x0200009C RID: 156
		internal class ClonedMailboxProperties
		{
			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x000121B0 File Offset: 0x000103B0
			// (set) Token: 0x0600061E RID: 1566 RVA: 0x000121B8 File Offset: 0x000103B8
			public LogonType LogonType { get; private set; }

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x0600061F RID: 1567 RVA: 0x000121C1 File Offset: 0x000103C1
			// (set) Token: 0x06000620 RID: 1568 RVA: 0x000121C9 File Offset: 0x000103C9
			public Guid MailboxGuid { get; private set; }

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000621 RID: 1569 RVA: 0x000121D2 File Offset: 0x000103D2
			// (set) Token: 0x06000622 RID: 1570 RVA: 0x000121DA File Offset: 0x000103DA
			public CultureInfo MailboxCulture { get; private set; }

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000623 RID: 1571 RVA: 0x000121E3 File Offset: 0x000103E3
			// (set) Token: 0x06000624 RID: 1572 RVA: 0x000121EB File Offset: 0x000103EB
			public string ClientInfoString { get; private set; }

			// Token: 0x06000625 RID: 1573 RVA: 0x000121F4 File Offset: 0x000103F4
			public ClonedMailboxProperties(LogonType logonType, Guid mailboxGuid, CultureInfo mailboxCulture, string clientInfoString)
			{
				this.LogonType = logonType;
				this.MailboxGuid = mailboxGuid;
				this.MailboxCulture = mailboxCulture;
				this.ClientInfoString = clientInfoString;
			}
		}
	}
}
