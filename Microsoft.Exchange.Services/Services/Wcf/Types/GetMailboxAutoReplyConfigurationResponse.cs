using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAA RID: 2730
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxAutoReplyConfigurationResponse : OptionsResponseBase
	{
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x00106A7A File Offset: 0x00104C7A
		// (set) Token: 0x06004CFB RID: 19707 RVA: 0x00106A82 File Offset: 0x00104C82
		[DataMember(IsRequired = true)]
		public MailboxAutoReplyConfigurationOptions Options { get; set; }

		// Token: 0x06004CFC RID: 19708 RVA: 0x00106A8B File Offset: 0x00104C8B
		public override string ToString()
		{
			return string.Format("GetMailboxAutoReplyConfigurationResponse: {0}", this.Options);
		}
	}
}
