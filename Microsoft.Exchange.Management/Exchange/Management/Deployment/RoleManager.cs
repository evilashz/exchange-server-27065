using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000230 RID: 560
	internal class RoleManager
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00052B94 File Offset: 0x00050D94
		static RoleManager()
		{
			RoleManager.roleList.Add(new BridgeheadRole());
			RoleManager.roleList.Add(new GatewayRole());
			RoleManager.roleList.Add(new ClientAccessRole());
			RoleManager.roleList.Add(new MailboxRole());
			RoleManager.roleList.Add(new UnifiedMessagingRole());
			RoleManager.roleList.Add(new FrontendTransportRole());
			RoleManager.roleList.Add(new AdminToolsRole());
			RoleManager.roleList.Add(new MonitoringRole());
			RoleManager.roleList.Add(new CentralAdminRole());
			RoleManager.roleList.Add(new CentralAdminDatabaseRole());
			RoleManager.roleList.Add(new CentralAdminFrontEndRole());
			RoleManager.roleList.Add(new LanguagePacksRole());
			RoleManager.roleList.Add(new CafeRole());
			RoleManager.roleList.Add(new FfoWebServiceRole());
			RoleManager.roleList.Add(new OSPRole());
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00052C8C File Offset: 0x00050E8C
		public static RoleCollection Roles
		{
			get
			{
				return RoleManager.roleList;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00052CB4 File Offset: 0x00050EB4
		internal static SetupComponentInfoCollection GetRequiredComponents(string roleName, InstallationModes mode)
		{
			TaskLogger.LogEnter(new object[]
			{
				roleName
			});
			Role roleByName = RoleManager.GetRoleByName(roleName);
			RoleCollection currentRoles = RoleManager.GetCurrentRoles();
			currentRoles.Remove(roleByName);
			SetupComponentInfoCollection setupComponentInfoCollection = new SetupComponentInfoCollection();
			foreach (Role role in currentRoles)
			{
				setupComponentInfoCollection.AddRange(role.AllComponents);
			}
			SetupComponentInfoCollection allComponents = roleByName.AllComponents;
			SetupComponentInfoCollection setupComponentInfoCollection2 = new SetupComponentInfoCollection();
			if (mode == InstallationModes.BuildToBuildUpgrade)
			{
				setupComponentInfoCollection2.AddRange(allComponents);
			}
			else
			{
				using (List<SetupComponentInfo>.Enumerator enumerator2 = allComponents.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SetupComponentInfo candidate = enumerator2.Current;
						bool flag = false;
						if (candidate.AlwaysExecute)
						{
							TaskLogger.Log(Strings.RunningAlwaysToRunComponent(candidate.Name));
							flag = true;
						}
						else if (setupComponentInfoCollection.Find((SetupComponentInfo sci) => sci.Name == candidate.Name) == null)
						{
							TaskLogger.Log(Strings.AddingUniqueComponent(candidate.Name));
							flag = true;
						}
						else
						{
							TaskLogger.Log(Strings.AlreadyConfiguredComponent(candidate.Name));
						}
						if (flag)
						{
							setupComponentInfoCollection2.Add(candidate);
						}
					}
				}
			}
			TaskLogger.LogExit();
			return setupComponentInfoCollection2;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00052E40 File Offset: 0x00051040
		public static Role GetRoleByName(string roleName)
		{
			if (!roleName.ToLower().Contains("role"))
			{
				roleName += "role";
			}
			foreach (Role role in RoleManager.Roles)
			{
				if (string.Compare(role.RoleName, roleName, true, CultureInfo.InvariantCulture) == 0)
				{
					return role;
				}
			}
			return null;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00052EC8 File Offset: 0x000510C8
		public static RoleCollection GetInstalledRoles()
		{
			RoleCollection roleCollection = new RoleCollection();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Role role in RoleManager.Roles)
			{
				if (role.IsInstalled)
				{
					roleCollection.Add(role);
					stringBuilder.Append(role.RoleName);
					stringBuilder.Append(" ");
				}
			}
			TaskLogger.Log(Strings.InstalledRoles(stringBuilder.ToString()));
			return roleCollection;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00052F58 File Offset: 0x00051158
		public static RoleCollection GetUnpackedRoles()
		{
			RoleCollection roleCollection = new RoleCollection();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Role role in RoleManager.Roles)
			{
				if (role.IsUnpacked)
				{
					roleCollection.Add(role);
					stringBuilder.Append(role.RoleName);
					stringBuilder.Append(" ");
				}
			}
			TaskLogger.Log(Strings.UnpackedRoles(stringBuilder.ToString()));
			return roleCollection;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00052FE8 File Offset: 0x000511E8
		public static RoleCollection GetUnpackedDatacenterRoles()
		{
			RoleCollection roleCollection = new RoleCollection();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Role role in RoleManager.Roles)
			{
				if (role.IsDatacenterUnpacked)
				{
					roleCollection.Add(role);
					stringBuilder.Append(role.RoleName);
					stringBuilder.Append(" ");
				}
			}
			TaskLogger.Log(Strings.UnpackedDatacenterRoles(stringBuilder.ToString()));
			return roleCollection;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00053078 File Offset: 0x00051278
		public static RoleCollection GetCurrentRoles()
		{
			RoleCollection roleCollection = new RoleCollection();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Role role in RoleManager.Roles)
			{
				if (role.IsCurrent)
				{
					roleCollection.Add(role);
					stringBuilder.Append(role.RoleName);
					stringBuilder.Append(" ");
				}
			}
			TaskLogger.Log(Strings.CurrentRoles(stringBuilder.ToString()));
			return roleCollection;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00053108 File Offset: 0x00051308
		internal static void Reset()
		{
			foreach (Role role in RoleManager.Roles)
			{
				role.Reset();
			}
		}

		// Token: 0x040007F9 RID: 2041
		protected static RoleCollection roleList = new RoleCollection();
	}
}
