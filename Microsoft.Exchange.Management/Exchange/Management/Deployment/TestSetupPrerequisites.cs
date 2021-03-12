using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Analysis;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200026B RID: 619
	[Cmdlet("test", "SetupPrerequisites")]
	public class TestSetupPrerequisites : Task
	{
		// Token: 0x060016EE RID: 5870 RVA: 0x000619B4 File Offset: 0x0005FBB4
		public TestSetupPrerequisites()
		{
			this.IsDatacenter = false;
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x000619C8 File Offset: 0x0005FBC8
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x000619DF File Offset: 0x0005FBDF
		[Parameter(Mandatory = true)]
		public string[] Roles
		{
			get
			{
				return (string[])base.Fields["Roles"];
			}
			set
			{
				base.Fields["Roles"] = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x000619F2 File Offset: 0x0005FBF2
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00061A09 File Offset: 0x0005FC09
		[Parameter(Mandatory = false)]
		public string ScanType
		{
			get
			{
				return (string)base.Fields["ScanType"];
			}
			set
			{
				base.Fields["ScanType"] = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00061A1C File Offset: 0x0005FC1C
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00061A33 File Offset: 0x0005FC33
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDatacenter
		{
			get
			{
				return (SwitchParameter)base.Fields["IsDatacenter"];
			}
			set
			{
				base.Fields["IsDatacenter"] = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00061A4B File Offset: 0x0005FC4B
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00061A62 File Offset: 0x0005FC62
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TargetDir
		{
			get
			{
				return (LocalLongFullPath)base.Fields["TargetDir"];
			}
			set
			{
				base.Fields["TargetDir"] = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00061A75 File Offset: 0x0005FC75
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00061A8C File Offset: 0x0005FC8C
		[Parameter(Mandatory = true)]
		public string ExchangeVersion
		{
			get
			{
				return (string)base.Fields["ExchangeVersion"];
			}
			set
			{
				base.Fields["ExchangeVersion"] = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00061A9F File Offset: 0x0005FC9F
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00061AB6 File Offset: 0x0005FCB6
		[Parameter(Mandatory = false)]
		public string[] SetupRoles
		{
			get
			{
				return (string[])base.Fields["SetupRoles"];
			}
			set
			{
				base.Fields["SetupRoles"] = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00061AC9 File Offset: 0x0005FCC9
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00061AE0 File Offset: 0x0005FCE0
		[Parameter(Mandatory = false)]
		public int ADAMPort
		{
			get
			{
				return (int)base.Fields["ADAMPort"];
			}
			set
			{
				base.Fields["ADAMPort"] = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00061AF8 File Offset: 0x0005FCF8
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x00061B0F File Offset: 0x0005FD0F
		[Parameter(Mandatory = false)]
		public int ADAMSSLPort
		{
			get
			{
				return (int)base.Fields["ADAMSSLPort"];
			}
			set
			{
				base.Fields["ADAMSSLPort"] = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00061B27 File Offset: 0x0005FD27
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x00061B3E File Offset: 0x0005FD3E
		[Parameter(Mandatory = false)]
		public bool CreatePublicDB
		{
			get
			{
				return (bool)base.Fields["CreatePublicDB"];
			}
			set
			{
				base.Fields["CreatePublicDB"] = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00061B56 File Offset: 0x0005FD56
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x00061B6D File Offset: 0x0005FD6D
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00061B85 File Offset: 0x0005FD85
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x00061B9C File Offset: 0x0005FD9C
		[Parameter(Mandatory = false)]
		public string NewProvisionedServerName
		{
			get
			{
				return (string)base.Fields["NewProvisionedServerName"];
			}
			set
			{
				base.Fields["NewProvisionedServerName"] = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00061BAF File Offset: 0x0005FDAF
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x00061BC6 File Offset: 0x0005FDC6
		[Parameter(Mandatory = false)]
		public string RemoveProvisionedServerName
		{
			get
			{
				return (string)base.Fields["RemoveProvisionedServerName"];
			}
			set
			{
				base.Fields["RemoveProvisionedServerName"] = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00061BD9 File Offset: 0x0005FDD9
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x00061BF0 File Offset: 0x0005FDF0
		[Parameter(Mandatory = false)]
		public string PrepareDomain
		{
			get
			{
				return (string)base.Fields["PrepareDomain"];
			}
			set
			{
				base.Fields["PrepareDomain"] = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00061C03 File Offset: 0x0005FE03
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00061C1A File Offset: 0x0005FE1A
		[Parameter(Mandatory = false)]
		public bool PrepareSCT
		{
			get
			{
				return (bool)base.Fields["PrepareSCT"];
			}
			set
			{
				base.Fields["PrepareSCT"] = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00061C32 File Offset: 0x0005FE32
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x00061C49 File Offset: 0x0005FE49
		[Parameter(Mandatory = false)]
		public bool PrepareOrganization
		{
			get
			{
				return (bool)base.Fields["PrepareOrganization"];
			}
			set
			{
				base.Fields["PrepareOrganization"] = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00061C61 File Offset: 0x0005FE61
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00061C78 File Offset: 0x0005FE78
		[Parameter(Mandatory = false)]
		public bool PrepareSchema
		{
			get
			{
				return (bool)base.Fields["PrepareSchema"];
			}
			set
			{
				base.Fields["PrepareSchema"] = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00061C90 File Offset: 0x0005FE90
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00061CA7 File Offset: 0x0005FEA7
		[Parameter(Mandatory = false)]
		public bool PrepareAllDomains
		{
			get
			{
				return (bool)base.Fields["PrepareAllDomains"];
			}
			set
			{
				base.Fields["PrepareAllDomains"] = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00061CBF File Offset: 0x0005FEBF
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00061CD6 File Offset: 0x0005FED6
		[Parameter(Mandatory = false)]
		public string ADInitError
		{
			get
			{
				return (string)base.Fields["ADInitError"];
			}
			set
			{
				base.Fields["ADInitError"] = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00061CE9 File Offset: 0x0005FEE9
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00061D00 File Offset: 0x0005FF00
		[Parameter(Mandatory = false)]
		public string LanguagePackDir
		{
			get
			{
				return (string)base.Fields["LanguagePackDir"];
			}
			set
			{
				base.Fields["LanguagePackDir"] = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00061D13 File Offset: 0x0005FF13
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00061D2A File Offset: 0x0005FF2A
		[Parameter(Mandatory = false)]
		public bool LanguagesAvailableToInstall
		{
			get
			{
				return (bool)base.Fields["LanguagesAvailableToInstall"];
			}
			set
			{
				base.Fields["LanguagesAvailableToInstall"] = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00061D42 File Offset: 0x0005FF42
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00061D59 File Offset: 0x0005FF59
		[Parameter(Mandatory = false)]
		public bool LanguagePackVersioning
		{
			get
			{
				return (bool)base.Fields["LanguagePackVersioning"];
			}
			set
			{
				base.Fields["LanguagePackVersioning"] = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00061D71 File Offset: 0x0005FF71
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x00061D88 File Offset: 0x0005FF88
		[Parameter(Mandatory = false)]
		public bool SufficientLanguagePackDiskSpace
		{
			get
			{
				return (bool)base.Fields["SufficientLanguagePackDiskSpace"];
			}
			set
			{
				base.Fields["SufficientLanguagePackDiskSpace"] = value;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x00061DB7 File Offset: 0x0005FFB7
		[Parameter(Mandatory = false)]
		public bool LanguagePacksInstalled
		{
			get
			{
				return (bool)base.Fields["LanguagePacksInstalled"];
			}
			set
			{
				base.Fields["LanguagePacksInstalled"] = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00061DCF File Offset: 0x0005FFCF
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x00061DE6 File Offset: 0x0005FFE6
		[Parameter(Mandatory = false)]
		public string AlreadyInstallUMLanguages
		{
			get
			{
				return (string)base.Fields["AlreadyInstallUMLanguages"];
			}
			set
			{
				base.Fields["AlreadyInstallUMLanguages"] = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00061DF9 File Offset: 0x0005FFF9
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x00061E10 File Offset: 0x00060010
		[Parameter(Mandatory = false)]
		public bool? ActiveDirectorySplitPermissions
		{
			get
			{
				return (bool?)base.Fields["ActiveDirectorySplitPermissions"];
			}
			set
			{
				base.Fields["ActiveDirectorySplitPermissions"] = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00061E28 File Offset: 0x00060028
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00061E3F File Offset: 0x0006003F
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00061E52 File Offset: 0x00060052
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00061E69 File Offset: 0x00060069
		[Parameter(Mandatory = false)]
		public bool HostingDeploymentEnabled
		{
			get
			{
				return (bool)base.Fields["HostingDeploymentEnabled"];
			}
			set
			{
				base.Fields["HostingDeploymentEnabled"] = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00061E81 File Offset: 0x00060081
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00061E98 File Offset: 0x00060098
		[Parameter(Mandatory = false)]
		public string PathToDCHybridConfigFile
		{
			get
			{
				return (string)base.Fields["PathToDCHybridConfigFile"];
			}
			set
			{
				base.Fields["PathToDCHybridConfigFile"] = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00061EAC File Offset: 0x000600AC
		internal SetupRole PrereqSetupRoles
		{
			get
			{
				SetupRole setupRole = SetupRole.None;
				if (this.Roles != null)
				{
					foreach (string value in this.Roles)
					{
						if (!string.IsNullOrEmpty(value))
						{
							try
							{
								setupRole |= (SetupRole)Enum.Parse(typeof(SetupRole), value, true);
							}
							catch (ArgumentException)
							{
							}
							catch (OverflowException)
							{
							}
						}
					}
				}
				return setupRole;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00061F24 File Offset: 0x00060124
		internal SetupMode PrereqSetupMode
		{
			get
			{
				SetupMode result = SetupMode.None;
				string a;
				if (!string.IsNullOrEmpty(this.ScanType) && (a = this.ScanType.ToLower()) != null)
				{
					if (!(a == "precheckdr"))
					{
						if (!(a == "precheckinstall"))
						{
							if (!(a == "precheckuninstall"))
							{
								if (a == "precheckupgrade")
								{
									result = SetupMode.Upgrade;
								}
							}
							else
							{
								result = SetupMode.Uninstall;
							}
						}
						else
						{
							result = SetupMode.Install;
						}
					}
					else
					{
						result = SetupMode.DisasterRecovery;
					}
				}
				return result;
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00061F94 File Offset: 0x00060194
		protected override void InternalBeginProcessing()
		{
			if (!base.Fields.Contains("TargetDir"))
			{
				this.TargetDir = null;
			}
			if (!base.Fields.Contains("ExchangeVersion"))
			{
				this.ExchangeVersion = "1.0.0.0";
			}
			if (!base.Fields.Contains("SetupRoles"))
			{
				this.SetupRoles = null;
			}
			if (!base.Fields.Contains("ADAMPort"))
			{
				this.ADAMPort = 50389;
			}
			if (!base.Fields.Contains("ADAMSSLPort"))
			{
				this.ADAMSSLPort = 50636;
			}
			if (!base.Fields.Contains("CreatePublicDB"))
			{
				this.CreatePublicDB = false;
			}
			if (!base.Fields.Contains("CustomerFeedbackEnabled"))
			{
				this.CustomerFeedbackEnabled = null;
			}
			if (!base.Fields.Contains("NewProvisionedServerName"))
			{
				this.NewProvisionedServerName = "";
			}
			if (!base.Fields.Contains("RemoveProvisionedServerName"))
			{
				this.RemoveProvisionedServerName = "";
			}
			if (!base.Fields.Contains("PrepareDomain"))
			{
				this.PrepareDomain = "";
			}
			if (!base.Fields.Contains("PrepareSCT"))
			{
				this.PrepareSCT = false;
			}
			if (!base.Fields.Contains("PrepareOrganization"))
			{
				this.PrepareOrganization = false;
			}
			if (!base.Fields.Contains("PrepareSchema"))
			{
				this.PrepareSchema = false;
			}
			if (!base.Fields.Contains("PrepareAllDomains"))
			{
				this.PrepareAllDomains = false;
			}
			if (!base.Fields.Contains("ADInitError"))
			{
				this.ADInitError = "";
			}
			if (!base.Fields.Contains("LanguagePackDir"))
			{
				this.LanguagePackDir = "";
			}
			if (!base.Fields.Contains("LanguagesAvailableToInstall"))
			{
				this.LanguagesAvailableToInstall = false;
			}
			if (!base.Fields.Contains("SufficientLanguagePackDiskSpace"))
			{
				this.SufficientLanguagePackDiskSpace = false;
			}
			if (!base.Fields.Contains("LanguagePacksInstalled"))
			{
				this.LanguagePacksInstalled = false;
			}
			if (!base.Fields.Contains("AlreadyInstallUMLanguages"))
			{
				this.AlreadyInstallUMLanguages = "";
			}
			if (!base.Fields.Contains("LanguagePackVersioning"))
			{
				this.LanguagePackVersioning = true;
			}
			if (!base.Fields.Contains("ActiveDirectorySplitPermissions"))
			{
				this.ActiveDirectorySplitPermissions = null;
			}
			if (!base.Fields.Contains("HostingDeploymentEnabled"))
			{
				this.HostingDeploymentEnabled = false;
			}
			if (!base.Fields.Contains("PathToDCHybridConfigFile"))
			{
				this.PathToDCHybridConfigFile = "";
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00062231 File Offset: 0x00060431
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00062244 File Offset: 0x00060444
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 468, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\TestSetupPrerequisites.cs");
			string targetDir = (this.TargetDir != null) ? this.TargetDir.ToString() : null;
			Version exchangeVersion = new Version(this.ExchangeVersion);
			int adamport = this.ADAMPort;
			int adamsslport = this.ADAMSSLPort;
			bool createPublicDB = this.CreatePublicDB;
			bool customerFeedbackEnabled = this.CustomerFeedbackEnabled != null && this.CustomerFeedbackEnabled.Value;
			string newProvisionedServerName = this.NewProvisionedServerName;
			string removeProvisionedServerName = this.RemoveProvisionedServerName;
			Fqdn fqdn = topologyConfigurationSession.ServerSettings.PreferredGlobalCatalog(TopologyProvider.LocalForestFqdn);
			GlobalParameters globalParameters = new GlobalParameters(targetDir, exchangeVersion, adamport, adamsslport, createPublicDB, customerFeedbackEnabled, newProvisionedServerName, removeProvisionedServerName, (fqdn != null) ? fqdn : "", (this.DomainController != null) ? this.DomainController.ToString() : "", this.PrepareDomain, this.PrepareSCT, this.PrepareOrganization, this.PrepareSchema, this.PrepareAllDomains, this.ADInitError, this.LanguagePackDir, this.LanguagesAvailableToInstall, this.SufficientLanguagePackDiskSpace, this.LanguagePacksInstalled, this.AlreadyInstallUMLanguages, this.LanguagePackVersioning, this.ActiveDirectorySplitPermissions != null && this.ActiveDirectorySplitPermissions.Value, this.SetupRoles, this.GetIgnoreFilesInUseFlag(), this.HostingDeploymentEnabled, this.PathToDCHybridConfigFile, this.IsDatacenter);
			try
			{
				TaskLogger.LogAllAsInfo = true;
				SetupPrereqChecks setupPrereqChecks = new SetupPrereqChecks(this.PrereqSetupMode, this.PrereqSetupRoles, globalParameters);
				setupPrereqChecks.DoCheckPrereqs(new Action<int>(this.WriteProgressRecord), this);
			}
			finally
			{
				TaskLogger.LogAllAsInfo = false;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000623F8 File Offset: 0x000605F8
		private bool GetIgnoreFilesInUseFlag()
		{
			if (this.IsDatacenter)
			{
				ParameterCollection parameterCollection = RolesUtility.ReadSetupParameters(this.IsDatacenter);
				foreach (Parameter parameter in parameterCollection)
				{
					if (string.Equals(parameter.Name, "IgnoreFilesInUse", StringComparison.InvariantCultureIgnoreCase))
					{
						return (bool)parameter.EffectiveValue;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00062484 File Offset: 0x00060684
		private void WriteProgressRecord(int percentage)
		{
			base.WriteProgress(Strings.SetupPrereqAnalysis, (percentage >= 100) ? Strings.SetupPrereqAnalysisCompleted : Strings.SetupPrereqAnalysisInProgress, percentage);
		}
	}
}
