using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000074 RID: 116
	[Cmdlet("Get", "MobileDevice", DefaultParameterSetName = "Identity")]
	public class GetMobileDevice : GetMobileDeviceBase<MobileDeviceIdParameter, MobileDevice>
	{
	}
}
