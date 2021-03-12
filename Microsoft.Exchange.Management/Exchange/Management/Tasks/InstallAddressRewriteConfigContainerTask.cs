using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CB RID: 715
	[Cmdlet("install", "AddressRewriteConfigContainer")]
	public sealed class InstallAddressRewriteConfigContainerTask : InstallContainerTaskBase<AddressRewriteConfigContainer>
	{
		// Token: 0x06001931 RID: 6449 RVA: 0x00070D98 File Offset: 0x0006EF98
		protected override ADObjectId GetBaseContainer()
		{
			return new ADObjectId("OU=MSExchangeGateway");
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00070DA4 File Offset: 0x0006EFA4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}
	}
}
