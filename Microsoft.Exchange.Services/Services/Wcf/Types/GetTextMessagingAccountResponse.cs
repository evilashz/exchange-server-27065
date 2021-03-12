using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A86 RID: 2694
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTextMessagingAccountResponse : OptionsResponseBase
	{
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x00106294 File Offset: 0x00104494
		// (set) Token: 0x06004C43 RID: 19523 RVA: 0x0010629C File Offset: 0x0010449C
		[DataMember(IsRequired = true)]
		public TextMessagingAccount Data { get; set; }

		// Token: 0x06004C44 RID: 19524 RVA: 0x001062A5 File Offset: 0x001044A5
		public override string ToString()
		{
			return string.Format("GetTextMessagingAccountResponse: {0}", this.Data);
		}
	}
}
