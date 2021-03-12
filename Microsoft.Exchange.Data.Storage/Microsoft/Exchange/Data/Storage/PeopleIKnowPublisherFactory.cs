using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000520 RID: 1312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleIKnowPublisherFactory : IPeopleIKnowPublisherFactory
	{
		// Token: 0x0600381E RID: 14366 RVA: 0x000E5DA8 File Offset: 0x000E3FA8
		public PeopleIKnowPublisherFactory(IXSOFactory xsoFactory, ITracer tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.xsoFactory = xsoFactory;
			this.tracer = tracer;
			this.traceId = traceId;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000E5DDB File Offset: 0x000E3FDB
		public IPeopleIKnowPublisher Create()
		{
			return new PeopleIKnowEmailAddressCollectionFolderProperty(this.xsoFactory, this.tracer, this.traceId);
		}

		// Token: 0x04001DF5 RID: 7669
		private readonly ITracer tracer;

		// Token: 0x04001DF6 RID: 7670
		private readonly int traceId;

		// Token: 0x04001DF7 RID: 7671
		private readonly IXSOFactory xsoFactory;
	}
}
