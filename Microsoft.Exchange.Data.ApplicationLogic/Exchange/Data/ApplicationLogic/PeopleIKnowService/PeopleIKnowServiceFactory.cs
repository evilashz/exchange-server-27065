using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x0200018A RID: 394
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeopleIKnowServiceFactory : IPeopleIKnowServiceFactory
	{
		// Token: 0x06000F36 RID: 3894 RVA: 0x0003D589 File Offset: 0x0003B789
		internal PeopleIKnowServiceFactory(IPeopleIKnowSerializerFactory serializerFactory)
		{
			this.serializerFactory = serializerFactory;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003D598 File Offset: 0x0003B798
		public IPeopleIKnowService CreatePeopleIKnowService(ITracer tracer)
		{
			return new PeopleIKnowService(this.serializerFactory, tracer);
		}

		// Token: 0x04000805 RID: 2053
		private readonly IPeopleIKnowSerializerFactory serializerFactory;
	}
}
