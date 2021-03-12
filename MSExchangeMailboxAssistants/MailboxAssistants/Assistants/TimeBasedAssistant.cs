using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.Assistants.FilteredTracing;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200001F RID: 31
	internal abstract class TimeBasedAssistant : AssistantBase
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000056DE File Offset: 0x000038DE
		protected TimeBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000056EC File Offset: 0x000038EC
		public virtual List<ResourceKey> GetResourceDependencies()
		{
			List<ResourceKey> list = new List<ResourceKey>();
			if (base.DatabaseInfo != null)
			{
				list.Add(new MdbResourceHealthMonitorKey(base.DatabaseInfo.Guid));
				list.Add(new MdbAvailabilityResourceHealthMonitorKey(base.DatabaseInfo.Guid));
				list.Add(new MdbReplicationResourceHealthMonitorKey(base.DatabaseInfo.Guid));
			}
			return list;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000574C File Offset: 0x0000394C
		public virtual AssistantTaskContext InitializeContext(MailboxData mailbox, TimeBasedDatabaseJob job)
		{
			return new AssistantTaskContext(mailbox, job, null);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005763 File Offset: 0x00003963
		public virtual AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			this.Invoke(context.Args, context.CustomDataToLog);
			return null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005778 File Offset: 0x00003978
		public void Invoke(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog = null)
		{
			StoreSession storeSession = invokeArgs.StoreSession;
			Guid databaseGuid = invokeArgs.MailboxData.DatabaseGuid;
			string displayName = invokeArgs.MailboxData.DisplayName;
			Guid mailboxGuid = Guid.Empty;
			if (customDataToLog == null)
			{
				customDataToLog = new List<KeyValuePair<string, object>>();
			}
			StoreMailboxData storeMailboxData = invokeArgs.MailboxData as StoreMailboxData;
			if (storeMailboxData != null)
			{
				mailboxGuid = storeMailboxData.Guid;
			}
			using (new GuidTraceFilter(databaseGuid, mailboxGuid))
			{
				TimeBasedAssistant.Tracer.TraceDebug<TimeBasedAssistant, string>((long)this.GetHashCode(), "{0}: Started invoke for mailbox {1}.", this, displayName);
				if (storeMailboxData != null)
				{
					TraceContext.Set(storeSession);
				}
				try
				{
					invokeArgs.ActivityId = ((ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty);
					this.InvokeInternal(invokeArgs, customDataToLog);
				}
				finally
				{
					TraceContext.Reset();
					TimeBasedAssistant.Tracer.TraceDebug<TimeBasedAssistant, string>((long)this.GetHashCode(), "{0}: Ended invoke for mailbox {1}.", this, displayName);
					TimeBasedAssistant.TracerPfd.TracePfd<int, TimeBasedAssistant, string>((long)this.GetHashCode(), "PFD IWE {0} {1}: Invoke completed for mailbox '{2}'.", 22167, this, displayName);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005894 File Offset: 0x00003A94
		public void OnStart()
		{
			TimeBasedAssistant.Tracer.TraceDebug<TimeBasedAssistant>((long)this.GetHashCode(), "{0}: OnStart started", this);
			this.OnStartInternal();
			TimeBasedAssistant.Tracer.TraceDebug<TimeBasedAssistant>((long)this.GetHashCode(), "{0}: OnStart completed", this);
			TimeBasedAssistant.TracerPfd.TracePfd<int, TimeBasedAssistant>((long)this.GetHashCode(), "PFD IWS{0} {1}: Started", 27415, this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000058F1 File Offset: 0x00003AF1
		public virtual List<MailboxData> GetMailboxesToProcess()
		{
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000058F4 File Offset: 0x00003AF4
		protected virtual void OnStartInternal()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000058F6 File Offset: 0x00003AF6
		public virtual MailboxData CreateOnDemandMailboxData(Guid itemGuid, string parameters)
		{
			return null;
		}

		// Token: 0x060000E7 RID: 231
		protected abstract void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog);

		// Token: 0x060000E8 RID: 232 RVA: 0x000058FC File Offset: 0x00003AFC
		protected static void TrackAdminRpcCalls(DatabaseInfo databaseInfo, string clientType, Action<ExRpcAdmin> rpcDelegate)
		{
			ExRpcAdmin exRpcAdmin = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				exRpcAdmin = ExRpcAdmin.Create(clientType, null, null, null, null);
				rpcDelegate(exRpcAdmin);
			}
			finally
			{
				stopwatch.Stop();
				if (exRpcAdmin != null)
				{
					IRPCLatencyProvider mdbHealthMonitor = TimeBasedAssistant.GetMdbHealthMonitor(databaseInfo.Guid);
					if (mdbHealthMonitor != null)
					{
						PerRPCPerformanceStatistics storePerRPCStats = exRpcAdmin.GetStorePerRPCStats();
						mdbHealthMonitor.Update((int)storePerRPCStats.avgDbLatency, (storePerRPCStats.validVersion >= 2U) ? storePerRPCStats.totalDbOperations : 100U);
					}
					exRpcAdmin.Dispose();
					exRpcAdmin = null;
				}
				ActivityContext.AddOperation(ActivityOperationType.ExRpcAdmin, databaseInfo.DatabaseName, (float)stopwatch.Elapsed.TotalMilliseconds, 1);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000599C File Offset: 0x00003B9C
		private static IRPCLatencyProvider GetMdbHealthMonitor(Guid databaseGuid)
		{
			MdbResourceHealthMonitorKey key = new MdbResourceHealthMonitorKey(databaseGuid);
			return ResourceHealthMonitorManager.Singleton.Get(key) as IRPCLatencyProvider;
		}

		// Token: 0x04000108 RID: 264
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.AssistantBaseTracer;

		// Token: 0x04000109 RID: 265
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
