using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000019 RID: 25
	public static class ServerRole
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000054BC File Offset: 0x000036BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		static ServerRole()
		{
			object obj;
			if (CommonUtils.TryGetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics", "RoleName", string.Empty, out obj))
			{
				string text = obj.ToString().Trim();
				if (!string.IsNullOrEmpty(text))
				{
					ServerRole.installedRoles.Add(text);
				}
			}
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly == null)
			{
				ServerRole.version = Environment.OSVersion.Version;
			}
			else
			{
				object[] customAttributes = callingAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
				if (customAttributes.Length != 1 || !ServerRole.TryParseVersion(((AssemblyFileVersionAttribute)customAttributes[0]).Version, out ServerRole.version))
				{
					ServerRole.version = Environment.OSVersion.Version;
				}
			}
			foreach (ServerRole.Role role in ServerRole.roles)
			{
				ServerRole.CheckRole(role.Name, role.RegistryKey, role.RegistryValue);
			}
			if (ServerRole.installedRoles.Count == 0)
			{
				ServerRole.installedRoles.Add("Other");
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000056C1 File Offset: 0x000038C1
		public static bool IsRole(string roleName)
		{
			return ServerRole.installedRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000056D3 File Offset: 0x000038D3
		public static List<string> GetRoleAndVersion(out Version roleVersion)
		{
			roleVersion = ServerRole.version;
			return ServerRole.installedRoles;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000056E4 File Offset: 0x000038E4
		private static void CheckRole(string role, string registryKey, string valueName)
		{
			object obj;
			if (CommonUtils.TryGetRegistryValue(registryKey, valueName, null, out obj))
			{
				ServerRole.installedRoles.Add(role);
				if (valueName != "ConfiguredVersion")
				{
					return;
				}
				if (!ServerRole.TryParseVersion(obj.ToString(), out ServerRole.version))
				{
					throw new FormatException("Unable to parse the version of this machine.");
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005734 File Offset: 0x00003934
		private static bool TryParseVersion(string input, out Version result)
		{
			bool result2 = false;
			int major = 0;
			int minor = 0;
			int build = 0;
			int revision = 0;
			if (!string.IsNullOrEmpty(input))
			{
				string[] array = input.Split(new char[]
				{
					'.'
				});
				if (array.Length >= 2)
				{
					for (int i = 0; i < array.Length; i++)
					{
						switch (i)
						{
						case 0:
							int.TryParse(array[0], out major);
							break;
						case 1:
							int.TryParse(array[1], out minor);
							break;
						case 2:
							int.TryParse(array[2], out build);
							break;
						case 3:
							int.TryParse(array[3], out revision);
							break;
						}
					}
					result2 = true;
				}
			}
			result = new Version(major, minor, build, revision);
			return result2;
		}

		// Token: 0x040002E4 RID: 740
		private const string ExchangeRegistryValueName = "ConfiguredVersion";

		// Token: 0x040002E5 RID: 741
		private const string ExchangeServerRegistryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040002E6 RID: 742
		private static readonly ServerRole.Role[] roles = new ServerRole.Role[]
		{
			new ServerRole.Role("DomainController", "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\services\\NTDS\\Parameters", "DSA Database file"),
			new ServerRole.Role("Cafe", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CafeRole", "ConfiguredVersion"),
			new ServerRole.Role("ClientAccess", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ClientAccessRole", "ConfiguredVersion"),
			new ServerRole.Role("Mailbox", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole", "ConfiguredVersion"),
			new ServerRole.Role("UnifiedMessaging", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", "ConfiguredVersion"),
			new ServerRole.Role("HubTransport", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\HubTransportRole", "ConfiguredVersion"),
			new ServerRole.Role("CentralAdmin", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CentralAdminRole", "ConfiguredVersion"),
			new ServerRole.Role("FfoWebService", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\FfoWebServiceRole", "ConfiguredVersion"),
			new ServerRole.Role("Arr", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\IIS Extensions\\Application Request Routing", "Install"),
			new ServerRole.Role("AuxECS", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15AuxECS", "ConfiguredVersion")
		};

		// Token: 0x040002E7 RID: 743
		private static List<string> installedRoles = new List<string>();

		// Token: 0x040002E8 RID: 744
		private static Version version;

		// Token: 0x0200001A RID: 26
		private class Role
		{
			// Token: 0x0600005A RID: 90 RVA: 0x000057EB File Offset: 0x000039EB
			public Role(string name, string registrykey, string registryValue)
			{
				this.name = name;
				this.registryKey = registrykey;
				this.registryValue = registryValue;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600005B RID: 91 RVA: 0x00005808 File Offset: 0x00003A08
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600005C RID: 92 RVA: 0x00005810 File Offset: 0x00003A10
			public string RegistryKey
			{
				get
				{
					return this.registryKey;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00005818 File Offset: 0x00003A18
			public string RegistryValue
			{
				get
				{
					return this.registryValue;
				}
			}

			// Token: 0x040002E9 RID: 745
			private readonly string name;

			// Token: 0x040002EA RID: 746
			private readonly string registryKey;

			// Token: 0x040002EB RID: 747
			private readonly string registryValue;
		}
	}
}
