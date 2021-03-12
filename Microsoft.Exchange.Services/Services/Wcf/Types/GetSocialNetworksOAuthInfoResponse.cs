using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFA RID: 2810
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetSocialNetworksOAuthInfoResponse : OptionsResponseBase
	{
		// Token: 0x06004FEA RID: 20458 RVA: 0x00109106 File Offset: 0x00107306
		public GetSocialNetworksOAuthInfoResponse()
		{
			this.SocialNetworksOAuthInfo = new SocialNetworksOAuthInfo();
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x00109119 File Offset: 0x00107319
		// (set) Token: 0x06004FEC RID: 20460 RVA: 0x00109121 File Offset: 0x00107321
		[DataMember(IsRequired = true)]
		public SocialNetworksOAuthInfo SocialNetworksOAuthInfo { get; set; }

		// Token: 0x06004FED RID: 20461 RVA: 0x0010912A File Offset: 0x0010732A
		public override string ToString()
		{
			return string.Format("GetSocialNetworksOAuthInfoResponse: {0}", this.SocialNetworksOAuthInfo);
		}
	}
}
