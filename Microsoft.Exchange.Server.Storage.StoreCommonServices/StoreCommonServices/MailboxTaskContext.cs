using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000BF RID: 191
	public class MailboxTaskContext : Context
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00027098 File Offset: 0x00025298
		public MailboxTaskContext()
		{
			base.SkipDatabaseLogsFlush = true;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000270A7 File Offset: 0x000252A7
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000270AF File Offset: 0x000252AF
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000270B7 File Offset: 0x000252B7
		public override IMailboxContext PrimaryMailboxContext
		{
			get
			{
				return this.Mailbox;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000270BF File Offset: 0x000252BF
		public SecurityIdentifier UserSid
		{
			get
			{
				return this.userSid;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000270C7 File Offset: 0x000252C7
		public StoreDatabase MailboxDatabase
		{
			get
			{
				return this.mailboxDatabase;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000270CF File Offset: 0x000252CF
		public TaskExecutionDiagnostics TaskDiagnostics
		{
			get
			{
				return (TaskExecutionDiagnostics)base.Diagnostics;
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000270DC File Offset: 0x000252DC
		internal static TMailboxTaskContext CreateTaskExecutionContext<TMailboxTaskContext>(TaskTypeId taskTypeId, StoreDatabase database, int mailboxNumber, ClientType clientType, Guid clientActivityId, string clientComponentName, string clientProtocolName, string clientActionString, Guid userIdentity, SecurityIdentifier userSid, CultureInfo culture) where TMailboxTaskContext : MailboxTaskContext, new()
		{
			TMailboxTaskContext result = Activator.CreateInstance<TMailboxTaskContext>();
			result.PostConstructionInitialize(new TaskExecutionDiagnostics(taskTypeId, clientActivityId, clientComponentName, clientProtocolName, clientActionString), database, mailboxNumber, clientType, userIdentity, userSid, culture);
			return result;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00027114 File Offset: 0x00025314
		protected void PostConstructionInitialize(TaskExecutionDiagnostics executionDiagnostics, StoreDatabase database, int mailboxNumber, ClientType clientType, Guid userIdentity, SecurityIdentifier userSid, CultureInfo culture)
		{
			base.PostConstructionInitialize(executionDiagnostics, Globals.ProcessSecurityContext, clientType, culture);
			this.mailboxDatabase = database;
			this.mailboxNumber = mailboxNumber;
			this.userSid = userSid;
			base.UserIdentity = userIdentity;
			base.InitializeMailboxExclusiveOperation(this.mailboxNumber, ExecutionDiagnostics.OperationSource.MailboxTask, LockManager.InfiniteTimeout);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00027161 File Offset: 0x00025361
		protected override void ConnectDatabase()
		{
			base.ConnectDatabase();
			base.Connect(this.mailboxDatabase);
			if (!this.mailboxDatabase.IsOnlineActive)
			{
				base.Disconnect();
				throw new StoreException((LID)35456U, ErrorCodeValue.MdbNotInitialized);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002719D File Offset: 0x0002539D
		protected override void DisconnectDatabase()
		{
			base.Disconnect();
			base.DisconnectDatabase();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000271AC File Offset: 0x000253AC
		protected override void ConnectMailboxes()
		{
			base.ConnectMailboxes();
			bool flag = this.mailbox == null;
			if (flag)
			{
				DiagnosticContext.TraceLocation((LID)51840U);
				if (!base.LockedMailboxState.IsAccessible)
				{
					throw new StoreException((LID)37404U, ErrorCodeValue.TaskRequestFailed);
				}
				this.mailbox = Mailbox.OpenMailbox(this, base.LockedMailboxState);
				if (this.mailbox == null)
				{
					throw new StoreException((LID)53788U, ErrorCodeValue.TaskRequestFailed);
				}
			}
			else
			{
				DiagnosticContext.TraceLocation((LID)62080U);
				if (this.mailbox.IsDead)
				{
					throw new StoreException((LID)61980U, ErrorCodeValue.TaskRequestFailed);
				}
				this.mailbox.Reconnect(this);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00027269 File Offset: 0x00025469
		protected override void DisconnectMailboxes(bool pulseOnly)
		{
			base.DisconnectMailboxes(pulseOnly);
			if (this.mailbox != null && !pulseOnly)
			{
				this.mailbox.Dispose();
				this.mailbox = null;
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002728F File Offset: 0x0002548F
		public override ErrorCode StartMailboxOperation(MailboxCreation mailboxCreation, bool findRemovedMailbox, bool skipQuarantineCheck)
		{
			return base.StartMailboxOperation(mailboxCreation, findRemovedMailbox, skipQuarantineCheck);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002729C File Offset: 0x0002549C
		public override void EndMailboxOperation(bool commit, bool skipDisconnectingDatabase, bool pulseOnly)
		{
			try
			{
				base.EndMailboxOperation(commit, skipDisconnectingDatabase, pulseOnly);
			}
			finally
			{
				((TaskExecutionDiagnostics)base.Diagnostics).OnTaskEnd();
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00027364 File Offset: 0x00025564
		private void Cleanup()
		{
			base.SystemCriticalOperation(new TryDelegate(this, (UIntPtr)ldftn(<Cleanup>b__0)));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00027378 File Offset: 0x00025578
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxTaskContext>(this);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00027380 File Offset: 0x00025580
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.mailbox != null)
			{
				this.Cleanup();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000494 RID: 1172
		private StoreDatabase mailboxDatabase;

		// Token: 0x04000495 RID: 1173
		private int mailboxNumber;

		// Token: 0x04000496 RID: 1174
		private SecurityIdentifier userSid;

		// Token: 0x04000497 RID: 1175
		private Mailbox mailbox;
	}
}
