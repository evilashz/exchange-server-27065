using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009B RID: 155
	[Parent("communication")]
	[Post]
	internal class MakeMeUnavailableResource : Resource
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000A6B7 File Offset: 0x000088B7
		public MakeMeUnavailableResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002A7 RID: 679
		public const string Token = "MakeMeUnavailable";
	}
}
