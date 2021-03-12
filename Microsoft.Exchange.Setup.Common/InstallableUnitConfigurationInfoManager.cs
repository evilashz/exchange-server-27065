using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class InstallableUnitConfigurationInfoManager
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00006FFC File Offset: 0x000051FC
		static InstallableUnitConfigurationInfoManager()
		{
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("BridgeheadRole", new BridgeheadRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("ClientAccessRole", new ClientAccessRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("MailboxRole", new MailboxRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("GatewayRole", new GatewayRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("UnifiedMessagingRole", new UnifiedMessagingRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("FrontendTransportRole", new FrontendTransportRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("CafeRole", new CafeRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("AdminToolsRole", new AdminToolsRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("CentralAdminDatabaseRole", new CentralAdminDatabaseRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("CentralAdminRole", new CentralAdminRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("CentralAdminFrontEndRole", new CentralAdminFrontEndRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("MonitoringRole", new MonitoringRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("OSPRole", new OSPRoleConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("UmLanguagePack", new UmLanguagePackConfigurationInfo());
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add("LanguagePacks", new LanguagePackConfigurationInfo());
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007140 File Offset: 0x00005340
		public static InstallableUnitConfigurationInfo GetInstallableUnitConfigurationInfoByName(string installableUnitName)
		{
			InstallableUnitConfigurationInfo result = null;
			if (InstallableUnitConfigurationInfoManager.installableConfigurationInfos.ContainsKey(installableUnitName))
			{
				result = InstallableUnitConfigurationInfoManager.installableConfigurationInfos[installableUnitName];
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007169 File Offset: 0x00005369
		public static bool IsRoleBasedConfigurableInstallableUnit(string installableUnitName)
		{
			return InstallableUnitConfigurationInfoManager.IsServerRoleBasedConfigurableInstallableUnit(installableUnitName) || installableUnitName == "AdminToolsRole";
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007180 File Offset: 0x00005380
		public static bool IsServerRoleBasedConfigurableInstallableUnit(string installableUnitName)
		{
			return installableUnitName == "BridgeheadRole" || installableUnitName == "ClientAccessRole" || installableUnitName == "GatewayRole" || installableUnitName == "MailboxRole" || installableUnitName == "UnifiedMessagingRole" || installableUnitName == "FrontendTransportRole" || installableUnitName == "CafeRole" || installableUnitName == "CentralAdminRole" || installableUnitName == "CentralAdminDatabaseRole" || installableUnitName == "CentralAdminFrontEndRole" || installableUnitName == "MonitoringRole" || installableUnitName == "OSPRole";
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000722F File Offset: 0x0000542F
		public static bool IsUmLanguagePackInstallableUnit(string installableUnitName)
		{
			return installableUnitName.StartsWith("UmLanguagePack");
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000723C File Offset: 0x0000543C
		public static bool IsLanguagePacksInstallableUnit(string installableUnitName)
		{
			return installableUnitName.StartsWith("LanguagePacks");
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000724C File Offset: 0x0000544C
		public static void InitializeUmLanguagePacksConfigurationInfo(params CultureInfo[] cultures)
		{
			foreach (CultureInfo umlang in cultures)
			{
				string umLanguagePackNameForCultureInfo = UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(umlang);
				if (InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(umLanguagePackNameForCultureInfo) == null)
				{
					InstallableUnitConfigurationInfoManager.AddInstallableUnit(UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(umlang), new UmLanguagePackConfigurationInfo(umlang));
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007294 File Offset: 0x00005494
		public static void AddInstallableUnit(string name, InstallableUnitConfigurationInfo info)
		{
			InstallableUnitConfigurationInfoManager.installableConfigurationInfos.Add(name, info);
		}

		// Token: 0x04000063 RID: 99
		private static Dictionary<string, InstallableUnitConfigurationInfo> installableConfigurationInfos = new Dictionary<string, InstallableUnitConfigurationInfo>();
	}
}
