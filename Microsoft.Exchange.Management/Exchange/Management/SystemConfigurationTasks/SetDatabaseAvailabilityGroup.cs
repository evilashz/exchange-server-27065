using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008C1 RID: 2241
	[Cmdlet("Set", "DatabaseAvailabilityGroup", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetDatabaseAvailabilityGroup : SetTopologySystemConfigurationObjectTask<DatabaseAvailabilityGroupIdParameter, DatabaseAvailabilityGroup>
	{
		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06004F7D RID: 20349 RVA: 0x0014BB0E File Offset: 0x00149D0E
		// (set) Token: 0x06004F7E RID: 20350 RVA: 0x0014BB25 File Offset: 0x00149D25
		[Parameter(Mandatory = false)]
		public FileShareWitnessServerName WitnessServer
		{
			get
			{
				return (FileShareWitnessServerName)base.Fields["WitnessServer"];
			}
			set
			{
				base.Fields["WitnessServer"] = value;
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x0014BB38 File Offset: 0x00149D38
		// (set) Token: 0x06004F80 RID: 20352 RVA: 0x0014BB4F File Offset: 0x00149D4F
		[Parameter(Mandatory = false)]
		public ushort ReplicationPort
		{
			get
			{
				return (ushort)base.Fields["ReplicationPort"];
			}
			set
			{
				base.Fields["ReplicationPort"] = value;
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x0014BB67 File Offset: 0x00149D67
		// (set) Token: 0x06004F82 RID: 20354 RVA: 0x0014BB7E File Offset: 0x00149D7E
		[Parameter(Mandatory = false)]
		public DatabaseAvailabilityGroup.NetworkOption NetworkCompression
		{
			get
			{
				return (DatabaseAvailabilityGroup.NetworkOption)base.Fields["NetworkCompression"];
			}
			set
			{
				base.Fields["NetworkCompression"] = value;
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0014BB96 File Offset: 0x00149D96
		// (set) Token: 0x06004F84 RID: 20356 RVA: 0x0014BBAD File Offset: 0x00149DAD
		[Parameter(Mandatory = false)]
		public DatabaseAvailabilityGroup.NetworkOption NetworkEncryption
		{
			get
			{
				return (DatabaseAvailabilityGroup.NetworkOption)base.Fields["NetworkEncryption"];
			}
			set
			{
				base.Fields["NetworkEncryption"] = value;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0014BBC5 File Offset: 0x00149DC5
		// (set) Token: 0x06004F86 RID: 20358 RVA: 0x0014BBDC File Offset: 0x00149DDC
		[Parameter(Mandatory = false)]
		public bool ManualDagNetworkConfiguration
		{
			get
			{
				return (bool)base.Fields["ManualDagNetworkConfiguration"];
			}
			set
			{
				base.Fields["ManualDagNetworkConfiguration"] = value;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x0014BBF4 File Offset: 0x00149DF4
		// (set) Token: 0x06004F88 RID: 20360 RVA: 0x0014BC1A File Offset: 0x00149E1A
		[Parameter]
		public SwitchParameter DiscoverNetworks
		{
			get
			{
				return (SwitchParameter)(base.Fields["DiscoverNetworks"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DiscoverNetworks"] = value;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x0014BC32 File Offset: 0x00149E32
		// (set) Token: 0x06004F8A RID: 20362 RVA: 0x0014BC58 File Offset: 0x00149E58
		[Parameter]
		public SwitchParameter AllowCrossSiteRpcClientAccess
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllowCrossSiteRpcClientAccess"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AllowCrossSiteRpcClientAccess"] = value;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06004F8B RID: 20363 RVA: 0x0014BC70 File Offset: 0x00149E70
		// (set) Token: 0x06004F8C RID: 20364 RVA: 0x0014BC87 File Offset: 0x00149E87
		[Parameter(Mandatory = false)]
		public NonRootLocalLongFullPath WitnessDirectory
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["WitnessDirectory"];
			}
			set
			{
				base.Fields["WitnessDirectory"] = value;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x0014BC9A File Offset: 0x00149E9A
		// (set) Token: 0x06004F8E RID: 20366 RVA: 0x0014BCB1 File Offset: 0x00149EB1
		[Parameter(Mandatory = false)]
		public FileShareWitnessServerName AlternateWitnessServer
		{
			get
			{
				return (FileShareWitnessServerName)base.Fields["AlternateWitnessServer"];
			}
			set
			{
				base.Fields["AlternateWitnessServer"] = value;
				this.m_alternateWitnessServerParameterSpecified = true;
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x0014BCCB File Offset: 0x00149ECB
		// (set) Token: 0x06004F90 RID: 20368 RVA: 0x0014BCE2 File Offset: 0x00149EE2
		[Parameter(Mandatory = false)]
		public NonRootLocalLongFullPath AlternateWitnessDirectory
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["AlternateWitnessDirectory"];
			}
			set
			{
				base.Fields["AlternateWitnessDirectory"] = value;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x0014BCF5 File Offset: 0x00149EF5
		// (set) Token: 0x06004F92 RID: 20370 RVA: 0x0014BD0C File Offset: 0x00149F0C
		[Parameter(Mandatory = false)]
		public DatacenterActivationModeOption DatacenterActivationMode
		{
			get
			{
				return (DatacenterActivationModeOption)base.Fields["DatacenterActivationMode"];
			}
			set
			{
				base.Fields["DatacenterActivationMode"] = value;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x0014BD24 File Offset: 0x00149F24
		// (set) Token: 0x06004F94 RID: 20372 RVA: 0x0014BD3B File Offset: 0x00149F3B
		[Parameter(Mandatory = false)]
		public IPAddress[] DatabaseAvailabilityGroupIpAddresses
		{
			get
			{
				return (IPAddress[])base.Fields["DatabaseAvailabilityGroupIpAddresses"];
			}
			set
			{
				base.Fields["DatabaseAvailabilityGroupIpAddresses"] = value;
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x0014BD4E File Offset: 0x00149F4E
		// (set) Token: 0x06004F96 RID: 20374 RVA: 0x0014BD65 File Offset: 0x00149F65
		[Parameter(Mandatory = false)]
		public DatabaseAvailabilityGroupConfigurationIdParameter DagConfiguration
		{
			get
			{
				return (DatabaseAvailabilityGroupConfigurationIdParameter)base.Fields["DagConfiguration"];
			}
			set
			{
				base.Fields["DagConfiguration"] = value;
				this.m_dagConfigParameterSpecified = true;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06004F97 RID: 20375 RVA: 0x0014BD7F File Offset: 0x00149F7F
		// (set) Token: 0x06004F98 RID: 20376 RVA: 0x0014BDA5 File Offset: 0x00149FA5
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipDagValidation
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipDagValidation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipDagValidation"] = value;
			}
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x0014BDBD File Offset: 0x00149FBD
		protected override bool IsKnownException(Exception e)
		{
			return DagTaskHelper.IsKnownException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x0014BDD1 File Offset: 0x00149FD1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDatabaseAvailabilityGroup(this.Identity.ToString());
			}
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x0014BDE4 File Offset: 0x00149FE4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.m_output = new HaTaskOutputHelper("set-databaseavailabilitygroup", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_dag = this.DataObject;
			this.m_skipDagValidation = this.SkipDagValidation;
			DagTaskHelper.VerifyDagAndServersAreWithinScopes<DatabaseAvailabilityGroup>(this, this.m_dag, true);
			if (this.DatabaseAvailabilityGroupIpAddresses != null)
			{
				if (!this.m_dag.IsDagEmpty())
				{
					this.PrepareServersInDagIfRequired();
					foreach (AmServerName amServerName in this.m_serverNamesInDag)
					{
						DagTaskHelper.ValidateIPAddressList(this.m_output, amServerName.NetbiosName, this.DatabaseAvailabilityGroupIpAddresses, this.m_dag.Name);
						DagTaskHelper.ValidateIPAddressList(this.m_output, amServerName.Fqdn, this.DatabaseAvailabilityGroupIpAddresses, this.m_dag.Name);
					}
				}
				foreach (IPAddress ipaddress in this.DatabaseAvailabilityGroupIpAddresses)
				{
					if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
					{
						this.m_output.WriteErrorSimple(new DagTaskDagIpAddressesMustBeIpv4Exception());
					}
				}
			}
			this.m_useAlternateWitnessServer = (this.m_dag.AlternateWitnessServer != null && DagTaskHelper.IsDagFailedOverToOtherSite(this.m_output, this.m_dag));
			if (!this.m_useAlternateWitnessServer)
			{
				this.m_fsw = new FileShareWitness((ITopologyConfigurationSession)base.DataSession, this.m_dag.Name, this.WitnessServer ?? this.m_dag.WitnessServer, this.WitnessDirectory ?? this.m_dag.WitnessDirectory);
				try
				{
					this.m_fsw.Initialize();
				}
				catch (LocalizedException error)
				{
					this.m_output.WriteErrorSimple(error);
				}
				this.CheckFsw(this.m_fsw.WitnessServerFqdn);
			}
			if (this.AlternateWitnessServer != null || this.AlternateWitnessDirectory != null || (this.m_dag.AlternateWitnessServer != null && this.m_dag.AlternateWitnessDirectory != null && !this.m_alternateWitnessServerParameterSpecified) || this.m_useAlternateWitnessServer)
			{
				this.m_afsw = new FileShareWitness((ITopologyConfigurationSession)base.DataSession, this.m_dag.Name, this.AlternateWitnessServer ?? this.m_dag.AlternateWitnessServer, this.AlternateWitnessDirectory ?? this.m_dag.AlternateWitnessDirectory);
				try
				{
					this.m_afsw.Initialize();
				}
				catch (LocalizedException error2)
				{
					this.m_output.WriteErrorSimple(error2);
				}
				if (this.m_fsw != null && SharedHelper.StringIEquals(this.m_afsw.WitnessServerFqdn, this.m_fsw.WitnessServerFqdn) && this.m_fsw.WitnessDirectory != this.m_afsw.WitnessDirectory)
				{
					this.m_output.WriteErrorSimple(new DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(this.m_fsw.WitnessServer.ToString(), this.m_fsw.WitnessDirectory.ToString(), this.m_afsw.WitnessDirectory.ToString()));
				}
				this.CheckFsw(this.m_afsw.WitnessServerFqdn);
			}
			Dictionary<AmServerName, Server> startedServers = new Dictionary<AmServerName, Server>();
			DatabaseAvailabilityGroupAction.ResolveServers(this.m_output, this.m_dag, this.m_allServers, startedServers, this.m_stoppedServers);
			if (!this.m_dag.IsDagEmpty())
			{
				this.PrepareServersInDagIfRequired();
				List<AmServerName> list = new List<AmServerName>(this.m_stoppedServers.Count);
				IEnumerable<AmServerName> serversInCluster = null;
				foreach (Server server in this.m_stoppedServers.Values)
				{
					list.Add(new AmServerName(server.Id));
				}
				using (AmCluster amCluster = AmCluster.OpenDagClus(this.m_dag))
				{
					serversInCluster = amCluster.EnumerateNodeNames();
				}
				if (!this.m_skipDagValidation)
				{
					DagTaskHelper.CompareDagClusterMembership(this.m_output, this.m_dag.Name, this.m_serverNamesInDag, serversInCluster, list);
				}
			}
			if (base.Fields["DatacenterActivationMode"] != null && this.DatacenterActivationMode != DatacenterActivationModeOption.Off)
			{
				DagTaskHelper.CheckDagCanBeActivatedInDatacenter(this.m_output, this.m_dag, null, (ITopologyConfigurationSession)base.DataSession);
			}
			if (base.Fields["DatacenterActivationMode"] != null && this.DatacenterActivationMode == DatacenterActivationModeOption.Off)
			{
				this.DataObject.StartedMailboxServers = null;
				this.DataObject.StoppedMailboxServers = null;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0014C2CC File Offset: 0x0014A4CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			if ((base.Fields["ReplicationPort"] != null || base.Fields["NetworkCompression"] != null || base.Fields["NetworkEncryption"] != null || base.Fields["ManualDagNetworkConfiguration"] != null || base.Fields["DiscoverNetworks"] != null) && !this.m_dag.IsDagEmpty())
			{
				flag = true;
				this.m_IsObjectChanged = true;
			}
			if (this.DataObject.AllowCrossSiteRpcClientAccess != this.AllowCrossSiteRpcClientAccess)
			{
				if (base.Fields["AllowCrossSiteRpcClientAccess"] != null)
				{
					this.DataObject.AllowCrossSiteRpcClientAccess = this.AllowCrossSiteRpcClientAccess;
					this.m_IsObjectChanged = true;
				}
				else
				{
					this.AllowCrossSiteRpcClientAccess = this.DataObject.AllowCrossSiteRpcClientAccess;
				}
			}
			if (this.m_fsw != null)
			{
				this.m_dag.SetWitnessServer(this.m_fsw.FileShareWitnessShare, this.m_fsw.WitnessDirectory);
				this.m_IsObjectChanged = true;
			}
			if (this.m_afsw != null)
			{
				this.m_dag.SetAlternateWitnessServer(this.m_afsw.FileShareWitnessShare, this.m_afsw.WitnessDirectory);
				this.m_IsObjectChanged = true;
			}
			else if (this.AlternateWitnessServer == null && this.m_alternateWitnessServerParameterSpecified)
			{
				this.m_dag.SetAlternateWitnessServer(null, null);
			}
			base.InternalProcessRecord();
			if (flag && !this.m_dag.IsDagEmpty())
			{
				SetDagNetworkConfigRequest setDagNetworkConfigRequest = new SetDagNetworkConfigRequest();
				if (base.Fields["ReplicationPort"] != null)
				{
					setDagNetworkConfigRequest.ReplicationPort = this.ReplicationPort;
				}
				setDagNetworkConfigRequest.NetworkCompression = this.m_dag.NetworkCompression;
				setDagNetworkConfigRequest.NetworkEncryption = this.m_dag.NetworkEncryption;
				setDagNetworkConfigRequest.ManualDagNetworkConfiguration = this.m_dag.ManualDagNetworkConfiguration;
				if (base.Fields["DiscoverNetworks"] != null)
				{
					setDagNetworkConfigRequest.DiscoverNetworks = true;
				}
				DagNetworkRpc.SetDagNetworkConfig(this.m_dag, setDagNetworkConfigRequest);
			}
			if (!this.m_dag.IsDagEmpty())
			{
				using (AmCluster amCluster = AmCluster.OpenDagClus(this.m_dag))
				{
					if (amCluster.CnoName != string.Empty)
					{
						using (IAmClusterGroup amClusterGroup = amCluster.FindCoreClusterGroup())
						{
							using (IAmClusterResource amClusterResource = amClusterGroup.FindResourceByTypeName("Network Name"))
							{
								IPAddress[] dagIpAddressesFromAd = this.GetDagIpAddressesFromAd(this.m_output, this.m_dag);
								AmClusterResourceHelper.FixUpIpAddressesForNetName(this.m_output, amCluster, (AmClusterGroup)amClusterGroup, (AmClusterResource)amClusterResource, dagIpAddressesFromAd);
								DagTaskHelper.LogCnoState(this.m_output, this.m_dag.Name, amClusterResource);
							}
						}
					}
				}
				this.UpdateFileShareWitness();
				DagTaskHelper.NotifyServersOfConfigChange(this.m_allServers.Keys);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0014C5C4 File Offset: 0x0014A7C4
		private IPAddress[] GetDagIpAddressesFromAd(ITaskOutputHelper output, DatabaseAvailabilityGroup dag)
		{
			MultiValuedProperty<IPAddress> databaseAvailabilityGroupIpv4Addresses = dag.DatabaseAvailabilityGroupIpv4Addresses;
			IPAddress[] array = new IPAddress[0];
			if (databaseAvailabilityGroupIpv4Addresses.Count > 0)
			{
				array = databaseAvailabilityGroupIpv4Addresses.ToArray();
			}
			string[] value = (from addr in array
			select addr.ToString()).ToArray<string>();
			output.AppendLogMessage("Got the following IP addresses for the DAG (blank means DHCP): {0}", new object[]
			{
				string.Join(",", value)
			});
			return array;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0014C63A File Offset: 0x0014A83A
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.m_allServers.Clear();
			this.m_serverNamesInDag = null;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0014C654 File Offset: 0x0014A854
		protected override bool IsObjectStateChanged()
		{
			return this.m_IsObjectChanged || base.IsObjectStateChanged();
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0014C668 File Offset: 0x0014A868
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			DatabaseAvailabilityGroup databaseAvailabilityGroup = (DatabaseAvailabilityGroup)base.PrepareDataObject();
			if (base.Fields["NetworkCompression"] != null)
			{
				databaseAvailabilityGroup.NetworkCompression = this.NetworkCompression;
			}
			if (base.Fields["NetworkEncryption"] != null)
			{
				databaseAvailabilityGroup.NetworkEncryption = this.NetworkEncryption;
			}
			if (base.Fields["ManualDagNetworkConfiguration"] != null)
			{
				databaseAvailabilityGroup.ManualDagNetworkConfiguration = this.ManualDagNetworkConfiguration;
			}
			if (base.Fields["DatacenterActivationMode"] != null)
			{
				databaseAvailabilityGroup.DatacenterActivationMode = this.DatacenterActivationMode;
			}
			if (base.Fields.IsChanged("DatabaseAvailabilityGroupIpAddresses"))
			{
				if (base.Fields["DatabaseAvailabilityGroupIpAddresses"] == null)
				{
					databaseAvailabilityGroup.DatabaseAvailabilityGroupIpv4Addresses = new MultiValuedProperty<IPAddress>
					{
						IPAddress.Any
					};
				}
				else
				{
					databaseAvailabilityGroup.DatabaseAvailabilityGroupIpv4Addresses = this.DatabaseAvailabilityGroupIpAddresses;
				}
			}
			if (base.Fields["DagConfiguration"] != null)
			{
				DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = DagConfigurationHelper.DagConfigIdParameterToDagConfig(this.DagConfiguration, this.ConfigurationSession);
				databaseAvailabilityGroup.DatabaseAvailabilityGroupConfiguration = databaseAvailabilityGroupConfiguration.Id;
			}
			else if (this.m_dagConfigParameterSpecified)
			{
				databaseAvailabilityGroup.DatabaseAvailabilityGroupConfiguration = null;
			}
			TaskLogger.LogExit();
			return databaseAvailabilityGroup;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x0014C794 File Offset: 0x0014A994
		private void PrepareServersInDagIfRequired()
		{
			if (this.m_serverNamesInDag == null)
			{
				this.m_serverNamesInDag = new HashSet<AmServerName>();
				if (this.m_dag != null && this.m_dag.Servers != null)
				{
					foreach (ADObjectId serverId in this.m_dag.Servers)
					{
						this.m_serverNamesInDag.Add(new AmServerName(serverId));
					}
				}
			}
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0014C820 File Offset: 0x0014AA20
		private void CheckFsw(string fswComputerNameFqdn)
		{
			this.PrepareServersInDagIfRequired();
			AmServerName item = new AmServerName(fswComputerNameFqdn);
			if (this.m_serverNamesInDag.Contains(item))
			{
				this.m_output.WriteErrorSimple(new DagTaskServerMailboxServerAlsoServesAsFswNodeException(fswComputerNameFqdn));
			}
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0014C85C File Offset: 0x0014AA5C
		private void UpdateFileShareWitness()
		{
			if (this.m_allServers.Count == 0)
			{
				this.m_output.WriteErrorSimple(new DagTaskNoServersAreStartedException(this.m_dag.Name));
			}
			IEnumerable<AmServerName> amServerNamesFromServers = RestoreDatabaseAvailabilityGroup.GetAmServerNamesFromServers(this.m_allServers.Values);
			using (AmCluster amCluster = AmCluster.OpenByNames(amServerNamesFromServers))
			{
				bool flag = DagTaskHelper.IsQuorumTypeFileShareWitness(this.m_output, amCluster);
				bool flag2 = DagTaskHelper.ShouldBeFileShareWitness(this.m_output, this.m_dag, amCluster, false);
				string fswShareCurrent = string.Empty;
				if (flag)
				{
					using (AmClusterResource amClusterResource = amCluster.OpenQuorumResource())
					{
						if (amClusterResource != null)
						{
							fswShareCurrent = amClusterResource.GetPrivateProperty<string>("SharePath");
						}
					}
				}
				if (flag2)
				{
					if (this.m_fsw != null)
					{
						try
						{
							this.m_output.AppendLogMessage("Creating/modififying the primary FSW, if needed.", new object[0]);
							this.m_fsw.Create();
							if (this.m_dag.Servers.Count == 0 && this.m_fsw.IsJustCreated)
							{
								this.m_fsw.Delete();
							}
						}
						catch (LocalizedException ex)
						{
							if (this.m_fsw.GetExceptionType(ex) != FileShareWitnessExceptionType.FswDeleteError)
							{
								this.m_output.WriteWarning(ex.LocalizedString);
							}
						}
					}
					if (this.m_afsw != null && !this.m_afsw.Equals(this.m_fsw))
					{
						try
						{
							this.m_output.AppendLogMessage("Creating/modififying the alternate FSW, if needed.", new object[0]);
							this.m_afsw.Create();
							if (this.m_dag.Servers.Count == 0 && this.m_afsw.IsJustCreated)
							{
								this.m_afsw.Delete();
							}
						}
						catch (LocalizedException ex2)
						{
							if (this.m_afsw.GetExceptionType(ex2) != FileShareWitnessExceptionType.FswDeleteError)
							{
								this.m_output.WriteWarning(ex2.LocalizedString);
							}
						}
					}
					bool useAlternateWitnessServer = this.m_useAlternateWitnessServer;
					if (!this.m_skipDagValidation || (flag2 && !flag))
					{
						DagTaskHelper.ChangeQuorumToMnsOrFswAsAppropriate(this.m_output, this, this.m_dag, amCluster, this.m_fsw, this.m_afsw, flag2, this.m_useAlternateWitnessServer);
					}
				}
				else if (!this.m_skipDagValidation && flag)
				{
					DagTaskHelper.RevertToMnsQuorum(this.m_output, amCluster, fswShareCurrent);
				}
			}
		}

		// Token: 0x04002F0D RID: 12045
		private FileShareWitness m_fsw;

		// Token: 0x04002F0E RID: 12046
		private FileShareWitness m_afsw;

		// Token: 0x04002F0F RID: 12047
		private bool m_useAlternateWitnessServer;

		// Token: 0x04002F10 RID: 12048
		private bool m_alternateWitnessServerParameterSpecified;

		// Token: 0x04002F11 RID: 12049
		private bool m_dagConfigParameterSpecified;

		// Token: 0x04002F12 RID: 12050
		private DatabaseAvailabilityGroup m_dag;

		// Token: 0x04002F13 RID: 12051
		private bool m_skipDagValidation;

		// Token: 0x04002F14 RID: 12052
		private HaTaskOutputHelper m_output;

		// Token: 0x04002F15 RID: 12053
		private bool m_IsObjectChanged;

		// Token: 0x04002F16 RID: 12054
		private HashSet<AmServerName> m_serverNamesInDag;

		// Token: 0x04002F17 RID: 12055
		private Dictionary<AmServerName, Server> m_allServers = new Dictionary<AmServerName, Server>(16);

		// Token: 0x04002F18 RID: 12056
		private Dictionary<AmServerName, Server> m_stoppedServers = new Dictionary<AmServerName, Server>(16);

		// Token: 0x020008C2 RID: 2242
		private static class ParameterNames
		{
			// Token: 0x04002F1A RID: 12058
			public const string ReplicationPort = "ReplicationPort";

			// Token: 0x04002F1B RID: 12059
			public const string NetworkCompression = "NetworkCompression";

			// Token: 0x04002F1C RID: 12060
			public const string NetworkEncryption = "NetworkEncryption";

			// Token: 0x04002F1D RID: 12061
			public const string ManualDagNetworkConfiguration = "ManualDagNetworkConfiguration";

			// Token: 0x04002F1E RID: 12062
			public const string DiscoverNetworks = "DiscoverNetworks";

			// Token: 0x04002F1F RID: 12063
			public const string DatacenterActivationMode = "DatacenterActivationMode";

			// Token: 0x04002F20 RID: 12064
			public const string WitnessServer = "WitnessServer";

			// Token: 0x04002F21 RID: 12065
			public const string AlternateWitnessServer = "AlternateWitnessServer";

			// Token: 0x04002F22 RID: 12066
			public const string WitnessDirectory = "WitnessDirectory";

			// Token: 0x04002F23 RID: 12067
			public const string AlternateWitnessDirectory = "AlternateWitnessDirectory";

			// Token: 0x04002F24 RID: 12068
			public const string DatabaseAvailabilityGroupIpAddresses = "DatabaseAvailabilityGroupIpAddresses";

			// Token: 0x04002F25 RID: 12069
			public const string AllowCrossSiteRpcClientAccess = "AllowCrossSiteRpcClientAccess";

			// Token: 0x04002F26 RID: 12070
			public const string DagConfiguration = "DagConfiguration";
		}
	}
}
