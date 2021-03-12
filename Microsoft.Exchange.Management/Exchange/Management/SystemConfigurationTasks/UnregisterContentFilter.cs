using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A33 RID: 2611
	[Cmdlet("Unregister", "ContentFilter")]
	public sealed class UnregisterContentFilter : ContentFilterRegistration
	{
		// Token: 0x06005D4E RID: 23886 RVA: 0x00189399 File Offset: 0x00187599
		public UnregisterContentFilter() : base(false)
		{
		}
	}
}
