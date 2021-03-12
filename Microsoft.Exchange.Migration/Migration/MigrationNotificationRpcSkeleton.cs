using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MigrationService;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationNotificationRpcSkeleton : MigrationNotificationRpcServer
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000587F File Offset: 0x00003A7F
		public override byte[] UpdateMigrationRequest(int version, byte[] inputBlob)
		{
			return MigrationNotificationRpcSkeleton.UpdateMigrationRequest(this.implementation, version, inputBlob);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005890 File Offset: 0x00003A90
		internal static void StartServer(IMigrationNotification implementation)
		{
			lock (MigrationNotificationRpcSkeleton.startStopLock)
			{
				if (MigrationNotificationRpcSkeleton.instance != null)
				{
					throw new InvalidOperationException("Cannot Start the server since it is already started. Call StopServer() before calling TryStartServer()");
				}
				MigrationNotificationRpcSkeleton.instance = (MigrationNotificationRpcSkeleton)RpcServerBase.RegisterServer(typeof(MigrationNotificationRpcSkeleton), MigrationNotificationRpcSkeleton.GetSecuritySettings(), 1, false, (uint)ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationNotificationRpcSkeletonMaxThreads"));
				MigrationNotificationRpcSkeleton.instance.implementation = implementation;
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationService RPC server started", new object[0]);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005924 File Offset: 0x00003B24
		internal static void StopServer()
		{
			lock (MigrationNotificationRpcSkeleton.startStopLock)
			{
				if (MigrationNotificationRpcSkeleton.instance != null)
				{
					RpcServerBase.StopServer(MigrationNotificationRpcServer.RpcIntfHandle);
					MigrationNotificationRpcSkeleton.instance.implementation = null;
					MigrationNotificationRpcSkeleton.instance = null;
					MigrationLogger.Log(MigrationEventType.Verbose, "MigrationNotification RPC Server stopped and deregistered from RPC", new object[0]);
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005994 File Offset: 0x00003B94
		internal static byte[] UpdateMigrationRequest(IMigrationNotification implementation, int version, byte[] inputBlob)
		{
			byte[] result;
			try
			{
				if (implementation == null)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "MigrationNotification RPC Server will return {0} since the the server has not been started.", new object[]
					{
						MigrationServiceRpcResultCode.ServerShutdown
					});
					result = MigrationRpcHelper.CreateResponsePropertyCollection(MigrationServiceRpcResultCode.ServerShutdown).GetBytes();
				}
				else if (version != 1)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "MigrationNotification RPC Server will return {0} since the current version is {1} and the requested version was {2}.", new object[]
					{
						MigrationServiceRpcResultCode.VersionMismatchError,
						1,
						version
					});
					result = MigrationRpcHelper.CreateResponsePropertyCollection(MigrationServiceRpcResultCode.VersionMismatchError).GetBytes();
				}
				else
				{
					MdbefPropertyCollection mdbefPropertyCollection = null;
					try
					{
						mdbefPropertyCollection = MdbefPropertyCollection.Create(inputBlob, 0, inputBlob.Length);
					}
					catch (MdbefException exception)
					{
						MigrationLogger.Log(MigrationEventType.Error, exception, "MigrationNotification RPC Server will return {0} since inputblob could not be parsed", new object[]
						{
							MigrationServiceRpcResultCode.PropertyBagMissingError
						});
						return MigrationRpcHelper.CreateResponsePropertyCollection(MigrationServiceRpcResultCode.PropertyBagMissingError).GetBytes();
					}
					MigrationServiceRpcMethodCode migrationServiceRpcMethodCode = (MigrationServiceRpcMethodCode)((int)mdbefPropertyCollection[2684420099U]);
					MdbefPropertyCollection mdbefPropertyCollection2;
					switch (migrationServiceRpcMethodCode)
					{
					case MigrationServiceRpcMethodCode.RegisterMigrationBatch:
						mdbefPropertyCollection2 = MigrationNotificationRpcSkeleton.RegisterMigrationBatch(implementation, mdbefPropertyCollection);
						break;
					case MigrationServiceRpcMethodCode.SubscriptionStatusChanged:
						mdbefPropertyCollection2 = MigrationNotificationRpcSkeleton.SubscriptionStatusChanged(implementation, mdbefPropertyCollection);
						break;
					default:
						MigrationLogger.Log(MigrationEventType.Error, "MigrationService RPC server will return {0} since an invalid method {1} was requested", new object[]
						{
							MigrationServiceRpcResultCode.UnknownMethodError,
							migrationServiceRpcMethodCode
						});
						mdbefPropertyCollection2 = MigrationRpcHelper.CreateResponsePropertyCollection(MigrationServiceRpcResultCode.UnknownMethodError);
						break;
					}
					result = mdbefPropertyCollection2.GetBytes();
				}
			}
			catch (Exception exception2)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception2);
				result = null;
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005B6C File Offset: 0x00003D6C
		private static MdbefPropertyCollection SubscriptionStatusChanged(IMigrationNotification implementation, MdbefPropertyCollection inputArgs)
		{
			MdbefPropertyCollection result2;
			try
			{
				UpdateMigrationRequestArgs args = UpdateMigrationRequestArgs.UnMarshal(inputArgs);
				UpdateMigrationRequestResult result = null;
				MigrationServiceHelper.SafeInvokeImplMethod(delegate
				{
					result = implementation.UpdateMigrationRequest(args);
				}, MigrationServiceRpcMethodCode.SubscriptionStatusChanged);
				MdbefPropertyCollection mdbefPropertyCollection = result.Marshal();
				result2 = mdbefPropertyCollection;
			}
			catch (MigrationServiceRpcException ex)
			{
				MigrationLogger.Log(MigrationEventType.Error, ex, "SubscriptionStatusChanged failed", new object[0]);
				result2 = new UpdateMigrationRequestResult(MigrationServiceRpcMethodCode.SubscriptionStatusChanged, ex.ResultCode, ex.Message).Marshal();
			}
			return result2;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005C3C File Offset: 0x00003E3C
		private static MdbefPropertyCollection RegisterMigrationBatch(IMigrationNotification implementation, MdbefPropertyCollection inputArgs)
		{
			MdbefPropertyCollection result2;
			try
			{
				RegisterMigrationBatchArgs args = RegisterMigrationBatchArgs.UnMarshal(inputArgs);
				RegisterMigrationBatchResult result = null;
				MigrationServiceHelper.SafeInvokeImplMethod(delegate
				{
					result = implementation.RegisterMigrationBatch(args);
				}, MigrationServiceRpcMethodCode.RegisterMigrationBatch);
				result2 = result.Marshal();
			}
			catch (MigrationServiceRpcException ex)
			{
				MigrationLogger.Log(MigrationEventType.Error, ex, "RegisterMigrationBatch failed", new object[0]);
				result2 = new RegisterMigrationBatchResult(MigrationServiceRpcMethodCode.RegisterMigrationBatch, ex.ResultCode, ex.Message).Marshal();
			}
			return result2;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005CD4 File Offset: 0x00003ED4
		private static FileSecurity GetSecuritySettings()
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			return fileSecurity;
		}

		// Token: 0x04000036 RID: 54
		private const int CurrentVersion = 1;

		// Token: 0x04000037 RID: 55
		private static readonly object startStopLock = new object();

		// Token: 0x04000038 RID: 56
		private static MigrationNotificationRpcSkeleton instance;

		// Token: 0x04000039 RID: 57
		private IMigrationNotification implementation;
	}
}
