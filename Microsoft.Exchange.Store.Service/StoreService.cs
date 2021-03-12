using System;
using System.Security.Principal;
using System.ServiceProcess;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.RpcProxy;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.WorkerManager;

namespace Microsoft.Exchange.Server.Storage.Service
{
	// Token: 0x02000002 RID: 2
	internal sealed class StoreService : ExServiceBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private StoreService()
		{
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.ServiceName = "MSExchangeIS";
			base.AutoLog = false;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		internal static void Main(string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeImpersonatePrivilege",
				"SeCreateGlobalPrivilege"
			});
			if (num != 0)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<int>(0L, "Service: Error removing unnecessary privileges: {0}", num);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ServiceInitializationFailure, new object[]
				{
					1.ToString(),
					num.ToString()
				});
				Environment.Exit(num);
			}
			ExWatson.Register("E12");
			using (StoreService storeService = new StoreService())
			{
				if (StoreService.IsRunningAsService())
				{
					ServiceBase.Run(storeService);
				}
				else
				{
					ExServiceBase.RunAsConsole(storeService);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021C4 File Offset: 0x000003C4
		protected override void OnStartInternal(string[] args)
		{
			ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting MSExchangeIS service");
			NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(null);
			LoggerManager.StopAllTraceSessions();
			Directory.InitializeConfiguration();
			ConfigurationSchema.SetDatabaseContext(null, null);
			ThreadManager.Initialize();
			this.logFileMaintenanceTask = new RecurringTask<StoreService>(delegate(TaskExecutionDiagnosticsProxy diagnostics, StoreService context, Func<bool> shouldCallbackContinue)
			{
				LoggerManager.DoTraceLogDirectoryMaintenance();
			}, this, TimeSpan.FromHours(12.0));
			this.logFileMaintenanceTask.Start();
			LoggerManager.StartAllTraceSessions();
			Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.Initialize(null, null, null, null);
			Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Initialize(null, null, null, null, null, null, null);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ErrorCode errorCode;
			using (Context context2 = Context.CreateForSystem())
			{
				errorCode = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.PrimeDirectoryCaches(context2);
				if (errorCode != ErrorCode.NoError)
				{
					ExTraceGlobals.StartupShutdownTracer.TraceDebug<string>(0L, "Failed to prime AD caches. Error={0}", errorCode.ToString());
					base.ExitCode = (int)errorCode;
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ServiceInitializationFailure, new object[]
					{
						2.ToString(),
						base.ExitCode.ToString()
					});
					base.GracefullyAbortStartup();
				}
				ServerInfo serverInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context2);
				this.publishSchemaVersionsTask = new RecurringTask<ServerInfo>(new Task<ServerInfo>.TaskCallback(this.PublishSchemaVersions), serverInfo, TimeSpan.FromMinutes(15.0));
				this.publishSchemaVersionsTask.Start();
				num = ((serverInfo.Edition == ServerEditionType.Enterprise) ? 1 : 1);
				if (serverInfo.MaxRecoveryDatabases <= num)
				{
					num = serverInfo.MaxRecoveryDatabases;
				}
				else
				{
					errorCode = ErrorCode.CreateWithLid((LID)53232U, ErrorCodeValue.CallFailed);
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "The AD is configured for more total recovery databases than is permitted for this SKU");
					base.ExitCode = (int)errorCode;
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TooManyRecoveryMDBsMounted, new object[]
					{
						"MaxRecoveryDatabases",
						serverInfo.MaxRecoveryDatabases,
						num
					});
					base.GracefullyAbortStartup();
				}
				num2 = (int)((serverInfo.Edition == ServerEditionType.Enterprise) ? Microsoft.Exchange.Server.Storage.RpcProxy.Globals.MaxDatabasesMountedEnterprise : 5);
				if (serverInfo.MaxTotalDatabases <= num2)
				{
					num2 = serverInfo.MaxTotalDatabases;
				}
				else
				{
					errorCode = ErrorCode.CreateWithLid((LID)47088U, ErrorCodeValue.CallFailed);
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "The AD is configured for more total mounted databases than is permitted for this SKU");
					base.ExitCode = (int)errorCode;
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TooManyDBsConfigured, new object[]
					{
						"MaxTotalDatabases",
						serverInfo.MaxTotalDatabases,
						num2
					});
					base.GracefullyAbortStartup();
				}
				num3 = serverInfo.MaxActiveDatabases.GetValueOrDefault(int.MaxValue);
				if (1 > num || 1 > num2 || 1 > num3)
				{
					errorCode = ErrorCode.CreateWithLid((LID)63472U, ErrorCodeValue.NotFound);
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Max databases is set to zero in DS");
					base.ExitCode = (int)errorCode;
					if (num3 < 1)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_NoActiveDatabase, new object[0]);
					}
					if (num < 1 || num2 < 1)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_NoDatabase, new object[0]);
					}
					base.GracefullyAbortStartup();
				}
				StoreService.WriteServerInformationTrace(context2);
			}
			errorCode = WorkerManager.Initialize();
			if (errorCode != ErrorCode.NoError)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug<string>(0L, "Failed to initialize worker manager. Error={0}", errorCode.ToString());
				base.ExitCode = (int)errorCode;
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ServiceInitializationFailure, new object[]
				{
					3.ToString(),
					base.ExitCode.ToString()
				});
				base.GracefullyAbortStartup();
			}
			if (!ProxyRpcEndpoints.Initialize(num2, num, num3))
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Failed to initialize proxy RPC endpoints.");
				base.ExitCode = -2147467259;
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ServiceInitializationFailure, new object[]
				{
					3.ToString(),
					base.ExitCode.ToString()
				});
				base.GracefullyAbortStartup();
			}
			StoreServiceDiagnosticHandler.Instance.Register();
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData();
			ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "MSExchangeIS service started successfully");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000261C File Offset: 0x0000081C
		protected override void OnStopInternal()
		{
			ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping MSExchangeIS service");
			StoreServiceDiagnosticHandler.Instance.Deregister();
			ProxyRpcEndpoints.Terminate();
			WorkerManager.Terminate();
			if (this.publishSchemaVersionsTask != null)
			{
				this.publishSchemaVersionsTask.Stop();
				this.publishSchemaVersionsTask.WaitForCompletion();
				this.publishSchemaVersionsTask.Dispose();
			}
			Directory.TerminateConfiguration();
			Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Terminate();
			Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.Terminate();
			if (this.logFileMaintenanceTask != null)
			{
				this.logFileMaintenanceTask.Stop();
				this.logFileMaintenanceTask.WaitForCompletion();
				this.logFileMaintenanceTask.Dispose();
			}
			ThreadManager.Terminate();
			LoggerManager.StopAllTraceSessions();
			LoggerManager.Terminate();
			ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "MSExchangeIS service stopped successfully");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000026D4 File Offset: 0x000008D4
		private static bool IsRunningAsService()
		{
			bool flag = false;
			bool flag2 = false;
			bool result;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IdentityReferenceCollection groups = current.Groups;
				foreach (IdentityReference identityReference in groups)
				{
					if (identityReference is SecurityIdentifier)
					{
						SecurityIdentifier securityIdentifier = (SecurityIdentifier)identityReference;
						if (securityIdentifier.IsWellKnown(WellKnownSidType.ServiceSid))
						{
							flag = true;
						}
						if (securityIdentifier.IsWellKnown(WellKnownSidType.InteractiveSid))
						{
							flag2 = true;
						}
					}
				}
				result = (flag || (!flag && !flag2));
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002784 File Offset: 0x00000984
		private static void WriteServerInformationTrace(Context context)
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.ReferenceData);
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return;
			}
			using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.ServerInfo, true, true, ExWatson.ApplicationVersion.ToString(), Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context).ForestName, ExWatson.ApplicationVersion.Major, ExWatson.ApplicationVersion.Minor, ExWatson.ApplicationVersion.Build, ExWatson.ApplicationVersion.Revision))
			{
				logger.TryWrite(traceBuffer);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002818 File Offset: 0x00000A18
		private void PublishSchemaVersions(TaskExecutionDiagnosticsProxy diagnosticsContext, ServerInfo server, Func<bool> shouldCallbackContinue)
		{
			int value = StoreDatabase.GetMinimumSchemaVersion().Value;
			int value2 = StoreDatabase.GetMaximumSchemaVersion().Value;
			int num = 0;
			int num2 = 0;
			try
			{
				using (IClusterDB clusterDB = ClusterDB.Open())
				{
					if (clusterDB != null && clusterDB.IsInitialized)
					{
						ClusterDBHelpers.ReadServerDatabaseSchemaVersionRange(clusterDB, server.Guid, 0, 0, out num, out num2);
						if (num != value || num2 != value2)
						{
							ClusterDBHelpers.WriteServerDatabaseSchemaVersionRange(clusterDB, server.Guid, value, value2);
						}
						this.publishSchemaVersionsTask.Stop();
						ComponentVersion componentVersion = new ComponentVersion(value);
						ComponentVersion componentVersion2 = new ComponentVersion(value2);
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SchemaVersionPublicationComplete, new object[]
						{
							componentVersion.ToString(),
							componentVersion2.ToString()
						});
					}
				}
			}
			catch (ClusterException exception)
			{
				diagnosticsContext.OnExceptionCatch(exception);
				ComponentVersion componentVersion3 = new ComponentVersion(value);
				ComponentVersion componentVersion4 = new ComponentVersion(value2);
				ComponentVersion componentVersion5 = new ComponentVersion(num);
				ComponentVersion componentVersion6 = new ComponentVersion(num2);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SchemaVersionPublicationFailure, new object[]
				{
					componentVersion5.ToString(),
					componentVersion6.ToString(),
					componentVersion3.ToString(),
					componentVersion4.ToString()
				});
			}
		}

		// Token: 0x04000001 RID: 1
		private RecurringTask<StoreService> logFileMaintenanceTask;

		// Token: 0x04000002 RID: 2
		private RecurringTask<ServerInfo> publishSchemaVersionsTask;

		// Token: 0x02000003 RID: 3
		private enum ServiceFailureReasons
		{
			// Token: 0x04000005 RID: 5
			PrivilegeRemoveFailure = 1,
			// Token: 0x04000006 RID: 6
			DirectoryCachePrimingFailure,
			// Token: 0x04000007 RID: 7
			WorkerManagerInitializationFailure,
			// Token: 0x04000008 RID: 8
			RpcInitializationFailure
		}
	}
}
