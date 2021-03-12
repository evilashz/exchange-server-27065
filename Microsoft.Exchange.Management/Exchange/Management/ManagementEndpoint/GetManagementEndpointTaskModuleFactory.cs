using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x0200047B RID: 1147
	public class GetManagementEndpointTaskModuleFactory : TaskModuleFactory
	{
		// Token: 0x06002858 RID: 10328 RVA: 0x0009EAB5 File Offset: 0x0009CCB5
		public GetManagementEndpointTaskModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(GetManagementEndpointHealthCountersModule));
		}
	}
}
