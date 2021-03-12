using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A32 RID: 2610
	[Cmdlet("Register", "ContentFilter")]
	public sealed class RegisterContentFilter : ContentFilterRegistration
	{
		// Token: 0x06005D4D RID: 23885 RVA: 0x00189390 File Offset: 0x00187590
		public RegisterContentFilter() : base(true)
		{
		}
	}
}
