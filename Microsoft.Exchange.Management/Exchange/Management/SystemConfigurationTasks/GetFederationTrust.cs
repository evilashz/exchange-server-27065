using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E5 RID: 2533
	[Cmdlet("Get", "FederationTrust", DefaultParameterSetName = "Identity")]
	public sealed class GetFederationTrust : GetSystemConfigurationObjectTask<FederationTrustIdParameter, FederationTrust>
	{
		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x0017B042 File Offset: 0x00179242
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x0017B045 File Offset: 0x00179245
		protected override IConfigDataProvider CreateSession()
		{
			return base.ReadWriteRootOrgGlobalConfigSession;
		}
	}
}
