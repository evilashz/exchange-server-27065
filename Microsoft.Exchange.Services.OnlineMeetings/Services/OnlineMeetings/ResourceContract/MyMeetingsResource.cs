using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000A2 RID: 162
	[Link("scheduled/summaries")]
	[Parent("Application")]
	[Link("scheduled/conferences")]
	[Link("scheduled/schedulingoptions")]
	internal class MyMeetingsResource : Resource
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		public MyMeetingsResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002B2 RID: 690
		public const string Token = "onlineMeetings";
	}
}
