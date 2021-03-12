using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000094 RID: 148
	[Namespace("Events")]
	[DataContract(Name = "EventsResponse")]
	internal class EventsResponse : ResponseContainer
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000A5ED File Offset: 0x000087ED
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000A5F5 File Offset: 0x000087F5
		[DataMember(Name = "Events", EmitDefaultValue = false)]
		public EventsEntity EventsResource { get; set; }

		// Token: 0x04000299 RID: 665
		public const string ContentType = "multipart/related";
	}
}
