using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A82 RID: 2690
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxRegionalConfigurationData : OptionsPropertyChangeTracker
	{
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x001061CD File Offset: 0x001043CD
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x001061D5 File Offset: 0x001043D5
		[DataMember]
		public string DateFormat { get; set; }

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x001061DE File Offset: 0x001043DE
		// (set) Token: 0x06004C30 RID: 19504 RVA: 0x001061E6 File Offset: 0x001043E6
		[DataMember]
		public bool DefaultFolderNameMatchingUserLanguage { get; set; }

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x001061EF File Offset: 0x001043EF
		// (set) Token: 0x06004C32 RID: 19506 RVA: 0x001061F7 File Offset: 0x001043F7
		[DataMember]
		public string Language { get; set; }

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x00106200 File Offset: 0x00104400
		// (set) Token: 0x06004C34 RID: 19508 RVA: 0x00106208 File Offset: 0x00104408
		[DataMember]
		public string TimeFormat { get; set; }

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x00106211 File Offset: 0x00104411
		// (set) Token: 0x06004C36 RID: 19510 RVA: 0x00106219 File Offset: 0x00104419
		[DataMember]
		public string TimeZone { get; set; }
	}
}
