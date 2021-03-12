using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000066 RID: 102
	[Parent("onlineMeeting")]
	[Post(typeof(OnlineMeetingExtensionResource), typeof(OnlineMeetingExtensionResource))]
	[DataContract(Name = "OnlineMeetingExtensionsResource")]
	[Get(typeof(OnlineMeetingExtensionsResource))]
	internal class OnlineMeetingExtensionsResource : CollectionContainerResource<OnlineMeetingExtensionResource>
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x000094FD File Offset: 0x000076FD
		public OnlineMeetingExtensionsResource(string selfUri) : base("onlineMeetingExtension", selfUri)
		{
		}

		// Token: 0x040001DA RID: 474
		public const string Token = "onlineMeetingExtensions";
	}
}
