using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000292 RID: 658
	internal class GetTaskBaseModuleFactory : TaskModuleFactory
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x0005592B File Offset: 0x00053B2B
		public GetTaskBaseModuleFactory()
		{
			base.RegisterModule(TaskModuleKey.PiiRedaction, typeof(GetTaskPiiRedactionModule));
		}
	}
}
