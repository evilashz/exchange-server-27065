using System;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000260 RID: 608
	internal static class NamedQueryProcessorExtension
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x0004E9ED File Offset: 0x0004CBED
		public static void Register(this INamedQueryProcessor processor)
		{
			RbacQuery.RegisterQueryProcessor(processor.Name, (RbacQuery.RbacQueryProcessor)processor);
		}
	}
}
