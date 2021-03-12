using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C08 RID: 3080
	public abstract class GetVirtualDirectory<T> : GetSystemConfigurationObjectTask<VirtualDirectoryIdParameter, T> where T : ADVirtualDirectory, new()
	{
		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06007471 RID: 29809 RVA: 0x001DB66B File Offset: 0x001D986B
		// (set) Token: 0x06007472 RID: 29810 RVA: 0x001DB682 File Offset: 0x001D9882
		[Parameter(Mandatory = true, ParameterSetName = "Server", ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06007473 RID: 29811 RVA: 0x001DB695 File Offset: 0x001D9895
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x001DB6A0 File Offset: 0x001D98A0
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				Server server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				if (!server.IsClientAccessServer && !server.IsMailboxServer && !server.IsHubTransportServer && !server.IsUnifiedMessagingServer && !server.IsFrontendTransportServer && !server.IsFfoWebServiceRole && !server.IsCafeServer && !server.IsOSPRole)
				{
					base.WriteError(server.GetServerRoleError(ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.FrontendTransport | ServerRole.FfoWebService | ServerRole.OSP), ErrorCategory.InvalidOperation, this.Server);
					return;
				}
				this.rootId = (ADObjectId)server.Identity;
			}
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x001DB770 File Offset: 0x001D9970
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			try
			{
				if (ServerIdParameter.HasRole((ADObjectId)dataObject.Identity, ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.FrontendTransport | ServerRole.FfoWebService | ServerRole.OSP, base.DataSession))
				{
					ADVirtualDirectory advirtualDirectory = dataObject as ADVirtualDirectory;
					if (advirtualDirectory != null)
					{
						advirtualDirectory.AdminDisplayVersion = Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.GetServerVersion(advirtualDirectory.Server.Name);
					}
					base.WriteResult(dataObject);
				}
			}
			catch (InvalidOperationException)
			{
				base.WriteError(new InvalidADObjectOperationException(Strings.ErrorFoundInvalidADObject(((ADObjectId)dataObject.Identity).ToDNString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x001DB81C File Offset: 0x001D9A1C
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003B55 RID: 15189
		private ADObjectId rootId;
	}
}
