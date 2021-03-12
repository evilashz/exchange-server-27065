using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000085 RID: 133
	[Get(typeof(EndpointsResource))]
	[Link("webticket")]
	[Link("oauth")]
	[DataContract(Name = "Endpoints")]
	internal class EndpointsResource : Resource
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0000A000 File Offset: 0x00008200
		public EndpointsResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x04000245 RID: 581
		public const string Token = "endpoints";
	}
}
