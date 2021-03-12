using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.JobQueue;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000998 RID: 2456
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcServerWrapper : JobQueueRpcServer
	{
		// Token: 0x06005A91 RID: 23185 RVA: 0x00178F2C File Offset: 0x0017712C
		public static bool Start(TimeSpan enqueueRequestTimeout, out Exception e)
		{
			RpcServerWrapper.enqueueRequestTimeout = enqueueRequestTimeout;
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

		// Token: 0x06005A92 RID: 23186 RVA: 0x00178FC8 File Offset: 0x001771C8
		public static void Stop()
		{
			int num = Interlocked.CompareExchange(ref RpcServerWrapper.Registered, 0, 1);
			if (num == 1)
			{
				RpcServerBase.StopServer(JobQueueRpcServer.RpcIntfHandle);
			}
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x00178FF0 File Offset: 0x001771F0
		private static void CrashOnCallTimeout(object state)
		{
			Thread thread = (Thread)state;
			Exception exception = new TimeoutException(string.Format("RpcServerWrapper.EnqueueRequest call timed out after {0} on thread {1}", RpcServerWrapper.enqueueRequestTimeout, (thread != null) ? thread.ManagedThreadId : -1));
			ExWatson.SendReportAndCrashOnAnotherThread(exception);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x00179038 File Offset: 0x00177238
		public override byte[] EnqueueRequest(int version, int type, byte[] inputParameterBytes)
		{
			byte[] result;
			try
			{
				using (new Timer(new TimerCallback(RpcServerWrapper.CrashOnCallTimeout), Thread.CurrentThread, RpcServerWrapper.enqueueRequestTimeout, TimeSpan.FromMilliseconds(-1.0)))
				{
					EnqueueResult enqueueResult = JobQueueManager.Enqueue((QueueType)type, inputParameterBytes);
					EnqueueRequestRpcOutParameters enqueueRequestRpcOutParameters = new EnqueueRequestRpcOutParameters(enqueueResult);
					result = enqueueRequestRpcOutParameters.Serialize();
				}
			}
			catch (Exception ex)
			{
				ExWatson.SendReport(ex, ReportOptions.None, null);
				EnqueueRequestRpcOutParameters enqueueRequestRpcOutParameters = new EnqueueRequestRpcOutParameters(new EnqueueResult(EnqueueResultType.UnexpectedServerError, ServerStrings.RpcServerUnhandledException(ex.Message)));
				result = enqueueRequestRpcOutParameters.Serialize();
			}
			return result;
		}

		// Token: 0x04003200 RID: 12800
		private static TimeSpan enqueueRequestTimeout = TimeSpan.MaxValue;

		// Token: 0x04003201 RID: 12801
		public static int Registered;
	}
}
