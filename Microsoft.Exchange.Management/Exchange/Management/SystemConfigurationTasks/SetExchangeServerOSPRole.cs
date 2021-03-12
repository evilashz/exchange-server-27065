using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C8 RID: 2504
	[Cmdlet("Set", "ExchangeServerOSPRole", DefaultParameterSetName = "Identity")]
	public sealed class SetExchangeServerOSPRole : SetSystemConfigurationObjectTask<ServerIdParameter, Server>
	{
		// Token: 0x0600593F RID: 22847 RVA: 0x00176F60 File Offset: 0x00175160
		public SetExchangeServerOSPRole()
		{
			this.Remove = new SwitchParameter(false);
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x00176F74 File Offset: 0x00175174
		// (set) Token: 0x06005941 RID: 22849 RVA: 0x00176F8B File Offset: 0x0017518B
		[Parameter(Mandatory = true)]
		public ServerRole ServerRole
		{
			get
			{
				return (ServerRole)base.Fields["OSPServerRole"];
			}
			set
			{
				base.Fields["OSPServerRole"] = value;
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06005942 RID: 22850 RVA: 0x00176FA3 File Offset: 0x001751A3
		// (set) Token: 0x06005943 RID: 22851 RVA: 0x00176FBA File Offset: 0x001751BA
		[Parameter(Mandatory = false)]
		public SwitchParameter Remove
		{
			get
			{
				return (SwitchParameter)base.Fields["OSPServerRoleRemove"];
			}
			set
			{
				base.Fields["OSPServerRoleRemove"] = value;
			}
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x00176FD4 File Offset: 0x001751D4
		protected override IConfigurable PrepareDataObject()
		{
			Server server = (Server)base.PrepareDataObject();
			if (this.Remove)
			{
				server.CurrentServerRole &= ~this.ServerRole;
			}
			else
			{
				server.CurrentServerRole |= this.ServerRole;
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
					TcpNetworkAddress value = new TcpNetworkAddress(NetworkProtocol.TcpIP, localComputerFqdn);
					server.NetworkAddress = new NetworkAddressCollection(value);
				}
			}
			return server;
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x00177050 File Offset: 0x00175250
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if ((ServerRole.OSP & this.ServerRole) != this.ServerRole)
			{
				base.WriteError(new ArgumentException(Strings.ErrorInvalidOSPServerRole, "ServerRole"), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x04003322 RID: 13090
		private const string OSPServerRoleField = "OSPServerRole";

		// Token: 0x04003323 RID: 13091
		private const string OSPServerRoleRemoveField = "OSPServerRoleRemove";

		// Token: 0x04003324 RID: 13092
		private const ServerRole SupportedOSPRoles = ServerRole.OSP;
	}
}
