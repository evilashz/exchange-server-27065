using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000082 RID: 130
	[Post(typeof(ApplicationSettings), typeof(ApplicationResource))]
	[CollectionDataContract(Name = "Applications")]
	[Parent("user")]
	internal class ApplicationsResource : Resource
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00009FC4 File Offset: 0x000081C4
		public ApplicationsResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x0400023E RID: 574
		public const string Token = "applications";
	}
}
