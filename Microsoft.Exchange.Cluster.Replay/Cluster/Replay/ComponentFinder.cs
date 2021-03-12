using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C8 RID: 712
	internal class ComponentFinder : IFindComponent
	{
		// Token: 0x06001BE0 RID: 7136 RVA: 0x000788A4 File Offset: 0x00076AA4
		public MonitoredDatabase FindMonitoredDatabase(string nodeName, Guid dbGuid)
		{
			return RemoteDataProvider.GetMonitoredDatabase(dbGuid);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000788AC File Offset: 0x00076AAC
		public LogCopier FindLogCopier(string nodeName, Guid dbGuid)
		{
			ReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			ReplicaInstance replicaInstance = null;
			LogCopier logCopier = null;
			if (replicaInstanceManager.TryGetReplicaInstance(dbGuid, out replicaInstance))
			{
				logCopier = replicaInstance.GetComponent<LogCopier>();
				if (logCopier == null)
				{
					ComponentFinder.Tracer.TraceError<Guid>(0L, "FindLogCopier failed to find LogCopier for database {0}", dbGuid);
				}
			}
			else
			{
				ComponentFinder.Tracer.TraceError<Guid>(0L, "FindLogCopier failed to find RI database {0}", dbGuid);
			}
			return logCopier;
		}

		// Token: 0x04000B92 RID: 2962
		private static readonly Trace Tracer = ExTraceGlobals.ReplayManagerTracer;
	}
}
