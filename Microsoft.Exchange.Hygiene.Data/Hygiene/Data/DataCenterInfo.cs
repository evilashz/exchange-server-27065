using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Win32;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000076 RID: 118
	internal class DataCenterInfo
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000F3DD File Offset: 0x0000D5DD
		public static FFORole GetConfiguredFFORolesBitMask()
		{
			if (DataCenterInfo.configuredFfoRolesBitMask == null)
			{
				DataCenterInfo.LoadFfoConfiguredRoles();
			}
			return DataCenterInfo.configuredFfoRolesBitMask.Value;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000F3FA File Offset: 0x0000D5FA
		public static IEnumerable<FFORole> GetConfiguredFFORoles()
		{
			if (DataCenterInfo.configuredFfoRolesBitMask == null)
			{
				DataCenterInfo.LoadFfoConfiguredRoles();
			}
			return DataCenterInfo.configuredFfoRoles;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000F412 File Offset: 0x0000D612
		public static EXORole GetConfiguredEXORolesBitMask()
		{
			if (DataCenterInfo.configuredExoRolesBitMask == null)
			{
				DataCenterInfo.LoadExoConfiguredRoles();
			}
			return DataCenterInfo.configuredExoRolesBitMask.Value;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000F42F File Offset: 0x0000D62F
		public static IEnumerable<EXORole> GetConfiguredEXORoles()
		{
			if (DataCenterInfo.configuredExoRolesBitMask == null)
			{
				DataCenterInfo.LoadExoConfiguredRoles();
			}
			return DataCenterInfo.configuredExoRoles;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		private static void LoadFfoConfiguredRoles()
		{
			DataCenterInfo.configuredFfoRoles = new FFORole[0];
			DataCenterInfo.configuredFfoRolesBitMask = new FFORole?(FFORole.None);
			EnvironmentStrategy environmentStrategy = new EnvironmentStrategy();
			if (environmentStrategy.IsForefrontForOffice() || environmentStrategy.IsForefrontDALOverrideUseSQL())
			{
				string[] configRoles = null;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeLabs"))
				{
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("Role");
						if (!string.IsNullOrWhiteSpace(text))
						{
							configRoles = text.Split(new char[]
							{
								','
							});
						}
					}
				}
				if (configRoles != null && configRoles.Length != 0)
				{
					DataCenterInfo.configuredFfoRoles = (from FFORole ffoRole in Enum.GetValues(typeof(FFORole))
					where configRoles.Any((string configRole) => configRole.Equals(ffoRole.ToString(), StringComparison.OrdinalIgnoreCase))
					select ffoRole).ToArray<FFORole>();
					DataCenterInfo.configuredFfoRolesBitMask = DataCenterInfo.configuredFfoRoles.Aggregate(DataCenterInfo.configuredFfoRolesBitMask, (FFORole? current, FFORole next) => current |= next);
				}
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		private static void LoadExoConfiguredRoles()
		{
			DataCenterInfo.configuredExoRoles = new EXORole[0];
			DataCenterInfo.configuredExoRolesBitMask = new EXORole?(EXORole.None);
			EnvironmentStrategy environmentStrategy = new EnvironmentStrategy();
			if (!environmentStrategy.IsForefrontForOffice())
			{
				string[] allInstalledMsis = null;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15"))
				{
					if (registryKey != null)
					{
						allInstalledMsis = registryKey.GetSubKeyNames();
					}
				}
				if (allInstalledMsis != null && allInstalledMsis.Length != 0)
				{
					DataCenterInfo.configuredExoRoles = (from EXORole exoRole in Enum.GetValues(typeof(EXORole))
					where allInstalledMsis.Any((string installedMsi) => installedMsi.Equals(exoRole.ToString(), StringComparison.OrdinalIgnoreCase))
					select exoRole).ToArray<EXORole>();
					DataCenterInfo.configuredExoRolesBitMask = DataCenterInfo.configuredExoRoles.Aggregate(DataCenterInfo.configuredExoRolesBitMask, (EXORole? current, EXORole next) => current |= next);
				}
			}
		}

		// Token: 0x040002E3 RID: 739
		internal const string ExchangeLabsKey = "Software\\Microsoft\\ExchangeLabs";

		// Token: 0x040002E4 RID: 740
		internal const string ExchangeServerVersionRootKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040002E5 RID: 741
		internal const string RoleRegValue = "Role";

		// Token: 0x040002E6 RID: 742
		private static FFORole? configuredFfoRolesBitMask;

		// Token: 0x040002E7 RID: 743
		private static FFORole[] configuredFfoRoles;

		// Token: 0x040002E8 RID: 744
		private static EXORole? configuredExoRolesBitMask;

		// Token: 0x040002E9 RID: 745
		private static EXORole[] configuredExoRoles;
	}
}
