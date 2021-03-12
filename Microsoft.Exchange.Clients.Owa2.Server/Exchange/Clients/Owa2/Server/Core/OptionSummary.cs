using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E6 RID: 486
	[DataContract]
	public class OptionSummary
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00042030 File Offset: 0x00040230
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x00042038 File Offset: 0x00040238
		[DataMember]
		public bool Oof { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00042041 File Offset: 0x00040241
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00042049 File Offset: 0x00040249
		[DataMember]
		public string TimeZone { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00042052 File Offset: 0x00040252
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x0004205A File Offset: 0x0004025A
		[DataMember]
		public EmailSignatureConfiguration Signature { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00042063 File Offset: 0x00040263
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x0004206B File Offset: 0x0004026B
		[DataMember]
		public bool AlwaysShowBcc { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00042074 File Offset: 0x00040274
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0004207C File Offset: 0x0004027C
		[DataMember]
		public bool AlwaysShowFrom { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00042085 File Offset: 0x00040285
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0004208D File Offset: 0x0004028D
		[DataMember]
		public bool ShowSenderOnTopInListView { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00042096 File Offset: 0x00040296
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0004209E File Offset: 0x0004029E
		[DataMember]
		public bool ShowPreviewTextInListView { get; set; }
	}
}
