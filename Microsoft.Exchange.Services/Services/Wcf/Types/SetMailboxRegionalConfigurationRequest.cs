using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A8E RID: 2702
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxRegionalConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x00106462 File Offset: 0x00104662
		// (set) Token: 0x06004C70 RID: 19568 RVA: 0x0010646A File Offset: 0x0010466A
		[DataMember(IsRequired = true)]
		public SetMailboxRegionalConfigurationData Options { get; set; }

		// Token: 0x06004C71 RID: 19569 RVA: 0x00106473 File Offset: 0x00104673
		public override string ToString()
		{
			return string.Format("SetMailboxRegionalConfigurationRequest: {0}", this.Options);
		}
	}
}
