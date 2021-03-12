using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000083 RID: 131
	[Post(typeof(BatchingResource))]
	[Parent("Application")]
	internal class BatchingResource : Resource
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00009FCD File Offset: 0x000081CD
		public BatchingResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x0400023F RID: 575
		public const string Token = "Batch";

		// Token: 0x04000240 RID: 576
		public const bool IsXmlSupported = false;

		// Token: 0x04000241 RID: 577
		public const bool IsJsonSupported = false;

		// Token: 0x04000242 RID: 578
		public const string ContentType = "multipart/batching";
	}
}
