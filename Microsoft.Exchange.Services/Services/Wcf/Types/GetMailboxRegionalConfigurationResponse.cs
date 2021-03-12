using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A84 RID: 2692
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxRegionalConfigurationResponse : OptionsResponseBase
	{
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x0010625A File Offset: 0x0010445A
		// (set) Token: 0x06004C3D RID: 19517 RVA: 0x00106262 File Offset: 0x00104462
		[DataMember(IsRequired = true)]
		public GetMailboxRegionalConfigurationData Options { get; set; }

		// Token: 0x06004C3E RID: 19518 RVA: 0x0010626B File Offset: 0x0010446B
		public override string ToString()
		{
			return string.Format("GetMailboxRegionalConfiguration: {0}", this.Options);
		}
	}
}
