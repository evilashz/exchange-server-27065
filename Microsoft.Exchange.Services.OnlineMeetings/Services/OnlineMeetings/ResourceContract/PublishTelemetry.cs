using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B3 RID: 179
	[Parent("application")]
	[Post(typeof(Telemetry))]
	internal class PublishTelemetry : Resource
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000AA69 File Offset: 0x00008C69
		public PublishTelemetry(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x040002CE RID: 718
		public const string Token = "publishTelemetry";
	}
}
