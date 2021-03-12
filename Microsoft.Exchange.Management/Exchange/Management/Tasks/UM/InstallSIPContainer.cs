using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2A RID: 3370
	[Cmdlet("Install", "SIPContainer")]
	public sealed class InstallSIPContainer : InstallContainerTaskBase<SIPContainer>
	{
		// Token: 0x0600813F RID: 33087 RVA: 0x00210CD0 File Offset: 0x0020EED0
		public InstallSIPContainer()
		{
			base.Name = new string[]
			{
				"SIP"
			};
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x00210CF9 File Offset: 0x0020EEF9
		protected override ADObjectId GetBaseContainer()
		{
			return SIPContainer.GetBaseContainer(base.DataSession as ITopologyConfigurationSession);
		}
	}
}
