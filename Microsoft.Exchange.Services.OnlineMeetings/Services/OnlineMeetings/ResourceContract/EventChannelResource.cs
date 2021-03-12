using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008A RID: 138
	[Parent("application")]
	[Get(typeof(EventChannelResource))]
	internal class EventChannelResource : Resource
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000A49D File Offset: 0x0000869D
		public EventChannelResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x0400027E RID: 638
		public const string Token = "event";

		// Token: 0x0400027F RID: 639
		public const bool IsXmlSupported = false;

		// Token: 0x04000280 RID: 640
		public const bool IsJsonSupported = false;

		// Token: 0x04000281 RID: 641
		public const string ContentType = "multipart/related+json";
	}
}
