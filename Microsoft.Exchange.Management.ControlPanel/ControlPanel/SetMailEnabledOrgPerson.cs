using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E1 RID: 225
	[DataContract]
	public abstract class SetMailEnabledOrgPerson : SetObjectProperties
	{
		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0005B26A File Offset: 0x0005946A
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x0005B272 File Offset: 0x00059472
		public IEnumerable<string> EmailAddresses { get; set; }

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0005B27B File Offset: 0x0005947B
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0005B28D File Offset: 0x0005948D
		public string MailTip
		{
			get
			{
				return (string)base["MailTip"];
			}
			set
			{
				base["MailTip"] = value;
			}
		}
	}
}
