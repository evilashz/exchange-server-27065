using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.SignatureVerification;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PrereqBaseTaskDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000B514 File Offset: 0x00009714
		public PrereqBaseTaskDataHandler(string command, string workUnitText, Icon workUnitIcon, ISetupContext context, MonadConnection connection) : base(context, command, connection)
		{
			base.WorkUnit.Text = workUnitText;
			base.WorkUnit.Icon = workUnitIcon;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B568 File Offset: 0x00009768
		public PrereqBaseTaskDataHandler(string command, string role, string workUnitText, Icon workUnitIcon, ISetupContext context, MonadConnection connection) : base(context, command, connection)
		{
			base.WorkUnit.Text = workUnitText;
			base.WorkUnit.Icon = workUnitIcon;
			this.Roles = new List<string>(new string[]
			{
				role
			});
			this.InitializeParameters();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B5D8 File Offset: 0x000097D8
		public PrereqBaseTaskDataHandler(string command, string installableUnitName, ISetupContext context, MonadConnection connection) : base(context, command, connection)
		{
			InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
			base.WorkUnit.Text = installableUnitConfigurationInfoByName.DisplayName + " " + Strings.Prereqs;
			base.WorkUnit.Icon = null;
			base.WorkUnit.CanShowExecutedCommand = false;
			this.Roles = new List<string>(new string[]
			{
				this.GetRoleName(installableUnitConfigurationInfoByName.Name)
			});
			this.InitializeParameters();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000B681 File Offset: 0x00009881
		public bool HasRoles
		{
			get
			{
				return this.Roles != null && this.Roles.Count != 0;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000B69E File Offset: 0x0000989E
		public bool HasSelectedInstallableUnits
		{
			get
			{
				return this.SelectedInstallableUnits != null && this.SelectedInstallableUnits.Count != 0;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000B6BB File Offset: 0x000098BB
		public void AddRole(string role)
		{
			if (!this.Roles.Contains(role))
			{
				this.Roles.Add(role);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public void AddRoleByUnitName(string installableUnitName)
		{
			InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
			this.AddRole(this.GetRoleName(installableUnitConfigurationInfoByName.Name));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B6FE File Offset: 0x000098FE
		public void AddSelectedInstallableUnit(string unit)
		{
			if (!this.SelectedInstallableUnits.Contains(unit))
			{
				this.SelectedInstallableUnits.Add(unit);
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B71A File Offset: 0x0000991A
		public void AddSelectedInstallableUnits(IEnumerable<string> units)
		{
			this.SelectedInstallableUnits.AddRange(units);
			this.SelectedInstallableUnits = this.SelectedInstallableUnits.Distinct<string>().ToList<string>();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B740 File Offset: 0x00009940
		public void InitializeParameters()
		{
			if (!this.HasRoles)
			{
				throw new ArgumentNullException("this.Roles should not be null or empty.");
			}
			this.ScanType = this.GetScanType(base.SetupContext.InstallationMode);
			this.DomainController = base.SetupContext.DomainController;
			this.RunningVersion = base.SetupContext.RunningVersion;
			string roleName = this.GetRoleName("AdminToolsRole");
			string roleName2 = this.GetRoleName("GatewayRole");
			if (base.SetupContext.ADInitializationError != null && base.SetupContext.ADInitializationError.GetType() == typeof(ADInitializationException) && (this.Roles.Count != 2 || !this.Roles.Contains(roleName) || !this.Roles.Contains(roleName2)) && (this.Roles.Count != 1 || (!this.Roles.Contains(roleName) && !this.Roles.Contains(roleName2))))
			{
				this.ADInitError = base.SetupContext.ADInitializationError.Message;
			}
			if (this.Roles.Contains(this.GetRoleName("LanguagePacks")))
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.LanguagePackDir] = base.SetupContext.LanguagePackPath;
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.LanguagesAvailableToInstall] = (base.SetupContext.LanguagePacksToInstall.Count > 0);
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.SufficientLanguagePackDiskSpace] = this.IsDiskSpaceSufficientForLanguagePackInstallation;
				bool flag = true;
				if (this.Roles.Contains(this.GetRoleName("MailboxRole")))
				{
					flag = this.IsLPVersionCompatible;
				}
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.LanguagePackVersioning] = flag;
			}
			if (this.Roles.Contains(this.GetRoleName("UmLanguagePack")))
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.AlreadyInstallUMLanguages] = this.UMLanguagePackInstalledFromSelectedCultures;
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.LanguagePacksInstalled] = this.IsClientLanguagePackInstalledForSelectedCultures;
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.SufficientLanguagePackDiskSpace] = this.IsDiskSpaceSufficientForLanguagePackInstallation;
			}
			this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.HostingDeploymentEnabled] = base.SetupContext.HostingDeployment;
			this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PathToDCHybridConfigFile] = base.SetupContext.TenantOrganizationConfig;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000B980 File Offset: 0x00009B80
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000B9AE File Offset: 0x00009BAE
		internal List<string> Roles
		{
			get
			{
				if (this.GetValueOrDefault(PrereqBaseTaskDataHandler.MandatoryParameterNames.Roles, null) == null)
				{
					this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.Roles] = new List<string>();
				}
				return (List<string>)this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.Roles];
			}
			private set
			{
				this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.Roles] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000B9BD File Offset: 0x00009BBD
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		private ScanType ScanType
		{
			get
			{
				return (ScanType)this.GetValueOrDefault(PrereqBaseTaskDataHandler.MandatoryParameterNames.ScanType, ScanType.PrecheckInstall);
			}
			set
			{
				this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.ScanType] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000B9E5 File Offset: 0x00009BE5
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000B9ED File Offset: 0x00009BED
		public List<string> SelectedInstallableUnits
		{
			get
			{
				return this.selectedInstallableUnits;
			}
			set
			{
				this.selectedInstallableUnits = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B9F6 File Offset: 0x00009BF6
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000BA05 File Offset: 0x00009C05
		public string DomainController
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.MandatoryParameterNames.DomainController, null);
			}
			set
			{
				this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.DomainController] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BA14 File Offset: 0x00009C14
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000BA23 File Offset: 0x00009C23
		public Version RunningVersion
		{
			get
			{
				return (Version)this.GetValueOrDefault(PrereqBaseTaskDataHandler.MandatoryParameterNames.ExchangeVersion, null);
			}
			set
			{
				this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.ExchangeVersion] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000BA32 File Offset: 0x00009C32
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000BA47 File Offset: 0x00009C47
		public ushort AdamLdapPort
		{
			get
			{
				return (ushort)((int)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.AdamPort, 0));
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.AdamPort] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000BA5B File Offset: 0x00009C5B
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000BA70 File Offset: 0x00009C70
		public ushort AdamSslPort
		{
			get
			{
				return (ushort)((int)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.AdamSslPort, 0));
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.AdamSslPort] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000BA84 File Offset: 0x00009C84
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000BA94 File Offset: 0x00009C94
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.CustomerFeedbackEnabled, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.CustomerFeedbackEnabled] = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public string NewProvisionedServerName
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.NewProvisionedServerName, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.NewProvisionedServerName] = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000BAD6 File Offset: 0x00009CD6
		public string RemoveProvisionedServerName
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.RemoveProvisionedServerName, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.RemoveProvisionedServerName] = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000BAE5 File Offset: 0x00009CE5
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		public LocalLongFullPath TargetDir
		{
			get
			{
				return (LocalLongFullPath)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.TargetDir, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.TargetDir] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000BB03 File Offset: 0x00009D03
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000BB17 File Offset: 0x00009D17
		public bool PrepareAllDomains
		{
			get
			{
				return (bool)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareAllDomains, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareAllDomains] = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BB2B File Offset: 0x00009D2B
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000BB3F File Offset: 0x00009D3F
		public bool PrepareSCT
		{
			get
			{
				return (bool)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareSCT, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareSCT] = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000BB53 File Offset: 0x00009D53
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public bool PrepareDomain
		{
			get
			{
				return this.setPrepareDomain;
			}
			set
			{
				this.setPrepareDomain = value;
				if (this.setPrepareDomain)
				{
					if (!this.optionalParameters.ContainsKey(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain))
					{
						this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain] = null;
						return;
					}
				}
				else
				{
					this.optionalParameters.Remove(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain);
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000BB95 File Offset: 0x00009D95
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public string PrepareDomainTarget
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain] = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public bool PrepareSchema
		{
			get
			{
				return (bool)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareSchema, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareSchema] = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000BBDD File Offset: 0x00009DDD
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000BBF2 File Offset: 0x00009DF2
		public bool PrepareOrganization
		{
			get
			{
				return (bool)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareOrganization, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareOrganization] = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000BC07 File Offset: 0x00009E07
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000BC17 File Offset: 0x00009E17
		public string ADInitError
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.ADInitError, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.ADInitError] = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000BC27 File Offset: 0x00009E27
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000BC37 File Offset: 0x00009E37
		public bool? ActiveDirectorySplitPermissions
		{
			get
			{
				return (bool?)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.ActiveDirectorySplitPermissions, null);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.ActiveDirectorySplitPermissions] = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000BC4C File Offset: 0x00009E4C
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000BC61 File Offset: 0x00009E61
		public bool HostingDeploymentEnabled
		{
			get
			{
				return (bool)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.HostingDeploymentEnabled, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.HostingDeploymentEnabled] = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000BC76 File Offset: 0x00009E76
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000BC8B File Offset: 0x00009E8B
		public string PathToDCHybridConfigFile
		{
			get
			{
				return (string)this.GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames.PathToDCHybridConfigFile, false);
			}
			set
			{
				this.optionalParameters[PrereqBaseTaskDataHandler.OptionalParameterNames.PathToDCHybridConfigFile] = value;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000BC9C File Offset: 0x00009E9C
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.IsDatacenter)
			{
				base.Parameters.AddWithValue("IsDatacenter", base.SetupContext.IsDatacenter);
			}
			if (this.Roles.Contains(this.GetRoleName("ClientAccessRole")) || this.Roles.Contains(this.GetRoleName("MailboxRole")) || this.Roles.Contains(this.GetRoleName("CafeRole")) || this.Roles.Contains(this.GetRoleName("FrontendTransportRole")))
			{
				base.SetupContext.UpdateIsW3SVCStartOk();
			}
			List<string> list = new List<string>();
			if (this.HasSelectedInstallableUnits)
			{
				foreach (string installableUnitName in this.SelectedInstallableUnits)
				{
					list.Add(this.GetRoleName(installableUnitName));
				}
			}
			if (this.Roles.Contains("Global"))
			{
				list.Add("Global");
			}
			this.commonParameters[PrereqBaseTaskDataHandler.MandatoryParameterNames.SetupRoles] = list;
			List<string> list2 = new List<string>();
			list2.AddRange(Enum.GetNames(typeof(PrereqBaseTaskDataHandler.MandatoryParameterNames)));
			list2.Sort();
			foreach (string value in list2)
			{
				PrereqBaseTaskDataHandler.MandatoryParameterNames mandatoryParameterNames = (PrereqBaseTaskDataHandler.MandatoryParameterNames)Enum.Parse(typeof(PrereqBaseTaskDataHandler.MandatoryParameterNames), value);
				object value2 = this.commonParameters[mandatoryParameterNames];
				base.Parameters.AddWithValue(mandatoryParameterNames.ToString(), value2);
			}
			List<PrereqBaseTaskDataHandler.OptionalParameterNames> list3 = new List<PrereqBaseTaskDataHandler.OptionalParameterNames>();
			list3.AddRange(this.optionalParameters.Keys);
			list3.Sort();
			foreach (PrereqBaseTaskDataHandler.OptionalParameterNames optionalParameterNames in list3)
			{
				object obj = this.optionalParameters[optionalParameterNames];
				if (obj != null || (optionalParameterNames == PrereqBaseTaskDataHandler.OptionalParameterNames.PrepareDomain && this.setPrepareDomain))
				{
					base.Parameters.AddWithValue(optionalParameterNames.ToString(), obj);
				}
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000BF08 File Offset: 0x0000A108
		private ScanType GetScanType(InstallationModes mode)
		{
			ScanType result;
			switch (base.SetupContext.InstallationMode)
			{
			case InstallationModes.Install:
				result = ScanType.PrecheckInstall;
				break;
			case InstallationModes.BuildToBuildUpgrade:
				result = ScanType.PrecheckUpgrade;
				break;
			case InstallationModes.DisasterRecovery:
				result = ScanType.PrecheckDR;
				break;
			case InstallationModes.Uninstall:
				result = ScanType.PrecheckUninstall;
				break;
			default:
				throw new ArgumentException("There is no scan type for this mode", "mode");
			}
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private string GetRoleName(string installableUnitName)
		{
			if (InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName))
			{
				return "UmLanguagePack";
			}
			return installableUnitName.Replace("Role", "");
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000BF7C File Offset: 0x0000A17C
		private object GetValueOrDefault(PrereqBaseTaskDataHandler.OptionalParameterNames parameterName, object defaultValue)
		{
			object result;
			if (this.optionalParameters.TryGetValue(parameterName, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000BF9C File Offset: 0x0000A19C
		private object GetValueOrDefault(PrereqBaseTaskDataHandler.MandatoryParameterNames parameterName, object defaultValue)
		{
			object result;
			if (this.commonParameters.TryGetValue(parameterName, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private bool IsClientLanguagePackInstalledForSelectedCultures
		{
			get
			{
				bool flag = true;
				foreach (CultureInfo language in base.SetupContext.SelectedCultures)
				{
					flag = (flag && this.IsClientLanguageInstalledForCulture(language));
				}
				return flag;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000C020 File Offset: 0x0000A220
		private bool IsClientLanguageInstalledForCulture(CultureInfo language)
		{
			List<CultureInfo> supportedClientCultures = UmCultures.GetSupportedClientCultures();
			return UmCultures.GetBestSupportedCulture(supportedClientCultures, language) != null;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C040 File Offset: 0x0000A240
		private string UMLanguagePackInstalledFromSelectedCultures
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (CultureInfo cultureInfo in base.SetupContext.SelectedCultures)
				{
					if (base.SetupContext.InstalledUMLanguagePacks.Contains(cultureInfo))
					{
						stringBuilder.Append(cultureInfo.ToString()).Append(" ");
					}
				}
				return stringBuilder.ToString().Trim();
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		private bool IsDiskSpaceSufficientForLanguagePackInstallation
		{
			get
			{
				long num = 0L;
				DriveInfo driveInfo = new DriveInfo(base.SetupContext.TargetDir.DriveName.Substring(0, 1));
				if (this.Roles.Contains(this.GetRoleName("LanguagePacks")))
				{
					foreach (long num2 in base.SetupContext.LanguagesToInstall.Values)
					{
						num += num2;
					}
				}
				if (this.Roles.Contains(this.GetRoleName("UmLanguagePack")))
				{
					foreach (CultureInfo culture in base.SetupContext.SelectedCultures)
					{
						LongPath umlanguagePackFilename = UMLanguagePackHelper.GetUMLanguagePackFilename(base.SetupContext.SourceDir.PathName, culture);
						if (umlanguagePackFilename != null && File.Exists(umlanguagePackFilename.PathName))
						{
							long length = new FileInfo(umlanguagePackFilename.PathName).Length;
							num += length * 4L;
						}
					}
				}
				return driveInfo.AvailableFreeSpace > num;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C210 File Offset: 0x0000A410
		private bool IsLPVersionCompatible
		{
			get
			{
				bool result;
				try
				{
					bool flag = true;
					string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LPVersioning.xml");
					LanguagePackVersion languagePackVersion = null;
					if (base.SetupContext.LanguagePackSourceIsBundle)
					{
						string text2 = Path.Combine(Path.GetTempPath(), "LBVersioningFromBundle");
						EmbeddedCabWrapper.ExtractFiles(base.SetupContext.LanguagePackPath.ToString(), text2, "LPVersioning.xml");
						text2 = Path.Combine(text2, "LPVersioning.xml");
						if (!File.Exists(text2))
						{
							throw new LanguagePackBundleLoadException(Strings.LPVersioningExtractionFailed(text2));
						}
						languagePackVersion = new LanguagePackVersion(text, text2);
						flag = languagePackVersion.IsExchangeInApplicableRange(new Version(LanguagePackVersion.GetBuildVersion(text2)));
					}
					else if (base.SetupContext.InstallationMode == InstallationModes.BuildToBuildUpgrade && base.SetupContext.IsLanaguagePacksInstalled)
					{
						languagePackVersion = new LanguagePackVersion(text, text);
					}
					if (flag && (base.SetupContext.LanguagePackSourceIsBundle || (base.SetupContext.InstallationMode == InstallationModes.BuildToBuildUpgrade && base.SetupContext.IsLanaguagePacksInstalled)))
					{
						string text3 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LPVersioning.xml");
						if (text3 != languagePackVersion.LanguagePackVersioningPath)
						{
							File.Copy(languagePackVersion.LanguagePackVersioningPath, text3, true);
						}
					}
					result = flag;
				}
				catch (SignatureVerificationException e)
				{
					SetupLogger.LogError(e);
					result = true;
				}
				catch (LPVersioningValueException e2)
				{
					SetupLogger.LogError(e2);
					result = false;
				}
				catch (LanguagePackBundleLoadException e3)
				{
					SetupLogger.LogError(e3);
					result = false;
				}
				catch (CabUtilityWrapperException e4)
				{
					SetupLogger.LogError(e4);
					result = false;
				}
				return result;
			}
		}

		// Token: 0x040000CE RID: 206
		public const string OrganizationRole = "Global";

		// Token: 0x040000CF RID: 207
		private List<string> selectedInstallableUnits = new List<string>();

		// Token: 0x040000D0 RID: 208
		private Dictionary<PrereqBaseTaskDataHandler.MandatoryParameterNames, object> commonParameters = new Dictionary<PrereqBaseTaskDataHandler.MandatoryParameterNames, object>();

		// Token: 0x040000D1 RID: 209
		private Dictionary<PrereqBaseTaskDataHandler.OptionalParameterNames, object> optionalParameters = new Dictionary<PrereqBaseTaskDataHandler.OptionalParameterNames, object>();

		// Token: 0x040000D2 RID: 210
		private bool setPrepareDomain;

		// Token: 0x0200004B RID: 75
		private enum MandatoryParameterNames
		{
			// Token: 0x040000D4 RID: 212
			DomainController,
			// Token: 0x040000D5 RID: 213
			ExchangeVersion,
			// Token: 0x040000D6 RID: 214
			Roles,
			// Token: 0x040000D7 RID: 215
			ScanType,
			// Token: 0x040000D8 RID: 216
			SetupRoles
		}

		// Token: 0x0200004C RID: 76
		private enum OptionalParameterNames
		{
			// Token: 0x040000DA RID: 218
			AdamPort,
			// Token: 0x040000DB RID: 219
			AdamSslPort,
			// Token: 0x040000DC RID: 220
			CreatePublicDB,
			// Token: 0x040000DD RID: 221
			NewProvisionedServerName,
			// Token: 0x040000DE RID: 222
			RemoveProvisionedServerName,
			// Token: 0x040000DF RID: 223
			TargetDir,
			// Token: 0x040000E0 RID: 224
			PrepareAllDomains,
			// Token: 0x040000E1 RID: 225
			PrepareDomain,
			// Token: 0x040000E2 RID: 226
			PrepareSCT,
			// Token: 0x040000E3 RID: 227
			PrepareOrganization,
			// Token: 0x040000E4 RID: 228
			PrepareSchema,
			// Token: 0x040000E5 RID: 229
			ADInitError,
			// Token: 0x040000E6 RID: 230
			CustomerFeedbackEnabled,
			// Token: 0x040000E7 RID: 231
			LanguagePackDir,
			// Token: 0x040000E8 RID: 232
			LanguagesAvailableToInstall,
			// Token: 0x040000E9 RID: 233
			SufficientLanguagePackDiskSpace,
			// Token: 0x040000EA RID: 234
			LanguagePackVersioning,
			// Token: 0x040000EB RID: 235
			ActiveDirectorySplitPermissions,
			// Token: 0x040000EC RID: 236
			LanguagePacksInstalled,
			// Token: 0x040000ED RID: 237
			AlreadyInstallUMLanguages,
			// Token: 0x040000EE RID: 238
			HostingDeploymentEnabled,
			// Token: 0x040000EF RID: 239
			PathToDCHybridConfigFile
		}
	}
}
