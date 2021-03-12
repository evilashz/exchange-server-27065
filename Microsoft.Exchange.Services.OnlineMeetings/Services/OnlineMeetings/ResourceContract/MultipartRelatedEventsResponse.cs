using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000A0 RID: 160
	[DataContract(Name = "MultipartRelatedEventsResponse")]
	[Namespace("MultipartRelatedEventsResponse")]
	internal class MultipartRelatedEventsResponse : ResponseContainer
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000A87D File Offset: 0x00008A7D
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000A885 File Offset: 0x00008A85
		[DataMember(Name = "EventsResponse", EmitDefaultValue = false)]
		public EventsResponse EventsResponse { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A88E File Offset: 0x00008A8E
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000A896 File Offset: 0x00008A96
		[DataMember(Name = "Part", EmitDefaultValue = false)]
		public Collection<object> Parts { get; set; }
	}
}
