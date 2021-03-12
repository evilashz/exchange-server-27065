using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C8 RID: 968
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetDlpPolicyTipsRequest
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00076F33 File Offset: 0x00075133
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x00076F3B File Offset: 0x0007513B
		[DataMember]
		public BaseItemId ItemId { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00076F44 File Offset: 0x00075144
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x00076F4C File Offset: 0x0007514C
		[DataMember]
		public bool NeedToReclassify { get; set; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00076F55 File Offset: 0x00075155
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x00076F5D File Offset: 0x0007515D
		[DataMember]
		public bool BodyOrSubjectChanged { get; set; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00076F66 File Offset: 0x00075166
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x00076F6E File Offset: 0x0007516E
		[DataMember]
		public bool CustomizedStringsNeeded { get; set; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x00076F77 File Offset: 0x00075177
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x00076F7F File Offset: 0x0007517F
		[DataMember]
		public EventTrigger EventTrigger { get; set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00076F88 File Offset: 0x00075188
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x00076F90 File Offset: 0x00075190
		[DataMember]
		public EmailAddressWrapper[] Recipients { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00076F99 File Offset: 0x00075199
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x00076FA1 File Offset: 0x000751A1
		[DataMember]
		public bool ClientSupportsScanResultData { get; set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00076FAA File Offset: 0x000751AA
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x00076FB2 File Offset: 0x000751B2
		[DataMember]
		public string ScanResultData { get; set; }

		// Token: 0x0400118C RID: 4492
		public static GetDlpPolicyTipsRequest Ping = new GetDlpPolicyTipsRequest
		{
			ItemId = new ItemId(Guid.Empty.ToString(), Guid.Empty.ToString()),
			NeedToReclassify = false,
			BodyOrSubjectChanged = false
		};
	}
}
