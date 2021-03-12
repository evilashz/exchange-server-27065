using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C7 RID: 2503
	[Cmdlet("Set", "ExchangeServerFfoRole", DefaultParameterSetName = "Identity")]
	public sealed class SetExchangeServerFfoRole : SetSystemConfigurationObjectTask<ServerIdParameter, Server>
	{
		// Token: 0x06005938 RID: 22840 RVA: 0x00176E37 File Offset: 0x00175037
		public SetExchangeServerFfoRole()
		{
			this.Remove = new SwitchParameter(false);
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06005939 RID: 22841 RVA: 0x00176E4B File Offset: 0x0017504B
		// (set) Token: 0x0600593A RID: 22842 RVA: 0x00176E62 File Offset: 0x00175062
		[Parameter(Mandatory = true)]
		public ServerRole ServerRole
		{
			get
			{
				return (ServerRole)base.Fields["FfoServerRole"];
			}
			set
			{
				base.Fields["FfoServerRole"] = value;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x00176E7A File Offset: 0x0017507A
		// (set) Token: 0x0600593C RID: 22844 RVA: 0x00176E91 File Offset: 0x00175091
		[Parameter(Mandatory = false)]
		public SwitchParameter Remove
		{
			get
			{
				return (SwitchParameter)base.Fields["FfoServerRoleRemove"];
			}
			set
			{
				base.Fields["FfoServerRoleRemove"] = value;
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00176EAC File Offset: 0x001750AC
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

		// Token: 0x0600593E RID: 22846 RVA: 0x00176F28 File Offset: 0x00175128
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if ((ServerRole.FfoWebService & this.ServerRole) != this.ServerRole)
			{
				base.WriteError(new ArgumentException(Strings.ErrorInvalidFfoServerRole, "ServerRole"), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x0400331F RID: 13087
		private const string FfoServerRoleField = "FfoServerRole";

		// Token: 0x04003320 RID: 13088
		private const string FfoServerRoleRemoveField = "FfoServerRoleRemove";

		// Token: 0x04003321 RID: 13089
		private const ServerRole SupportedFfoRoles = ServerRole.FfoWebService;
	}
}
