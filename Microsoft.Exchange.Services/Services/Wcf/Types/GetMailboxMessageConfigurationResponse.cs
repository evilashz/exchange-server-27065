using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAC RID: 2732
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxMessageConfigurationResponse : OptionsResponseBase
	{
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x00106AD0 File Offset: 0x00104CD0
		// (set) Token: 0x06004D03 RID: 19715 RVA: 0x00106AD8 File Offset: 0x00104CD8
		[DataMember(IsRequired = true)]
		public MailboxMessageConfigurationOptions Options { get; set; }

		// Token: 0x06004D04 RID: 19716 RVA: 0x00106AE1 File Offset: 0x00104CE1
		public override string ToString()
		{
			return string.Format("GetMailboxMessageConfigurationResponse: {0}", this.Options);
		}
	}
}
