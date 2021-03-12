using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000617 RID: 1559
	[Cmdlet("Get", "AuthServer")]
	public sealed class GetAuthServer : GetSystemConfigurationObjectTask<AuthServerIdParameter, AuthServer>
	{
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000E4D0D File Offset: 0x000E2F0D
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000E4D10 File Offset: 0x000E2F10
		protected override IConfigDataProvider CreateSession()
		{
			return base.RootOrgGlobalConfigSession;
		}
	}
}
