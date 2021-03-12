using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B6 RID: 182
	[DataContract(Name = "UserResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class UserResponse : AutodiscoverResponse
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0001826C File Offset: 0x0001646C
		public UserResponse()
		{
			this.UserSettings = new UserSettingCollection();
			this.UserSettingErrors = new UserSettingErrorCollection();
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001828A File Offset: 0x0001648A
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00018292 File Offset: 0x00016492
		[DataMember(Name = "RedirectTarget", IsRequired = false)]
		public string RedirectTarget { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001829B File Offset: 0x0001649B
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000182A3 File Offset: 0x000164A3
		[DataMember(Name = "UserSettings", IsRequired = false)]
		public UserSettingCollection UserSettings { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000182AC File Offset: 0x000164AC
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x000182B4 File Offset: 0x000164B4
		[DataMember(Name = "UserSettingErrors", IsRequired = false)]
		public UserSettingErrorCollection UserSettingErrors { get; set; }
	}
}
