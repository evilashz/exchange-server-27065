using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001AD RID: 429
	internal class ComponentInfoBaseThrottlingModule : ThrottlingModule<ResourceThrottlingCallback>
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x0004317E File Offset: 0x0004137E
		public ComponentInfoBaseThrottlingModule(TaskContext context) : base(context, true)
		{
		}
	}
}
