using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CB RID: 2251
	public enum UseDagDefaultOnOff
	{
		// Token: 0x04002F34 RID: 12084
		[LocDescription(Strings.IDs.UseDagDefaultOnOffNone)]
		UseDagDefault,
		// Token: 0x04002F35 RID: 12085
		[LocDescription(Strings.IDs.UseDagDefaultOnOffOff)]
		Off,
		// Token: 0x04002F36 RID: 12086
		[LocDescription(Strings.IDs.UseDagDefaultOnOffOn)]
		On
	}
}
