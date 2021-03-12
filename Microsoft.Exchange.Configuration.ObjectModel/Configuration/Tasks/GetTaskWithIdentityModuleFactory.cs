using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000293 RID: 659
	internal class GetTaskWithIdentityModuleFactory : GetTaskBaseModuleFactory
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x00055944 File Offset: 0x00053B44
		public GetTaskWithIdentityModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.PiiRedaction, typeof(GetWithIdentityTaskPiiRedactionModule));
		}
	}
}
