using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000272 RID: 626
	internal class ADResourceThrottlingModule : ThrottlingModule<ResourceThrottlingCallback>
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x000506B4 File Offset: 0x0004E8B4
		public ADResourceThrottlingModule(TaskContext context) : base(context)
		{
			Array.Resize<ResourceKey>(ref this.resourceKeys, this.resourceKeys.Length + 1);
			this.resourceKeys[this.resourceKeys.Length - 1] = ADResourceKey.Key;
		}
	}
}
