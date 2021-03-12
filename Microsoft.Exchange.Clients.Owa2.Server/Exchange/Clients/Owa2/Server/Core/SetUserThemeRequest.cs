using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FE RID: 1022
	[DataContract(Name = "SetUserThemeRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserThemeRequest : BaseJsonResponse
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0007A116 File Offset: 0x00078316
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0007A11E File Offset: 0x0007831E
		[DataMember(Name = "ThemeId", IsRequired = true)]
		public string ThemeId { get; set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0007A127 File Offset: 0x00078327
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0007A12F File Offset: 0x0007832F
		[DataMember(Name = "SkipO365Call", IsRequired = false)]
		public bool SkipO365Call { get; set; }
	}
}
