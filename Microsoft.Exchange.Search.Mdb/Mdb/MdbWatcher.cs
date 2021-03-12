using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000023 RID: 35
	internal sealed class MdbWatcher : IDisposeTrackable, IMdbWatcher, IDisposable
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000070DC File Offset: 0x000052DC
		public MdbWatcher()
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession(base.GetType().Name, ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.MdbWatcherTracer, (long)this.GetHashCode());
			this.localServer = AdDataProvider.Create(this.diagnosticsSession).GetLocalServer();
			this.dbCache = DatabaseCache.Create(this.diagnosticsSession);
			this.RegisterDatabaseChangeNotification(new ADNotificationCallback(this.ADCallback));
			this.diagnosticsSession.TraceDebug<string>("Successfully found mailbox server object: {0}", this.localServer.Fqdn);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000E8 RID: 232 RVA: 0x0000717C File Offset: 0x0000537C
		// (remove) Token: 0x060000E9 RID: 233 RVA: 0x000071B4 File Offset: 0x000053B4
		public event EventHandler Changed;

		// Token: 0x060000EA RID: 234 RVA: 0x000071E9 File Offset: 0x000053E9
		public IMdbCollection GetDatabases()
		{
			return new MdbWatcher.MdbCollection(this.localServer, this.diagnosticsSession);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000071FC File Offset: 0x000053FC
		public bool Exists(Guid mdbGuid)
		{
			return this.dbCache.DatabaseExists(mdbGuid);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000720A File Offset: 0x0000540A
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MdbWatcher>(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007212 File Offset: 0x00005412
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007221 File Offset: 0x00005421
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007236 File Offset: 0x00005436
		private void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.UnregisterDatabaseChangeNotification();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000725C File Offset: 0x0000545C
		private void OnChanged(EventArgs e)
		{
			EventHandler eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Changed, null, null);
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007284 File Offset: 0x00005484
		private void RegisterDatabaseChangeNotification(ADNotificationCallback notifyDatabasesChanged)
		{
			Util.ThrowOnNullArgument(notifyDatabasesChanged, "notifyDatabasesChanged");
			try
			{
				AdDataProvider adDataProvider = AdDataProvider.Create(this.diagnosticsSession);
				this.cookie = adDataProvider.RegisterChangeNotification(notifyDatabasesChanged);
			}
			catch (ComponentException arg)
			{
				this.diagnosticsSession.TraceError<ComponentException>("Failed to register database change notification. Exception={0}", arg);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000072DC File Offset: 0x000054DC
		private void UnregisterDatabaseChangeNotification()
		{
			try
			{
				AdDataProvider adDataProvider = AdDataProvider.Create(this.diagnosticsSession);
				adDataProvider.UnRegisterChangeNotification(this.cookie);
			}
			catch (ComponentException arg)
			{
				this.diagnosticsSession.TraceError<ComponentException>("Failed to unregister database change notification. Exception={0}", arg);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007328 File Offset: 0x00005528
		private void ADCallback(ADNotificationEventArgs args)
		{
			this.OnChanged(EventArgs.Empty);
		}

		// Token: 0x040000A7 RID: 167
		private readonly Server localServer;

		// Token: 0x040000A8 RID: 168
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000A9 RID: 169
		private readonly DatabaseCache dbCache;

		// Token: 0x040000AA RID: 170
		private DisposeTracker disposeTracker;

		// Token: 0x040000AB RID: 171
		private ADNotificationRequestCookie cookie;

		// Token: 0x02000024 RID: 36
		internal class MdbCollection : IMdbCollection
		{
			// Token: 0x060000F4 RID: 244 RVA: 0x00007335 File Offset: 0x00005535
			internal MdbCollection(Server localServer, IDiagnosticsSession diagnosticsSession)
			{
				this.localServer = localServer;
				this.diagnosticsSession = diagnosticsSession;
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000734B File Offset: 0x0000554B
			public IEnumerable<MdbInfo> Databases
			{
				get
				{
					return this.MdbInfos;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007354 File Offset: 0x00005554
			private MailboxDatabase[] MailboxDatabases
			{
				get
				{
					if (this.mailboxDatabases == null)
					{
						AdDataProvider adDataProvider = AdDataProvider.Create(this.diagnosticsSession);
						this.mailboxDatabases = adDataProvider.GetLocalMailboxDatabases(this.localServer);
					}
					return this.mailboxDatabases;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007390 File Offset: 0x00005590
			private MdbInfo[] MdbInfos
			{
				get
				{
					if (this.mdbInfos == null)
					{
						this.mdbInfos = new MdbInfo[this.MailboxDatabases.Length];
						for (int i = 0; i < this.MailboxDatabases.Length; i++)
						{
							this.mdbInfos[i] = new MdbInfo(this.MailboxDatabases[i]);
						}
					}
					return this.mdbInfos;
				}
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x000073E8 File Offset: 0x000055E8
			public void UpdateDatabasesIndexStatusInfo(int numberOfCopiesToIndexPerDatabase)
			{
				ServerSchemaVersionSource serverSchemaVersionSource = new ServerSchemaVersionSource(this.localServer.Guid, this.diagnosticsSession);
				HashSet<Guid> hashSet = new HashSet<Guid>();
				foreach (MailboxDatabase mailboxDatabase in this.MailboxDatabases)
				{
					foreach (KeyValuePair<ADObjectId, int> keyValuePair in mailboxDatabase.ActivationPreference)
					{
						if (keyValuePair.Key.ObjectGuid != this.localServer.Guid)
						{
							hashSet.Add(keyValuePair.Key.ObjectGuid);
						}
					}
				}
				serverSchemaVersionSource.LoadVersions(hashSet);
				for (int k = 0; k < this.MailboxDatabases.Length; k++)
				{
					MailboxDatabase mailboxDatabase2 = this.MailboxDatabases[k];
					MdbInfo mdbInfo = this.MdbInfos[k];
					this.diagnosticsSession.Assert(object.Equals(mdbInfo.Guid, mailboxDatabase2.Guid), "MdbGuid must match", new object[0]);
					mdbInfo.ActivationPreference = this.GetOrdinalOfDatabaseCopyOnLocalServer(mailboxDatabase2);
					mdbInfo.PreferredActiveCopy = (mdbInfo.ActivationPreference == 1);
					mdbInfo.DatabaseCopies = MdbWatcher.MdbCollection.GetDatabaseCopies(serverSchemaVersionSource, mailboxDatabase2.ActivationPreference);
					mdbInfo.MaxSupportedVersion = MdbWatcher.MdbCollection.GetMaxFeedingVersion(mdbInfo.DatabaseCopies);
					if (mdbInfo.ActivationPreference > numberOfCopiesToIndexPerDatabase)
					{
						this.diagnosticsSession.TraceDebug<MailboxDatabase, int, int>("The ordinal of '{0}' copy on this server is {1} (> Threshold {2})", mailboxDatabase2, mdbInfo.ActivationPreference, numberOfCopiesToIndexPerDatabase);
						mdbInfo.NotIndexed = IndexStatusErrorCode.ActivationPreferenceSkipped;
					}
					else if (mailboxDatabase2.Recovery)
					{
						this.diagnosticsSession.TraceDebug<MailboxDatabase>("'{0}' is a recovery mailbox database", mailboxDatabase2);
						mdbInfo.NotIndexed = IndexStatusErrorCode.RecoveryDatabaseSkipped;
					}
					else if (!mailboxDatabase2.IndexEnabled)
					{
						this.diagnosticsSession.TraceDebug<MailboxDatabase>("'{0}' is not index-enabled", mailboxDatabase2);
						mdbInfo.NotIndexed = IndexStatusErrorCode.IndexNotEnabled;
					}
					else
					{
						mdbInfo.NotIndexed = IndexStatusErrorCode.Unknown;
					}
				}
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00007650 File Offset: 0x00005850
			public void UpdateDatabasesCopyStatusInfo()
			{
				Guid[] dbGuids = new Guid[this.MdbInfos.Length];
				for (int i = 0; i < this.MdbInfos.Length; i++)
				{
					dbGuids[i] = this.MdbInfos[i].Guid;
				}
				RpcDatabaseCopyStatus2[] statusResults = null;
				RpcErrorExceptionInfo errorInfo = null;
				TasksRpcExceptionWrapper.Instance.ClientRetryableOperation(this.localServer.Fqdn, delegate
				{
					using (ReplayRpcClient replayRpcClient = new ReplayRpcClient(this.localServer.Fqdn))
					{
						this.diagnosticsSession.TraceDebug<string>("Now making RpccGetCopyStatusBasic() RPC to server {0}.", this.localServer.Fqdn);
						errorInfo = replayRpcClient.RpccGetCopyStatusEx4(RpcGetDatabaseCopyStatusFlags2.None, dbGuids, ref statusResults);
					}
				});
				TasksRpcExceptionWrapper.Instance.ClientRethrowIfFailed(null, this.localServer.Fqdn, errorInfo);
				if (statusResults == null || statusResults.Length == 0)
				{
					this.diagnosticsSession.TraceDebug<string>("No CopyStatus returned for server {0}.", this.localServer.Fqdn);
					return;
				}
				Dictionary<Guid, RpcDatabaseCopyStatus2> dictionary = new Dictionary<Guid, RpcDatabaseCopyStatus2>(statusResults.Length);
				for (int j = 0; j < statusResults.Length; j++)
				{
					dictionary[statusResults[j].DBGuid] = statusResults[j];
				}
				for (int k = 0; k < this.MdbInfos.Length; k++)
				{
					MdbInfo mdbInfo = this.MdbInfos[k];
					RpcDatabaseCopyStatus2 rpcDatabaseCopyStatus;
					if (dictionary.TryGetValue(mdbInfo.Guid, out rpcDatabaseCopyStatus))
					{
						this.diagnosticsSession.TraceDebug<CopyStatusEnum, MdbInfo>("Get CopyStatus '{0}' for mdb {1}", rpcDatabaseCopyStatus.CopyStatus, mdbInfo);
						switch (rpcDatabaseCopyStatus.CopyStatus)
						{
						case CopyStatusEnum.FailedAndSuspended:
						case CopyStatusEnum.Suspended:
							mdbInfo.IsSuspended = true;
							break;
						case CopyStatusEnum.Seeding:
							goto IL_18B;
						case CopyStatusEnum.Mounting:
						case CopyStatusEnum.Mounted:
							mdbInfo.MountedOnLocalServer = true;
							break;
						default:
							goto IL_18B;
						}
						IL_19B:
						mdbInfo.IsLagCopy = (rpcDatabaseCopyStatus.ConfiguredReplayLagTime > TimeSpan.Zero);
						goto IL_1C7;
						IL_18B:
						mdbInfo.IsSuspended = false;
						mdbInfo.MountedOnLocalServer = false;
						goto IL_19B;
					}
					this.diagnosticsSession.TraceDebug<MdbInfo>("GetCopyStatus() didn't find replica instance for mdb {0}", mdbInfo);
					IL_1C7:;
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00007838 File Offset: 0x00005A38
			internal static List<MdbCopy> GetDatabaseCopies(ServerSchemaVersionSource serverSchemaVersionSource, KeyValuePair<ADObjectId, int>[] servers)
			{
				List<MdbCopy> list = new List<MdbCopy>(servers.Length);
				foreach (KeyValuePair<ADObjectId, int> keyValuePair in servers)
				{
					int serverVersion = serverSchemaVersionSource.GetServerVersion(keyValuePair.Key.ObjectGuid);
					list.Add(new MdbCopy(keyValuePair.Key.Name, keyValuePair.Value, serverVersion));
				}
				return list;
			}

			// Token: 0x060000FB RID: 251 RVA: 0x000078A4 File Offset: 0x00005AA4
			internal static int GetMaxFeedingVersion(ICollection<MdbCopy> copies)
			{
				VersionInfo latest = VersionInfo.Latest;
				int num = latest.FeedingVersion;
				foreach (MdbCopy mdbCopy in copies)
				{
					num = Math.Min(num, mdbCopy.SchemaVersion);
				}
				return num;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00007904 File Offset: 0x00005B04
			private int GetOrdinalOfDatabaseCopyOnLocalServer(MailboxDatabase mailboxDatabase)
			{
				int num = -1;
				foreach (KeyValuePair<ADObjectId, int> keyValuePair in mailboxDatabase.ActivationPreference)
				{
					ADObjectId key = keyValuePair.Key;
					if (key.Equals(this.localServer.Identity))
					{
						num = keyValuePair.Value;
						break;
					}
				}
				if (num < 0)
				{
					throw new InvalidOperationException(string.Format("The database '{0}' has no ActivationPreference on server '{1}'", mailboxDatabase, this.localServer.Fqdn));
				}
				int num2 = 1;
				foreach (KeyValuePair<ADObjectId, int> keyValuePair2 in mailboxDatabase.ActivationPreference)
				{
					if (keyValuePair2.Value < num)
					{
						num2++;
					}
				}
				return num2;
			}

			// Token: 0x040000AD RID: 173
			private readonly Server localServer;

			// Token: 0x040000AE RID: 174
			private readonly IDiagnosticsSession diagnosticsSession;

			// Token: 0x040000AF RID: 175
			private MailboxDatabase[] mailboxDatabases;

			// Token: 0x040000B0 RID: 176
			private MdbInfo[] mdbInfos;
		}
	}
}
