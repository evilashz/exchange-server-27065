using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB2 RID: 2738
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRetentionPolicyTagsResponse : OptionsResponseBase
	{
		// Token: 0x06004D17 RID: 19735 RVA: 0x00106BC5 File Offset: 0x00104DC5
		public GetRetentionPolicyTagsResponse()
		{
			this.RetentionPolicyTagDisplayCollection = new RetentionPolicyTagDisplayCollection();
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x00106BD8 File Offset: 0x00104DD8
		// (set) Token: 0x06004D19 RID: 19737 RVA: 0x00106BE0 File Offset: 0x00104DE0
		[DataMember(IsRequired = true)]
		public RetentionPolicyTagDisplayCollection RetentionPolicyTagDisplayCollection { get; set; }

		// Token: 0x06004D1A RID: 19738 RVA: 0x00106BE9 File Offset: 0x00104DE9
		public override string ToString()
		{
			return string.Format("GetRetentionPolicyTagsResponse: {0}", this.RetentionPolicyTagDisplayCollection);
		}
	}
}
