using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Security;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExRpcConnection : MapiUnk
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003808 File Offset: 0x00001A08
		internal ExRpcConnection(IExRpcConnectionInterface iExRpcConnection, WebServiceConnection webServiceConnection, bool isCrossServer, TimeSpan callTimeout) : base(iExRpcConnection, null, null)
		{
			this.iExRpcConnection = iExRpcConnection;
			this.webServiceConnection = webServiceConnection;
			this.IsCrossServer = isCrossServer;
			this.mapiStoreList = new List<MapiStore>(2);
			this.creationTime = DateTime.UtcNow;
			this.apartmentState = Thread.CurrentThread.GetApartmentState();
			this.creationThreadId = Environment.CurrentManagedThreadId;
			int serverVersion = this.iExRpcConnection.GetServerVersion(out this.versionMajor, out this.versionMinor, out this.buildMajor, out this.buildMinor);
			if (serverVersion != 0)
			{
				base.ThrowIfError("Unable to get server version information.", serverVersion);
			}
			this.threadLockCount = 0U;
			this.owningThread = null;
			this.callTimeoutTimer = null;
			if (callTimeout != TimeSpan.Zero)
			{
				this.CrashTimeout = callTimeout + TimeSpan.FromHours(1.0);
			}
			else
			{
				this.CrashTimeout = TimeSpan.Zero;
			}
			ExRpcPerf.ExRpcConnectionBirth();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000038E8 File Offset: 0x00001AE8
		public int VersionMajor
		{
			get
			{
				return this.versionMajor;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000038F0 File Offset: 0x00001AF0
		public int VersionMinor
		{
			get
			{
				return this.versionMinor;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000038F8 File Offset: 0x00001AF8
		public int BuildMajor
		{
			get
			{
				return this.buildMajor;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003900 File Offset: 0x00001B00
		public int BuildMinor
		{
			get
			{
				return this.buildMinor;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003908 File Offset: 0x00001B08
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003910 File Offset: 0x00001B10
		public TimeSpan CrashTimeout { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003919 File Offset: 0x00001B19
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003921 File Offset: 0x00001B21
		public bool IsCrossServer { get; private set; }

		// Token: 0x0600008D RID: 141 RVA: 0x0000392A File Offset: 0x00001B2A
		public void ClearStorePerRPCStats()
		{
			this.iExRpcConnection.ClearStorePerRPCStats();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003938 File Offset: 0x00001B38
		public PerRPCPerformanceStatistics GetStorePerRPCStats()
		{
			PerRpcStats nativeStats;
			uint storePerRPCStats = this.iExRpcConnection.GetStorePerRPCStats(out nativeStats);
			return PerRPCPerformanceStatistics.CreateFromNative(storePerRPCStats, nativeStats);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000395A File Offset: 0x00001B5A
		public void ClearRpcStatistics()
		{
			this.iExRpcConnection.ClearRPCStats();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003968 File Offset: 0x00001B68
		public RpcStatistics GetRpcStatistics()
		{
			RpcStats nativeStats;
			int rpcstats = this.iExRpcConnection.GetRPCStats(out nativeStats);
			if (rpcstats != 0)
			{
				base.ThrowIfError("Unable to get RPC statistics.", rpcstats);
			}
			return RpcStatistics.CreateFromNative(nativeStats);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003998 File Offset: 0x00001B98
		public void ExecuteWithInternalAccess(Action actionDelegate)
		{
			int num = this.iExRpcConnection.SetInternalAccess();
			if (num != 0)
			{
				base.ThrowIfError("Unable to set internal access elevation", num);
			}
			bool flag = false;
			try
			{
				actionDelegate();
				flag = true;
			}
			finally
			{
				num = this.iExRpcConnection.ClearInternalAccess();
				if (flag && num != 0)
				{
					base.ThrowIfError("Failure during clearing of internal access elevation", num);
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039FC File Offset: 0x00001BFC
		public void CheckForNotifications()
		{
			if (this.IsWebServiceConnection)
			{
				this.iExRpcConnection.CheckForNotifications();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003A11 File Offset: 0x00001C11
		internal bool IsWebServiceConnection
		{
			get
			{
				return this.webServiceConnection != null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003A1F File Offset: 0x00001C1F
		internal Exception InternalLowLevelException
		{
			get
			{
				if (this.webServiceConnection != null)
				{
					return this.webServiceConnection.LastException;
				}
				return null;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A38 File Offset: 0x00001C38
		protected override void MapiInternalDispose()
		{
			ExRpcPerf.ExRpcConnectionGone();
			lock (this)
			{
				if (this.mapiStoreList != null)
				{
					for (int i = this.mapiStoreList.Count; i > 0; i--)
					{
						if (this.mapiStoreList[i - 1] != null)
						{
							this.mapiStoreList[i - 1].Dispose();
						}
					}
				}
				this.iExRpcConnection = null;
				this.mapiStoreList = null;
			}
			base.MapiInternalDispose();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003AC8 File Offset: 0x00001CC8
		protected override void PostMapiInternalDispose()
		{
			if (this.webServiceConnection != null)
			{
				this.webServiceConnection.Dispose();
				this.webServiceConnection = null;
			}
			if (this.callTimeoutTimer != null)
			{
				this.callTimeoutTimer.Dispose();
				this.callTimeoutTimer = null;
			}
			base.PostMapiInternalDispose();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B04 File Offset: 0x00001D04
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExRpcConnection>(this);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B0C File Offset: 0x00001D0C
		internal void RemoveStoreReference(MapiStore mapiStore)
		{
			bool flag = false;
			lock (this)
			{
				this.mapiStoreList.Remove(mapiStore);
				flag = (this.mapiStoreList.Count == 0);
			}
			if (flag)
			{
				this.Dispose();
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B68 File Offset: 0x00001D68
		internal void Lock()
		{
			lock (this)
			{
				if (this.threadLockCount > 0U)
				{
					if (this.owningThread.ManagedThreadId != Environment.CurrentManagedThreadId)
					{
						StackTrace owningThreadStack = this.GetOwningThreadStack();
						throw MapiExceptionHelper.ObjectReenteredException(string.Concat(new object[]
						{
							"ExRpcConnection (MapiStore) object is already being used by thread ",
							this.owningThread.ManagedThreadId,
							".",
							(owningThreadStack != null) ? ("\nCall stack of the thread using the connection:\n" + owningThreadStack.ToString()) : ""
						}));
					}
					this.threadLockCount += 1U;
					if (this.threadLockCount == 0U)
					{
						throw MapiExceptionHelper.ObjectLockCountOverflowException("ExRpcConnection object lock count has overflowed.");
					}
				}
				else
				{
					this.threadLockCount = 1U;
					this.owningThread = Thread.CurrentThread;
					if (this.CrashTimeout > TimeSpan.Zero && this.CrashTimeout <= ExRpcConnection.MaxCallTimeout)
					{
						if (this.callTimeoutTimer == null)
						{
							this.callTimeoutTimer = new Timer(new TimerCallback(this.CrashOnCallTimeout));
						}
						this.callTimeoutTimer.Change((int)this.CrashTimeout.TotalMilliseconds, -1);
					}
				}
				if (this.webServiceConnection != null)
				{
					this.webServiceConnection.LastException = null;
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CC8 File Offset: 0x00001EC8
		internal void Unlock()
		{
			lock (this)
			{
				if (this.threadLockCount <= 0U)
				{
					throw MapiExceptionHelper.ObjectNotLockedException("MapiStore object is being unlocked, but not currently locked.");
				}
				if (this.owningThread.ManagedThreadId != Environment.CurrentManagedThreadId)
				{
					StackTrace owningThreadStack = this.GetOwningThreadStack();
					throw MapiExceptionHelper.ObjectNotLockedException(string.Concat(new object[]
					{
						"ExRpcConnection (MapiStore) object is being unlocked, but currently locked by thread ",
						this.owningThread.ManagedThreadId,
						".",
						(owningThreadStack != null) ? ("\nCall stack of the thread using the connection:\n" + owningThreadStack.ToString()) : ""
					}));
				}
				this.threadLockCount -= 1U;
				if (this.webServiceConnection != null)
				{
					this.webServiceConnection.LastException = null;
				}
				if (this.threadLockCount == 0U)
				{
					if (this.callTimeoutTimer != null)
					{
						this.callTimeoutTimer.Change(-1, -1);
					}
					if (ComponentTrace<MapiNetTags>.CheckEnabled(70) && this.RpcSentToServer)
					{
						StackTrace owningThreadStack2 = this.GetOwningThreadStack();
						ComponentTrace<MapiNetTags>.Trace<string>(31442, 70, (long)this.GetHashCode(), "RPC went to server\r\n{0}", owningThreadStack2.ToString());
					}
					this.owningThread = null;
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003E14 File Offset: 0x00002014
		internal MapiStore OpenMsgStore(OpenStoreFlag storeFlags, string mailboxDn, Guid mailboxGuid, Guid mdbGuid, out string correctServerDn, ClientIdentityInfo clientIdentityAs, string userDnAs, bool unifiedLogon, string applicationId, byte[] tenantHint, CultureInfo cultureInfo)
		{
			IExMapiStore exMapiStore = null;
			MapiStore mapiStore = null;
			int num = 0;
			if ((storeFlags & (OpenStoreFlag)((ulong)-2147483648)) != (OpenStoreFlag)((ulong)-2147483648))
			{
				num = 1;
			}
			correctServerDn = null;
			this.Lock();
			bool flag = false;
			MapiStore result;
			try
			{
				lock (this)
				{
					if (this.mapiStoreList.Count >= 256)
					{
						throw MapiExceptionHelper.ExceededMapiStoreLimitException("Can only have 256 MapiStore objects open on same connection.");
					}
				}
				int ulLcidString;
				int ulLcidSort;
				int ulCpid;
				MapiCultureInfo.RetrieveConnectParameters(cultureInfo, out ulLcidString, out ulLcidSort, out ulCpid);
				int num2;
				if (clientIdentityAs != null && IntPtr.Zero != clientIdentityAs.hAuthZ)
				{
					byte[] array = new byte[clientIdentityAs.sidUser.BinaryLength];
					byte[] array2 = new byte[clientIdentityAs.sidPrimaryGroup.BinaryLength];
					clientIdentityAs.sidUser.GetBinaryForm(array, 0);
					clientIdentityAs.sidPrimaryGroup.GetBinaryForm(array2, 0);
					num2 = this.iExRpcConnection.OpenMsgStore(num | 2, (long)storeFlags, mailboxDn, (mailboxGuid.CompareTo(Guid.Empty) == 0) ? null : mailboxGuid.ToByteArray(), (mdbGuid.CompareTo(Guid.Empty) == 0) ? null : mdbGuid.ToByteArray(), out correctServerDn, clientIdentityAs.hAuthZ, array, array2, userDnAs, ulLcidString, ulLcidSort, ulCpid, unifiedLogon, applicationId, tenantHint, (tenantHint != null) ? tenantHint.Length : 0, out exMapiStore);
				}
				else
				{
					num2 = this.iExRpcConnection.OpenMsgStore(num, (long)storeFlags, mailboxDn, (mailboxGuid.CompareTo(Guid.Empty) == 0) ? null : mailboxGuid.ToByteArray(), (mdbGuid.CompareTo(Guid.Empty) == 0) ? null : mdbGuid.ToByteArray(), out correctServerDn, (clientIdentityAs != null) ? clientIdentityAs.hToken : ((IntPtr)null), null, null, userDnAs, ulLcidString, ulLcidSort, ulCpid, unifiedLogon, applicationId, tenantHint, (tenantHint != null) ? tenantHint.Length : 0, out exMapiStore);
				}
				if (num2 < 0)
				{
					try
					{
						MapiExceptionHelper.ThrowIfError("Unable to open message store.", num2, (SafeExInterfaceHandle)this.iExRpcConnection, this.InternalLowLevelException);
					}
					catch (MapiExceptionWrongServer)
					{
					}
				}
				if (exMapiStore == null || exMapiStore.IsInvalid)
				{
					result = null;
				}
				else
				{
					mapiStore = new MapiStore(exMapiStore, null, this, applicationId);
					exMapiStore = null;
					lock (this)
					{
						if (this.mapiStoreList.Count >= 256)
						{
							throw MapiExceptionHelper.ExceededMapiStoreLimitException("Can only have 256 MapiStore objects open on same connection.");
						}
						this.mapiStoreList.Add(mapiStore);
					}
					flag = true;
					result = mapiStore;
				}
			}
			finally
			{
				exMapiStore.DisposeIfValid();
				if (!flag && mapiStore != null)
				{
					mapiStore.Dispose();
				}
				this.Unlock();
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000040DC File Offset: 0x000022DC
		internal void SendAuxBuffer(int ulFlags, byte[] auxBuffer, bool forceSend)
		{
			int num = this.iExRpcConnection.SendAuxBuffer(ulFlags, auxBuffer.Length, auxBuffer, forceSend ? 1 : 0);
			if (num != 0)
			{
				base.ThrowIfError("Unable to set/send auxillary buffer on connection.", num);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004110 File Offset: 0x00002310
		internal void FlushRPCBuffer(bool forceSend)
		{
			int num = this.iExRpcConnection.FlushRPCBuffer(forceSend);
			if (num != 0)
			{
				base.ThrowIfError("Unable to flush ROP buffer on connection.", num);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000413C File Offset: 0x0000233C
		internal bool IsDead
		{
			get
			{
				bool result = false;
				if (!base.IsDisposed && this.iExRpcConnection != null)
				{
					int num = this.iExRpcConnection.IsDead(out result);
					if (num != 0)
					{
						base.ThrowIfError("Unable to get connection dead state.", num);
					}
				}
				else
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004180 File Offset: 0x00002380
		internal bool IsMapiMT
		{
			get
			{
				bool result;
				int num = this.iExRpcConnection.IsMapiMT(out result);
				if (num != 0)
				{
					base.ThrowIfError("Unable to find if the connection is using MapiMT interface.", num);
				}
				return result;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000041AB File Offset: 0x000023AB
		internal static void SetForceMapiRpc(bool forceMapiRpc)
		{
			NativeMethods.SetForceMapiRpc(forceMapiRpc);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000041B4 File Offset: 0x000023B4
		internal bool IsConnectedToMapiServer
		{
			get
			{
				bool result;
				int num = this.iExRpcConnection.IsConnectedToMapiServer(out result);
				if (num != 0)
				{
					base.ThrowIfError("Unable to get if the connection is accessing MapiServer or Store.", num);
				}
				return result;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000041E0 File Offset: 0x000023E0
		internal bool RpcSentToServer
		{
			get
			{
				bool result = false;
				if (!base.IsDisposed && this.iExRpcConnection != null)
				{
					int num = this.iExRpcConnection.RpcSentToServer(out result);
					if (num != 0)
					{
						base.ThrowIfError("Unable to determine if we should trace the RPC stack.", num);
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004224 File Offset: 0x00002424
		private StackTrace GetOwningThreadStack()
		{
			StackTrace result = null;
			lock (this)
			{
				if (this.owningThread != null)
				{
					try
					{
						if (this.owningThread.ManagedThreadId != Environment.CurrentManagedThreadId)
						{
							this.owningThread.Suspend();
						}
						try
						{
							result = new StackTrace(this.owningThread, true);
						}
						finally
						{
							if (this.owningThread.ManagedThreadId != Environment.CurrentManagedThreadId)
							{
								this.owningThread.Resume();
							}
						}
					}
					catch
					{
					}
				}
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042CC File Offset: 0x000024CC
		private void CrashOnCallTimeout(object state)
		{
			StackTrace owningThreadStack = this.GetOwningThreadStack();
			Exception exception = new TimeoutException(string.Format("MAPI call timed out after {0}, thread {1}, stack {2}", this.CrashTimeout, (this.owningThread != null) ? this.owningThread.ManagedThreadId : -1, (owningThreadStack != null) ? owningThreadStack.ToString() : ""));
			if (this.owningThread != null)
			{
				this.owningThread.Abort();
				this.owningThread.Join(TimeSpan.FromMinutes(2.0));
			}
			ExWatson.SendReportAndCrashOnAnotherThread(exception);
		}

		// Token: 0x0400008B RID: 139
		private static readonly TimeSpan MaxCallTimeout = TimeSpan.FromDays(1.0);

		// Token: 0x0400008C RID: 140
		private IExRpcConnectionInterface iExRpcConnection;

		// Token: 0x0400008D RID: 141
		private int versionMajor;

		// Token: 0x0400008E RID: 142
		private int versionMinor;

		// Token: 0x0400008F RID: 143
		private int buildMajor;

		// Token: 0x04000090 RID: 144
		private int buildMinor;

		// Token: 0x04000091 RID: 145
		private List<MapiStore> mapiStoreList;

		// Token: 0x04000092 RID: 146
		private uint threadLockCount;

		// Token: 0x04000093 RID: 147
		private Thread owningThread;

		// Token: 0x04000094 RID: 148
		private DateTime creationTime;

		// Token: 0x04000095 RID: 149
		private ApartmentState apartmentState;

		// Token: 0x04000096 RID: 150
		private int creationThreadId;

		// Token: 0x04000097 RID: 151
		private WebServiceConnection webServiceConnection;

		// Token: 0x04000098 RID: 152
		private Timer callTimeoutTimer;
	}
}
