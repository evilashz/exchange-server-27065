using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.Rpc.Assistants;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200008F RID: 143
	internal sealed class TimeBasedDriverManager : Base
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00016838 File Offset: 0x00014A38
		public TimeBasedDriverManager(Throttle throttle, ITimeBasedAssistantType[] timeBasedAssistantTypeArray, bool provideRpc)
		{
			this.provideAssistantsRpc = provideRpc;
			Throttle baseThreadPool = new Throttle("TimeBasedDriverManager", Configuration.MaxThreadsForAllTimeBasedAssistants, throttle);
			bool flag = false;
			List<TimeBasedAssistantControllerWrapper> list = new List<TimeBasedAssistantControllerWrapper>();
			try
			{
				foreach (ITimeBasedAssistantType timeBasedAssistantType in timeBasedAssistantTypeArray)
				{
					ServerGovernor governor = new ServerGovernor("Governor for " + timeBasedAssistantType.Name, new Throttle(timeBasedAssistantType.NonLocalizedName, Configuration.MaxThreadsPerTimeBasedAssistantType, baseThreadPool));
					TimeBasedAssistantControllerWrapper item = new TimeBasedAssistantControllerWrapper(new TimeBasedAssistantController(governor, timeBasedAssistantType));
					list.Add(item);
				}
				flag = true;
				this.TimeBasedAssistantControllerArray = list.ToArray();
			}
			finally
			{
				if (!flag)
				{
					foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in list)
					{
						timeBasedAssistantControllerWrapper.Dispose();
					}
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00016930 File Offset: 0x00014B30
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00016938 File Offset: 0x00014B38
		public TimeBasedAssistantControllerWrapper[] TimeBasedAssistantControllerArray { get; private set; }

		// Token: 0x06000463 RID: 1123 RVA: 0x00016944 File Offset: 0x00014B44
		public void RequestStop(HangDetector hangDetector)
		{
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDriverManager>((long)this.GetHashCode(), "{0}: Stopping", this);
			if (this.rpcServerStarted)
			{
				AssistantsRpcServerBase.StopServer();
				this.rpcServerStarted = false;
			}
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				AIBreadcrumbs.ShutdownTrail.Drop("Stopping controller: " + timeBasedAssistantControllerWrapper.Controller.TimeBasedAssistantType);
				timeBasedAssistantControllerWrapper.Controller.RequestStop(hangDetector);
				SystemWorkloadManager.UnregisterWorkload(timeBasedAssistantControllerWrapper);
				AIBreadcrumbs.ShutdownTrail.Drop("Finished stopping " + timeBasedAssistantControllerWrapper.Controller.TimeBasedAssistantType);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000169E8 File Offset: 0x00014BE8
		public void RequestStopDatabase(Guid databaseGuid, HangDetector hangDetector)
		{
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDriverManager, Guid>((long)this.GetHashCode(), "{0}: Requesting stop of assistants for database {1}", this, databaseGuid);
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				timeBasedAssistantControllerWrapper.Controller.RequestStopDatabase(databaseGuid, hangDetector);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016A34 File Offset: 0x00014C34
		public void Start(SecurityIdentifier exchangeServersSid)
		{
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDriverManager>((long)this.GetHashCode(), "{0}: Starting", this);
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				timeBasedAssistantControllerWrapper.Controller.Start();
				SystemWorkloadManager.RegisterWorkload(timeBasedAssistantControllerWrapper);
			}
			if (this.provideAssistantsRpc)
			{
				AssistantsRpcServer.StartServer(exchangeServersSid);
				this.rpcServerStarted = true;
			}
			base.TracePfd("PFD AIS {0} {1}: Started", new object[]
			{
				25175,
				this
			});
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016ABC File Offset: 0x00014CBC
		public void StartDatabase(DatabaseInfo databaseInfo, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDriverManager, DatabaseInfo>((long)this.GetHashCode(), "{0}: Starting assistants for database {1}", this, databaseInfo);
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				timeBasedAssistantControllerWrapper.Controller.StartDatabase(databaseInfo, poisonControl, databaseCounters);
			}
			base.TracePfd("PFD AIS {0} {1}: Started assistants for database {2}", new object[]
			{
				31319,
				this,
				databaseInfo
			});
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016B30 File Offset: 0x00014D30
		public override string ToString()
		{
			return "Time-based driver manager";
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016B38 File Offset: 0x00014D38
		public void WaitUntilStopped()
		{
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				timeBasedAssistantControllerWrapper.Controller.WaitUntilStopped();
			}
			base.TracePfd("PFD AIS {0} {1}: Stopped", new object[]
			{
				21079,
				this
			});
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016B90 File Offset: 0x00014D90
		public void WaitUntilStoppedDatabase(Guid databaseGuid)
		{
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedDriverManager, Guid>((long)this.GetHashCode(), "{0}: Waiting stop of assistants for database {1}", this, databaseGuid);
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in this.TimeBasedAssistantControllerArray)
			{
				timeBasedAssistantControllerWrapper.Controller.WaitUntilStoppedDatabase(databaseGuid);
			}
			base.TracePfd("PFD AIS {0} {1}: Stopped assistants for database {2}", new object[]
			{
				16983,
				this,
				databaseGuid
			});
		}

		// Token: 0x0400027F RID: 639
		private bool provideAssistantsRpc;

		// Token: 0x04000280 RID: 640
		private bool rpcServerStarted;
	}
}
