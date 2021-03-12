using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000004 RID: 4
	internal sealed class Connection : BaseObject, IConnection, IConnectionInformation
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022B8 File Offset: 0x000004B8
		public Connection(int connectionId, ConnectionInfo connectionInfo, ClientInfo clientInfo, IUser user, IBudget budget, ClientSecurityContext clientSecurityContext, HandlerFactory handlerFactory, IDriverFactory driverFactory)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.usageCount = 0;
				this.removeAction = null;
				this.connectionId = connectionId;
				this.connectionInfo = connectionInfo;
				this.clientInfo = clientInfo;
				this.budget = budget;
				this.clientSecurityContext = clientSecurityContext;
				this.user = user;
				this.user.AddReference();
				this.cultureInfo = StoreSession.CreateMapiCultureInfo(this.connectionInfo.LocaleInfo.StringLocaleId, this.connectionInfo.LocaleInfo.SortLocaleId, this.connectionInfo.LocaleInfo.CodePageId);
				if (ExMonHandler.IsEnabled)
				{
					this.handler = new ExMonHandler(Configuration.ServiceConfiguration.EnableExMonTestMode, connectionId, user.LegacyDistinguishedName, connectionInfo.ClientIpAddress.ToString(), connectionInfo.ClientVersion, handlerFactory(this), "MSExchangeRPC");
				}
				else
				{
					this.handler = handlerFactory(this);
				}
				this.ropDriver = driverFactory.CreateIRopDriver(this.handler, this);
				RopDriver ropDriver = this.ropDriver as RopDriver;
				if (ropDriver != null)
				{
					ropDriver.OnBeforeRopExecuted = new RopProcessingCallbackDelegate(Connection.OnBeforeRopExecuted);
					ropDriver.OnAfterRopExecuted = new RopProcessingCallbackDelegate(Connection.OnAfterRopExecuted);
					ropDriver.IsMimumResponseSizeEnforcementEnabled = new Func<bool>(Connection.IsMimumResponseSizeEnforcementEnabled);
				}
				ExTraceGlobals.ConnectRpcTracer.TraceInformation<string, int>(0, Activity.TraceId, "Connected user '{0}'. ConnectionId = {1:X}", user.LegacyDistinguishedName, this.connectionId);
				this.lastLogTimeBinary = ExDateTime.UtcNow.ToBinary();
				this.RegisterActivity();
				ReferencedActivityScope referencedActivityScope = ReferencedActivityScope.Current;
				if (referencedActivityScope != null)
				{
					referencedActivityScope.AddRef();
					this.referencedActivityScope = referencedActivityScope;
				}
				else
				{
					this.referencedActivityScope = ReferencedActivityScope.Create(null);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000024B4 File Offset: 0x000006B4
		public IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000024BC File Offset: 0x000006BC
		internal DispatchOptions DispatchOptions
		{
			get
			{
				return this.connectionInfo.DispatchOptions;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000024C9 File Offset: 0x000006C9
		ushort IConnectionInformation.SessionId
		{
			get
			{
				base.CheckDisposed();
				return this.InternalSessionId;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024D7 File Offset: 0x000006D7
		bool IConnectionInformation.ClientSupportsBackoffResult
		{
			get
			{
				return this.clientInfo.Mode != ClientMode.ExchangeServer && this.connectionInfo.ClientVersion >= MapiVersion.Outlook12;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000024FE File Offset: 0x000006FE
		bool IConnectionInformation.ClientSupportsBufferTooSmallBreakup
		{
			get
			{
				return this.clientInfo.Mode != ClientMode.ExchangeServer && this.connectionInfo.ClientVersion >= MapiVersion.Outlook12;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002525 File Offset: 0x00000725
		Encoding IConnectionInformation.String8Encoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000252C File Offset: 0x0000072C
		public ClientInfo ClientInformation
		{
			get
			{
				return this.clientInfo;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002534 File Offset: 0x00000734
		public IPAddress ServerIpAddress
		{
			get
			{
				return this.connectionInfo.ServerIpAddress;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002541 File Offset: 0x00000741
		public string ProtocolSequence
		{
			get
			{
				return this.connectionInfo.ProtocolSequence;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000254E File Offset: 0x0000074E
		public ConnectionFlags ConnectionFlags
		{
			get
			{
				return this.connectionInfo.ConnectionFlags;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000255B File Offset: 0x0000075B
		public OrganizationId OrganizationId
		{
			get
			{
				return this.connectionInfo.OrganizationId ?? this.User.OrganizationId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002577 File Offset: 0x00000777
		public bool IsWebService
		{
			get
			{
				return this.connectionInfo.ProtocolSequence == "xrop";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000258E File Offset: 0x0000078E
		public string ActAsLegacyDN
		{
			get
			{
				return this.connectionInfo.UserDn;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000259B File Offset: 0x0000079B
		public bool IsFederatedSystemAttendant
		{
			get
			{
				return this.user.IsFederatedSystemAttendant;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000025A8 File Offset: 0x000007A8
		public ClientSecurityContext AccessingClientSecurityContext
		{
			get
			{
				base.CheckDisposed();
				return this.clientSecurityContext;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025B6 File Offset: 0x000007B6
		public MiniRecipient MiniRecipient
		{
			get
			{
				return this.User.MiniRecipient;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025C3 File Offset: 0x000007C3
		public void BackoffConnect(Exception reason)
		{
			base.CheckDisposed();
			ExTraceGlobals.ClientThrottledTracer.TraceInformation<string, int>(0, Activity.TraceId, "Backing off future connect requests for user {0}. ConnectionId = {1:X}", this.User.LegacyDistinguishedName, this.connectionId);
			this.User.BackoffConnect(reason);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025FD File Offset: 0x000007FD
		public ExchangePrincipal FindExchangePrincipalByLegacyDN(string legacyDN)
		{
			base.CheckDisposed();
			return this.User.GetExchangePrincipal(legacyDN);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002611 File Offset: 0x00000811
		public void InvalidateCachedUserInfo()
		{
			base.CheckDisposed();
			this.User.InvalidatePrincipalCache();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002624 File Offset: 0x00000824
		public void MarkAsDeadAndDropAllAsyncCalls()
		{
			ExTraceGlobals.ConnectRpcTracer.TraceDebug<Connection>(0, Activity.TraceId, "MarkAsDeadAndDropAllAsyncCalls. Connection = {0}.", this);
			this.MarkAsDead();
			this.CompleteAction("backend connection is dead", true, RpcErrorCode.None);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002650 File Offset: 0x00000850
		public void ExecuteInContext<T>(T input, Action<T> action)
		{
			Activity.Guard guard = null;
			try
			{
				if (Activity.Current == null)
				{
					guard = new Activity.Guard();
					guard.AssociateWithCurrentThread(this.Activity, false);
				}
				bool flag = false;
				if (ExUserTracingAdaptor.Instance.IsTracingEnabledUser(this.ActAsLegacyDN))
				{
					flag = true;
					BaseTrace.CurrentThreadSettings.EnableTracing();
				}
				try
				{
					action(input);
				}
				finally
				{
					if (flag)
					{
						BaseTrace.CurrentThreadSettings.DisableTracing();
					}
				}
			}
			finally
			{
				if (guard != null)
				{
					guard.Dispose();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026D8 File Offset: 0x000008D8
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000026E0 File Offset: 0x000008E0
		public Fqdn TargetServer { private get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026E9 File Offset: 0x000008E9
		public bool IsEncrypted
		{
			get
			{
				return this.connectionInfo.IsEncrypted;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026F6 File Offset: 0x000008F6
		public CultureInfo CultureInfo
		{
			get
			{
				base.CheckDisposed();
				return this.cultureInfo;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002704 File Offset: 0x00000904
		public int CodePageId
		{
			get
			{
				base.CheckDisposed();
				return this.connectionInfo.LocaleInfo.CodePageId;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000272A File Offset: 0x0000092A
		public string RpcServerTarget
		{
			get
			{
				base.CheckDisposed();
				return this.connectionInfo.RpcServerTarget;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000273D File Offset: 0x0000093D
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Connection>(this);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002745 File Offset: 0x00000945
		internal Activity Activity
		{
			get
			{
				return this.connectionInfo.Activity;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002752 File Offset: 0x00000952
		internal ReferencedActivityScope ReferencedActivityScope
		{
			get
			{
				return this.referencedActivityScope;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000275A File Offset: 0x0000095A
		internal IRopDriver RopDriver
		{
			get
			{
				base.CheckDisposed();
				return this.ropDriver;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002768 File Offset: 0x00000968
		internal IConnectionHandler Handler
		{
			get
			{
				base.CheckDisposed();
				return this.handler;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002776 File Offset: 0x00000976
		internal IUser User
		{
			get
			{
				base.CheckDisposed();
				return this.user;
			}
		}

		// Token: 0x1700001B RID: 27
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002784 File Offset: 0x00000984
		internal Action<bool, int> CompletionAction
		{
			set
			{
				this.completionAction = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000278D File Offset: 0x0000098D
		internal bool CompletionActionAssigned
		{
			get
			{
				return this.completionAction != null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000279B File Offset: 0x0000099B
		internal bool IsDead
		{
			get
			{
				return this.isDead;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027A3 File Offset: 0x000009A3
		internal void MarkAsDead()
		{
			this.isDead = true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027AC File Offset: 0x000009AC
		internal void CompleteAction(string reason, bool eventsPending, RpcErrorCode storeError)
		{
			Action<bool, int> action = Interlocked.Exchange<Action<bool, int>>(ref this.completionAction, null);
			if (ExTraceGlobals.AsyncRpcTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AsyncRpcTracer.TraceDebug(Activity.TraceId, "CompleteAction ({0}). Connection = {1}, eventsPending = {2}, storeError = {3}, completionActionAssigned = {4}.", new object[]
				{
					reason,
					this,
					eventsPending,
					storeError,
					action != null
				});
			}
			if (action != null)
			{
				action(eventsPending, (int)storeError);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002823 File Offset: 0x00000A23
		internal static int GetConnectionId(Connection connection)
		{
			if (connection == null || connection.IsDisposed)
			{
				return 0;
			}
			return connection.connectionId;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002838 File Offset: 0x00000A38
		internal void RegisterActivity()
		{
			this.LastAccessTime = ExDateTime.UtcNow;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002845 File Offset: 0x00000A45
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002852 File Offset: 0x00000A52
		internal ExDateTime LastAccessTime
		{
			get
			{
				return ExDateTime.FromBinary(this.lastAccessTimeBinary);
			}
			private set
			{
				this.lastAccessTimeBinary = value.ToBinary();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000286E File Offset: 0x00000A6E
		internal ExDateTime LastLogTime
		{
			get
			{
				return ExDateTime.FromBinary(this.lastLogTimeBinary);
			}
			set
			{
				this.lastLogTimeBinary = value.ToBinary();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002880 File Offset: 0x00000A80
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.ropDriver);
			Util.DisposeIfPresent(this.handler);
			this.connectionId = 0;
			Util.DisposeIfPresent(this.clientSecurityContext);
			this.connectionInfo.Activity.RegisterBudget(null);
			Util.DisposeIfPresent(this.budget);
			if (this.user != null)
			{
				this.user.Release();
				this.user = null;
			}
			if (this.referencedActivityScope != null)
			{
				this.referencedActivityScope.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002905 File Offset: 0x00000B05
		internal void BeginServerPerformanceCounting()
		{
			if (this.serverPerformanceObject == null && this.TargetServer != null)
			{
				this.serverPerformanceObject = new ServerPerformanceObject(this.TargetServer);
			}
			if (this.serverPerformanceObject != null)
			{
				this.serverPerformanceObject.Start();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000293C File Offset: 0x00000B3C
		internal void EndServerPerformanceCounting()
		{
			if (this.serverPerformanceObject != null)
			{
				this.serverPerformanceObject.StopAndCollectPerformanceData();
				if (this.serverPerformanceObject.LastRpcLatency != null)
				{
					ProtocolLog.UpdateMailboxServerRpcProcessingTime(this.serverPerformanceObject.LastRpcLatency.Value);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000298C File Offset: 0x00000B8C
		internal bool TryIncrementUsageCount()
		{
			bool result;
			lock (this.usageCountLock)
			{
				if (this.removeAction != null)
				{
					result = false;
				}
				else
				{
					this.usageCount++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal void DecrementUsageCount()
		{
			bool flag = false;
			lock (this.usageCountLock)
			{
				this.usageCount--;
				if (this.usageCount == 0 && this.removeAction != null)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.removeAction();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A50 File Offset: 0x00000C50
		internal void MarkForRemoval(Action action)
		{
			lock (this.usageCountLock)
			{
				if (this.removeAction == null)
				{
					this.removeAction = action;
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A9C File Offset: 0x00000C9C
		internal void StartNewActivityScope()
		{
			lock (this.referencedActivityScopeLock)
			{
				ReferencedActivityScope referencedActivityScope = null;
				bool flag2 = false;
				try
				{
					referencedActivityScope = ReferencedActivityScope.Create(this.referencedActivityScope.ActivityScope.Metadata);
					if (this.referencedActivityScope != null)
					{
						this.referencedActivityScope.Release();
					}
					this.referencedActivityScope = referencedActivityScope;
					flag2 = true;
				}
				finally
				{
					if (!flag2 && referencedActivityScope != null)
					{
						referencedActivityScope.Release();
					}
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B2C File Offset: 0x00000D2C
		internal ReferencedActivityScope GetReferencedActivityScope()
		{
			ReferencedActivityScope result;
			lock (this.referencedActivityScopeLock)
			{
				this.referencedActivityScope.AddRef();
				result = this.referencedActivityScope;
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B7C File Offset: 0x00000D7C
		internal void UpdateBudgetBalance()
		{
			string budgetBalance;
			if (this.budget != null && this.referencedActivityScope != null && this.budget.TryGetBudgetBalance(out budgetBalance))
			{
				WorkloadManagementLogger.SetBudgetBalance(budgetBalance, this.referencedActivityScope.ActivityScope);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BBC File Offset: 0x00000DBC
		public override string ToString()
		{
			return string.Format("UserDn = {0}, SessionId = {1}, ProtocolSequence = {2}, ClientIpAddress = {3}, ClientVersion = {4}, ServerIpAddress = {5}, LocaleInfo = {6}. IsDead = {7}, isDisposed = {8}, CompletionActionAssigned = {9}.", new object[]
			{
				this.connectionInfo.UserDn,
				this.InternalSessionId,
				this.connectionInfo.ProtocolSequence,
				this.connectionInfo.ClientIpAddress,
				this.connectionInfo.ClientVersion,
				this.connectionInfo.ServerIpAddress,
				this.connectionInfo.LocaleInfo,
				this.isDead,
				base.IsDisposed,
				this.CompletionActionAssigned
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C74 File Offset: 0x00000E74
		private static void OnBeforeRopExecuted(Rop rop, ServerObjectHandleTable serverObjectHandleTable)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.Action = rop.RopId.ToString();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private static void OnAfterRopExecuted(Rop rop, ServerObjectHandleTable serverObjectHandleTable)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.Action = null;
			}
			RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.OperationsRate.Increment();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CD0 File Offset: 0x00000ED0
		private static bool IsMimumResponseSizeEnforcementEnabled()
		{
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).RpcClientAccess.MimumResponseSizeEnforcement.Enabled;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CFB File Offset: 0x00000EFB
		private ushort InternalSessionId
		{
			get
			{
				return (ushort)(this.connectionId & 65535);
			}
		}

		// Token: 0x04000007 RID: 7
		public const string WebServiceProtocolSequence = "xrop";

		// Token: 0x04000008 RID: 8
		private readonly ConnectionInfo connectionInfo;

		// Token: 0x04000009 RID: 9
		private readonly ClientInfo clientInfo;

		// Token: 0x0400000A RID: 10
		private readonly IConnectionHandler handler;

		// Token: 0x0400000B RID: 11
		private readonly IRopDriver ropDriver;

		// Token: 0x0400000C RID: 12
		private readonly IBudget budget;

		// Token: 0x0400000D RID: 13
		private readonly object usageCountLock = new object();

		// Token: 0x0400000E RID: 14
		private readonly CultureInfo cultureInfo;

		// Token: 0x0400000F RID: 15
		private readonly object referencedActivityScopeLock = new object();

		// Token: 0x04000010 RID: 16
		private IUser user;

		// Token: 0x04000011 RID: 17
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x04000012 RID: 18
		private int connectionId;

		// Token: 0x04000013 RID: 19
		private Action<bool, int> completionAction;

		// Token: 0x04000014 RID: 20
		private bool isDead;

		// Token: 0x04000015 RID: 21
		private long lastAccessTimeBinary;

		// Token: 0x04000016 RID: 22
		private long lastLogTimeBinary;

		// Token: 0x04000017 RID: 23
		private int usageCount;

		// Token: 0x04000018 RID: 24
		private Action removeAction;

		// Token: 0x04000019 RID: 25
		private ServerPerformanceObject serverPerformanceObject;

		// Token: 0x0400001A RID: 26
		private ReferencedActivityScope referencedActivityScope;
	}
}
