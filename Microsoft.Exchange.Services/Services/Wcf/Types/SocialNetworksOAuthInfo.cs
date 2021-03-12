using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B07 RID: 2823
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class SocialNetworksOAuthInfo
	{
		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x0010936F File Offset: 0x0010756F
		// (set) Token: 0x06005020 RID: 20512 RVA: 0x00109377 File Offset: 0x00107577
		[DataMember(IsRequired = true)]
		public string OAuthUri { get; set; }
	}
}
