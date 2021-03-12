using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008C4 RID: 2244
	[Cmdlet("Set", "DatabaseAvailabilityGroupNetwork", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetDatabaseAvailabilityGroupNetwork : SetTenantADTaskBase<DatabaseAvailabilityGroupNetworkIdParameter, DatabaseAvailabilityGroupNetwork, DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x0014CCEB File Offset: 0x0014AEEB
		// (set) Token: 0x06004FB3 RID: 20403 RVA: 0x0014CD02 File Offset: 0x0014AF02
		[Parameter]
		public DatabaseAvailabilityGroupSubnetId[] Subnets
		{
			get
			{
				return (DatabaseAvailabilityGroupSubnetId[])base.Fields["Subnets"];
			}
			set
			{
				base.Fields["Subnets"] = value;
			}
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0014CD15 File Offset: 0x0014AF15
		protected override bool IsKnownException(Exception e)
		{
			return DagTaskHelper.IsKnownException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x0014CD2C File Offset: 0x0014AF2C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				DatabaseAvailabilityGroupNetwork dataObject = this.DataObject;
				DagNetworkObjectId dagNetworkObjectId = (DagNetworkObjectId)dataObject.Identity;
				return Strings.ConfirmationMessageSetDatabaseAvailabilityGroupNetwork(dagNetworkObjectId.FullName);
			}
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0014CD57 File Offset: 0x0014AF57
		private IConfigurationSession SetupAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 135, "SetupAdSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\SetDatabaseAvailabilityGroupNetwork.cs");
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0014CD7C File Offset: 0x0014AF7C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession adSession = this.SetupAdSession();
			return new DagNetworkConfigDataProvider(adSession, null, null);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0014CD9C File Offset: 0x0014AF9C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			DatabaseAvailabilityGroupNetwork dataObject = this.DataObject;
			DagNetworkObjectId dagNetworkObjectId = (DagNetworkObjectId)dataObject.Identity;
			ExTraceGlobals.CmdletsTracer.TraceError<string>((long)this.GetHashCode(), "Validating SetDAGNetwork({0})", dagNetworkObjectId.FullName);
			IConfigurationSession configSession = this.SetupAdSession();
			DatabaseAvailabilityGroup dag = DagTaskHelper.ReadDagByName(dagNetworkObjectId.DagName, configSession);
			DagTaskHelper.VerifyDagAndServersAreWithinScopes<DatabaseAvailabilityGroupNetwork>(this, dag, true);
			DagTaskHelper.PreventTaskWhenAutoNetConfigIsEnabled(dag, this);
			DagNetworkConfigDataProvider dagNetworkConfigDataProvider = (DagNetworkConfigDataProvider)base.DataSession;
			DagNetworkConfiguration networkConfig = dagNetworkConfigDataProvider.NetworkConfig;
			DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork = (DatabaseAvailabilityGroupNetwork)dataObject.GetOriginalObject();
			string name = databaseAvailabilityGroupNetwork.Name;
			DatabaseAvailabilityGroupNetwork networkBeingChanged = null;
			foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork2 in networkConfig.Networks)
			{
				if (databaseAvailabilityGroupNetwork2 == dataObject)
				{
					networkBeingChanged = databaseAvailabilityGroupNetwork2;
				}
				else if (DatabaseAvailabilityGroupNetwork.NameComparer.Equals(databaseAvailabilityGroupNetwork2.Name, dataObject.Name))
				{
					throw new DagNetworkManagementException(ServerStrings.DagNetworkRenameDupName(name, dataObject.Name));
				}
			}
			DagNetworkValidation.ValidateSwitches(dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (base.Fields["Subnets"] != null)
			{
				DagNetworkValidation.ValidateSubnets(this.Subnets, networkConfig, dataObject.Name, networkBeingChanged, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				dataObject.ReplaceSubnets(this.Subnets);
			}
			DagNetworkValidation.WarnIfAllNetsAreDisabled(networkConfig, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			TaskLogger.LogExit();
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0014CF18 File Offset: 0x0014B118
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			DatabaseAvailabilityGroupNetwork dataObject = this.DataObject;
			ExTraceGlobals.CmdletsTracer.TraceError<string>((long)this.GetHashCode(), "Processing SetDAGNetwork({0})", dataObject.Name);
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x020008C5 RID: 2245
		private static class ParameterNames
		{
			// Token: 0x04002F28 RID: 12072
			public const string Name = "Name";

			// Token: 0x04002F29 RID: 12073
			public const string Description = "Description";

			// Token: 0x04002F2A RID: 12074
			public const string Subnets = "Subnets";

			// Token: 0x04002F2B RID: 12075
			public const string ReplicationEnabled = "ReplicationEnabled";

			// Token: 0x04002F2C RID: 12076
			public const string IgnoreNetwork = "IgnoreNetwork";
		}
	}
}
