using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A4 RID: 2212
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Get", "MailboxServerRedundancy", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(ServerRedundancy)
	})]
	public sealed class GetMailboxServerRedundancy : GetRedundancyTaskBase<ServerIdParameter, Server>
	{
		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06004DFF RID: 19967 RVA: 0x0014412F File Offset: 0x0014232F
		// (set) Token: 0x06004E00 RID: 19968 RVA: 0x00144137 File Offset: 0x00142337
		[Alias(new string[]
		{
			"Server"
		})]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public override ServerIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00144140 File Offset: 0x00142340
		protected override ADObjectId LookupIdentityObjectAndGetDagId()
		{
			Server server = base.LookupServer(this.Identity);
			ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
			if (databaseAvailabilityGroup == null)
			{
				base.WriteError(new ServerMustBeInDagException(server.Fqdn), ErrorCategory.InvalidData, this.Identity);
				return null;
			}
			return databaseAvailabilityGroup;
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00144180 File Offset: 0x00142380
		protected override void WriteResultsFromHealthInfo(HealthInfoPersisted hip, string serverContactedFqdn)
		{
			bool flag = false;
			foreach (ServerHealthInfoPersisted serverHealthInfoPersisted in hip.Servers)
			{
				if (this.Identity == null || serverHealthInfoPersisted.ServerFqdn.IndexOf(this.Identity.RawIdentity, StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					flag = true;
					ServerRedundancy dataObject = new ServerRedundancy(hip, serverHealthInfoPersisted, serverContactedFqdn);
					this.WriteResult(dataObject);
				}
			}
			if (!flag)
			{
				this.WriteWarning(Strings.GetDagHealthInfoNoResultsWarning);
			}
		}
	}
}
