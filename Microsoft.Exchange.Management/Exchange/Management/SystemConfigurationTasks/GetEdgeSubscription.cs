using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009AE RID: 2478
	[Cmdlet("Get", "EdgeSubscription", DefaultParameterSetName = "Identity")]
	public sealed class GetEdgeSubscription : GetSystemConfigurationObjectTask<TransportServerIdParameter, Server>
	{
		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x0600587A RID: 22650 RVA: 0x00170EC0 File Offset: 0x0016F0C0
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskOrFilter(ServerSchema.CurrentServerRole, 64UL);
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x00170ECF File Offset: 0x0016F0CF
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x00170ED4 File Offset: 0x0016F0D4
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			Server server = (Server)dataObject;
			if (server.GatewayEdgeSyncSubscribed)
			{
				base.WriteResult(new EdgeSubscription(server));
			}
			else
			{
				base.WriteVerbose(Strings.EdgeServerNotSubscribed);
			}
			TaskLogger.LogExit();
		}
	}
}
