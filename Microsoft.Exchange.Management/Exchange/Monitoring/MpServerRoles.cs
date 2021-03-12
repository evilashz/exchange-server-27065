using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000512 RID: 1298
	internal static class MpServerRoles
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x000BA698 File Offset: 0x000B8898
		internal static ServerRole GetLocalE12ServerRolesFromRegistry()
		{
			ServerRole serverRole = ServerRole.None;
			using (RegistryKey localMachine = Registry.LocalMachine)
			{
				using (RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15", RegistryKeyPermissionCheck.Default))
				{
					if (registryKey != null)
					{
						foreach (MpServerRoles.RoleOnRegistry roleOnRegistry in MpServerRoles.E12MpRolesOnRegistry)
						{
							using (RegistryKey registryKey2 = registryKey.OpenSubKey(roleOnRegistry.SubKey))
							{
								if (registryKey2 != null)
								{
									serverRole |= roleOnRegistry.RoleFlag;
								}
							}
						}
					}
				}
			}
			return serverRole;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000BA754 File Offset: 0x000B8954
		internal static string DisplayRoleName(ServerRole role)
		{
			if (role <= ServerRole.UnifiedMessaging)
			{
				switch (role)
				{
				case ServerRole.Mailbox:
					return Strings.MailboxRoleDisplayName;
				case ServerRole.Cafe | ServerRole.Mailbox:
					break;
				case ServerRole.ClientAccess:
					return Strings.ClientAccessRoleDisplayName;
				default:
					if (role == ServerRole.UnifiedMessaging)
					{
						return Strings.UnifiedMessagingRoleDisplayName;
					}
					break;
				}
			}
			else
			{
				if (role == ServerRole.HubTransport)
				{
					return Strings.BridgeheadRoleDisplayName;
				}
				if (role == ServerRole.Edge)
				{
					return Strings.GatewayRoleDisplayName;
				}
			}
			return string.Empty;
		}

		// Token: 0x0400214A RID: 8522
		internal static readonly ServerRole[] ValidE12MpRoles = new ServerRole[]
		{
			ServerRole.Mailbox,
			ServerRole.ClientAccess,
			ServerRole.UnifiedMessaging,
			ServerRole.HubTransport,
			ServerRole.Edge
		};

		// Token: 0x0400214B RID: 8523
		private static readonly MpServerRoles.RoleOnRegistry[] E12MpRolesOnRegistry = new MpServerRoles.RoleOnRegistry[]
		{
			new MpServerRoles.RoleOnRegistry(ServerRole.HubTransport, "HubTransportRole"),
			new MpServerRoles.RoleOnRegistry(ServerRole.ClientAccess, "ClientAccessRole"),
			new MpServerRoles.RoleOnRegistry(ServerRole.Edge, "EdgeTransportRole"),
			new MpServerRoles.RoleOnRegistry(ServerRole.Mailbox, "MailboxRole"),
			new MpServerRoles.RoleOnRegistry(ServerRole.UnifiedMessaging, "UnifiedMessagingRole")
		};

		// Token: 0x02000513 RID: 1299
		internal struct RoleOnRegistry
		{
			// Token: 0x17000DDB RID: 3547
			// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000BA890 File Offset: 0x000B8A90
			public ServerRole RoleFlag
			{
				get
				{
					return this.roleFlag;
				}
			}

			// Token: 0x17000DDC RID: 3548
			// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000BA898 File Offset: 0x000B8A98
			public string SubKey
			{
				get
				{
					return this.subKey;
				}
			}

			// Token: 0x06002E96 RID: 11926 RVA: 0x000BA8A0 File Offset: 0x000B8AA0
			public RoleOnRegistry(ServerRole role, string subKey)
			{
				this = default(MpServerRoles.RoleOnRegistry);
				this.roleFlag = role;
				this.subKey = subKey;
			}

			// Token: 0x0400214C RID: 8524
			public const string RootSubKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

			// Token: 0x0400214D RID: 8525
			private ServerRole roleFlag;

			// Token: 0x0400214E RID: 8526
			private string subKey;
		}
	}
}
