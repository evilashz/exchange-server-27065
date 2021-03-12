using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000180 RID: 384
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageOrganizationTaskBase : ComponentInfoBasedTask
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00040BD8 File Offset: 0x0003EDD8
		internal IConfigurationSession Session
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00040BE0 File Offset: 0x0003EDE0
		protected ADObjectId RootOrgContainerId
		{
			get
			{
				return this.rootOrgId;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00040BE8 File Offset: 0x0003EDE8
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00040BF0 File Offset: 0x0003EDF0
		protected bool InternalCreateSharedConfiguration
		{
			get
			{
				return this.createSharedConfig;
			}
			set
			{
				this.createSharedConfig = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00040BF9 File Offset: 0x0003EDF9
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00040C01 File Offset: 0x0003EE01
		protected bool InternalIsSharedConfigServicePlan
		{
			get
			{
				return this.isSharedConfigServicePlan;
			}
			set
			{
				this.isSharedConfigServicePlan = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00040C0A File Offset: 0x0003EE0A
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00040C12 File Offset: 0x0003EE12
		protected OrganizationId InternalSharedConfigurationId
		{
			get
			{
				return this.sharedConfigurationId;
			}
			set
			{
				this.sharedConfigurationId = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00040C1B File Offset: 0x0003EE1B
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00040C23 File Offset: 0x0003EE23
		protected bool InternalLocalStaticConfigEnabled
		{
			get
			{
				return this.localStaticConfigEnabled;
			}
			set
			{
				this.localStaticConfigEnabled = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00040C2C File Offset: 0x0003EE2C
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00040C34 File Offset: 0x0003EE34
		protected bool InternalLocalHydrateableConfigEnabled
		{
			get
			{
				return this.localHydrateableConfigEnabled;
			}
			set
			{
				this.localHydrateableConfigEnabled = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00040C3D File Offset: 0x0003EE3D
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00040C45 File Offset: 0x0003EE45
		protected bool InternalPilotEnabled { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00040C4E File Offset: 0x0003EE4E
		protected override bool IsInnerRunspaceThrottlingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00040C51 File Offset: 0x0003EE51
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00040C77 File Offset: 0x0003EE77
		[Parameter(Mandatory = false)]
		public SwitchParameter EnableFileLogging
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnableFileLogging"] ?? false);
			}
			set
			{
				base.Fields["EnableFileLogging"] = value;
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00040C8F File Offset: 0x0003EE8F
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (ProvisioningLayer.Disabled)
			{
				ProvisioningLayer.Disabled = false;
			}
			base.ShouldWriteLogFile = this.EnableFileLogging;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00040CB8 File Offset: 0x0003EEB8
		public ManageOrganizationTaskBase()
		{
			base.ImplementsResume = false;
			base.IsTenantOrganization = true;
			base.IsDatacenter = Datacenter.IsMicrosoftHostedOnly(false);
			base.IsPartnerHosted = Datacenter.IsPartnerHostedOnly(false);
			base.ComponentInfoFileNames = new List<string>();
			base.ShouldLoadDatacenterConfigFile = false;
			this.InitializeComponentInfoFileNames();
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00040D21 File Offset: 0x0003EF21
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ManageOrganizationTaskModuleFactory();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00040D28 File Offset: 0x0003EF28
		protected virtual void InitializeComponentInfoFileNames()
		{
			base.ComponentInfoFileNames.Add("setup\\data\\CommonGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\TransportGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\BridgeheadGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\ClientAccessGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\MailboxGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\UnifiedMessagingGlobalConfig.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\ProvisioningFeatureCatalog.xml");
			base.ComponentInfoFileNames.Add("setup\\data\\PostPrepForestGlobalConfig.xml");
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00040DB5 File Offset: 0x0003EFB5
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00040DBC File Offset: 0x0003EFBC
		public new LongPath UpdatesDir
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		internal virtual IConfigurationSession CreateSession()
		{
			this.rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, this.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.RescopeToSubtree(sessionSettings), 213, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\ManageOrganizationTaskBase.cs");
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00040E27 File Offset: 0x0003F027
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.configurationSession = this.CreateSession();
			TaskLogger.LogExit();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00040E48 File Offset: 0x0003F048
		protected override bool IsKnownException(Exception e)
		{
			return e is DataSourceOperationException || e is DataSourceTransientException || e is DataValidationException || e is ManagementObjectNotFoundException || e is ManagementObjectAmbiguousException || e is XmlDeserializationException || e is ScriptExecutionException || e is RedirectionEntryManagerException || base.IsKnownException(e);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		protected override void SetRunspaceVariables()
		{
			base.SetRunspaceVariables();
			if (this.InternalSharedConfigurationId != null && !this.InternalSharedConfigurationId.Equals(OrganizationId.ForestWideOrgId))
			{
				base.Fields["SharedConfiguration"] = this.InternalSharedConfigurationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00040EF4 File Offset: 0x0003F0F4
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			this.monadConnection.RunspaceProxy.SetVariable(ManageOrganizationTaskBase.ParameterCreateSharedConfig, this.InternalCreateSharedConfiguration);
			this.monadConnection.RunspaceProxy.SetVariable(ManageOrganizationTaskBase.ParameterCommonHydrateableObjectsSharedEnabled, !this.InternalLocalHydrateableConfigEnabled);
			this.monadConnection.RunspaceProxy.SetVariable(ManageOrganizationTaskBase.ParameterAdvancedHydrateableObjectsSharedEnabled, !this.InternalLocalStaticConfigEnabled);
			this.monadConnection.RunspaceProxy.SetVariable(ManageOrganizationTaskBase.ParameterPilotEnabled, this.InternalPilotEnabled);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00040F90 File Offset: 0x0003F190
		protected bool IsMSITTenant(OrganizationId id)
		{
			string strA = id.ConfigurationUnit.ToString();
			return string.Compare(strA, "sdflabs.com", true) == 0 || string.Compare(strA, "msft.ccsctp.net", true) == 0 || string.Compare(strA, "microsoft.onmicrosoft.com", true) == 0;
		}

		// Token: 0x040006B6 RID: 1718
		protected const string OrganizationHierarchicalPath = "OrganizationHierarchicalPath";

		// Token: 0x040006B7 RID: 1719
		private OrganizationId sharedConfigurationId;

		// Token: 0x040006B8 RID: 1720
		private bool createSharedConfig;

		// Token: 0x040006B9 RID: 1721
		private bool isSharedConfigServicePlan;

		// Token: 0x040006BA RID: 1722
		private bool localStaticConfigEnabled = true;

		// Token: 0x040006BB RID: 1723
		private bool localHydrateableConfigEnabled = true;

		// Token: 0x040006BC RID: 1724
		internal static readonly string ParameterCreateSharedConfig = "CreateSharedConfiguration";

		// Token: 0x040006BD RID: 1725
		internal static readonly string ParameterCommonHydrateableObjectsSharedEnabled = "CommonHydrateableObjectsSharedEnabled";

		// Token: 0x040006BE RID: 1726
		internal static readonly string ParameterAdvancedHydrateableObjectsSharedEnabled = "AdvancedHydrateableObjectsSharedEnabled";

		// Token: 0x040006BF RID: 1727
		internal static readonly string ParameterPilotEnabled = "PilotEnabled";

		// Token: 0x040006C0 RID: 1728
		protected ADObjectId rootOrgId;

		// Token: 0x040006C1 RID: 1729
		private IConfigurationSession configurationSession;
	}
}
