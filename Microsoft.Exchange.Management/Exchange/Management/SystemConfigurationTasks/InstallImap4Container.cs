using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B01 RID: 2817
	[Cmdlet("Install", "Imap4Container")]
	public sealed class InstallImap4Container : InstallContainerTaskBase<Imap4Container>
	{
		// Token: 0x0600643C RID: 25660 RVA: 0x001A2AA4 File Offset: 0x001A0CA4
		public InstallImap4Container()
		{
			string protocolName = new Imap4AdConfiguration().ProtocolName;
			base.Name = new string[]
			{
				protocolName
			};
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x001A2AD4 File Offset: 0x001A0CD4
		protected override ADObjectId GetBaseContainer()
		{
			return Imap4Container.GetBaseContainer(base.DataSession as ITopologyConfigurationSession);
		}
	}
}
