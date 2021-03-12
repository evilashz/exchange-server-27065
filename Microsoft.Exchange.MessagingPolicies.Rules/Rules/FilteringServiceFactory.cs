using System;
using Microsoft.Filtering;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000013 RID: 19
	internal class FilteringServiceFactory
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000044C3 File Offset: 0x000026C3
		public static void Create(out IFipsDataStreamFilteringService filteringService)
		{
			filteringService = ((FilteringServiceFactory.InstanceBuilder != null) ? FilteringServiceFactory.InstanceBuilder() : new FipsDataStreamFilteringService());
		}

		// Token: 0x040000C9 RID: 201
		public static FilteringServiceFactory.FilteringServiceBuilder InstanceBuilder;

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000AF RID: 175
		public delegate IFipsDataStreamFilteringService FilteringServiceBuilder();
	}
}
