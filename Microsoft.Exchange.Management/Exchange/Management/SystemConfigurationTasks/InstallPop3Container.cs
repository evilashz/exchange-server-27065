using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B02 RID: 2818
	[Cmdlet("Install", "Pop3Container")]
	public sealed class InstallPop3Container : InstallContainerTaskBase<Pop3Container>
	{
		// Token: 0x0600643E RID: 25662 RVA: 0x001A2AE8 File Offset: 0x001A0CE8
		public InstallPop3Container()
		{
			string protocolName = new Pop3AdConfiguration().ProtocolName;
			base.Name = new string[]
			{
				protocolName
			};
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001A2B18 File Offset: 0x001A0D18
		protected override ADObjectId GetBaseContainer()
		{
			return Pop3Container.GetBaseContainer(base.DataSession as ITopologyConfigurationSession);
		}
	}
}
