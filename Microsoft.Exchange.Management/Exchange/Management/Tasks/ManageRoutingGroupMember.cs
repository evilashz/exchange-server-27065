using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000168 RID: 360
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ManageRoutingGroupMember : SetupTaskBase
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0003CEDF File Offset: 0x0003B0DF
		[Parameter(Mandatory = false)]
		public string ServerName
		{
			get
			{
				return (string)base.Fields["ServerName"];
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (string.IsNullOrEmpty(this.ServerName))
			{
				this.server = ((ITopologyConfigurationSession)this.configurationSession).FindLocalServer();
			}
			else
			{
				this.server = ((ITopologyConfigurationSession)this.configurationSession).FindServerByName(this.ServerName);
			}
			if (this.server == null)
			{
				base.ThrowTerminatingError(new CouldNotFindServerDirectoryEntryException(string.IsNullOrEmpty(this.ServerName) ? NativeHelpers.GetLocalComputerFqdn(false) : this.ServerName), ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(this.server);
			if (this.add)
			{
				this.routingGroup = ((ITopologyConfigurationSession)this.configurationSession).GetRoutingGroup();
			}
			else if (this.server.HomeRoutingGroup != null)
			{
				IConfigurable configurable = this.configurationSession.Read<RoutingGroup>(this.server.HomeRoutingGroup);
				this.routingGroup = (RoutingGroup)configurable;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000680 RID: 1664
		protected Server server;

		// Token: 0x04000681 RID: 1665
		protected RoutingGroup routingGroup;

		// Token: 0x04000682 RID: 1666
		protected bool add = true;
	}
}
