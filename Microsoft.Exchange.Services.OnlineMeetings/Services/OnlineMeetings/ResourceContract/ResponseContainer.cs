using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000093 RID: 147
	[KnownType(typeof(EventsResponse))]
	[DataContract]
	[KnownType(typeof(EmbeddedMultipartRelatedResponse))]
	internal abstract class ResponseContainer
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000A5C3 File Offset: 0x000087C3
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000A5CB File Offset: 0x000087CB
		[IgnoreDataMember]
		public Resource ContainedResource { get; protected set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000A5D4 File Offset: 0x000087D4
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000A5DC File Offset: 0x000087DC
		[DataMember(Name = "Error", EmitDefaultValue = false)]
		public ErrorInformation Error { get; set; }
	}
}
