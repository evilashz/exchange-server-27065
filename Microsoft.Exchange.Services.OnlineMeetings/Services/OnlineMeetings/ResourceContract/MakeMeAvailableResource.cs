using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000099 RID: 153
	[Post(typeof(MakeMeAvailableSettings))]
	[Parent("communication")]
	internal class MakeMeAvailableResource : Resource
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000A60F File Offset: 0x0000880F
		public MakeMeAvailableResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002A6 RID: 678
		public const string Token = "MakeMeAvailable";
	}
}
