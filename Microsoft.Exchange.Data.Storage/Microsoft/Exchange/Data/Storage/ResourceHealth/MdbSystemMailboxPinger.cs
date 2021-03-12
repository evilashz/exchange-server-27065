using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B31 RID: 2865
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbSystemMailboxPinger : DisposeTrackableBase, IMdbSystemMailboxPinger
	{
		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x001B5C9F File Offset: 0x001B3E9F
		// (set) Token: 0x060067A5 RID: 26533 RVA: 0x001B5CA6 File Offset: 0x001B3EA6
		public static Func<Guid, MdbSystemMailboxPinger, bool> OnTestPing { get; set; }

		// Token: 0x060067A6 RID: 26534 RVA: 0x001B5CB0 File Offset: 0x001B3EB0
		private static TimeSpan ReadPingTimeoutFromConfig()
		{
			TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry("PingTimeoutInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(120.0), ExTraceGlobals.DatabasePingerTracer);
			return timeSpanAppSettingsEntry.Value;
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x060067A7 RID: 26535 RVA: 0x001B5CE2 File Offset: 0x001B3EE2
		// (set) Token: 0x060067A8 RID: 26536 RVA: 0x001B5CE9 File Offset: 0x001B3EE9
		internal static Action<Guid, TimeSpan> OnPingTimeout { get; set; }

		// Token: 0x060067A9 RID: 26537 RVA: 0x001B5CF1 File Offset: 0x001B3EF1
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MdbSystemMailboxPinger>(this);
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x001B5CFC File Offset: 0x001B3EFC
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (this.instanceLock)
				{
					this.DisposeAccessInfo();
					if (this.registeredWaitHandle != null)
					{
						this.registeredWaitHandle.Unregister(this.remoteCallDoneEvent);
						this.registeredWaitHandle = null;
					}
					this.remoteCallDoneEvent.Close();
					this.remoteCallDoneEvent = null;
				}
			}
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x001B5D74 File Offset: 0x001B3F74
		public MdbSystemMailboxPinger(Guid databaseGuid)
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("databaseGuid cannot be Guid.Empty", "databaseGuid");
			}
			this.waitOrTimerCallback = new WaitOrTimerCallback(this.TimeoutCallback);
			this.databaseGuid = databaseGuid;
			this.systemMailboxName = "SystemMailbox{" + this.databaseGuid + "}";
			ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Creating MdbSystemMailboxPinger for private mdb guid {0}", this.databaseGuid);
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x060067AC RID: 26540 RVA: 0x001B5E43 File Offset: 0x001B4043
		// (set) Token: 0x060067AD RID: 26541 RVA: 0x001B5E4A File Offset: 0x001B404A
		internal static TimeSpan PingTimeout
		{
			get
			{
				return MdbSystemMailboxPinger.pingTimeout;
			}
			set
			{
				MdbSystemMailboxPinger.pingTimeout = value;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x060067AE RID: 26542 RVA: 0x001B5E52 File Offset: 0x001B4052
		public DateTime LastSuccessfulPingUtc
		{
			get
			{
				base.CheckDisposed();
				return this.lastSuccessfulPingUtc;
			}
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x060067AF RID: 26543 RVA: 0x001B5E60 File Offset: 0x001B4060
		// (set) Token: 0x060067B0 RID: 26544 RVA: 0x001B5E6E File Offset: 0x001B406E
		public bool Pinging
		{
			get
			{
				base.CheckDisposed();
				return this.pinging;
			}
			private set
			{
				this.pinging = value;
			}
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x001B5E77 File Offset: 0x001B4077
		public DateTime LastPingAttemptUtc
		{
			get
			{
				base.CheckDisposed();
				return this.lastPingAttemptUtc;
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x001B5E88 File Offset: 0x001B4088
		public bool Ping()
		{
			base.CheckDisposed();
			bool flag = false;
			bool flag2 = false;
			this.lastPingAttemptUtc = TimeProvider.UtcNow;
			try
			{
				if (!this.Pinging)
				{
					lock (this.instanceLock)
					{
						if (!this.Pinging)
						{
							this.Pinging = true;
							flag2 = true;
						}
					}
				}
				if (flag2)
				{
					ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Attempting XSO ping against database {0}", this.databaseGuid);
					lock (this.instanceLock)
					{
						PerformanceContext performanceContext = default(PerformanceContext);
						bool flag5 = false;
						bool flag6 = ExTraceGlobals.DatabasePingerTracer.IsTraceEnabled(TraceType.DebugTrace);
						if (flag6)
						{
							flag5 = NativeMethods.GetTLSPerformanceContext(out performanceContext);
						}
						DateTime utcNow = TimeProvider.UtcNow;
						flag = this.OpenSession();
						if (flag)
						{
							PingerPerfCounterWrapper.PingSuccessful();
						}
						else
						{
							PingerPerfCounterWrapper.PingFailed();
						}
						TimeSpan timeSpan = TimeProvider.UtcNow - utcNow;
						PerformanceContext performanceContext2;
						if (flag6 && flag5 && NativeMethods.GetTLSPerformanceContext(out performanceContext2))
						{
							uint num = performanceContext2.rpcCount - performanceContext.rpcCount;
							ulong num2 = performanceContext2.rpcLatency - performanceContext.rpcLatency;
							ExTraceGlobals.DatabasePingerTracer.TraceDebug((long)this.GetHashCode(), "Ping Stats - Mdb: {0}, Rpc Count: {1}, Rpc Latency: {2}. Elapsed: {3}", new object[]
							{
								this.databaseGuid,
								num,
								num2,
								timeSpan
							});
						}
						goto IL_17F;
					}
				}
				ExTraceGlobals.DatabasePingerTracer.TraceError<Guid>((long)this.GetHashCode(), "Could not make ping call against mdb {0} because there is already an outstanding ping.", this.databaseGuid);
				IL_17F:;
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<Guid, StoragePermanentException>((long)this.GetHashCode(), "Encountered permanent exception acquiring and pinging mdb {0}.  Exception: {1}", this.databaseGuid, arg);
				this.principal = null;
				this.DisposeAccessInfo();
				this.pingerState = MdbSystemMailboxPinger.PingerState.NeedReinitialization;
				PingerPerfCounterWrapper.PingFailed();
			}
			catch (StorageTransientException arg2)
			{
				PingerPerfCounterWrapper.PingFailed();
				ExTraceGlobals.DatabasePingerTracer.TraceError<Guid, StorageTransientException>((long)this.GetHashCode(), "Encountered transient exception acquiring and pinging mdb {0}.  Exception: {1}", this.databaseGuid, arg2);
			}
			catch (Exception)
			{
				PingerPerfCounterWrapper.PingFailed();
				throw;
			}
			finally
			{
				if (flag2)
				{
					lock (this.instanceLock)
					{
						this.Pinging = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x001B615C File Offset: 0x001B435C
		private MailboxSession GetMailboxSession()
		{
			if (MdbSystemMailboxPinger.OnTestPing != null)
			{
				return null;
			}
			return MailboxSession.ConfigurableOpen(this.principal, this.accessInfo, CultureInfo.InvariantCulture, "Client=ResourceHealth;Action=DatabasePing", LogonType.SystemService, MdbSystemMailboxPinger.LocalePropertyDefinition, MailboxSession.InitializationFlags.None, Array<DefaultFolderType>.Empty);
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x001B6190 File Offset: 0x001B4390
		private bool OpenSession()
		{
			bool flag = false;
			if (this.AcquireADObjectsForPrivateMdb())
			{
				using (MailboxSession mailboxSession = this.GetMailboxSession())
				{
					bool flag2 = false;
					try
					{
						lock (this.instanceLock)
						{
							if (this.registeredWaitHandle == null && this.remoteCallDoneEvent != null)
							{
								this.remoteCallDoneEvent.Reset();
								this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.remoteCallDoneEvent, this.waitOrTimerCallback, null, MdbSystemMailboxPinger.PingTimeout, true);
								flag2 = true;
							}
						}
						if (MdbSystemMailboxPinger.OnTestPing != null)
						{
							flag = MdbSystemMailboxPinger.OnTestPing(this.databaseGuid, this);
						}
						else
						{
							mailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
							{
								MailboxSchema.QuotaUsedExtended
							});
							flag = true;
						}
						if (flag)
						{
							this.pingerState = MdbSystemMailboxPinger.PingerState.Normal;
						}
					}
					finally
					{
						if (flag2)
						{
							this.UnregisterWaitHandle();
						}
					}
				}
			}
			if (flag)
			{
				this.lastSuccessfulPingUtc = TimeProvider.UtcNow;
				ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Successfully pinged database {0}", this.databaseGuid);
			}
			return flag;
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x001B62C0 File Offset: 0x001B44C0
		private void TimeoutCallback(object state, bool timedOut)
		{
			this.UnregisterWaitHandle();
			TimeSpan timeSpan = TimeProvider.UtcNow - this.lastPingAttemptUtc;
			if (timeSpan < MdbSystemMailboxPinger.PingTimeout)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<TimeSpan, TimeSpan, Guid>((long)this.GetHashCode(), "Timeout was called before PingTimeout reached.  Ignoring.  Elapsed: {0}, PingTimeout: {1}, Database: {2}", timeSpan, MdbSystemMailboxPinger.PingTimeout, this.databaseGuid);
				return;
			}
			if (timedOut)
			{
				PingerPerfCounterWrapper.PingTimedOut();
				if (MdbSystemMailboxPinger.OnPingTimeout != null)
				{
					MdbSystemMailboxPinger.OnPingTimeout(this.databaseGuid, MdbSystemMailboxPinger.PingTimeout);
				}
				ExTraceGlobals.DatabasePingerTracer.TraceError<Guid>((long)this.GetHashCode(), "Ping for Mdb '{0}' timed out.  This might suggest that the remote server is down.", this.databaseGuid);
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorDatabasePingTimedOut, string.Empty, new object[]
				{
					this.databaseGuid,
					timeSpan,
					(this.lastSuccessfulPingUtc == DateTime.MinValue) ? "[never]" : this.lastSuccessfulPingUtc.ToString()
				});
			}
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x001B63B8 File Offset: 0x001B45B8
		private void UnregisterWaitHandle()
		{
			lock (this.instanceLock)
			{
				if (this.registeredWaitHandle != null && this.remoteCallDoneEvent != null)
				{
					this.registeredWaitHandle.Unregister(this.remoteCallDoneEvent);
					this.registeredWaitHandle = null;
				}
			}
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x001B641C File Offset: 0x001B461C
		private bool AcquireADObjectsForPrivateMdb()
		{
			if (MdbSystemMailboxPinger.OnTestPing != null)
			{
				return true;
			}
			bool flag = false;
			try
			{
				if (this.principal != null)
				{
					if (!this.VerifyLocalBoxCall(this.principal.MailboxInfo.Location.ServerFqdn))
					{
						return false;
					}
					flag = true;
					return true;
				}
				else
				{
					if ((this.pingerState == MdbSystemMailboxPinger.PingerState.NeedReinitialization || this.pingerState == MdbSystemMailboxPinger.PingerState.NotInitialized) && TimeProvider.UtcNow - this.lastSessionAttemptUtc < MdbSystemMailboxPinger.OpenSessionAttemptInterval)
					{
						ExTraceGlobals.DatabasePingerTracer.TraceDebug((long)this.GetHashCode(), "Need to acquire principal, but not enough time has passed between attempts.");
						return false;
					}
					this.lastSessionAttemptUtc = TimeProvider.UtcNow;
					ActiveManager noncachingActiveManagerInstance = ActiveManager.GetNoncachingActiveManagerInstance();
					DatabaseLocationInfo serverForDatabase = noncachingActiveManagerInstance.GetServerForDatabase(this.databaseGuid, true);
					if (!this.VerifyLocalBoxCall(serverForDatabase.ServerFqdn))
					{
						return false;
					}
					ADSessionSettings adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					ADSystemMailbox adSystemMailbox = this.FindSystemMailbox(adsessionSettings);
					Server server = this.FindMdbServer(adsessionSettings, serverForDatabase.ServerFqdn);
					if (server == null)
					{
						ExTraceGlobals.DatabasePingerTracer.TraceError<string>((long)this.GetHashCode(), "[MdbSystemMailboxPinger.AcquireADObjectsForPrivateMdb] Failed to find server with FQDN: '{0}'", serverForDatabase.ServerFqdn);
						return false;
					}
					this.principal = ExchangePrincipal.FromADSystemMailbox(adsessionSettings, adSystemMailbox, server);
					this.accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
					flag = true;
				}
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "Encountered StoragePermanentException obtaining ExchangePrincipal.  Exception: {0}", arg);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "Encountered StorageTransientException obtaining ExchangePrincipal.  Exception: {0}", arg2);
			}
			catch (DataSourceOperationException arg3)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<DataSourceOperationException>((long)this.GetHashCode(), "EncounteredDataSourceOperationException obtaining ExchangePrincipal.  Exception :{0}", arg3);
			}
			catch (DataSourceTransientException arg4)
			{
				ExTraceGlobals.DatabasePingerTracer.TraceError<DataSourceTransientException>((long)this.GetHashCode(), "Encountered DataSourceTransientException obtaining ExchangePrincipal.  Exception :{0}", arg4);
			}
			finally
			{
				if (!flag)
				{
					this.principal = null;
					this.DisposeAccessInfo();
				}
			}
			return flag;
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x001B6660 File Offset: 0x001B4860
		private bool VerifyLocalBoxCall(string serverFqdn)
		{
			if (serverFqdn == null || !serverFqdn.StartsWith(Environment.MachineName + ".", StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.DatabasePingerTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "[MdbSystemMailboxPinger.VerifyLocalBoxCall] Will not ping database '{0}' since it is on a different server: '{1}'", this.databaseGuid, serverFqdn);
				return false;
			}
			return true;
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x001B66A0 File Offset: 0x001B48A0
		private void DisposeAccessInfo()
		{
			lock (this.instanceLock)
			{
				if (this.accessInfo != null)
				{
					if (this.accessInfo.AuthenticatedUserPrincipal != null)
					{
						IDisposable disposable = this.accessInfo.AuthenticatedUserPrincipal.Identity as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					this.accessInfo = null;
				}
			}
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x001B673C File Offset: 0x001B493C
		private Server FindMdbServer(ADSessionSettings settings, string serverFQDN)
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, settings, 764, "FindMdbServer", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ResourceHealth\\MdbSystemMailboxPinger.cs");
			Server server = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				server = session.FindServerByFqdn(serverFQDN);
			});
			if (adoperationResult.Succeeded)
			{
				return server;
			}
			ExTraceGlobals.DatabasePingerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "[MdbSystmeMailboxPinger.FindMdbServer] Encountered exception looking up server by Fqdn '{0}'.  Exception: {1}", serverFQDN, adoperationResult.Exception);
			return null;
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x001B67F8 File Offset: 0x001B49F8
		private ADSystemMailbox FindSystemMailbox(ADSessionSettings settings)
		{
			IRootOrganizationRecipientSession session = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, settings, 796, "FindSystemMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ResourceHealth\\MdbSystemMailboxPinger.cs");
			ADRecipient[] adRecipients = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				adRecipients = session.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.systemMailboxName), null, 1);
			});
			if (!adoperationResult.Succeeded)
			{
				throw adoperationResult.Exception;
			}
			if (adRecipients.Length != 1 || !(adRecipients[0] is ADSystemMailbox))
			{
				throw new ObjectNotFoundException(ServerStrings.AdUserNotFoundException(string.Format("SystemMailbox {0} was not found", this.systemMailboxName)));
			}
			return (ADSystemMailbox)adRecipients[0];
		}

		// Token: 0x04003AB6 RID: 15030
		private const int DefaultPingTimeoutSeconds = 120;

		// Token: 0x04003AB7 RID: 15031
		private const string MapiClientIdAndAction = "Client=ResourceHealth;Action=DatabasePing";

		// Token: 0x04003AB8 RID: 15032
		private static readonly TimeSpan OpenSessionAttemptInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04003AB9 RID: 15033
		private static readonly PropertyDefinition[] LocalePropertyDefinition = new PropertyDefinition[]
		{
			MailboxSchema.LocaleId
		};

		// Token: 0x04003ABA RID: 15034
		private static TimeSpan pingTimeout = MdbSystemMailboxPinger.ReadPingTimeoutFromConfig();

		// Token: 0x04003ABB RID: 15035
		private ManualResetEvent remoteCallDoneEvent = new ManualResetEvent(false);

		// Token: 0x04003ABC RID: 15036
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04003ABD RID: 15037
		private bool pinging;

		// Token: 0x04003ABE RID: 15038
		private Guid databaseGuid;

		// Token: 0x04003ABF RID: 15039
		private object instanceLock = new object();

		// Token: 0x04003AC0 RID: 15040
		private DateTime lastSuccessfulPingUtc = DateTime.MinValue;

		// Token: 0x04003AC1 RID: 15041
		private DateTime lastPingAttemptUtc = DateTime.MinValue;

		// Token: 0x04003AC2 RID: 15042
		private DateTime lastSessionAttemptUtc = TimeProvider.UtcNow.Add(-MdbSystemMailboxPinger.OpenSessionAttemptInterval);

		// Token: 0x04003AC3 RID: 15043
		private MdbSystemMailboxPinger.PingerState pingerState;

		// Token: 0x04003AC4 RID: 15044
		private ExchangePrincipal principal;

		// Token: 0x04003AC5 RID: 15045
		private MailboxAccessInfo accessInfo;

		// Token: 0x04003AC6 RID: 15046
		private string systemMailboxName;

		// Token: 0x04003AC7 RID: 15047
		private WaitOrTimerCallback waitOrTimerCallback;

		// Token: 0x02000B32 RID: 2866
		private enum PingerState
		{
			// Token: 0x04003ACB RID: 15051
			NotInitialized,
			// Token: 0x04003ACC RID: 15052
			NeedReinitialization,
			// Token: 0x04003ACD RID: 15053
			Normal
		}
	}
}
