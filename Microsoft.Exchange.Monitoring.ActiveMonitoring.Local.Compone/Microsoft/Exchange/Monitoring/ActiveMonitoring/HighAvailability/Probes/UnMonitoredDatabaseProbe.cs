using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B8 RID: 440
	public class UnMonitoredDatabaseProbe : ProbeWorkItem
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x00051330 File Offset: 0x0004F530
		public static ProbeDefinition CreateDefinition(string name, string serviceName, MailboxDatabaseInfo targetDatabase, int recurrenceInterval, int timeout, int maxRetry)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.ServiceName = serviceName;
			probeDefinition.TypeName = typeof(UnMonitoredDatabaseProbe).FullName;
			probeDefinition.Name = name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			probeDefinition.TimeoutSeconds = timeout;
			probeDefinition.MaxRetryAttempts = maxRetry;
			probeDefinition.TargetResource = targetDatabase.MailboxDatabaseName;
			probeDefinition.Attributes[UnMonitoredDatabaseProbe.DBGuidAttrName] = targetDatabase.MailboxDatabaseGuid.ToString();
			return probeDefinition;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000513BE File Offset: 0x0004F5BE
		public static ProbeDefinition CreateDefinition(string name, string serviceName, MailboxDatabaseInfo targetDatabase, int recurrenceInterval)
		{
			return UnMonitoredDatabaseProbe.CreateDefinition(name, serviceName, targetDatabase, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000513D0 File Offset: 0x0004F5D0
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
			if (pDef == null)
			{
				throw new ArgumentException("Please specify a value for probeDefinition");
			}
			if (propertyBag.ContainsKey(UnMonitoredDatabaseProbe.DBGuidAttrName))
			{
				pDef.Attributes[UnMonitoredDatabaseProbe.DBGuidAttrName] = propertyBag[UnMonitoredDatabaseProbe.DBGuidAttrName].ToString().Trim();
				return;
			}
			throw new ArgumentException("Please specify value for" + UnMonitoredDatabaseProbe.DBGuidAttrName);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00051440 File Offset: 0x0004F640
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (HighAvailabilityUtility.CheckCancellationRequested(cancellationToken))
			{
				base.Result.StateAttribute1 = "Cancellation Requested!";
				return;
			}
			this.databaseGuid = new Guid(base.Definition.Attributes["dbGuid"]);
			IADDatabase databaseOnLocalServer = CachedAdReader.Instance.GetDatabaseOnLocalServer(this.databaseGuid);
			base.Result.StateAttribute1 = base.Definition.Name;
			base.Result.StateAttribute2 = string.Format("{0}", string.IsNullOrEmpty(base.Definition.TargetResource) ? "NULL" : base.Definition.TargetResource);
			if (databaseOnLocalServer == null)
			{
				throw new HighAvailabilityMAProbeException(string.Format("Unable to find Database AdObject for database {0} (Guid={1})!", string.IsNullOrEmpty(base.Definition.TargetResource) ? "NULL" : base.Definition.TargetResource, string.IsNullOrEmpty(base.Definition.Attributes["dbGuid"]) ? "NULL" : base.Definition.Attributes["dbGuid"]));
			}
			base.Result.StateAttribute3 = string.Format("AutoDagExcludeFromMonitoring = {0}", databaseOnLocalServer.AutoDagExcludeFromMonitoring);
			if (databaseOnLocalServer.AutoDagExcludeFromMonitoring)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 142, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\UnMonitoredDatabaseProbe.cs");
				MailboxDatabase mailboxDatabase = topologyConfigurationSession.FindDatabaseByGuid<MailboxDatabase>(this.databaseGuid);
				if (mailboxDatabase == null)
				{
					throw new DatabaseNotFoundInADException(this.databaseGuid.ToString());
				}
				base.Result.StateAttribute4 = string.Format("IsExcludedFromProvisioning = {0}", mailboxDatabase.IsExcludedFromProvisioning);
				if (!mailboxDatabase.IsExcludedFromProvisioning)
				{
					base.Result.StateAttribute11 = "Failed";
					throw new HighAvailabilityMAProbeRedResultException(string.Format("Database {0} has AutoDagExcludeFromMonitoring {1} but IsExcludedFromProvisioning {2} ", base.Definition.TargetResource, "Enabled", "Disabled"));
				}
			}
			base.Result.StateAttribute11 = "Passed";
		}

		// Token: 0x0400093E RID: 2366
		private Guid databaseGuid;

		// Token: 0x0400093F RID: 2367
		public static readonly string DBGuidAttrName = "dbGuid";
	}
}
