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
using Microsoft.Exchange.Rpc.SubscriptionNotification;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Notification;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionNotificationServer : SubscriptionNotificationRpcServer
	{
		// Token: 0x170000DA RID: 218
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00015F50 File Offset: 0x00014150
		internal static bool IgnoreNotifications
		{
			set
			{
				SubscriptionNotificationServer.ignoreNotifications = value;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00015F58 File Offset: 0x00014158
		public override byte[] InvokeSubscriptionNotificationEndPoint(int version, byte[] inputBlob)
		{
			if (this.stopped)
			{
				return RpcHelper.CreateResponsePropertyCollection(2835349507U, 2);
			}
			if (version > 1)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)395UL, SubscriptionNotificationServer.tracer, (long)this.GetHashCode(), "Subscription Notification RPC server will return {0} since the current version is {1} and the requested version was {2}.", new object[]
				{
					SubscriptionNotificationResult.ServerVersionMismatch,
					1,
					version
				});
				return RpcHelper.CreateResponsePropertyCollection(2835349507U, 1);
			}
			if (SubscriptionNotificationServer.ignoreNotifications)
			{
				return SubscriptionNotificationServer.SuccessOutput;
			}
			byte[] result;
			try
			{
				result = this.notificationProcessor.InvokeSubscriptionNotificationEndPoint(inputBlob);
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00016014 File Offset: 0x00014214
		internal static bool TryStartServer()
		{
			if (SubscriptionNotificationServer.ignoreNotifications)
			{
				return true;
			}
			if (SubscriptionNotificationServer.notificationServerInstance != null)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)1357UL, SubscriptionNotificationServer.tracer, 0L, "Subscription Notification RPC Server already registered and running.", new object[0]);
				return true;
			}
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier exchangeServersUsgSid;
			try
			{
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 180, "TryStartServer", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Manager\\RPC\\SubscriptionNotificationServer.cs");
				exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
			}
			catch (LocalizedException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)1366UL, SubscriptionNotificationServer.tracer, 0L, "Failed to extract SID for Exchange Servers Usage role due to exception {0}.", new object[]
				{
					ex
				});
				return false;
			}
			FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetAccessRule(fileSystemAccessRule);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			fileSecurity.SetOwner(securityIdentifier);
			string securityDescriptorSddlForm = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			SubscriptionNotificationServer.notificationServerInstance = (SubscriptionNotificationServer)RpcServerBase.RegisterServer(typeof(SubscriptionNotificationServer), fileSecurity, 1, false, (uint)ContentAggregationConfig.MaxNotificationThreads);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)1367UL, SubscriptionNotificationServer.tracer, 0L, "Subscription Notification RPC Server loaded with rpcSDDL {0}.", new object[]
			{
				securityDescriptorSddlForm
			});
			SubscriptionNotificationServer.notificationServerInstance.Start();
			return true;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00016190 File Offset: 0x00014390
		internal static void StopServer()
		{
			if (SubscriptionNotificationServer.ignoreNotifications)
			{
				return;
			}
			if (SubscriptionNotificationServer.notificationServerInstance != null)
			{
				SubscriptionNotificationServer.notificationServerInstance.Stop();
				SubscriptionNotificationServer.notificationServerInstance = null;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000161B4 File Offset: 0x000143B4
		private void Start()
		{
			lock (this.syncObject)
			{
				this.stopped = false;
			}
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)1375UL, SubscriptionNotificationServer.tracer, (long)this.GetHashCode(), "Started the SubscriptionNotificationServer.", new object[0]);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00016224 File Offset: 0x00014424
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
			RpcServerBase.StopServer(SubscriptionNotificationRpcServer.RpcIntfHandle);
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)1376UL, SubscriptionNotificationServer.tracer, (long)this.GetHashCode(), "Waiting for existing completion server threads to finish.", new object[0]);
			this.workThreadsEvent.WaitOne();
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)1377UL, SubscriptionNotificationServer.tracer, (long)this.GetHashCode(), "Stopped the SubscriptionNotificationServer.", new object[0]);
		}

		// Token: 0x040001C0 RID: 448
		private const int CurrentVersion = 1;

		// Token: 0x040001C1 RID: 449
		private static readonly byte[] SuccessOutput = SubscriptionNotificationProcessor.SuccessOutput;

		// Token: 0x040001C2 RID: 450
		private static readonly Trace tracer = ExTraceGlobals.SubscriptionNotificationServerTracer;

		// Token: 0x040001C3 RID: 451
		private readonly ManualResetEvent workThreadsEvent = new ManualResetEvent(true);

		// Token: 0x040001C4 RID: 452
		private readonly object syncObject = new object();

		// Token: 0x040001C5 RID: 453
		private readonly SubscriptionNotificationProcessor notificationProcessor = new SubscriptionNotificationProcessor();

		// Token: 0x040001C6 RID: 454
		private static SubscriptionNotificationServer notificationServerInstance;

		// Token: 0x040001C7 RID: 455
		private static bool ignoreNotifications = false;

		// Token: 0x040001C8 RID: 456
		private bool stopped;
	}
}
