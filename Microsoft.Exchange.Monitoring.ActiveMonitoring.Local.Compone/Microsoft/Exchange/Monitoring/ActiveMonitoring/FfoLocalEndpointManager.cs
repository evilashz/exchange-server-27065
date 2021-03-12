using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200053E RID: 1342
	internal class FfoLocalEndpointManager
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x000C8E56 File Offset: 0x000C7056
		public static bool IsForefrontForOfficeDatacenter
		{
			get
			{
				return Datacenter.IsForefrontForOfficeDatacenter();
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000C8E5D File Offset: 0x000C705D
		public static bool IsForefrontForOfficeGallatinDatacenter
		{
			get
			{
				return Datacenter.IsFFOGallatinDatacenter();
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x000C8E64 File Offset: 0x000C7064
		public static bool IsBackgroundRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("Background");
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000C8E70 File Offset: 0x000C7070
		public static bool IsCentralAdminRoleInstalled
		{
			get
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CentralAdminRole");
				if (registryKey != null)
				{
					registryKey.Close();
					registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellSnapIns\\Microsoft.Exchange.Management.Powershell.FfoCentralAdmin");
					if (registryKey != null)
					{
						registryKey.Close();
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000C8EB4 File Offset: 0x000C70B4
		public static bool IsSyslogListenerRoleInstalled
		{
			get
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\MS Forefront Networking SysLogListener Service");
				if (registryKey != null)
				{
					registryKey.Close();
					return true;
				}
				return false;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000C8EDD File Offset: 0x000C70DD
		public static bool IsDatabaseRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("Database");
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x000C8EE9 File Offset: 0x000C70E9
		public static bool IsInfraDatabaseRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("InfraDatabase");
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000C8EF5 File Offset: 0x000C70F5
		public static bool IsDomainNameServerRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("DomainNameServer");
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000C8F01 File Offset: 0x000C7101
		public static bool IsHubTransportRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("HubTransport");
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000C8F0D File Offset: 0x000C710D
		public static bool IsFrontendTransportRoleInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("FrontendTransport");
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000C8F19 File Offset: 0x000C7119
		public static bool IsWebServiceInstalled
		{
			get
			{
				return FfoLocalEndpointManager.GetRole("WebService");
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000C8F28 File Offset: 0x000C7128
		public static bool IsWebstoreInstalled
		{
			get
			{
				int num = 0;
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Webstore", "regMasterControllerEvents", 0);
				if (value != null && value is int)
				{
					num = (int)value;
				}
				return num == 1;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000C8F64 File Offset: 0x000C7164
		internal static bool GetRole(string role)
		{
			if (string.IsNullOrWhiteSpace(role))
			{
				return false;
			}
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\ExchangeLabs", "Role", null);
			return !string.IsNullOrWhiteSpace(text) && new List<string>(text.ToLower().Split(new char[]
			{
				','
			})).Contains(role.ToLower());
		}
	}
}
