using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SubscriptionCompletion;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionCompletionServer : SubscriptionCompletionRpcServer
	{
		// Token: 0x06000305 RID: 773 RVA: 0x000154CD File Offset: 0x000136CD
		public SubscriptionCompletionServer()
		{
			this.syncLogSession = ContentAggregationConfig.SyncLogSession;
			this.subscriptionCompletionDataUnpacker = new SubscriptionCompletionDataUnpacker(this.syncLogSession);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000154FC File Offset: 0x000136FC
		public override byte[] SubscriptionComplete(int version, byte[] inputBlob)
		{
			byte[] result;
			try
			{
				if (this.stopped)
				{
					result = RpcHelper.CreateResponsePropertyCollection(2835349507U, 268435458);
				}
				else
				{
					SubscriptionCompletionData state;
					uint num = this.subscriptionCompletionDataUnpacker.UnpackData(inputBlob, out state);
					if (num == 0U)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.UpdateSubscription), state);
						this.syncLogSession.LogDebugging((TSLID)421UL, SubscriptionCompletionServer.tracer, (long)this.GetHashCode(), "SubscriptionCompletionServer returning success error code.", new object[0]);
					}
					result = RpcHelper.CreateResponsePropertyCollection(2835349507U, (int)num);
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000155AC File Offset: 0x000137AC
		internal static bool TryStartServer()
		{
			if (SubscriptionCompletionServer.completionServerInstance != null)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)422UL, ExTraceGlobals.CacheManagerLookupTracer, 0L, "Subscription Completion RPC Server already registered and running.", new object[0]);
				return true;
			}
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier exchangeServersUsgSid;
			try
			{
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 176, "TryStartServer", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Manager\\RPC\\SubscriptionCompletionServer.cs");
				exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
			}
			catch (LocalizedException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)423UL, ExTraceGlobals.CacheManagerLookupTracer, 0L, "Failed to extract SID for Exchange Servers Usage role due to exception {0}.", new object[]
				{
					ex
				});
				return false;
			}
			FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetAccessRule(fileSystemAccessRule);
			string securityDescriptorSddlForm = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			fileSecurity.SetOwner(securityIdentifier);
			string securityDescriptorSddlForm2 = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			SubscriptionCompletionServer.completionServerInstance = (SubscriptionCompletionServer)RpcServerBase.RegisterServer(typeof(SubscriptionCompletionServer), fileSecurity, 1, false, (uint)ContentAggregationConfig.MaxCompletionThreads);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)424UL, ExTraceGlobals.CacheManagerLookupTracer, 0L, "Subscription Completion RPC Server loaded with exchangerServerSDDL {0}, rpcSDDL {1}.", new object[]
			{
				securityDescriptorSddlForm,
				securityDescriptorSddlForm2
			});
			SubscriptionCompletionServer.completionServerInstance.Start();
			return true;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00015714 File Offset: 0x00013914
		internal static void StopServer()
		{
			if (SubscriptionCompletionServer.completionServerInstance != null)
			{
				SubscriptionCompletionServer.completionServerInstance.Stop();
				SubscriptionCompletionServer.completionServerInstance = null;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00015730 File Offset: 0x00013930
		private void Start()
		{
			lock (this.syncObject)
			{
				this.stopped = false;
				if (this.workThreadsEvent == null)
				{
					this.workThreadsEvent = new ManualResetEvent(true);
				}
			}
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)425UL, SubscriptionCompletionServer.tracer, (long)this.GetHashCode(), "Started the SubscriptionCompletionServer.", new object[0]);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000157B4 File Offset: 0x000139B4
		private void Stop()
		{
			lock (this.syncObject)
			{
				if (this.stopped)
				{
					return;
				}
				this.stopped = true;
			}
			RpcServerBase.StopServer(SubscriptionCompletionRpcServer.RpcIntfHandle);
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)426UL, SubscriptionCompletionServer.tracer, (long)this.GetHashCode(), "Waiting for existing completion server threads to finish.", new object[0]);
			this.workThreadsEvent.WaitOne();
			lock (this.syncObject)
			{
				this.workThreadsEvent.Close();
				this.workThreadsEvent = null;
			}
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)427UL, SubscriptionCompletionServer.tracer, (long)this.GetHashCode(), "Stopped the SubscriptionCompletionServer.", new object[0]);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000158AC File Offset: 0x00013AAC
		private void UpdateSubscription(object state)
		{
			try
			{
				lock (this.syncObject)
				{
					this.numberOfCurrentWorkThreads++;
					if (this.stopped)
					{
						return;
					}
					if (this.numberOfCurrentWorkThreads == 1)
					{
						this.workThreadsEvent.Reset();
					}
				}
				SubscriptionCompletionData subscriptionCompletionData = (SubscriptionCompletionData)state;
				DataAccessLayer.TryReportSubscriptionCompleted(subscriptionCompletionData);
			}
			finally
			{
				lock (this.syncObject)
				{
					this.numberOfCurrentWorkThreads--;
					if (this.numberOfCurrentWorkThreads == 0 && this.workThreadsEvent != null)
					{
						this.workThreadsEvent.Set();
					}
				}
			}
		}

		// Token: 0x040001B3 RID: 435
		private const int DefaultRpcInputArgumentsSize = 7;

		// Token: 0x040001B4 RID: 436
		private static readonly Trace tracer = ExTraceGlobals.SubscriptionCompletionServerTracer;

		// Token: 0x040001B5 RID: 437
		private readonly object syncObject = new object();

		// Token: 0x040001B6 RID: 438
		private readonly GlobalSyncLogSession syncLogSession;

		// Token: 0x040001B7 RID: 439
		private readonly SubscriptionCompletionDataUnpacker subscriptionCompletionDataUnpacker;

		// Token: 0x040001B8 RID: 440
		private static SubscriptionCompletionServer completionServerInstance;

		// Token: 0x040001B9 RID: 441
		private int numberOfCurrentWorkThreads;

		// Token: 0x040001BA RID: 442
		private ManualResetEvent workThreadsEvent;

		// Token: 0x040001BB RID: 443
		private bool stopped;
	}
}
