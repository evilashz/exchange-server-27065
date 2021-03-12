using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001AB RID: 427
	internal class CopyStatusProbeBase : ProbeWorkItem
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0004F741 File Offset: 0x0004D941
		protected CopyStatusProbeBase.CopyStatusEntry CopyStatus
		{
			get
			{
				return this.copyStatusEntry;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0004F74C File Offset: 0x0004D94C
		protected static ProbeDefinition CreateDefinition(string name, string className, string serviceName, MailboxDatabaseInfo targetDatabase, int recurrenceInterval, int timeout, int maxRetry)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.ServiceName = serviceName;
			probeDefinition.TypeName = className;
			probeDefinition.Name = name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			probeDefinition.TimeoutSeconds = timeout;
			probeDefinition.MaxRetryAttempts = maxRetry;
			probeDefinition.TargetResource = targetDatabase.MailboxDatabaseName;
			probeDefinition.Attributes[CopyStatusProbeBase.DBGuidAttrName] = targetDatabase.MailboxDatabaseGuid.ToString();
			return probeDefinition;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
			if (pDef == null)
			{
				throw new ArgumentException("Please specify a value for probeDefinition");
			}
			if (propertyBag.ContainsKey(CopyStatusProbeBase.DBGuidAttrName))
			{
				pDef.Attributes[CopyStatusProbeBase.DBGuidAttrName] = propertyBag[CopyStatusProbeBase.DBGuidAttrName].ToString().Trim();
				return;
			}
			throw new ArgumentException("Please specify value for" + CopyStatusProbeBase.DBGuidAttrName);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0004F840 File Offset: 0x0004DA40
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (HighAvailabilityUtility.CheckCancellationRequested(cancellationToken))
			{
				base.Result.StateAttribute1 = "Cancellation Requested!";
				return;
			}
			if (!base.Definition.Attributes.ContainsKey("dbGuid") || string.IsNullOrWhiteSpace(base.Definition.Attributes["dbGuid"]))
			{
				throw new HighAvailabilityMAProbeException("Probe Attribute 'dbGuid' is Undefined, Null or Empty");
			}
			Guid databaseGuid = new Guid(base.Definition.Attributes["dbGuid"]);
			this.copyStatusEntry = CopyStatusProbeBase.CopyStatusEntry.ConstructFromMdbGuid(databaseGuid);
			base.Result.StateAttribute1 = base.Definition.Name;
			base.Result.StateAttribute2 = string.Format("{0}\\{1}", this.CopyStatus.DatabaseName, Environment.MachineName);
			base.Result.StateAttribute3 = this.CopyStatus.CopyStatus.ToString();
			base.Result.StateAttribute4 = string.Format("Status Timestamp - {0}", this.CopyStatus.CopyStatusTimestamp.ToString());
			base.Result.StateAttribute5 = string.Format("ReplayLagConfigured - {0}", this.CopyStatus.ReplayLagEnabled);
			base.Result.StateAttribute6 = (double)this.CopyStatus.CopyQueueLength;
			base.Result.StateAttribute7 = (double)this.CopyStatus.ReplayQueueLength;
		}

		// Token: 0x04000921 RID: 2337
		private CopyStatusProbeBase.CopyStatusEntry copyStatusEntry;

		// Token: 0x04000922 RID: 2338
		public static readonly string DBGuidAttrName = "dbGuid";

		// Token: 0x020001AC RID: 428
		protected class CopyStatusEntry
		{
			// Token: 0x06000C48 RID: 3144 RVA: 0x0004F9BC File Offset: 0x0004DBBC
			private CopyStatusEntry(Guid databaseGuid)
			{
				this.databaseGuid = databaseGuid;
				this.databaseName = CachedAdReader.Instance.GetDatabaseOnLocalServer(this.databaseGuid).Name;
				this.ConstructEntry();
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0004F9EC File Offset: 0x0004DBEC
			public CopyStatusEnum CopyStatus
			{
				get
				{
					return this.currentCopyStatus;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0004F9F4 File Offset: 0x0004DBF4
			public DateTime CopyStatusTimestamp
			{
				get
				{
					return this.currentCopyStatusTimestamp;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0004F9FC File Offset: 0x0004DBFC
			public string DatabaseName
			{
				get
				{
					return this.databaseName;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0004FA04 File Offset: 0x0004DC04
			public Guid DatabaseGuid
			{
				get
				{
					return this.databaseGuid;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0004FA0C File Offset: 0x0004DC0C
			public long CopyQueueLength
			{
				get
				{
					return this.currentCopyQueueLength;
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0004FA14 File Offset: 0x0004DC14
			public long ReplayQueueLength
			{
				get
				{
					return this.currentReplayQueueLength;
				}
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0004FA1C File Offset: 0x0004DC1C
			public ReplayLagEnabledEnum ReplayLagEnabled
			{
				get
				{
					return this.currentReplayLagEnabled;
				}
			}

			// Token: 0x06000C50 RID: 3152 RVA: 0x0004FA24 File Offset: 0x0004DC24
			public static CopyStatusProbeBase.CopyStatusEntry ConstructFromMdbGuid(Guid databaseGuid)
			{
				return new CopyStatusProbeBase.CopyStatusEntry(databaseGuid);
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x0004FA2C File Offset: 0x0004DC2C
			private void ConstructEntry()
			{
				CopyStatusClientCachedEntry dbCopyStatusOnLocalServer = CachedDbStatusReader.Instance.GetDbCopyStatusOnLocalServer(this.databaseGuid);
				DateTime utcNow = DateTime.UtcNow;
				if (dbCopyStatusOnLocalServer == null)
				{
					throw new HighAvailabilityMAProbeException(string.Format("Unable to find copy status for database {0}!", this.databaseName));
				}
				if (dbCopyStatusOnLocalServer.Result != CopyStatusRpcResult.Success)
				{
					throw new HighAvailabilityMAProbeException(string.Format("GetCopyStatus RPC Error! Database {0}, RpcResult={1}, RpcTargetServer={2}", this.databaseName, dbCopyStatusOnLocalServer.Result, dbCopyStatusOnLocalServer.ServerContacted));
				}
				this.currentCopyStatus = dbCopyStatusOnLocalServer.CopyStatus.CopyStatus;
				this.currentCopyQueueLength = dbCopyStatusOnLocalServer.CopyStatus.GetCopyQueueLength();
				this.currentReplayQueueLength = dbCopyStatusOnLocalServer.CopyStatus.GetReplayQueueLength();
				this.currentReplayLagEnabled = dbCopyStatusOnLocalServer.CopyStatus.ReplayLagEnabled;
				this.currentCopyStatusTimestamp = dbCopyStatusOnLocalServer.CopyStatus.LastStatusTransitionTime.ToUniversalTime();
			}

			// Token: 0x04000923 RID: 2339
			private readonly Guid databaseGuid;

			// Token: 0x04000924 RID: 2340
			private readonly string databaseName;

			// Token: 0x04000925 RID: 2341
			private CopyStatusEnum currentCopyStatus;

			// Token: 0x04000926 RID: 2342
			private DateTime currentCopyStatusTimestamp;

			// Token: 0x04000927 RID: 2343
			private long currentCopyQueueLength;

			// Token: 0x04000928 RID: 2344
			private long currentReplayQueueLength;

			// Token: 0x04000929 RID: 2345
			private ReplayLagEnabledEnum currentReplayLagEnabled;
		}
	}
}
