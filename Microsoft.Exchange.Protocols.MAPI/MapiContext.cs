using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000041 RID: 65
	public class MapiContext : LogicalContext
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000619F File Offset: 0x0000439F
		private MapiContext(ExecutionDiagnostics executionDiagnostics) : base(executionDiagnostics)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000061A8 File Offset: 0x000043A8
		private MapiContext(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, int lcid) : base(executionDiagnostics, securityContext, clientType, CultureHelper.TranslateLcid(lcid))
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000061BA File Offset: 0x000043BA
		public static TimeSpan MailboxLockTimeout
		{
			get
			{
				if (MapiContext.mailboxLockTimeoutHook != null)
				{
					return MapiContext.mailboxLockTimeoutHook();
				}
				return MapiContext.mailboxLockTimeout;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000061D3 File Offset: 0x000043D3
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000061DB File Offset: 0x000043DB
		public MapiSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000061E3 File Offset: 0x000043E3
		public override ClientSecurityContext SecurityContext
		{
			get
			{
				if (this.Session != null)
				{
					return this.Session.CurrentSecurityContext;
				}
				return base.SecurityContext;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006200 File Offset: 0x00004400
		public bool HasMailboxFullRights
		{
			get
			{
				return this.mailboxFullRightsGranted || (this.logon != null && (this.logon.ExchangeTransportServiceRights || this.logon.SystemRights || this.logon.AdminRights || this.logon.IsOwner));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006255 File Offset: 0x00004455
		internal bool HasInternalAccessRights
		{
			get
			{
				return this.logon == null || this.logon.IsSystemServiceLogon || this.logon.ExchangeTransportServiceRights || this.internalAccessPrivileges;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006284 File Offset: 0x00004484
		public override ClientType ClientType
		{
			get
			{
				if (base.ClientType != ClientType.User)
				{
					return base.ClientType;
				}
				if (this.logon != null)
				{
					if (this.logon.SystemRights)
					{
						return ClientType.System;
					}
					if (this.logon.AdminRights)
					{
						return ClientType.Administrator;
					}
				}
				if (this.Session != null && this.Session.UsingTransportPrivilege)
				{
					return ClientType.Transport;
				}
				return base.ClientType;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000062E4 File Offset: 0x000044E4
		public override IMailboxContext PrimaryMailboxContext
		{
			get
			{
				if (this.logon == null)
				{
					return null;
				}
				return this.logon.MapiMailbox.StoreMailbox;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006300 File Offset: 0x00004500
		public static MapiContext Create(ExecutionDiagnostics executionDiagnostics)
		{
			return new MapiContext(executionDiagnostics, Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.ProcessSecurityContext, ClientType.System, CultureHelper.GetLcidFromCulture(CultureHelper.DefaultCultureInfo));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006318 File Offset: 0x00004518
		public static MapiContext Create(ExecutionDiagnostics executionDiagnostics, ClientType clientType)
		{
			return new MapiContext(executionDiagnostics, Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.ProcessSecurityContext, clientType, CultureHelper.GetLcidFromCulture(CultureHelper.DefaultCultureInfo));
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006330 File Offset: 0x00004530
		internal static MapiContext CreateSessionless(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, int lcid)
		{
			return new MapiContext(executionDiagnostics, securityContext, clientType, lcid);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000633B File Offset: 0x0000453B
		internal MapiContext.MapiLogonFrame SetMapiLogonForNotificationContext(MapiLogon logon)
		{
			return new MapiContext.MapiLogonFrame(this, logon);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006344 File Offset: 0x00004544
		public void Configure(MapiSession session)
		{
			this.Configure(session.SessionSecurityContext, session.InternalClientType, session.LcidSort);
			base.UpdateTestCaseId(session.TestCaseId);
			this.session = session;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006371 File Offset: 0x00004571
		internal void Configure(ClientSecurityContext securityContext, ClientType clientType, int lcid)
		{
			base.SetUserInfo(securityContext, clientType, CultureHelper.TranslateLcid(lcid));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006384 File Offset: 0x00004584
		public void Initialize(MapiLogon logon, bool sharedMailboxLock, bool sharedUserLock)
		{
			this.Initialize(logon.MapiMailbox.Database.MdbGuid, logon.Session.SessionSecurityContext, logon.Session.InternalClientType, (logon.LoggedOnUserAddressInfo == null || (logon.IsSystemServiceLogon && base.TestCaseId.IsNull)) ? Guid.Empty : logon.LoggedOnUserAddressInfo.ObjectId, logon.Session.LcidSort);
			this.logon = logon;
			this.SetCulture();
			base.InitializeMailboxOperation(logon.MapiMailbox.SharedState, ExecutionDiagnostics.OperationSource.Mapi, MapiContext.MailboxLockTimeout, sharedMailboxLock, sharedUserLock);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006420 File Offset: 0x00004620
		public void Initialize(Guid databaseGuid, Guid mailboxGuid, bool sharedMailboxLock, bool sharedUserLock)
		{
			ClientSecurityContext securityContext = this.SecurityContext;
			ClientType clientType = this.ClientType;
			int lcid = CultureHelper.GetLcidFromCulture(base.Culture);
			if (this.session != null)
			{
				securityContext = this.session.SessionSecurityContext;
				clientType = this.session.InternalClientType;
				lcid = this.session.LcidSort;
			}
			this.Initialize(databaseGuid, securityContext, clientType, Guid.Empty, lcid);
			base.InitializeMailboxOperation(mailboxGuid, ExecutionDiagnostics.OperationSource.Mapi, MapiContext.MailboxLockTimeout, sharedMailboxLock, sharedUserLock);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006494 File Offset: 0x00004694
		public void Initialize(Guid databaseGuid, int mailboxNumber, bool sharedMailboxLock, bool sharedUserLock)
		{
			ClientSecurityContext securityContext = this.SecurityContext;
			ClientType clientType = this.ClientType;
			int lcid = CultureHelper.GetLcidFromCulture(base.Culture);
			if (this.session != null)
			{
				securityContext = this.session.SessionSecurityContext;
				clientType = this.session.InternalClientType;
				lcid = this.session.LcidSort;
			}
			this.Initialize(databaseGuid, securityContext, clientType, Guid.Empty, lcid);
			base.InitializeMailboxOperation(mailboxNumber, ExecutionDiagnostics.OperationSource.Mapi, MapiContext.MailboxLockTimeout, sharedMailboxLock, sharedUserLock);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006506 File Offset: 0x00004706
		private void Initialize(Guid databaseGuid, ClientSecurityContext securityContext, ClientType clientType, Guid userIdentity, int lcid)
		{
			base.SetUserInfo(securityContext, clientType, CultureHelper.TranslateLcid(lcid));
			base.Diagnostics.DatabaseGuid = databaseGuid;
			this.databaseGuid = databaseGuid;
			this.logon = null;
			base.UserIdentity = userIdentity;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000653C File Offset: 0x0000473C
		public void SetMapiLogon(MapiLogon logon)
		{
			this.logon = logon;
			base.UserIdentity = ((logon == null || logon.LoggedOnUserAddressInfo == null || (logon.IsSystemServiceLogon && base.TestCaseId.IsNull)) ? Guid.Empty : logon.LoggedOnUserAddressInfo.ObjectId);
			this.SetCulture();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006591 File Offset: 0x00004791
		public MapiContext.MailboxFullRightsFrame GrantMailboxFullRights()
		{
			return new MapiContext.MailboxFullRightsFrame(this);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006599 File Offset: 0x00004799
		public void GrantInternalAccessPrivileges()
		{
			this.internalAccessPrivileges = true;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000065A2 File Offset: 0x000047A2
		public void RevokeInternalAccessPrivileges()
		{
			this.internalAccessPrivileges = false;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000065AB File Offset: 0x000047AB
		public MapiContext.DisableNotificationPumpingFrame DisableNotificationPumping()
		{
			return new MapiContext.DisableNotificationPumpingFrame(this);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000065B4 File Offset: 0x000047B4
		protected override void ConnectDatabase()
		{
			base.ConnectDatabase();
			if (base.IsConnected && this.logon == null)
			{
				return;
			}
			StoreDatabase storeDatabase;
			if (this.logon != null)
			{
				storeDatabase = this.logon.MapiMailbox.Database;
			}
			else
			{
				storeDatabase = Storage.FindDatabase(this.DatabaseGuid);
				if (storeDatabase == null)
				{
					using (base.CriticalBlock((LID)57824U, CriticalBlockScope.MailboxSession))
					{
						throw new StoreException((LID)53048U, ErrorCodeValue.MdbNotInitialized);
					}
				}
			}
			if (base.IsConnected && storeDatabase.MdbGuid != base.Database.MdbGuid)
			{
				base.Disconnect();
			}
			if (!base.IsConnected)
			{
				base.Connect(storeDatabase);
			}
			if (!storeDatabase.IsOnlineActive && !storeDatabase.IsOnlinePassiveAttachedReadOnly)
			{
				base.Disconnect();
				using (base.CriticalBlock((LID)33248U, CriticalBlockScope.MailboxSession))
				{
					throw new StoreException((LID)46904U, ErrorCodeValue.MdbNotInitialized);
				}
			}
			if (this.session != null)
			{
				this.session.LastUsedDatabase = base.Database;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000066EC File Offset: 0x000048EC
		protected override void DisconnectDatabase()
		{
			base.Disconnect();
			base.DisconnectDatabase();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000066FC File Offset: 0x000048FC
		protected override void ConnectMailboxes()
		{
			base.ConnectMailboxes();
			this.SetCulture();
			if (this.logon != null && this.logon.CannotLogonToInTransitMailbox(this))
			{
				using (base.CriticalBlock((LID)49120U, CriticalBlockScope.MailboxSession))
				{
					throw new StoreException((LID)55096U, ErrorCodeValue.MdbNotInitialized);
				}
			}
			using (base.CriticalBlock((LID)65504U, CriticalBlockScope.MailboxSession))
			{
				if (this.session != null)
				{
					this.session.ConnectMailboxes(this);
				}
				if (this.session != null && !this.notificationPumpingDisabled)
				{
					this.session.PumpPendingNotifications(this, base.LockedMailboxState);
				}
				base.EndCriticalBlock();
			}
			if (this.logon != null && this.logon.IsValid)
			{
				this.logon.EstablishQuotaInfo();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000067FC File Offset: 0x000049FC
		public override ErrorCode StartMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck)
		{
			if (base.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
			{
				throw new InvalidOperationException("critical block failure was ignored");
			}
			if (this.session != null)
			{
				this.session.ThrowIfNotValid(null);
			}
			ErrorCode first = base.StartMailboxOperation(mailboxCreation, findRemovedMailbox, skipQuarantineCheck);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)61692U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000685F File Offset: 0x00004A5F
		public override void EndMailboxOperation(bool commit, bool skipDisconnectingDatabase, bool pulseOnly)
		{
			base.EndMailboxOperation(commit, skipDisconnectingDatabase, pulseOnly);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000686C File Offset: 0x00004A6C
		protected override IMailboxContext CreateMailboxContext(int mailboxNumber)
		{
			if (!base.PartitionFullAccessGranted && this.logon != null && !this.logon.UnifiedLogon)
			{
				throw new StoreException((LID)48540U, ErrorCodeValue.NoAccess);
			}
			if (this.session != null)
			{
				return this.session.GetInternallyReferencedMailboxContext(this, mailboxNumber);
			}
			return base.CreateMailboxContext(mailboxNumber);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000068C8 File Offset: 0x00004AC8
		private void SetCulture()
		{
			if (this.logon != null && this.logon.Session.LcidSort == 1024 && this.logon.MapiMailbox.GetLocalized(this) && (this.logon.OpenStoreFlags & OpenStoreFlags.NoLocalization) == OpenStoreFlags.None)
			{
				base.Culture = CultureHelper.TranslateLcid(this.logon.MapiMailbox.Lcid);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006935 File Offset: 0x00004B35
		internal bool IsAssociatedWithMailbox(MapiMailbox mailbox)
		{
			return !(this.DatabaseGuid != mailbox.MdbGuid) && mailbox.MailboxNumber == base.LockedMailboxState.MailboxNumber;
		}

		// Token: 0x040000E6 RID: 230
		private static readonly TimeSpan mailboxLockTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000E7 RID: 231
		private static Func<TimeSpan> mailboxLockTimeoutHook = null;

		// Token: 0x040000E8 RID: 232
		private MapiSession session;

		// Token: 0x040000E9 RID: 233
		private Guid databaseGuid;

		// Token: 0x040000EA RID: 234
		private MapiLogon logon;

		// Token: 0x040000EB RID: 235
		private bool internalAccessPrivileges;

		// Token: 0x040000EC RID: 236
		private bool mailboxFullRightsGranted;

		// Token: 0x040000ED RID: 237
		private bool notificationPumpingDisabled;

		// Token: 0x02000042 RID: 66
		public struct MapiLogonFrame : IDisposable
		{
			// Token: 0x06000132 RID: 306 RVA: 0x00006980 File Offset: 0x00004B80
			internal MapiLogonFrame(MapiContext context, MapiLogon newLogon)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(context.logon == null || context.logon.MapiMailbox.StoreMailbox.SharedState.Equals(newLogon.MapiMailbox.StoreMailbox.SharedState), "Changing mailboxes?");
				this.context = context;
				this.oldLogon = this.context.logon;
				this.context.logon = newLogon;
				this.context.SetCulture();
			}

			// Token: 0x06000133 RID: 307 RVA: 0x000069FB File Offset: 0x00004BFB
			public void Dispose()
			{
				this.context.logon = this.oldLogon;
				this.context.SetCulture();
			}

			// Token: 0x040000EE RID: 238
			private MapiContext context;

			// Token: 0x040000EF RID: 239
			private MapiLogon oldLogon;
		}

		// Token: 0x02000043 RID: 67
		public struct MailboxFullRightsFrame : IDisposable
		{
			// Token: 0x06000134 RID: 308 RVA: 0x00006A19 File Offset: 0x00004C19
			internal MailboxFullRightsFrame(MapiContext context)
			{
				this.previousMailboxFullRightsGranted = context.mailboxFullRightsGranted;
				context.mailboxFullRightsGranted = true;
				this.context = context;
			}

			// Token: 0x06000135 RID: 309 RVA: 0x00006A35 File Offset: 0x00004C35
			public void Dispose()
			{
				this.context.mailboxFullRightsGranted = this.previousMailboxFullRightsGranted;
			}

			// Token: 0x040000F0 RID: 240
			private MapiContext context;

			// Token: 0x040000F1 RID: 241
			private bool previousMailboxFullRightsGranted;
		}

		// Token: 0x02000044 RID: 68
		public struct DisableNotificationPumpingFrame : IDisposable
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00006A48 File Offset: 0x00004C48
			internal DisableNotificationPumpingFrame(MapiContext context)
			{
				this.previousNotificationPumpingDisabled = context.notificationPumpingDisabled;
				context.notificationPumpingDisabled = true;
				this.context = context;
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00006A64 File Offset: 0x00004C64
			public void Dispose()
			{
				this.context.notificationPumpingDisabled = this.previousNotificationPumpingDisabled;
			}

			// Token: 0x040000F2 RID: 242
			private MapiContext context;

			// Token: 0x040000F3 RID: 243
			private bool previousNotificationPumpingDisabled;
		}
	}
}
