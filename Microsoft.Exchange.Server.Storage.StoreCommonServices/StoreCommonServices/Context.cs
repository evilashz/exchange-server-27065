using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000031 RID: 49
	public class Context : DisposableBase, ICriticalBlockFailureHandler, IDatabaseExecutionContext, IExecutionContext, IContextProvider, IConnectionProvider
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0001456C File Offset: 0x0001276C
		internal static void Initialize()
		{
			Context.maximumMailboxLockWaitingCount = Hookable<int>.Create(false, ConfigurationSchema.MailboxLockMaximumWaitCount.Value);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00014584 File Offset: 0x00012784
		protected Context()
		{
			this.executionDiagnostics = null;
			this.securityContext = null;
			this.ownSecurityContext = false;
			this.clientType = ClientType.System;
			this.culture = CultureHelper.DefaultCultureInfo;
			this.PerfInstance = null;
			this.testCaseId = TestCaseId.GetInProcessTestCaseId();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000145DB File Offset: 0x000127DB
		protected Context(ExecutionDiagnostics executionDiagnostics) : this(executionDiagnostics, Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.ProcessSecurityContext, false, ClientType.System, CultureHelper.DefaultCultureInfo)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000145F0 File Offset: 0x000127F0
		internal Context(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture) : this(executionDiagnostics, securityContext, false, clientType, culture)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00014600 File Offset: 0x00012800
		internal Context(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, bool ownSecurityContext, ClientType clientType, CultureInfo culture)
		{
			this.executionDiagnostics = executionDiagnostics;
			this.securityContext = securityContext;
			this.ownSecurityContext = ownSecurityContext;
			this.UpdateClientType(clientType);
			this.culture = culture;
			this.PerfInstance = null;
			this.testCaseId = TestCaseId.GetInProcessTestCaseId();
			if (ExTraceGlobals.ContextTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ContextTracer.TraceDebug(0L, "Context:Context(): Context created");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00014673 File Offset: 0x00012873
		public Context CurrentContext
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00014676 File Offset: 0x00012876
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0001467E File Offset: 0x0001287E
		public Guid UserIdentity { get; internal set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00014687 File Offset: 0x00012887
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0001468E File Offset: 0x0001288E
		internal static int ThreadIdCriticalDatabaseFailure
		{
			get
			{
				return Context.threadIdCriticalDatabaseFailure;
			}
			set
			{
				Context.threadIdCriticalDatabaseFailure = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00014698 File Offset: 0x00012898
		public bool TransactionStarted
		{
			get
			{
				return this.DatabaseTransactionStarted || (this.affectedObjects != null && this.affectedObjects.Count != 0) || (this.uncommittedNotifications != null && this.uncommittedNotifications.Count != 0) || (this.uncommittedTimedEvents != null && this.uncommittedTimedEvents.Count != 0);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000146F4 File Offset: 0x000128F4
		public bool DatabaseTransactionStarted
		{
			get
			{
				return this.connection != null && this.connection.TransactionStarted;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0001470B File Offset: 0x0001290B
		public bool IsConnected
		{
			get
			{
				return this.database != null;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00014719 File Offset: 0x00012919
		Database IConnectionProvider.Database
		{
			get
			{
				return this.database.PhysicalDatabase;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00014726 File Offset: 0x00012926
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0001472E File Offset: 0x0001292E
		public virtual ClientSecurityContext SecurityContext
		{
			get
			{
				return this.securityContext;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00014736 File Offset: 0x00012936
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0001473E File Offset: 0x0001293E
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00014747 File Offset: 0x00012947
		public virtual ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0001474F File Offset: 0x0001294F
		public TestCaseId TestCaseId
		{
			get
			{
				return this.testCaseId;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00014757 File Offset: 0x00012957
		public bool IsMailboxOperationStarted
		{
			get
			{
				return this.isMailboxOperationStarted;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0001475F File Offset: 0x0001295F
		public bool CriticalDatabaseFailureDetected
		{
			get
			{
				return this.criticalDatabaseFailureDetected;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00014767 File Offset: 0x00012967
		public bool DatabaseFailureDetected
		{
			get
			{
				return this.databaseFailureDetected;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0001476F File Offset: 0x0001296F
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00014777 File Offset: 0x00012977
		public object EventHistoryUncommittedTransactionLink { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00014780 File Offset: 0x00012980
		public MailboxState LockedMailboxState
		{
			get
			{
				return this.lockedMailboxState;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00014788 File Offset: 0x00012988
		public virtual IMailboxContext PrimaryMailboxContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0001478B File Offset: 0x0001298B
		public DatabaseType DatabaseType
		{
			get
			{
				return this.Database.PhysicalDatabase.DatabaseType;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0001479D File Offset: 0x0001299D
		public bool IsAnyCriticalBlockFailed
		{
			get
			{
				return this.highestFailedCriticalBlockScope != CriticalBlockScope.None;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000147AB File Offset: 0x000129AB
		public CriticalBlockScope HighestFailedCriticalBlockScope
		{
			get
			{
				return this.highestFailedCriticalBlockScope;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000147B3 File Offset: 0x000129B3
		public ExecutionDiagnostics Diagnostics
		{
			get
			{
				return this.executionDiagnostics;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000147BB File Offset: 0x000129BB
		IExecutionDiagnostics IExecutionContext.Diagnostics
		{
			get
			{
				return this.executionDiagnostics;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000147C3 File Offset: 0x000129C3
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000147D8 File Offset: 0x000129D8
		public bool SkipDatabaseLogsFlush
		{
			get
			{
				return this.skipDatabaseLogsFlush && this.connectionLevel == 0;
			}
			set
			{
				this.skipDatabaseLogsFlush = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000147E1 File Offset: 0x000129E1
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000147E9 File Offset: 0x000129E9
		public bool ForceNotificationPublish
		{
			get
			{
				return this.forceNotificationPublish;
			}
			set
			{
				this.forceNotificationPublish = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000147F2 File Offset: 0x000129F2
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000147FA File Offset: 0x000129FA
		internal StorePerDatabasePerformanceCountersInstance PerfInstance { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00014803 File Offset: 0x00012A03
		public bool IsSharedMailboxOperation
		{
			get
			{
				return this.isSharedMailboxOperation;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0001480B File Offset: 0x00012A0B
		public bool IsSharedUserOperation
		{
			get
			{
				return this.isSharedUserOperation;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00014813 File Offset: 0x00012A13
		protected bool PartitionFullAccessGranted
		{
			get
			{
				return this.partitionFullAccessGranted;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0001481B File Offset: 0x00012A1B
		private Stack<LockableMailboxComponent> CurrentLockableMailboxComponents
		{
			get
			{
				if (this.currentLockableMailboxComponents == null)
				{
					this.currentLockableMailboxComponents = new Stack<LockableMailboxComponent>(5);
				}
				return this.currentLockableMailboxComponents;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00014837 File Offset: 0x00012A37
		public static IDisposable SetStaticCommitTestHook(Action action)
		{
			return Context.staticCommitTestHook.SetTestHook(action);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00014844 File Offset: 0x00012A44
		public static IDisposable SetStaticAbortTestHook(Action action)
		{
			return Context.staticAbortTestHook.SetTestHook(action);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00014851 File Offset: 0x00012A51
		public static IDisposable SetRiseNotificationTestHook(Action<NotificationEvent> action)
		{
			return Context.riseNotificationTestHook.SetTestHook(action);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001485E File Offset: 0x00012A5E
		public static IDisposable SetStartMailboxOperationTestHook(Action<Context> action)
		{
			return Context.startMailboxOperationTestHook.SetTestHook(action);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001486B File Offset: 0x00012A6B
		public static IDisposable SetEndMailboxOperationTestHook(Action<Context, bool> action)
		{
			return Context.endMailboxOperationTestHook.SetTestHook(action);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00014878 File Offset: 0x00012A78
		public static IDisposable SetPulseOperationTestHook(Action<bool> action)
		{
			return Context.pulseOperationTestHook.SetTestHook(action);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00014885 File Offset: 0x00012A85
		public static Context CreateForSystem()
		{
			return Context.CreateForSystem(new ExecutionDiagnostics());
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00014891 File Offset: 0x00012A91
		public static Context CreateForSystem(ExecutionDiagnostics executionDiagnostics)
		{
			return Context.Create(executionDiagnostics, Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.ProcessSecurityContext, ClientType.System, CultureHelper.DefaultCultureInfo);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000148A4 File Offset: 0x00012AA4
		internal static Context Create(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture)
		{
			return new Context(executionDiagnostics, securityContext, clientType, culture);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000148B0 File Offset: 0x00012AB0
		internal static TimeSpan GetMailboxLockTimeout(MailboxState mailboxState, TimeSpan desiredTimeout)
		{
			if (mailboxState == null || desiredTimeout == LockManager.InfiniteTimeout || desiredTimeout == TimeSpan.Zero)
			{
				return desiredTimeout;
			}
			int waitingCount = LockManager.GetWaitingCount(mailboxState);
			if (waitingCount >= Context.maximumMailboxLockWaitingCount.Value)
			{
				DiagnosticContext.TraceDword((LID)53152U, (uint)waitingCount);
				return TimeSpan.Zero;
			}
			return desiredTimeout;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00014907 File Offset: 0x00012B07
		internal static IDisposable SetMaximumMailboxLockWaitingCount(int waitCount)
		{
			return Context.maximumMailboxLockWaitingCount.SetTestHook(waitCount);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00014914 File Offset: 0x00012B14
		[Conditional("DEBUG")]
		public void AssertUserLocked()
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00014916 File Offset: 0x00012B16
		[Conditional("DEBUG")]
		public void AssertUserLocked(Context.UserLockCheckFrame.Scope scope)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00014918 File Offset: 0x00012B18
		[Conditional("DEBUG")]
		public static void AssertUserLocked(Guid? userIdentity, IMailboxLockName mailboxLockName)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001491A File Offset: 0x00012B1A
		[Conditional("DEBUG")]
		public static void AssertUserLocked(ILockName userLockName, IMailboxLockName mailboxLockName)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001491C File Offset: 0x00012B1C
		[Conditional("DEBUG")]
		public void AssertUserExclusiveLocked()
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001491E File Offset: 0x00012B1E
		[Conditional("DEBUG")]
		public void AssertUserExclusiveLocked(Context.UserLockCheckFrame.Scope scope)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00014920 File Offset: 0x00012B20
		[Conditional("DEBUG")]
		public static void AssertUserExclusiveLocked(Guid? userIdentity, IMailboxLockName mailboxLockName)
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00014922 File Offset: 0x00012B22
		[Conditional("DEBUG")]
		public static void AssertUserExclusiveLocked(ILockName userLockName, IMailboxLockName mailboxLockName)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00014924 File Offset: 0x00012B24
		public bool IsUserLocked()
		{
			return Context.IsUserLocked(new Guid?(this.UserIdentity), this.lockedMailboxState);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001493C File Offset: 0x00012B3C
		public bool IsUserLocked(Context.UserLockCheckFrame.Scope scope)
		{
			return this.userLockChecker != null && this.userLockChecker.Value.IsScope(scope) && this.userLockChecker.Value.HasAnyLock();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00014984 File Offset: 0x00012B84
		public static bool IsUserLocked(Guid? userIdentity, IMailboxLockName mailboxLockName)
		{
			ILockName lockName = null;
			if (mailboxLockName != null && userIdentity != null)
			{
				lockName = new Context.UserLockName(mailboxLockName, userIdentity.Value);
			}
			return Context.IsUserLocked(lockName, mailboxLockName);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000149B4 File Offset: 0x00012BB4
		public static bool IsUserLocked(ILockName userLockName, IMailboxLockName mailboxLockName)
		{
			return (userLockName != null && (LockManager.TestLock(userLockName, LockManager.LockType.UserShared) || LockManager.TestLock(userLockName, LockManager.LockType.UserExclusive))) || (mailboxLockName != null && LockManager.TestLock(mailboxLockName, LockManager.LockType.MailboxExclusive));
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000149DC File Offset: 0x00012BDC
		public bool IsUserExclusiveLocked()
		{
			return Context.IsUserExclusiveLocked(new Guid?(this.UserIdentity), this.lockedMailboxState);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000149F4 File Offset: 0x00012BF4
		public bool IsUserExclusiveLocked(Context.UserLockCheckFrame.Scope scope)
		{
			return this.userLockChecker != null && this.userLockChecker.Value.IsScope(scope) && this.userLockChecker.Value.HasExclusiveLock();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00014A3C File Offset: 0x00012C3C
		public static bool IsUserExclusiveLocked(Guid? userIdentity, IMailboxLockName mailboxLockName)
		{
			ILockName lockName = null;
			if (mailboxLockName != null && userIdentity != null)
			{
				lockName = new Context.UserLockName(mailboxLockName, userIdentity.Value);
			}
			return Context.IsUserExclusiveLocked(lockName, mailboxLockName);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00014A6C File Offset: 0x00012C6C
		public static bool IsUserExclusiveLocked(ILockName userLockName, IMailboxLockName mailboxLockName)
		{
			return (userLockName != null && LockManager.TestLock(userLockName, LockManager.LockType.UserExclusive)) || (mailboxLockName != null && LockManager.TestLock(mailboxLockName, LockManager.LockType.MailboxExclusive));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00014A8A File Offset: 0x00012C8A
		internal void SetUserInfo(ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture)
		{
			this.securityContext = securityContext;
			this.UpdateClientType(clientType);
			this.culture = culture;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00014AA1 File Offset: 0x00012CA1
		public Context.DatabaseAssociationBlockFrame AssociateWithDatabase(StoreDatabase database)
		{
			return new Context.DatabaseAssociationBlockFrame(this, database, Context.LockKind.Shared);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00014AAB File Offset: 0x00012CAB
		public Context.DatabaseAssociationBlockFrame AssociateWithDatabaseExclusive(StoreDatabase database)
		{
			return new Context.DatabaseAssociationBlockFrame(this, database, Context.LockKind.Exclusive);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00014AB5 File Offset: 0x00012CB5
		public Context.DatabaseAssociationBlockFrame AssociateWithDatabaseNoLock(StoreDatabase database)
		{
			return new Context.DatabaseAssociationBlockFrame(this, database, Context.LockKind.None);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00014ABF File Offset: 0x00012CBF
		public Context.MailboxContextDisAssociationBlockFrameForTest TemporarilyDisassociateMailboxContextForTest()
		{
			return new Context.MailboxContextDisAssociationBlockFrameForTest(this);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00014AC7 File Offset: 0x00012CC7
		public Context.DatabaseDisAssociationBlockFrameForTest TemporarilyDisassociateDatabaseForTest()
		{
			return new Context.DatabaseDisAssociationBlockFrameForTest(this);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00014ACF File Offset: 0x00012CCF
		public void Connect(StoreDatabase database)
		{
			this.Connect(database, Context.LockKind.Shared);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00014AD9 File Offset: 0x00012CD9
		public void ConnectNoLock(StoreDatabase database)
		{
			this.Connect(database, Context.LockKind.None);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00014AE3 File Offset: 0x00012CE3
		public void Disconnect()
		{
			this.Disconnect(Context.LockKind.Shared);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00014AEC File Offset: 0x00012CEC
		public void DisconnectNoLock()
		{
			this.Disconnect(Context.LockKind.None);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00014AF8 File Offset: 0x00012CF8
		private void Connect(StoreDatabase database, Context.LockKind lockKind)
		{
			switch (lockKind)
			{
			case Context.LockKind.Shared:
				database.GetSharedLock(this.Diagnostics);
				break;
			case Context.LockKind.Exclusive:
				database.GetExclusiveLock();
				break;
			}
			this.database = database;
			if (database != null)
			{
				this.PerfInstance = PerformanceCounterFactory.GetDatabaseInstance(database);
				if (database.DatabaseHeaderInfo != null)
				{
					this.Diagnostics.DatabaseRepaired = new bool?(database.DatabaseHeaderInfo.DatabaseRepaired);
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00014B68 File Offset: 0x00012D68
		private void Disconnect(Context.LockKind lockKind)
		{
			try
			{
				this.Abort();
			}
			finally
			{
				try
				{
					if (this.connection != null && !this.criticalDatabaseFailureDetected)
					{
						using (this.CriticalBlock((LID)39392U, CriticalBlockScope.Database))
						{
							this.connection.FlushDatabaseLogs(false);
							this.EndCriticalBlock();
						}
					}
				}
				finally
				{
					if (this.database != null)
					{
						if (this.connection != null)
						{
							this.connection.Dispose();
							this.connection = null;
						}
						switch (lockKind)
						{
						case Context.LockKind.Shared:
							this.database.ReleaseSharedLock();
							break;
						case Context.LockKind.Exclusive:
							this.database.ReleaseExclusiveLock();
							break;
						}
						this.database = null;
					}
					this.PerfInstance = null;
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00014C64 File Offset: 0x00012E64
		public virtual void DismountOnCriticalFailure()
		{
			if (!this.criticalDatabaseFailureDetected)
			{
				return;
			}
			this.SystemCriticalOperation(new TryDelegate(this, (UIntPtr)ldftn(<DismountOnCriticalFailure>b__0)));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00014C81 File Offset: 0x00012E81
		public virtual Connection GetConnection()
		{
			if (this.connection == null && this.database != null)
			{
				this.connection = Factory.CreateConnection(this, this.database.PhysicalDatabase, string.Empty);
			}
			return this.connection;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public void PushConnection()
		{
			if (this.connectionLevel == 0)
			{
				if (this.connection != null)
				{
					this.connection.Suspend();
				}
				this.savedConnection = this.connection;
				this.savedAffectedObjects = this.affectedObjects;
				this.connection = null;
				this.affectedObjects = null;
			}
			this.connectionLevel++;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00014D14 File Offset: 0x00012F14
		public void PopConnection()
		{
			this.connectionLevel--;
			if (this.connectionLevel == 0)
			{
				if (this.connection != null)
				{
					if (this.connection != null && !this.criticalDatabaseFailureDetected)
					{
						using (this.CriticalBlock((LID)43488U, CriticalBlockScope.Database))
						{
							this.connection.FlushDatabaseLogs(false);
							this.EndCriticalBlock();
						}
					}
					this.connection.Dispose();
					this.connection = null;
				}
				this.connection = this.savedConnection;
				this.affectedObjects = this.savedAffectedObjects;
				this.savedConnection = null;
				this.savedAffectedObjects = null;
				if (this.connection != null)
				{
					this.connection.Resume();
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00014DE4 File Offset: 0x00012FE4
		public void BeginTransactionIfNeeded()
		{
			this.GetConnection().BeginTransactionIfNeeded();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public void Commit()
		{
			if (this.TransactionStarted)
			{
				List<NotificationEvent> list = null;
				FaultInjection.InjectFault(Context.staticCommitTestHook);
				if (this.connectionLevel == 0)
				{
					bool flag = false;
					try
					{
						if (this.affectedObjects != null)
						{
							foreach (IStateObject stateObject in this.affectedObjects)
							{
								ExTraceGlobals.FaultInjectionTracer.TraceTest(3435539773U);
								stateObject.OnBeforeCommit(this);
							}
						}
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.NotificationTracer.TraceDebug(58137L, "Dump uncommitted notifications because OnBeforeCommit failed");
							}
							this.uncommittedNotifications = null;
							this.uncommittedTimedEvents = null;
						}
					}
					flag = false;
					try
					{
						list = this.uncommittedNotifications;
						this.uncommittedNotifications = null;
						if (list != null)
						{
							Statistics.ContextNotifications.Max.Bump(list.Count);
							this.PublishNotificationsPreCommit(list);
						}
						List<TimedEventEntry> list2 = this.uncommittedTimedEvents;
						this.uncommittedTimedEvents = null;
						if (list2 != null)
						{
							this.PublishTimedEventsPreCommit(list2);
						}
						flag = true;
					}
					finally
					{
					}
				}
				if (this.DatabaseTransactionStarted)
				{
					using (this.CriticalBlock((LID)59872U, CriticalBlockScope.Database))
					{
						this.AddLogTransactionInformationCommon();
						this.connection.Commit(this.SerializeLogTransactionInformation());
						this.EndCriticalBlock();
					}
				}
				if (this.connectionLevel == 0)
				{
					if (this.affectedObjects != null)
					{
						ICollection<IStateObject> collection = this.affectedObjects;
						this.affectedObjects = null;
						using (this.CriticalBlock((LID)35296U, CriticalBlockScope.MailboxSession))
						{
							foreach (IStateObject stateObject2 in collection)
							{
								stateObject2.OnCommit(this);
							}
							this.EndCriticalBlock();
						}
					}
					if (this.mailboxOperationNotifications == null)
					{
						this.mailboxOperationNotifications = list;
					}
					else if (list != null)
					{
						this.mailboxOperationNotifications.AddRange(list);
					}
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.connection == null || this.connection.NumberOfDirtyObjects == 0, "We still have dirty objects");
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001504C File Offset: 0x0001324C
		public void Abort()
		{
			if (this.TransactionStarted)
			{
				this.Diagnostics.OnTransactionAbort();
				FaultInjection.InjectFault(Context.staticAbortTestHook);
				ICollection<IStateObject> collection = this.affectedObjects;
				this.affectedObjects = null;
				if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.NotificationTracer.TraceDebug(31804L, "Dump uncommited notifications because tx is being aborted");
				}
				this.uncommittedNotifications = null;
				this.uncommittedTimedEvents = null;
				if (this.DatabaseTransactionStarted)
				{
					using (this.CriticalBlock((LID)53728U, CriticalBlockScope.Database))
					{
						this.AddLogTransactionInformationCommon();
						this.connection.Abort(this.SerializeLogTransactionInformation());
						this.EndCriticalBlock();
					}
				}
				if (this.connectionLevel == 0 && collection != null)
				{
					using (this.CriticalBlock((LID)37344U, CriticalBlockScope.MailboxSession))
					{
						foreach (IStateObject stateObject in collection)
						{
							stateObject.OnAbort(this);
						}
						this.EndCriticalBlock();
					}
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.connection == null || this.connection.NumberOfDirtyObjects == 0, "We still have dirty objects");
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000151AC File Offset: 0x000133AC
		public bool IsStateObjectRegistered(IStateObject stateObject)
		{
			if (this.connectionLevel != 0)
			{
				return this.savedAffectedObjects != null && this.savedAffectedObjects.Contains(stateObject);
			}
			return this.affectedObjects != null && this.affectedObjects.Contains(stateObject);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000151E4 File Offset: 0x000133E4
		public void RegisterStateObject(IStateObject stateObject)
		{
			if (this.affectedObjects == null)
			{
				this.affectedObjects = new List<IStateObject>(10);
			}
			else if (this.affectedObjects is List<IStateObject> && this.affectedObjects.Count > 100)
			{
				this.affectedObjects = new HashSet<IStateObject>(this.affectedObjects);
			}
			this.affectedObjects.Add(stateObject);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00015244 File Offset: 0x00013444
		public IStateObject RegisterStateAction(Action<Context> commitAction, Action<Context> abortAction)
		{
			Context.SimpleStateObject simpleStateObject = new Context.SimpleStateObject(commitAction, abortAction);
			this.RegisterStateObject(simpleStateObject);
			return simpleStateObject;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00015261 File Offset: 0x00013461
		public void UnregisterStateObject(IStateObject stateObject)
		{
			if (this.affectedObjects != null)
			{
				this.affectedObjects.Remove(stateObject);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00015278 File Offset: 0x00013478
		public void ResetFailureHistory()
		{
			this.ResetHighestFailedCriticalBlockScope();
			this.Diagnostics.ClearExceptionHistory();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0001528B File Offset: 0x0001348B
		public void SystemCriticalOperation(TryDelegate operation)
		{
			WatsonOnUnhandledException.Guard(this.Diagnostics, operation);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00015299 File Offset: 0x00013499
		public Context.CriticalBlockFrame CriticalBlock(LID lid, ICriticalBlockFailureHandler criticalBlockHandler, CriticalBlockScope scope)
		{
			return new Context.CriticalBlockFrame(lid, this, criticalBlockHandler, scope);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000152A4 File Offset: 0x000134A4
		public Context.CriticalBlockFrame CriticalBlock(LID lid, CriticalBlockScope scope)
		{
			return new Context.CriticalBlockFrame(lid, this, this, scope);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000152AF File Offset: 0x000134AF
		public void EndCriticalBlock()
		{
			this.criticalBlockHandler = null;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000152B8 File Offset: 0x000134B8
		public virtual void OnCriticalBlockFailed(LID lid, CriticalBlockScope criticalBlockScope)
		{
			if (criticalBlockScope > this.highestFailedCriticalBlockScope)
			{
				this.highestFailedCriticalBlockScope = criticalBlockScope;
			}
			if (criticalBlockScope > CriticalBlockScope.MailboxSession)
			{
				this.reportExceptionCaught = true;
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_CriticalBlockFailure, new object[]
				{
					lid,
					criticalBlockScope,
					new StackTrace(true).ToString()
				});
			}
			if (criticalBlockScope == CriticalBlockScope.Database)
			{
				this.Database.PublishHaFailure(FailureTag.UnexpectedDismount);
				this.OnDatabaseFailure(true, lid);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001532B File Offset: 0x0001352B
		protected internal void ResetHighestFailedCriticalBlockScope()
		{
			this.highestFailedCriticalBlockScope = CriticalBlockScope.None;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00015334 File Offset: 0x00013534
		protected void PostConstructionInitialize(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture)
		{
			this.executionDiagnostics = executionDiagnostics;
			this.SetUserInfo(securityContext, clientType, culture);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00015347 File Offset: 0x00013547
		void ICriticalBlockFailureHandler.OnCriticalBlockFailed(LID lid, Context context, CriticalBlockScope criticalBlockScope)
		{
			this.OnCriticalBlockFailed(lid, criticalBlockScope);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00015354 File Offset: 0x00013554
		public void RiseNotificationEvent(NotificationEvent nev)
		{
			if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.NotificationTracer.TraceDebug<NotificationEvent>(46809L, "Queue Notification: {0}", nev);
			}
			if (this.Database.IsRecovery)
			{
				return;
			}
			Statistics.ContextNotifications.Total.Bump();
			if (this.uncommittedNotifications == null)
			{
				this.uncommittedNotifications = new List<NotificationEvent>(8);
			}
			this.uncommittedNotifications.Add(nev);
			if (Context.riseNotificationTestHook.Value != null)
			{
				Context.riseNotificationTestHook.Value(nev);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000153D8 File Offset: 0x000135D8
		public void RaiseTimedEvent(TimedEventEntry timedEvent)
		{
			if (this.uncommittedTimedEvents == null)
			{
				this.uncommittedTimedEvents = new List<TimedEventEntry>();
			}
			this.uncommittedTimedEvents.Add(timedEvent);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000153F9 File Offset: 0x000135F9
		public TOperationData RecordOperation<TOperationData>(IOperationExecutionTrackable operation) where TOperationData : class, IExecutionTrackingData<TOperationData>, new()
		{
			return this.executionDiagnostics.RecordOperation<TOperationData>(operation);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00015408 File Offset: 0x00013608
		public void OnDatabaseFailure(bool isCriticalFailure, LID lid)
		{
			if (!this.IsConnected)
			{
				return;
			}
			this.databaseFailureDetected = true;
			if (this.criticalDatabaseFailureDetected)
			{
				return;
			}
			if (isCriticalFailure)
			{
				this.criticalDatabaseFailureDetected = true;
				this.databaseWithCriticalFailure = this.database;
			}
			if (this.criticalDatabaseFailureDetected && Interlocked.CompareExchange(ref Context.threadIdCriticalDatabaseFailure, Environment.CurrentManagedThreadId, 0) == 0)
			{
				string condensedCallStack = ErrorHelper.GetCondensedCallStack(new StackTrace(1, false).ToString());
				string reason = string.Format("LID {0}: {1}", lid, condensedCallStack);
				this.executionDiagnostics.TryPrequarantineMailbox(reason);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001548E File Offset: 0x0001368E
		public void OnExceptionCatch(Exception exception)
		{
			this.OnExceptionCatch(exception, null);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00015498 File Offset: 0x00013698
		public void OnExceptionCatch(Exception exception, object diagnosticData)
		{
			if (this.reportExceptionCaught)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_UnhandledException, new object[]
				{
					exception
				});
				this.reportExceptionCaught = false;
			}
			this.Diagnostics.OnExceptionCatch(exception, diagnosticData);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000154D8 File Offset: 0x000136D8
		public virtual void OnBeforeTableAccess(Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (!this.IsSharedMailboxOperation)
			{
				return;
			}
			if (operationType == Connection.OperationType.Query && (table.Equals(DatabaseSchema.GlobalsTable(this.Database).Table) || table.Equals(DatabaseSchema.MailboxTable(this.Database).Table) || table.Equals(DatabaseSchema.TimedEventsTable(this.Database).Table)))
			{
				return;
			}
			foreach (LockableMailboxComponent lockableMailboxComponent in this.CurrentLockableMailboxComponents)
			{
				if (lockableMailboxComponent.IsValidTableOperation(this, operationType, table, partitionValues))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Format("Invalid table operation. operationType = {0}, table = {1}, partitionValues = {2}", operationType, table, (partitionValues == null) ? "null" : string.Join<object>(", ", partitionValues)));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000155B4 File Offset: 0x000137B4
		protected virtual void ConnectDatabase()
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000155B6 File Offset: 0x000137B6
		protected virtual void DisconnectDatabase()
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000155B8 File Offset: 0x000137B8
		protected virtual void ConnectMailboxes()
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000155BC File Offset: 0x000137BC
		protected virtual void DisconnectMailboxes(bool pulseOnly)
		{
			Mailbox[] array = this.mailboxContexts.Values.ToArray<Mailbox>();
			foreach (Mailbox mailbox in array)
			{
				if (mailbox.IsConnected)
				{
					mailbox.Disconnect();
				}
			}
			this.RemoveInternalMailboxes();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00015604 File Offset: 0x00013804
		private void SaveMailboxChanges()
		{
			foreach (KeyValuePair<int, Mailbox> keyValuePair in this.mailboxContexts)
			{
				Mailbox value = keyValuePair.Value;
				if (!value.IsDead)
				{
					value.Save(this);
				}
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00015668 File Offset: 0x00013868
		public void InitializeMailboxExclusiveOperation(MailboxState mailboxState, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout)
		{
			this.InitializeMailboxOperation(mailboxState, operationSource, lockTimeout, false, true);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00015675 File Offset: 0x00013875
		public void InitializeMailboxExclusiveOperation(int mailboxNumber, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout)
		{
			this.InitializeMailboxOperation(mailboxNumber, operationSource, lockTimeout, false, true);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00015682 File Offset: 0x00013882
		public void InitializeMailboxExclusiveOperation(Guid mailboxGuid, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout)
		{
			this.InitializeMailboxOperation(mailboxGuid, operationSource, lockTimeout, false, true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001568F File Offset: 0x0001388F
		public void InitializeMailboxOperation(MailboxState mailboxState, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
		{
			this.Diagnostics.MailboxNumber = mailboxState.MailboxNumber;
			this.Diagnostics.MailboxGuid = mailboxState.MailboxGuid;
			this.mailboxOperationParameters = new Context.MailboxOperationParameters(mailboxState, operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000156C5 File Offset: 0x000138C5
		public void InitializeMailboxOperation(int mailboxNumber, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
		{
			this.Diagnostics.MailboxNumber = mailboxNumber;
			this.mailboxOperationParameters = new Context.MailboxOperationParameters(mailboxNumber, operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000156E5 File Offset: 0x000138E5
		public void InitializeMailboxOperation(Guid mailboxGuid, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
		{
			this.Diagnostics.MailboxGuid = mailboxGuid;
			this.mailboxOperationParameters = new Context.MailboxOperationParameters(mailboxGuid, operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00015708 File Offset: 0x00013908
		public ErrorCode StartMailboxOperationForFailureHandling()
		{
			ErrorCode result;
			try
			{
				result = this.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
			}
			catch (StoreException ex)
			{
				this.OnExceptionCatch(ex);
				result = ErrorCode.CreateWithLid((LID)37628U, ex.Error);
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00015758 File Offset: 0x00013958
		public ErrorCode StartMailboxOperation()
		{
			return this.StartMailboxOperation(MailboxCreation.DontAllow, false, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00015767 File Offset: 0x00013967
		public ErrorCode StartMailboxOperation(bool allowMailboxCreation)
		{
			return this.StartMailboxOperation(allowMailboxCreation, false, false);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00015772 File Offset: 0x00013972
		public ErrorCode StartMailboxOperation(bool allowMailboxCreation, bool findRemovedMailbox)
		{
			return this.StartMailboxOperation(allowMailboxCreation, findRemovedMailbox, false);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00015780 File Offset: 0x00013980
		public ErrorCode StartMailboxOperation(bool allowMailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck)
		{
			return this.StartMailboxOperation(allowMailboxCreation ? MailboxCreation.Allow(null) : MailboxCreation.DontAllow, findRemovedMailbox, skipQuarantineCheck);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000157AD File Offset: 0x000139AD
		public virtual ErrorCode StartMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck)
		{
			return this.StartMailboxOperation(mailboxCreation, findRemovedMailbox, skipQuarantineCheck, false);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000157BC File Offset: 0x000139BC
		public virtual ErrorCode StartMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck, bool takeDababaseConnectionOwnership)
		{
			this.ResetHighestFailedCriticalBlockScope();
			bool operationSignalledAsStarted = false;
			ErrorCode noError;
			try
			{
				this.mailboxOperationOwnsDatabaseConnection = (takeDababaseConnectionOwnership || !this.IsConnected);
				if (Context.startMailboxOperationTestHook.Value != null)
				{
					Context.startMailboxOperationTestHook.Value(this);
				}
				this.ConnectDatabase();
				if (this.mailboxOperationParameters.MailboxState != null)
				{
					if (!this.TryLockMailboxForOperation(this.mailboxOperationParameters.MailboxState, this.mailboxOperationParameters.IsSharedMailboxOperation, this.mailboxOperationParameters.LockTimeout))
					{
						return ErrorCode.CreateTimeout((LID)43632U);
					}
					if (this.mailboxOperationParameters.IsSharedMailboxOperation && this.IsSpecialCacheCleanupRequired(this.mailboxOperationParameters.MailboxState))
					{
						this.UnlockMailboxForOperation(this.mailboxOperationParameters.MailboxState);
						if (!this.TryLockMailboxForOperation(this.mailboxOperationParameters.MailboxState, false, this.mailboxOperationParameters.LockTimeout))
						{
							return ErrorCode.CreateTimeout((LID)36768U);
						}
					}
					this.lockedMailboxState = this.mailboxOperationParameters.MailboxState;
				}
				else if (this.mailboxOperationParameters.MailboxNumber != null)
				{
					bool flag;
					if (!this.TryLockMailboxForOperation(this.mailboxOperationParameters.MailboxNumber.Value, this.mailboxOperationParameters.IsSharedMailboxOperation, this.mailboxOperationParameters.LockTimeout, out flag, out this.lockedMailboxState))
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.lockedMailboxState == null, "Got unlocked MailboxState?");
						if (flag)
						{
							return ErrorCode.CreateTimeout((LID)60016U);
						}
						return ErrorCode.CreateNotFound((LID)51640U);
					}
				}
				else if (this.mailboxOperationParameters.MailboxGuid != null)
				{
					ErrorCode first = this.LockMailboxByGuidForOperation(this.mailboxOperationParameters.MailboxGuid.Value, mailboxCreation, findRemovedMailbox, this.mailboxOperationParameters.IsSharedMailboxOperation, this.mailboxOperationParameters.LockTimeout, out this.lockedMailboxState);
					if (first != ErrorCode.NoError)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.lockedMailboxState == null, "Got unlocked MailboxState?");
						return first.Propagate((LID)57721U);
					}
				}
				else
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "No MailboxState, no mailboxGuid, no mailboxNumber?");
				}
				if (!this.lockedMailboxState.IsValid)
				{
					using (this.CriticalBlock((LID)49632U, CriticalBlockScope.MailboxSession))
					{
						throw new StoreException((LID)63288U, ErrorCodeValue.MdbNotInitialized);
					}
				}
				if (this.isSharedMailboxOperation && !this.TryLockCurrentUser(this.lockedMailboxState, this.mailboxOperationParameters.IsSharedUserOperation, this.mailboxOperationParameters.LockTimeout, this.Diagnostics))
				{
					throw new StoreException((LID)63388U, ErrorCodeValue.Timeout);
				}
				this.SignalStartMailboxOperation(this.lockedMailboxState, this.mailboxOperationParameters.OperationSource);
				operationSignalledAsStarted = true;
				if (!skipQuarantineCheck && this.LockedMailboxState.Quarantined)
				{
					bool flag2 = false;
					DiagnosticContext.TraceDword((LID)32848U, (uint)this.ClientType);
					if (this.ClientType == ClientType.Migration || this.ClientType == ClientType.PublicFolderSystem)
					{
						flag2 = MailboxQuarantineProvider.Instance.IsMigrationAccessAllowed(this.LockedMailboxState.DatabaseGuid, this.LockedMailboxState.MailboxGuid);
					}
					if (!flag2)
					{
						throw new StoreException((LID)61416U, ErrorCodeValue.MailboxQuarantined);
					}
				}
				this.ConnectMailboxes();
				this.isMailboxOperationStarted = true;
				Mailbox storeMailbox;
				if (!this.IsSharedMailboxOperation && this.mailboxContexts.TryGetValue(this.lockedMailboxState.MailboxNumber, out storeMailbox))
				{
					bool flag3 = false;
					try
					{
						LazyMailboxActionList.ExecuteMailboxActions(this, storeMailbox);
						flag3 = true;
					}
					finally
					{
						this.isMailboxOperationStarted = flag3;
					}
				}
				noError = ErrorCode.NoError;
			}
			finally
			{
				if (!this.isMailboxOperationStarted)
				{
					this.UnwindMailboxOperation(true, operationSignalledAsStarted, false, false);
				}
			}
			return noError;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00015BD4 File Offset: 0x00013DD4
		public void EndMailboxOperation(bool commit)
		{
			this.EndMailboxOperation(commit, false, false);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00015BDF File Offset: 0x00013DDF
		public void EndMailboxOperation(bool commit, bool skipDisconnectingDatabase)
		{
			this.EndMailboxOperation(commit, skipDisconnectingDatabase, false);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00015BEC File Offset: 0x00013DEC
		public virtual void EndMailboxOperation(bool commit, bool skipDisconnectingDatabase, bool pulseOnly)
		{
			bool flag = false;
			try
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2361797949U);
				if (Context.endMailboxOperationTestHook.Value != null)
				{
					Context.endMailboxOperationTestHook.Value(this, commit);
				}
				if (commit)
				{
					if (this.IsAnyCriticalBlockFailed)
					{
						throw new InvalidOperationException("We should not be committing a transaction if any critical block has failed.");
					}
					this.SaveMailboxChanges();
					this.Commit();
					flag = true;
				}
			}
			finally
			{
				this.UnwindMailboxOperation(!flag, true, skipDisconnectingDatabase, pulseOnly);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00015C6C File Offset: 0x00013E6C
		private void UnwindMailboxOperation(bool doAbort, bool operationSignalledAsStarted, bool skipDisconnectingDatabase, bool pulseOnly)
		{
			try
			{
				if (this.IsConnected)
				{
					try
					{
						if (doAbort)
						{
							using (this.CriticalBlock((LID)45024U, CriticalBlockScope.MailboxSession))
							{
								this.Abort();
								this.EndCriticalBlock();
							}
						}
					}
					finally
					{
						try
						{
							this.PublishAllPostCommitNotifications();
						}
						finally
						{
							this.DisconnectMailboxes(pulseOnly);
						}
					}
				}
			}
			finally
			{
				using (this.CriticalBlock((LID)49404U, CriticalBlockScope.MailboxSession))
				{
					try
					{
						try
						{
							if (this.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxShared)
							{
								MailboxStateCache.ResetMailboxState(this, this.lockedMailboxState, this.lockedMailboxState.Status);
							}
						}
						finally
						{
							if (this.lockedMailboxState != null)
							{
								try
								{
									if (operationSignalledAsStarted)
									{
										this.SignalBeforeEndMailboxOperation(this.lockedMailboxState.MailboxNumber);
									}
								}
								finally
								{
									try
									{
										try
										{
											this.UnlockCurrentUser();
										}
										finally
										{
											this.UnlockMailboxForOperation(this.lockedMailboxState);
											this.lockedMailboxState = null;
										}
									}
									finally
									{
										if (operationSignalledAsStarted)
										{
											this.SignalAfterEndMailboxOperation();
										}
									}
								}
							}
						}
					}
					finally
					{
						try
						{
							if (this.mailboxOperationOwnsDatabaseConnection && (!skipDisconnectingDatabase || this.Database.HasExclusiveLockContention()))
							{
								this.DisconnectDatabase();
								this.mailboxOperationOwnsDatabaseConnection = false;
							}
						}
						finally
						{
							this.mailboxOperationOwnsDatabaseConnection = false;
							this.isMailboxOperationStarted = false;
						}
					}
					this.EndCriticalBlock();
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00015E20 File Offset: 0x00014020
		private void PublishAllPostCommitNotifications()
		{
			bool flag = false;
			bool flag2 = this.skipDatabaseLogsFlush;
			this.skipDatabaseLogsFlush = true;
			try
			{
				List<NotificationEvent> list = this.mailboxOperationNotifications;
				this.mailboxOperationNotifications = null;
				if (list != null)
				{
					using (this.CriticalBlock((LID)51680U, CriticalBlockScope.MailboxSession))
					{
						this.PublishNotificationsPostCommit(list);
						this.EndCriticalBlock();
					}
				}
				flag = true;
			}
			finally
			{
				try
				{
					if (this.TransactionStarted)
					{
						ICollection<IStateObject> collection = this.affectedObjects;
						this.affectedObjects = null;
						if (this.DatabaseTransactionStarted)
						{
							Statistics.MiscelaneousNotifications.NotificationHandlingRestartedTransaction.Bump();
							using (this.CriticalBlock((LID)45536U, CriticalBlockScope.Database))
							{
								if (!flag)
								{
									this.AddLogTransactionInformationCommon();
									this.connection.Abort(this.SerializeLogTransactionInformation());
								}
								else
								{
									this.AddLogTransactionInformationCommon();
									this.connection.Commit(this.SerializeLogTransactionInformation());
								}
								this.EndCriticalBlock();
							}
						}
						if (collection != null)
						{
							using (this.CriticalBlock((LID)61920U, CriticalBlockScope.MailboxSession))
							{
								foreach (IStateObject stateObject in collection)
								{
									if (!flag)
									{
										stateObject.OnAbort(this);
									}
									else
									{
										stateObject.OnCommit(this);
									}
								}
								this.EndCriticalBlock();
							}
						}
					}
				}
				finally
				{
					this.skipDatabaseLogsFlush = flag2;
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00015FD0 File Offset: 0x000141D0
		public ErrorCode PulseMailboxOperation()
		{
			return this.PulseMailboxOperation(MailboxCreation.DontAllow, false, null);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00015FDF File Offset: 0x000141DF
		public ErrorCode PulseMailboxOperation(Action actionOutsideMailboxLock)
		{
			return this.PulseMailboxOperation(MailboxCreation.DontAllow, false, actionOutsideMailboxLock);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00015FEE File Offset: 0x000141EE
		public ErrorCode PulseMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, Action actionOutsideMailboxLock)
		{
			return this.PulseMailboxOperation(mailboxCreation, findRemovedMailbox, false, actionOutsideMailboxLock);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00015FFC File Offset: 0x000141FC
		public ErrorCode PulseMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck, Action actionOutsideMailboxLock)
		{
			bool flag = 0 != LockManager.GetWaitingCount(this.lockedMailboxState);
			bool flag2 = this.Database.HasExclusiveLockContention();
			bool flag3 = this.mailboxOperationOwnsDatabaseConnection;
			this.EndMailboxOperation(true, true, true);
			if (flag2)
			{
				return ErrorCode.CreateMdbNotInitialized((LID)46028U);
			}
			if (actionOutsideMailboxLock != null)
			{
				actionOutsideMailboxLock();
			}
			if (Context.pulseOperationTestHook.Value != null)
			{
				Context.pulseOperationTestHook.Value(flag);
			}
			if (flag)
			{
				Thread.Sleep(1);
			}
			ErrorCode result = this.StartMailboxOperation(mailboxCreation, findRemovedMailbox, skipQuarantineCheck);
			this.mailboxOperationOwnsDatabaseConnection = flag3;
			return result;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0001608C File Offset: 0x0001428C
		public bool HasMailboxLockContention
		{
			get
			{
				return LockManager.HasContention(this.lockedMailboxState);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00016099 File Offset: 0x00014299
		internal void SetMailboxStateForTest(MailboxState mailboxState)
		{
			this.lockedMailboxState = mailboxState;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000160B8 File Offset: 0x000142B8
		private ErrorCode LockMailboxByGuidForOperation(Guid mailboxGuid, MailboxCreation mailboxCreation, bool findRemovedMailbox, bool sharedLock, TimeSpan timeout, out MailboxState lockedMailboxState)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.TransactionStarted, "Transaction leaked.");
			bool flag;
			if (!MailboxStateCache.TryGetByGuidLocked(this, mailboxGuid, mailboxCreation, findRemovedMailbox, sharedLock, (MailboxState state) => Context.GetMailboxLockTimeout(state, timeout), this.Diagnostics, out flag, out lockedMailboxState) && flag)
			{
				return ErrorCode.CreateTimeout((LID)35440U);
			}
			if (lockedMailboxState == null)
			{
				return ErrorCode.CreateNotFound((LID)61208U);
			}
			this.isSharedMailboxOperation = sharedLock;
			return ErrorCode.NoError;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00016158 File Offset: 0x00014358
		private bool TryLockMailboxForOperation(int mailboxNumber, bool sharedLock, TimeSpan timeout, out bool timeoutReached, out MailboxState lockedMailboxState)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.TransactionStarted, "Transaction leaked.");
			bool flag = MailboxStateCache.TryGetLocked(this, mailboxNumber, sharedLock, (MailboxState state) => Context.GetMailboxLockTimeout(state, timeout), this.Diagnostics, out timeoutReached, out lockedMailboxState);
			if (flag)
			{
				this.isSharedMailboxOperation = sharedLock;
			}
			return flag;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000161AF File Offset: 0x000143AF
		protected void LockMailboxForOperation(MailboxState mailboxState, bool sharedLock)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.TransactionStarted, "Transaction leaked.");
			this.TryLockMailboxForOperation(mailboxState, sharedLock, LockManager.InfiniteTimeout);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000161D4 File Offset: 0x000143D4
		private bool TryLockMailboxForOperation(MailboxState mailboxState, bool sharedLock, TimeSpan timeout)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.TransactionStarted, "Transaction leaked.");
			bool flag = mailboxState.TryGetMailboxLock(sharedLock, Context.GetMailboxLockTimeout(mailboxState, timeout), this.Diagnostics);
			if (flag)
			{
				this.isSharedMailboxOperation = sharedLock;
			}
			return flag;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00016214 File Offset: 0x00014414
		protected void UnlockMailboxForOperation(MailboxState mailboxState)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.TransactionStarted, "Transaction leaked.");
			mailboxState.ReleaseMailboxLock(this.IsSharedMailboxOperation);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00016235 File Offset: 0x00014435
		public MailboxComponentOperationFrame MailboxComponentReadOperation(LockableMailboxComponent component)
		{
			return MailboxComponentOperationFrame.CreateRead(this, component);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001623E File Offset: 0x0001443E
		public MailboxComponentOperationFrame MailboxComponentWriteOperation(LockableMailboxComponent component)
		{
			return MailboxComponentOperationFrame.CreateWrite(this, component);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00016247 File Offset: 0x00014447
		public void StartMailboxComponentReadOperation(LockableMailboxComponent component)
		{
			component.LockShared(this.Diagnostics);
			this.CurrentLockableMailboxComponents.Push(component);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00016261 File Offset: 0x00014461
		public void EndMailboxComponentReadOperation(LockableMailboxComponent component)
		{
			this.CurrentLockableMailboxComponents.Pop();
			component.ReleaseShared();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00016275 File Offset: 0x00014475
		public void StartMailboxComponentWriteOperation(LockableMailboxComponent component)
		{
			component.LockExclusive(this.Diagnostics);
			this.CurrentLockableMailboxComponents.Push(component);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00016290 File Offset: 0x00014490
		public void EndMailboxComponentWriteOperation(LockableMailboxComponent component, bool success)
		{
			bool flag = false;
			try
			{
				if (this.IsSharedMailboxOperation && component.Committable && success)
				{
					this.Commit();
					flag = true;
				}
			}
			finally
			{
				if (this.IsSharedMailboxOperation && component.Committable && (!success || !flag))
				{
					this.Abort();
				}
				this.CurrentLockableMailboxComponents.Pop();
				component.ReleaseExclusive();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000162FC File Offset: 0x000144FC
		public Context.PartitionFullAccessFrame GrantPartitionFullAccess()
		{
			return new Context.PartitionFullAccessFrame(this);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00016304 File Offset: 0x00014504
		public void UpdateClientType(ClientType clientType)
		{
			this.clientType = clientType;
			this.executionDiagnostics.UpdateClientType(clientType);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00016319 File Offset: 0x00014519
		public void UpdateTestCaseId(TestCaseId testCaseId)
		{
			if (testCaseId.IsNull)
			{
				return;
			}
			this.testCaseId = testCaseId;
			if (this.executionDiagnostics != null)
			{
				this.executionDiagnostics.UpdateTestCaseId(testCaseId);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00016340 File Offset: 0x00014540
		public Context.UserIdentityFrame CreateUserIdentityFrame(Guid newUserIdentity)
		{
			return new Context.UserIdentityFrame(this, newUserIdentity);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001634C File Offset: 0x0001454C
		private bool TryLockCurrentUser(IMailboxLockName mailboxLockName, bool shared, TimeSpan timeout, ILockStatistics lockStats)
		{
			if (!(this.UserIdentity != Guid.Empty))
			{
				return true;
			}
			this.userLockName = new Context.UserLockName(mailboxLockName, this.UserIdentity);
			LockManager.LockType lockType = shared ? LockManager.LockType.UserShared : LockManager.LockType.UserExclusive;
			if (LockManager.TryGetLock(this.userLockName, lockType, timeout, lockStats))
			{
				this.isSharedUserOperation = shared;
				return true;
			}
			this.userLockName = null;
			return false;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000163AC File Offset: 0x000145AC
		private void UnlockCurrentUser()
		{
			if (this.userLockName != null)
			{
				LockManager.LockType lockType = this.isSharedUserOperation ? LockManager.LockType.UserShared : LockManager.LockType.UserExclusive;
				LockManager.ReleaseLock(this.userLockName, lockType);
				this.userLockName = null;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000163E3 File Offset: 0x000145E3
		internal void RegisterMailboxContext(Mailbox mailbox)
		{
			this.mailboxContexts.Add(mailbox.MailboxNumber, mailbox);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000163F7 File Offset: 0x000145F7
		internal void UnregisterMailboxContext(Mailbox mailbox)
		{
			this.mailboxContexts.Remove(mailbox.MailboxNumber);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001640C File Offset: 0x0001460C
		public IMailboxContext GetMailboxContext(int mailboxNumber)
		{
			Mailbox result;
			if (this.mailboxContexts.TryGetValue(mailboxNumber, out result))
			{
				return result;
			}
			IMailboxContext mailboxContext = this.CreateMailboxContext(mailboxNumber);
			Mailbox mailbox = mailboxContext as Mailbox;
			return mailboxContext;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001643C File Offset: 0x0001463C
		protected virtual IMailboxContext CreateMailboxContext(int mailboxNumber)
		{
			if (this.internalMailboxes == null)
			{
				this.internalMailboxes = new List<Mailbox>();
			}
			MailboxState mailboxState = MailboxStateCache.Get(this, mailboxNumber);
			if (mailboxState.Quarantined)
			{
				throw new StoreException((LID)63740U, ErrorCodeValue.MailboxQuarantined);
			}
			if (!mailboxState.IsAccessible)
			{
				throw new StoreException((LID)51452U, ErrorCodeValue.UnexpectedMailboxState);
			}
			Mailbox mailbox = Mailbox.OpenMailbox(this, mailboxState);
			if (mailbox == null)
			{
				throw new StoreException((LID)41212U, ErrorCodeValue.UnexpectedMailboxState);
			}
			this.internalMailboxes.Add(mailbox);
			return mailbox;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000164CC File Offset: 0x000146CC
		private void RemoveInternalMailboxes()
		{
			if (this.internalMailboxes == null)
			{
				return;
			}
			List<Mailbox> list = this.internalMailboxes;
			this.internalMailboxes = null;
			foreach (Mailbox mailbox in list)
			{
				mailbox.Dispose();
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00016530 File Offset: 0x00014730
		private bool IsSpecialCacheCleanupRequired(MailboxState mailboxState)
		{
			SharedObjectPropertyBagDataCache cacheForMailbox = SharedObjectPropertyBagDataCache.GetCacheForMailbox(mailboxState);
			return cacheForMailbox != null && cacheForMailbox.IsCleanupRequired();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00016550 File Offset: 0x00014750
		private void SignalStartMailboxOperation(MailboxState mailboxState, ExecutionDiagnostics.OperationSource operationSource)
		{
			if (operationSource != ExecutionDiagnostics.OperationSource.MailboxMaintenance && operationSource != ExecutionDiagnostics.OperationSource.MailboxCleanup && operationSource != ExecutionDiagnostics.OperationSource.LogicalIndexCleanup && operationSource != ExecutionDiagnostics.OperationSource.LogicalIndexMaintenanceTableTask && operationSource != ExecutionDiagnostics.OperationSource.MailboxTask)
			{
				MailboxStateCache.OnMailboxActivity(mailboxState);
			}
			if (this.connection != null)
			{
				this.connection.DumpRowStats();
			}
			this.executionDiagnostics.OnStartMailboxOperation(this.Database.MdbGuid, mailboxState.MailboxNumber, mailboxState.MailboxGuid, operationSource, mailboxState.ActivityDigestCollector, RopSummaryCollector.GetRopSummaryCollector(this), this.IsSharedMailboxOperation);
			if (ExTraceGlobals.ContextTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.ContextTracer.TraceInformation(0, 0L, "SignalStartMailboxOperation(mailboxNumber={0}, operationSource={1}), ResultState(MailboxGuid={2}, MailboxStatus={3})", new object[]
				{
					mailboxState.MailboxNumber,
					operationSource,
					mailboxState.MailboxGuid,
					mailboxState.Status
				});
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001661C File Offset: 0x0001481C
		private void SignalBeforeEndMailboxOperation(int mailboxNumber)
		{
			if (this.connection != null)
			{
				this.connection.DumpRowStats();
			}
			try
			{
				this.executionDiagnostics.OnBeforeEndMailboxOperation();
			}
			finally
			{
				if (ExTraceGlobals.ContextTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ContextTracer.TraceInformation<int>(0, 0L, "SignalBeforeEndMailboxOperation(mailboxNumber={0})", mailboxNumber);
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0001667C File Offset: 0x0001487C
		private void SignalAfterEndMailboxOperation()
		{
			this.executionDiagnostics.OnAfterEndMailboxOperation();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001668C File Offset: 0x0001488C
		private void PublishNotificationsPreCommit(IList<NotificationEvent> notifications)
		{
			for (int i = 0; i < notifications.Count; i++)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2965777725U);
				NotificationSubscription.EnumerateSubscriptionsForEvent(NotificationPublishPhase.PreCommit, this, notifications[i], NotificationSubscription.PreCommitPublishCallback);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000166CC File Offset: 0x000148CC
		private void PublishNotificationsPostCommit(IList<NotificationEvent> notifications)
		{
			for (int i = 0; i < notifications.Count; i++)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(4039519549U);
				NotificationSubscription.EnumerateSubscriptionsForEvent(NotificationPublishPhase.PostCommit, this, notifications[i], NotificationSubscription.PostCommitPublishCallback);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001670C File Offset: 0x0001490C
		private void PublishTimedEventsPreCommit(IList<TimedEventEntry> timedEvents)
		{
			if (timedEvents == null || timedEvents.Count == 0)
			{
				return;
			}
			TimedEventsQueue timedEventsQueue = (TimedEventsQueue)this.Database.ComponentData[TimedEventsQueue.TimedEventsQueueSlot];
			for (int i = 0; i < timedEvents.Count; i++)
			{
				timedEventsQueue.InsertTimedEventEntry(this, timedEvents[i]);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00016760 File Offset: 0x00014960
		private void AddLogTransactionInformationCommon()
		{
			ILogTransactionInformation logTransactionInformationBlock = new LogTransactionInformationIdentity(this.executionDiagnostics.MailboxNumber, this.executionDiagnostics.ClientType);
			this.executionDiagnostics.LogTransactionInformationCollector.AddLogTransactionInformationBlock(logTransactionInformationBlock);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001679C File Offset: 0x0001499C
		private byte[] SerializeLogTransactionInformation()
		{
			int num = 0;
			byte[] array = null;
			num = this.Diagnostics.LogTransactionInformationCollector.Serialize(array, num);
			if (num > 0)
			{
				array = new byte[num];
				num = 0;
				num = this.Diagnostics.LogTransactionInformationCollector.Serialize(array, num);
			}
			this.Diagnostics.ResetLogTransactionInformationCollector();
			return array;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000167EC File Offset: 0x000149EC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Context>(this);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000167F4 File Offset: 0x000149F4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.database != null)
				{
					this.DisconnectNoLock();
				}
				if (this.ownSecurityContext && this.securityContext != null)
				{
					this.securityContext.Dispose();
				}
			}
			if (ExTraceGlobals.ContextTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ContextTracer.TraceDebug(0L, "Context:Dispose(): Context disposed");
			}
		}

		// Token: 0x04000210 RID: 528
		private const int AvgNotificationsPerTransaction = 8;

		// Token: 0x04000211 RID: 529
		private const int AvgAffectedObjectsPerTransaction = 10;

		// Token: 0x04000212 RID: 530
		private static Hookable<Action> staticCommitTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000213 RID: 531
		private static Hookable<Action> staticAbortTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000214 RID: 532
		private static Hookable<Action<NotificationEvent>> riseNotificationTestHook = Hookable<Action<NotificationEvent>>.Create(false, null);

		// Token: 0x04000215 RID: 533
		private static Hookable<Action<bool>> pulseOperationTestHook = Hookable<Action<bool>>.Create(true, null);

		// Token: 0x04000216 RID: 534
		private static Hookable<int> maximumMailboxLockWaitingCount;

		// Token: 0x04000217 RID: 535
		private static Hookable<Action<Context>> startMailboxOperationTestHook = Hookable<Action<Context>>.Create(false, null);

		// Token: 0x04000218 RID: 536
		private static Hookable<Action<Context, bool>> endMailboxOperationTestHook = Hookable<Action<Context, bool>>.Create(false, null);

		// Token: 0x04000219 RID: 537
		private static int threadIdCriticalDatabaseFailure = 0;

		// Token: 0x0400021A RID: 538
		private Context.UserLockCheckFrame? userLockChecker;

		// Token: 0x0400021B RID: 539
		private ClientType clientType;

		// Token: 0x0400021C RID: 540
		private TestCaseId testCaseId;

		// Token: 0x0400021D RID: 541
		private ClientSecurityContext securityContext;

		// Token: 0x0400021E RID: 542
		private bool ownSecurityContext;

		// Token: 0x0400021F RID: 543
		private CultureInfo culture;

		// Token: 0x04000220 RID: 544
		private StoreDatabase database;

		// Token: 0x04000221 RID: 545
		private Connection connection;

		// Token: 0x04000222 RID: 546
		private int connectionLevel;

		// Token: 0x04000223 RID: 547
		private Connection savedConnection;

		// Token: 0x04000224 RID: 548
		private List<NotificationEvent> uncommittedNotifications;

		// Token: 0x04000225 RID: 549
		private List<NotificationEvent> mailboxOperationNotifications;

		// Token: 0x04000226 RID: 550
		private ICollection<IStateObject> affectedObjects;

		// Token: 0x04000227 RID: 551
		private ICollection<IStateObject> savedAffectedObjects;

		// Token: 0x04000228 RID: 552
		private ICriticalBlockFailureHandler criticalBlockHandler;

		// Token: 0x04000229 RID: 553
		private CriticalBlockScope criticalBlockScope;

		// Token: 0x0400022A RID: 554
		private CriticalBlockScope highestFailedCriticalBlockScope;

		// Token: 0x0400022B RID: 555
		private ExecutionDiagnostics executionDiagnostics;

		// Token: 0x0400022C RID: 556
		private List<TimedEventEntry> uncommittedTimedEvents;

		// Token: 0x0400022D RID: 557
		private bool criticalDatabaseFailureDetected;

		// Token: 0x0400022E RID: 558
		private bool databaseFailureDetected;

		// Token: 0x0400022F RID: 559
		protected StoreDatabase databaseWithCriticalFailure;

		// Token: 0x04000230 RID: 560
		private bool skipDatabaseLogsFlush;

		// Token: 0x04000231 RID: 561
		private bool forceNotificationPublish;

		// Token: 0x04000232 RID: 562
		private Dictionary<int, Mailbox> mailboxContexts = new Dictionary<int, Mailbox>();

		// Token: 0x04000233 RID: 563
		private List<Mailbox> internalMailboxes;

		// Token: 0x04000234 RID: 564
		private Stack<LockableMailboxComponent> currentLockableMailboxComponents;

		// Token: 0x04000235 RID: 565
		private bool reportExceptionCaught;

		// Token: 0x04000236 RID: 566
		private Context.MailboxOperationParameters mailboxOperationParameters;

		// Token: 0x04000237 RID: 567
		private MailboxState lockedMailboxState;

		// Token: 0x04000238 RID: 568
		private bool isMailboxOperationStarted;

		// Token: 0x04000239 RID: 569
		private bool isSharedMailboxOperation;

		// Token: 0x0400023A RID: 570
		private bool isSharedUserOperation;

		// Token: 0x0400023B RID: 571
		private bool mailboxOperationOwnsDatabaseConnection;

		// Token: 0x0400023C RID: 572
		private bool partitionFullAccessGranted;

		// Token: 0x0400023D RID: 573
		private ILockName userLockName;

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x06000288 RID: 648
		public delegate void EndTransactionHandler(Context context, bool committed);

		// Token: 0x02000033 RID: 51
		public struct UserIdentityFrame : IDisposable
		{
			// Token: 0x0600028B RID: 651 RVA: 0x000168A7 File Offset: 0x00014AA7
			public UserIdentityFrame(Context context, Guid newUserIdentity)
			{
				this.context = context;
				this.originalUserIdentity = context.UserIdentity;
				context.UserIdentity = newUserIdentity;
			}

			// Token: 0x0600028C RID: 652 RVA: 0x000168C3 File Offset: 0x00014AC3
			public void Dispose()
			{
				this.context.UserIdentity = this.originalUserIdentity;
			}

			// Token: 0x04000241 RID: 577
			private Context context;

			// Token: 0x04000242 RID: 578
			private Guid originalUserIdentity;
		}

		// Token: 0x02000034 RID: 52
		public struct UserLockCheckFrame : IDisposable
		{
			// Token: 0x0600028D RID: 653 RVA: 0x000168D6 File Offset: 0x00014AD6
			public UserLockCheckFrame(Context context, Context.UserLockCheckFrame.Scope scope, Guid? userIdentityContext, IMailboxLockName mailboxLockName)
			{
				this.context = context;
				this.scope = scope;
				this.userIdentityContext = userIdentityContext;
				this.mailboxLockName = mailboxLockName;
				context.userLockChecker = new Context.UserLockCheckFrame?(this);
			}

			// Token: 0x0600028E RID: 654 RVA: 0x00016906 File Offset: 0x00014B06
			public void Dispose()
			{
				this.context.userLockChecker = null;
			}

			// Token: 0x0600028F RID: 655 RVA: 0x00016919 File Offset: 0x00014B19
			public bool IsScope(Context.UserLockCheckFrame.Scope scope)
			{
				return this.scope == scope;
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00016924 File Offset: 0x00014B24
			public bool HasAnyLock()
			{
				return Context.IsUserLocked(this.userIdentityContext, this.mailboxLockName);
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00016937 File Offset: 0x00014B37
			public bool HasExclusiveLock()
			{
				return Context.IsUserExclusiveLocked(this.userIdentityContext, this.mailboxLockName);
			}

			// Token: 0x04000243 RID: 579
			private Context.UserLockCheckFrame.Scope scope;

			// Token: 0x04000244 RID: 580
			private Guid? userIdentityContext;

			// Token: 0x04000245 RID: 581
			private IMailboxLockName mailboxLockName;

			// Token: 0x04000246 RID: 582
			private Context context;

			// Token: 0x02000035 RID: 53
			public enum Scope
			{
				// Token: 0x04000248 RID: 584
				Folder,
				// Token: 0x04000249 RID: 585
				SearchFolder,
				// Token: 0x0400024A RID: 586
				LogicalIndex
			}
		}

		// Token: 0x02000036 RID: 54
		public struct CriticalBlockFrame : IDisposable
		{
			// Token: 0x06000292 RID: 658 RVA: 0x0001694A File Offset: 0x00014B4A
			internal CriticalBlockFrame(LID lid, Context context, ICriticalBlockFailureHandler criticalBlockHandler, CriticalBlockScope scope)
			{
				this.lid = lid;
				this.outerCriticalBlockHandler = context.criticalBlockHandler;
				this.outerCriticalBlockScope = context.criticalBlockScope;
				context.criticalBlockHandler = criticalBlockHandler;
				context.criticalBlockScope = scope;
				this.context = context;
			}

			// Token: 0x06000293 RID: 659 RVA: 0x00016984 File Offset: 0x00014B84
			public void Dispose()
			{
				if (this.context != null)
				{
					if (this.context.criticalBlockHandler != null)
					{
						if (ExTraceGlobals.CriticalBlockTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.CriticalBlockTracer.TraceError<string>(0L, "Critical Block failure, callstack:{0}", new StackTrace(true).ToString());
						}
						DiagnosticContext.TraceDword((LID)63456U, this.lid.Value);
						this.context.criticalBlockHandler.OnCriticalBlockFailed(this.lid, this.context, this.context.criticalBlockScope);
					}
					this.context.criticalBlockHandler = this.outerCriticalBlockHandler;
					this.context.criticalBlockScope = this.outerCriticalBlockScope;
					this.context = null;
				}
			}

			// Token: 0x0400024B RID: 587
			private LID lid;

			// Token: 0x0400024C RID: 588
			private Context context;

			// Token: 0x0400024D RID: 589
			private ICriticalBlockFailureHandler outerCriticalBlockHandler;

			// Token: 0x0400024E RID: 590
			private CriticalBlockScope outerCriticalBlockScope;
		}

		// Token: 0x02000037 RID: 55
		internal enum LockKind
		{
			// Token: 0x04000250 RID: 592
			None,
			// Token: 0x04000251 RID: 593
			Shared,
			// Token: 0x04000252 RID: 594
			Exclusive
		}

		// Token: 0x02000038 RID: 56
		public struct DatabaseAssociationBlockFrame : IDisposable
		{
			// Token: 0x06000294 RID: 660 RVA: 0x00016A3C File Offset: 0x00014C3C
			internal DatabaseAssociationBlockFrame(Context context, StoreDatabase database, Context.LockKind lockKind)
			{
				context.Connect(database, lockKind);
				this.context = context;
				this.lockKind = lockKind;
			}

			// Token: 0x06000295 RID: 661 RVA: 0x00016A54 File Offset: 0x00014C54
			public void Dispose()
			{
				if (this.context != null)
				{
					this.context.Disconnect(this.lockKind);
					this.context = null;
				}
			}

			// Token: 0x04000253 RID: 595
			private Context context;

			// Token: 0x04000254 RID: 596
			private Context.LockKind lockKind;
		}

		// Token: 0x02000039 RID: 57
		public struct MailboxContextDisAssociationBlockFrameForTest : IDisposable
		{
			// Token: 0x06000296 RID: 662 RVA: 0x00016A76 File Offset: 0x00014C76
			internal MailboxContextDisAssociationBlockFrameForTest(Context context)
			{
				this.context = context;
				this.savedMailboxContexts = context.mailboxContexts;
				context.mailboxContexts = new Dictionary<int, Mailbox>();
			}

			// Token: 0x06000297 RID: 663 RVA: 0x00016A96 File Offset: 0x00014C96
			public void Dispose()
			{
				if (this.context != null)
				{
					this.context.mailboxContexts = this.savedMailboxContexts;
					this.context = null;
				}
			}

			// Token: 0x04000255 RID: 597
			private Context context;

			// Token: 0x04000256 RID: 598
			private Dictionary<int, Mailbox> savedMailboxContexts;
		}

		// Token: 0x0200003A RID: 58
		public struct DatabaseDisAssociationBlockFrameForTest : IDisposable
		{
			// Token: 0x06000298 RID: 664 RVA: 0x00016AB8 File Offset: 0x00014CB8
			internal DatabaseDisAssociationBlockFrameForTest(Context context)
			{
				this.context = context;
				this.database = context.Database;
				if (context.Database.IsExclusiveLockHeld())
				{
					this.lockKind = Context.LockKind.Exclusive;
				}
				else if (context.Database.IsSharedLockHeld())
				{
					this.lockKind = Context.LockKind.Shared;
				}
				else
				{
					this.lockKind = Context.LockKind.None;
				}
				context.Disconnect(this.lockKind);
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00016B17 File Offset: 0x00014D17
			public void Dispose()
			{
				if (this.context != null)
				{
					this.context.Connect(this.database, this.lockKind);
					this.context = null;
				}
			}

			// Token: 0x04000257 RID: 599
			private Context context;

			// Token: 0x04000258 RID: 600
			private Context.LockKind lockKind;

			// Token: 0x04000259 RID: 601
			private StoreDatabase database;
		}

		// Token: 0x0200003B RID: 59
		public struct PartitionFullAccessFrame : IDisposable
		{
			// Token: 0x0600029A RID: 666 RVA: 0x00016B3F File Offset: 0x00014D3F
			internal PartitionFullAccessFrame(Context context)
			{
				this.previousPartitionFullAccessGranted = context.partitionFullAccessGranted;
				context.partitionFullAccessGranted = true;
				this.context = context;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00016B5B File Offset: 0x00014D5B
			public void Dispose()
			{
				if (this.context != null)
				{
					this.context.partitionFullAccessGranted = this.previousPartitionFullAccessGranted;
					this.context = null;
				}
			}

			// Token: 0x0400025A RID: 602
			private Context context;

			// Token: 0x0400025B RID: 603
			private bool previousPartitionFullAccessGranted;
		}

		// Token: 0x0200003C RID: 60
		public struct MailboxUnlockFrameForTest : IDisposable
		{
			// Token: 0x0600029C RID: 668 RVA: 0x00016B7D File Offset: 0x00014D7D
			internal MailboxUnlockFrameForTest(MailboxState mailboxState, Context context, bool sharedMailboxLock, bool sharedUserLock, ILockStatistics lockStats)
			{
				context.UnlockCurrentUser();
				mailboxState.ReleaseMailboxLock(sharedMailboxLock);
				this.mailboxState = mailboxState;
				this.sharedMailboxLock = sharedMailboxLock;
				this.sharedUserLock = sharedUserLock;
				this.lockStats = lockStats;
				this.context = context;
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00016BB1 File Offset: 0x00014DB1
			public void Dispose()
			{
				this.mailboxState.GetMailboxLock(this.sharedMailboxLock, this.lockStats);
				this.context.TryLockCurrentUser(this.mailboxState, this.sharedUserLock, LockManager.InfiniteTimeout, this.lockStats);
			}

			// Token: 0x0400025C RID: 604
			private readonly Context context;

			// Token: 0x0400025D RID: 605
			private readonly MailboxState mailboxState;

			// Token: 0x0400025E RID: 606
			private readonly bool sharedMailboxLock;

			// Token: 0x0400025F RID: 607
			private readonly bool sharedUserLock;

			// Token: 0x04000260 RID: 608
			private readonly ILockStatistics lockStats;
		}

		// Token: 0x0200003D RID: 61
		private class UserLockName : ILockName, IEquatable<ILockName>, IComparable<ILockName>
		{
			// Token: 0x0600029E RID: 670 RVA: 0x00016BED File Offset: 0x00014DED
			public UserLockName(IMailboxLockName mailboxLockName, Guid userGuid) : this(mailboxLockName.DatabaseGuid, mailboxLockName.MailboxPartitionNumber, userGuid)
			{
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00016C02 File Offset: 0x00014E02
			public UserLockName(Guid databaseGuid, int mailboxPartitionNumber, Guid userGuid)
			{
				this.databaseGuid = databaseGuid;
				this.mailboxPartitionNumber = mailboxPartitionNumber;
				this.userGuid = userGuid;
				this.hashCode = (databaseGuid.GetHashCode() ^ mailboxPartitionNumber ^ userGuid.GetHashCode());
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x00016C42 File Offset: 0x00014E42
			public int MailboxPartitionNumber
			{
				get
				{
					return this.mailboxPartitionNumber;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x00016C4A File Offset: 0x00014E4A
			public Guid DatabaseGuid
			{
				get
				{
					return this.databaseGuid;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002A2 RID: 674 RVA: 0x00016C52 File Offset: 0x00014E52
			public Guid UserGuid
			{
				get
				{
					return this.userGuid;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x00016C5A File Offset: 0x00014E5A
			public LockManager.LockLevel LockLevel
			{
				get
				{
					return LockManager.LockLevel.User;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x00016C5D File Offset: 0x00014E5D
			// (set) Token: 0x060002A5 RID: 677 RVA: 0x00016C65 File Offset: 0x00014E65
			public LockManager.NamedLockObject CachedLockObject { get; set; }

			// Token: 0x060002A6 RID: 678 RVA: 0x00016C6E File Offset: 0x00014E6E
			public ILockName GetLockNameToCache()
			{
				return this;
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x00016C71 File Offset: 0x00014E71
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x00016C79 File Offset: 0x00014E79
			public override bool Equals(object other)
			{
				return this.Equals(other as Context.UserLockName);
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x00016C87 File Offset: 0x00014E87
			public bool Equals(ILockName other)
			{
				return other != null && this.CompareTo(other) == 0;
			}

			// Token: 0x060002AA RID: 682 RVA: 0x00016C98 File Offset: 0x00014E98
			public int CompareTo(ILockName other)
			{
				int num = this.LockLevel.CompareTo(other.LockLevel);
				if (num == 0)
				{
					Context.UserLockName userLockName = other as Context.UserLockName;
					num = this.databaseGuid.CompareTo(userLockName.databaseGuid);
					if (num == 0)
					{
						num = this.mailboxPartitionNumber.CompareTo(userLockName.mailboxPartitionNumber);
						if (num == 0)
						{
							num = this.userGuid.CompareTo(userLockName.userGuid);
						}
					}
				}
				return num;
			}

			// Token: 0x060002AB RID: 683 RVA: 0x00016D14 File Offset: 0x00014F14
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"DB ",
					this.DatabaseGuid.ToString(),
					"/MBX ",
					this.MailboxPartitionNumber.ToString(),
					"/User ",
					this.userGuid.ToString()
				});
			}

			// Token: 0x04000261 RID: 609
			private readonly Guid databaseGuid;

			// Token: 0x04000262 RID: 610
			private readonly int mailboxPartitionNumber;

			// Token: 0x04000263 RID: 611
			private readonly Guid userGuid;

			// Token: 0x04000264 RID: 612
			private readonly int hashCode;
		}

		// Token: 0x0200003E RID: 62
		private struct MailboxOperationParameters
		{
			// Token: 0x060002AC RID: 684 RVA: 0x00016D88 File Offset: 0x00014F88
			internal MailboxOperationParameters(MailboxState mailboxState, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
			{
				this = new Context.MailboxOperationParameters(mailboxState, null, null, operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
			}

			// Token: 0x060002AD RID: 685 RVA: 0x00016DB4 File Offset: 0x00014FB4
			internal MailboxOperationParameters(int mailboxNumber, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
			{
				this = new Context.MailboxOperationParameters(null, new int?(mailboxNumber), null, operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
			}

			// Token: 0x060002AE RID: 686 RVA: 0x00016DE0 File Offset: 0x00014FE0
			internal MailboxOperationParameters(Guid mailboxGuid, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
			{
				this = new Context.MailboxOperationParameters(null, null, new Guid?(mailboxGuid), operationSource, lockTimeout, isSharedMailboxOperation, isSharedUserOperation);
			}

			// Token: 0x060002AF RID: 687 RVA: 0x00016E0C File Offset: 0x0001500C
			private MailboxOperationParameters(MailboxState mailboxState, int? mailboxNumber, Guid? mailboxGuid, ExecutionDiagnostics.OperationSource operationSource, TimeSpan lockTimeout, bool isSharedMailboxOperation, bool isSharedUserOperation)
			{
				this.mailboxState = mailboxState;
				this.mailboxNumber = mailboxNumber;
				this.mailboxGuid = mailboxGuid;
				this.operationSource = operationSource;
				this.lockTimeout = lockTimeout;
				this.isSharedMailboxOperation = isSharedMailboxOperation;
				this.isSharedUserOperation = (isSharedUserOperation || !isSharedMailboxOperation);
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x00016E58 File Offset: 0x00015058
			internal MailboxState MailboxState
			{
				get
				{
					return this.mailboxState;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x00016E60 File Offset: 0x00015060
			internal int? MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x00016E68 File Offset: 0x00015068
			internal Guid? MailboxGuid
			{
				get
				{
					return this.mailboxGuid;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x00016E70 File Offset: 0x00015070
			internal ExecutionDiagnostics.OperationSource OperationSource
			{
				get
				{
					return this.operationSource;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x00016E78 File Offset: 0x00015078
			internal TimeSpan LockTimeout
			{
				get
				{
					return this.lockTimeout;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x00016E80 File Offset: 0x00015080
			internal bool IsSharedMailboxOperation
			{
				get
				{
					return this.isSharedMailboxOperation;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x00016E88 File Offset: 0x00015088
			internal bool IsSharedUserOperation
			{
				get
				{
					return this.isSharedUserOperation;
				}
			}

			// Token: 0x04000266 RID: 614
			private readonly MailboxState mailboxState;

			// Token: 0x04000267 RID: 615
			private readonly int? mailboxNumber;

			// Token: 0x04000268 RID: 616
			private readonly Guid? mailboxGuid;

			// Token: 0x04000269 RID: 617
			private readonly ExecutionDiagnostics.OperationSource operationSource;

			// Token: 0x0400026A RID: 618
			private readonly TimeSpan lockTimeout;

			// Token: 0x0400026B RID: 619
			private readonly bool isSharedMailboxOperation;

			// Token: 0x0400026C RID: 620
			private readonly bool isSharedUserOperation;
		}

		// Token: 0x0200003F RID: 63
		public class SimpleStateObject : IStateObject
		{
			// Token: 0x060002B7 RID: 695 RVA: 0x00016E90 File Offset: 0x00015090
			public SimpleStateObject(Action<Context> commitAction, Action<Context> abortAction)
			{
				this.commitAction = commitAction;
				this.abortAction = abortAction;
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00016EA6 File Offset: 0x000150A6
			public void OnBeforeCommit(Context context)
			{
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00016EA8 File Offset: 0x000150A8
			public void OnCommit(Context context)
			{
				if (this.commitAction != null)
				{
					this.commitAction(context);
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00016EBE File Offset: 0x000150BE
			public void OnAbort(Context context)
			{
				if (this.abortAction != null)
				{
					this.abortAction(context);
				}
			}

			// Token: 0x0400026D RID: 621
			private Action<Context> commitAction;

			// Token: 0x0400026E RID: 622
			private Action<Context> abortAction;
		}
	}
}
