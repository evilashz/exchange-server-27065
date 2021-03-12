using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000412 RID: 1042
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UcwaUserConfiguration
	{
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0007F249 File Offset: 0x0007D449
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0007F251 File Offset: 0x0007D451
		[DataMember]
		public string SipUri { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0007F25A File Offset: 0x0007D45A
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0007F262 File Offset: 0x0007D462
		[DataMember]
		public bool IsUcwaSupported { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0007F26B File Offset: 0x0007D46B
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x0007F273 File Offset: 0x0007D473
		[DataMember(IsRequired = false)]
		public string DiagnosticInfo { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x0007F27C File Offset: 0x0007D47C
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0007F284 File Offset: 0x0007D484
		public string UcwaUrl { get; set; }
	}
}
