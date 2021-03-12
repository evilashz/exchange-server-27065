using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000090 RID: 144
	[DataContract(Name = "EventsEntity")]
	internal class EventsEntity : HyperReference
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000A589 File Offset: 0x00008789
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000A591 File Offset: 0x00008791
		[DataMember(Name = "event", EmitDefaultValue = false)]
		public Collection<EventSenderEntity> Senders { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000A59A File Offset: 0x0000879A
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000A5A2 File Offset: 0x000087A2
		public Link Link { get; set; }

		// Token: 0x04000293 RID: 659
		public const string Token = "events";

		// Token: 0x04000294 RID: 660
		public const string ContentType = "multipart/related";
	}
}
