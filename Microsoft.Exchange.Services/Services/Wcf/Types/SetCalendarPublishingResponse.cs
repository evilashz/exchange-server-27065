using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A34 RID: 2612
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarPublishingResponse : CalendarActionResponse
	{
		// Token: 0x060049AE RID: 18862 RVA: 0x00102B68 File Offset: 0x00100D68
		public SetCalendarPublishingResponse()
		{
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x00102B70 File Offset: 0x00100D70
		public SetCalendarPublishingResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x00102B79 File Offset: 0x00100D79
		// (set) Token: 0x060049B1 RID: 18865 RVA: 0x00102B81 File Offset: 0x00100D81
		[DataMember]
		public string BrowseUrl { get; set; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x00102B8A File Offset: 0x00100D8A
		// (set) Token: 0x060049B3 RID: 18867 RVA: 0x00102B92 File Offset: 0x00100D92
		[DataMember]
		public string ICalUrl { get; set; }

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x00102B9B File Offset: 0x00100D9B
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x00102BA3 File Offset: 0x00100DA3
		[DataMember]
		public string CurrentDetailLevel { get; set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00102BAC File Offset: 0x00100DAC
		// (set) Token: 0x060049B7 RID: 18871 RVA: 0x00102BB4 File Offset: 0x00100DB4
		[DataMember]
		public string MaxDetailLevel { get; set; }
	}
}
