using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAD RID: 2733
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxResponse : OptionsResponseBase
	{
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004D06 RID: 19718 RVA: 0x00106AFB File Offset: 0x00104CFB
		// (set) Token: 0x06004D07 RID: 19719 RVA: 0x00106B03 File Offset: 0x00104D03
		[DataMember(IsRequired = true)]
		public MailboxOptions MailboxOptions { get; set; }

		// Token: 0x06004D08 RID: 19720 RVA: 0x00106B0C File Offset: 0x00104D0C
		public override string ToString()
		{
			return string.Format("GetMailboxResponse: MailboxOptions = {0}", this.MailboxOptions);
		}
	}
}
