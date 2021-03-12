using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200017E RID: 382
	internal class UMMailboxRecipient : UMRecipient
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x0002C692 File Offset: 0x0002A892
		public UMMailboxRecipient(ADRecipient adrecipient)
		{
			this.Initialize(adrecipient, true);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002C6B9 File Offset: 0x0002A8B9
		public UMMailboxRecipient(ADRecipient adrecipient, MailboxSession mbxSession)
		{
			this.Initialize(adrecipient, mbxSession, true);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002C6E1 File Offset: 0x0002A8E1
		protected UMMailboxRecipient()
		{
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002C6FF File Offset: 0x0002A8FF
		public ADUser ADUser
		{
			get
			{
				return this.aduser;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002C707 File Offset: 0x0002A907
		public IConfigurationFolder ConfigFolder
		{
			get
			{
				return this.configFolder;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002C70F File Offset: 0x0002A90F
		public virtual CultureInfo TelephonyCulture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002C712 File Offset: 0x0002A912
		public virtual CultureInfo MessageSubmissionCulture
		{
			get
			{
				return this.messageSubmissionCulture ?? CommonConstants.DefaultCulture;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002C723 File Offset: 0x0002A923
		public string HomeServerLegacyDN
		{
			get
			{
				return this.ADUser.ServerLegacyDN;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002C730 File Offset: 0x0002A930
		public string ExchangeLegacyDN
		{
			get
			{
				return this.ADUser.LegacyExchangeDN;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002C73D File Offset: 0x0002A93D
		public ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return base.InternalExchangePrincipal;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002C745 File Offset: 0x0002A945
		public bool HasContactsFolder
		{
			get
			{
				this.EnsureLazyMailboxInitialization();
				return this.lazyHasContactsFolder;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002C753 File Offset: 0x0002A953
		public bool HasCalendarFolder
		{
			get
			{
				this.EnsureLazyMailboxInitialization();
				return this.lazyHasCalendarFolder;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002C761 File Offset: 0x0002A961
		public bool HasDraftsFolder
		{
			get
			{
				this.EnsureLazyMailboxInitialization();
				return this.lazyHasDraftsFolder;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002C76F File Offset: 0x0002A96F
		public ExDateTime Now
		{
			get
			{
				this.EnsureLazyMailboxInitialization();
				return ExDateTime.GetNow(this.lazyTimeZone);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002C782 File Offset: 0x0002A982
		public ExTimeZone TimeZone
		{
			get
			{
				this.EnsureLazyMailboxInitialization();
				return this.lazyTimeZone;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002C790 File Offset: 0x0002A990
		public CultureInfo[] PreferredCultures
		{
			get
			{
				return this.preferredCultures;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002C798 File Offset: 0x0002A998
		private MailboxSession UnsafeMailboxSession
		{
			get
			{
				base.CheckDisposed();
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UmUser(#{0})::UnsafeMailboxSession()", new object[]
				{
					this.GetHashCode()
				});
				if (!CommonUtil.IsServerCompatible(this.ExchangePrincipal.MailboxInfo.Location.ServerVersion))
				{
					throw new LegacyUmUserException(this.ExchangeLegacyDN);
				}
				this.EnsureLazyMailboxInitialization();
				return this.lazyMailboxSession;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002C808 File Offset: 0x0002AA08
		public new static bool TryCreate(ADRecipient adrecipient, out UMRecipient umrecipient)
		{
			UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient();
			if (ummailboxRecipient.Initialize(adrecipient, false))
			{
				umrecipient = ummailboxRecipient;
				return true;
			}
			ummailboxRecipient.Dispose();
			umrecipient = null;
			return false;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002C834 File Offset: 0x0002AA34
		public bool IsMailboxQuotaExceeded()
		{
			bool result = false;
			try
			{
				result = XsoUtil.IsMailboxQuotaExceeded(this, 2097152U);
			}
			catch (LocalizedException ex)
			{
				PIIMessage data = PIIMessage.Create(PIIType._User, this.ToString());
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "Exception trying to find out if the mailbox quota was exceeded for user=_User. e={0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002C890 File Offset: 0x0002AA90
		public UMMailboxRecipient.MailboxSessionLock CreateSessionLock()
		{
			return new UMMailboxRecipient.MailboxSessionLock(this);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002C898 File Offset: 0x0002AA98
		public UMMailboxRecipient.MailboxConnectionGuard CreateConnectionGuard()
		{
			return new UMMailboxRecipient.MailboxConnectionGuard(this);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		protected override bool Initialize(ADRecipient recipient, bool throwOnFailure)
		{
			bool flag = false;
			try
			{
				if (!base.Initialize(recipient, throwOnFailure))
				{
					return flag;
				}
				this.aduser = (recipient as ADUser);
				if (!base.CheckField(this.aduser, "aduser", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (!base.CheckField(this.ExchangeLegacyDN, "ExchangeLegacyDN", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (!base.CheckField(this.HomeServerLegacyDN, "HomeServerLegacyDN", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (!CommonConstants.UseDataCenterCallRouting)
				{
					if (!base.CheckField(base.InternalIsIncompatibleMailboxUser, "IncompatibleMailboxUser", (object o) => !base.InternalIsIncompatibleMailboxUser, throwOnFailure))
					{
						return flag;
					}
				}
				if (!base.CheckField(this.MailAddress, "MailAddress", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (!base.CheckField(base.DisplayName, "DisplayName", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				this.configFolder = new XsoConfigurationFolder(this);
				if (!base.CheckField(this.configFolder, "ConfigurationFolder", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (this.aduser.Languages == null)
				{
					this.preferredCultures = new CultureInfo[0];
				}
				else
				{
					this.preferredCultures = this.aduser.Languages.ToArray();
				}
				this.InitMessageSubmissionCulture();
				if (!base.CheckField(this.MessageSubmissionCulture, "MessageSubmissionCulture", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				this.clientOwnsMailboxSession = false;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002CA64 File Offset: 0x0002AC64
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMMailboxRecipient>(this);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002CA6C File Offset: 0x0002AC6C
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UmUser(#{0})::Dispose()", new object[]
					{
						this.GetHashCode()
					});
					if (!this.clientOwnsMailboxSession)
					{
						lock (this.sessionLock)
						{
							if (this.lazyMailboxSession != null)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UmUser(#{0})::Dispose() disposing Mailbox Connection", new object[]
								{
									this.GetHashCode()
								});
								this.lazyMailboxSession.Dispose();
								this.lazyMailboxSession = null;
							}
						}
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002CB34 File Offset: 0x0002AD34
		protected virtual bool Initialize(ADRecipient recipient, MailboxSession mbxSession, bool throwOnFailure)
		{
			bool flag = false;
			if (!this.Initialize(recipient, throwOnFailure))
			{
				return flag;
			}
			try
			{
				this.clientOwnsMailboxSession = true;
				this.InitializeLazyMailboxSession(mbxSession);
				if (!base.CheckField(this.lazyMailboxSession, "lazyMailboxSession", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		private void InitMessageSubmissionCulture()
		{
			if (this.PreferredCultures == null || this.PreferredCultures.Length < 1)
			{
				return;
			}
			this.messageSubmissionCulture = UmCultures.GetPreferredClientCulture(this.PreferredCultures);
			if (this.messageSubmissionCulture == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Could not find a suitable client culture, falling back to default.", new object[0]);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Initialized message submission Culture ={0}.", new object[]
			{
				this.messageSubmissionCulture
			});
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002CC10 File Offset: 0x0002AE10
		private void EnsureLazyMailboxInitialization()
		{
			if (this.lazyMailboxSession == null)
			{
				lock (this.sessionLock)
				{
					if (this.lazyMailboxSession == null)
					{
						this.InitializeLazyMailboxSession(MailboxSessionEstablisher.OpenAsAdmin(this.ExchangePrincipal, this.TelephonyCulture ?? this.MessageSubmissionCulture, "Client=UM"));
					}
				}
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002CC80 File Offset: 0x0002AE80
		private void InitializeLazyMailboxSession(MailboxSession mbxSession)
		{
			this.lazyMailboxSession = mbxSession;
			this.lazyTimeZone = CommonUtil.GetOwaTimeZone(this.lazyMailboxSession);
			this.lazyMailboxSession.ExTimeZone = this.lazyTimeZone;
			if (!this.MessageSubmissionCulture.IsNeutralCulture)
			{
				this.MessageSubmissionCulture.DateTimeFormat.ShortTimePattern = CommonUtil.GetOwaTimeFormat(this.lazyMailboxSession);
			}
			this.lazyHasContactsFolder = (this.lazyMailboxSession.GetDefaultFolderId(DefaultFolderType.Contacts) != null);
			this.lazyHasCalendarFolder = (this.lazyMailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar) != null);
			this.lazyHasDraftsFolder = (this.lazyMailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts) != null);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002CD28 File Offset: 0x0002AF28
		private void AcquireMailboxSession(bool withLock)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.ExchangeLegacyDN);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "UmUser(#{0})::AcquireMailboxSession called for user=_User, lock={1}, threadId={2}, guardCount={3}", new object[]
			{
				this.GetHashCode(),
				withLock,
				Thread.CurrentThread.ManagedThreadId,
				this.sessionGuardCount
			});
			lock (this.sessionGuardCountLock)
			{
				this.sessionGuardCount++;
			}
			if (withLock)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UmUser(#{0})::AcquireMailboxSession Acquiring Lock", new object[]
				{
					this.GetHashCode()
				});
				Monitor.Enter(this.sessionLock);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UmUser(#{0})::AcquireMailboxSession Acquired Lock", new object[]
				{
					this.GetHashCode()
				});
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002CE30 File Offset: 0x0002B030
		private void ReleaseMailboxSession(bool withLock)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.ExchangeLegacyDN);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "UmUser(#{0})::ReleaseMailboxSession called for user=_User, lock={1}, threadId={2}, guardCount={3}", new object[]
			{
				this.GetHashCode(),
				withLock,
				Thread.CurrentThread.ManagedThreadId,
				this.sessionGuardCount
			});
			try
			{
				lock (this.sessionGuardCountLock)
				{
					this.sessionGuardCount--;
					if (this.sessionGuardCount == 0 && this.lazyMailboxSession != null && this.lazyMailboxSession.IsConnected && !this.clientOwnsMailboxSession)
					{
						this.lazyMailboxSession.Disconnect();
					}
				}
			}
			finally
			{
				if (withLock)
				{
					Monitor.Exit(this.sessionLock);
				}
			}
		}

		// Token: 0x04000691 RID: 1681
		private ADUser aduser;

		// Token: 0x04000692 RID: 1682
		private MailboxSession lazyMailboxSession;

		// Token: 0x04000693 RID: 1683
		private object sessionLock = new object();

		// Token: 0x04000694 RID: 1684
		private int sessionGuardCount;

		// Token: 0x04000695 RID: 1685
		private object sessionGuardCountLock = new object();

		// Token: 0x04000696 RID: 1686
		private ExTimeZone lazyTimeZone;

		// Token: 0x04000697 RID: 1687
		private bool lazyHasContactsFolder;

		// Token: 0x04000698 RID: 1688
		private bool lazyHasCalendarFolder;

		// Token: 0x04000699 RID: 1689
		private bool lazyHasDraftsFolder;

		// Token: 0x0400069A RID: 1690
		private CultureInfo messageSubmissionCulture;

		// Token: 0x0400069B RID: 1691
		private IConfigurationFolder configFolder;

		// Token: 0x0400069C RID: 1692
		private bool clientOwnsMailboxSession;

		// Token: 0x0400069D RID: 1693
		private CultureInfo[] preferredCultures;

		// Token: 0x0200017F RID: 383
		internal class MailboxConnectionGuard : DisposableBase
		{
			// Token: 0x06000C43 RID: 3139 RVA: 0x0002CF24 File Offset: 0x0002B124
			internal MailboxConnectionGuard(UMMailboxRecipient u) : this(u, false)
			{
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x0002CF30 File Offset: 0x0002B130
			protected MailboxConnectionGuard(UMMailboxRecipient u, bool withLock)
			{
				using (DisposeGuard disposeGuard = this.Guard())
				{
					this.user = u;
					this.withLock = withLock;
					this.user.AcquireMailboxSession(withLock);
					disposeGuard.Success();
				}
			}

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002CF8C File Offset: 0x0002B18C
			protected UMMailboxRecipient User
			{
				get
				{
					return this.user;
				}
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0002CF94 File Offset: 0x0002B194
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.user.ReleaseMailboxSession(this.withLock);
				}
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x0002CFAA File Offset: 0x0002B1AA
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UMMailboxRecipient.MailboxConnectionGuard>(this);
			}

			// Token: 0x0400069E RID: 1694
			private UMMailboxRecipient user;

			// Token: 0x0400069F RID: 1695
			private bool withLock;
		}

		// Token: 0x02000180 RID: 384
		internal class MailboxSessionLock : UMMailboxRecipient.MailboxConnectionGuard
		{
			// Token: 0x06000C48 RID: 3144 RVA: 0x0002CFB2 File Offset: 0x0002B1B2
			internal MailboxSessionLock(UMMailboxRecipient u) : base(u, true)
			{
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002CFBC File Offset: 0x0002B1BC
			internal MailboxSession Session
			{
				get
				{
					MailboxSession unsafeMailboxSession = base.User.UnsafeMailboxSession;
					if (unsafeMailboxSession != null && !unsafeMailboxSession.IsConnected)
					{
						this.underlyingStoreRPCSessionDisconnected = MailboxSessionEstablisher.ConnectWithStatus(unsafeMailboxSession);
					}
					return unsafeMailboxSession;
				}
			}

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002CFED File Offset: 0x0002B1ED
			internal bool UnderlyingStoreRPCSessionDisconnected
			{
				get
				{
					return this.underlyingStoreRPCSessionDisconnected;
				}
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x0002CFF5 File Offset: 0x0002B1F5
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UMMailboxRecipient.MailboxSessionLock>(this);
			}

			// Token: 0x040006A0 RID: 1696
			private bool underlyingStoreRPCSessionDisconnected;
		}
	}
}
