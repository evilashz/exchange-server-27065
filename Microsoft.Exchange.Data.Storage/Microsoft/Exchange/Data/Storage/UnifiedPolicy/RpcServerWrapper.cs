using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UnifiedPolicyNotification;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E8F RID: 3727
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcServerWrapper : UnifiedPolicyNotificationRpcServer
	{
		// Token: 0x060081B5 RID: 33205 RVA: 0x002372C0 File Offset: 0x002354C0
		public static bool Start(TimeSpan notifyRequestTimeout, out Exception e)
		{
			RpcServerWrapper.notifyRequestTimeout = notifyRequestTimeout;
			e = null;
			if (RpcServerWrapper.Registered == 1)
			{
				return true;
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.Read, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			bool result;
			try
			{
				RpcServerBase.RegisterServer(typeof(RpcServerWrapper), fileSecurity, 131209);
				Interlocked.CompareExchange(ref RpcServerWrapper.Registered, 1, 0);
				result = true;
			}
			catch (RpcException ex)
			{
				e = ex;
				Interlocked.CompareExchange(ref RpcServerWrapper.Registered, 0, 1);
				result = false;
			}
			return result;
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x0023735C File Offset: 0x0023555C
		public static void Stop()
		{
			int num = Interlocked.CompareExchange(ref RpcServerWrapper.Registered, 0, 1);
			if (num == 1)
			{
				RpcServerBase.StopServer(UnifiedPolicyNotificationRpcServer.RpcIntfHandle);
			}
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x00237384 File Offset: 0x00235584
		private static void CrashOnCallTimeout(object state)
		{
			Thread thread = (Thread)state;
			Exception exception = new TimeoutException(string.Format("RpcServerWrapper.NotifyRequest call timed out after {0} on thread {1}", RpcServerWrapper.notifyRequestTimeout, (thread != null) ? thread.ManagedThreadId : -1));
			ExWatson.SendReportAndCrashOnAnotherThread(exception);
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x002373CC File Offset: 0x002355CC
		public override byte[] Notify(int version, int type, byte[] inputParameterBytes)
		{
			byte[] result2;
			try
			{
				using (new Timer(new TimerCallback(RpcServerWrapper.CrashOnCallTimeout), Thread.CurrentThread, RpcServerWrapper.notifyRequestTimeout, TimeSpan.FromMilliseconds(-1.0)))
				{
					WorkItemBase workItem = WorkItemBase.Deserialize(inputParameterBytes);
					SyncNotificationResult result = null;
					try
					{
						WorkItemBase workItem2 = SyncManager.EnqueueWorkItem(workItem);
						result = new SyncNotificationResult(UnifiedPolicyNotificationFactory.Create(workItem2, new ADObjectId()));
					}
					catch (SyncAgentExceptionBase error)
					{
						result = new SyncNotificationResult(error);
					}
					NotificationRpcOutParameters notificationRpcOutParameters = new NotificationRpcOutParameters(result);
					result2 = notificationRpcOutParameters.Serialize();
				}
			}
			catch (Exception ex)
			{
				ExWatson.SendReport(ex, ReportOptions.None, null);
				NotificationRpcOutParameters notificationRpcOutParameters = new NotificationRpcOutParameters(new SyncNotificationResult(ex));
				result2 = notificationRpcOutParameters.Serialize();
			}
			return result2;
		}

		// Token: 0x04005719 RID: 22297
		private static TimeSpan notifyRequestTimeout = TimeSpan.MaxValue;

		// Token: 0x0400571A RID: 22298
		public static int Registered;
	}
}
