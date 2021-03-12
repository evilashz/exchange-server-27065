using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000BFE RID: 3070
	public abstract class RemoveVirtualDirectory<T> : RemoveSystemConfigurationObjectTask<VirtualDirectoryIdParameter, T> where T : ADVirtualDirectory, new()
	{
	}
}
