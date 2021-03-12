using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000416 RID: 1046
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetLinkPreviewResponse
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000806B3 File Offset: 0x0007E8B3
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x000806BB File Offset: 0x0007E8BB
		[DataMember]
		public BaseLinkPreview LinkPreview { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000806C4 File Offset: 0x0007E8C4
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x000806CC File Offset: 0x0007E8CC
		[DataMember]
		public string Error { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000806D5 File Offset: 0x0007E8D5
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x000806DD File Offset: 0x0007E8DD
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000806E6 File Offset: 0x0007E8E6
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x000806EE File Offset: 0x0007E8EE
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000806F7 File Offset: 0x0007E8F7
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x000806FF File Offset: 0x0007E8FF
		[DataMember]
		public long ElapsedTimeToWebPageStepCompletion { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x00080708 File Offset: 0x0007E908
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x00080710 File Offset: 0x0007E910
		[DataMember]
		public long ElapsedTimeToRegExStepCompletion { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00080719 File Offset: 0x0007E919
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x00080721 File Offset: 0x0007E921
		[DataMember]
		public long WebPageContentLength { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0008072A File Offset: 0x0007E92A
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x00080732 File Offset: 0x0007E932
		[DataMember]
		public int ImageTagCount { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0008073B File Offset: 0x0007E93B
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x00080743 File Offset: 0x0007E943
		[DataMember]
		public int DescriptionTagCount { get; set; }
	}
}
