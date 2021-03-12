using System;
using System.DirectoryServices;
using System.Security.AccessControl;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.EdgeSync;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000008 RID: 8
	internal class SyncNowServer : EdgeSyncRpcServer
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003C98 File Offset: 0x00001E98
		public static bool Start(EdgeSync edgeSync, ObjectSecurity serverSecurityObject, out Exception e)
		{
			e = null;
			SyncNowServer.edgeSync = edgeSync;
			ActiveDirectoryRights accessMask = ActiveDirectoryRights.GenericRead;
			bool result;
			try
			{
				SyncNowServer.server = (SyncNowServer)RpcServerBase.RegisterServer(typeof(SyncNowServer), serverSecurityObject, accessMask, false);
				result = true;
			}
			catch (RpcException ex)
			{
				e = ex;
				result = false;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public static void Stop()
		{
			if (SyncNowServer.server != null)
			{
				RpcServerBase.StopServer(EdgeSyncRpcServer.RpcIntfHandle);
				SyncNowServer.server = null;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003D0C File Offset: 0x00001F0C
		public override StartResults StartSyncNow(string targetServer, bool forceFullSync, bool forceUpdateCookie)
		{
			StartResults result;
			try
			{
				bool flag = SyncNowServer.edgeSync.SyncNowState.Start(targetServer, forceFullSync, forceUpdateCookie);
				if (flag)
				{
					SyncNowServer.edgeSync.EdgeSyncLogSession.LogSyncNow("Manual sync started.");
					result = StartResults.Started;
				}
				else
				{
					SyncNowServer.edgeSync.EdgeSyncLogSession.LogSyncNow("Unable to start manual sync.  Try again after current sync finishes.");
					result = StartResults.AlreadyStarted;
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = StartResults.ErrorOnStart;
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003D7C File Offset: 0x00001F7C
		public override byte[] GetSyncNowResult(ref GetResultResults continueFlag)
		{
			byte[] result;
			try
			{
				continueFlag = GetResultResults.PendingResults;
				Status status = SyncNowServer.edgeSync.SyncNowState.TryGetNextResult();
				if (status == null)
				{
					if (!SyncNowServer.edgeSync.SyncNowState.Running)
					{
						continueFlag = GetResultResults.NoMoreData;
						SyncNowServer.edgeSync.EdgeSyncLogSession.LogSyncNow("Manual sync completed.");
					}
					result = null;
				}
				else
				{
					result = status.Serialize();
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				continueFlag = GetResultResults.Error;
				result = null;
			}
			return result;
		}

		// Token: 0x04000028 RID: 40
		private static SyncNowServer server = null;

		// Token: 0x04000029 RID: 41
		private static EdgeSync edgeSync;
	}
}
