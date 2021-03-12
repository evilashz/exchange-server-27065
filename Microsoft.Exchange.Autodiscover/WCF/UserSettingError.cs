using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B8 RID: 184
	[DataContract(Name = "UserSettingError", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class UserSettingError
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000182CD File Offset: 0x000164CD
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000182D5 File Offset: 0x000164D5
		[DataMember(IsRequired = true)]
		public string SettingName { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000182DE File Offset: 0x000164DE
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000182E6 File Offset: 0x000164E6
		[DataMember(IsRequired = true)]
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000182EF File Offset: 0x000164EF
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000182F7 File Offset: 0x000164F7
		[DataMember(IsRequired = true)]
		public string ErrorMessage { get; set; }
	}
}
