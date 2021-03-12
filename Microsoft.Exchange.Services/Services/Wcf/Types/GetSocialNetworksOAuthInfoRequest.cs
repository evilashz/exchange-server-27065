using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF9 RID: 2809
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetSocialNetworksOAuthInfoRequest : BaseJsonRequest
	{
		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x001090AA File Offset: 0x001072AA
		// (set) Token: 0x06004FE3 RID: 20451 RVA: 0x001090B2 File Offset: 0x001072B2
		[IgnoreDataMember]
		public ConnectSubscriptionType ConnectSubscriptionType { get; set; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x001090BB File Offset: 0x001072BB
		// (set) Token: 0x06004FE5 RID: 20453 RVA: 0x001090C8 File Offset: 0x001072C8
		[DataMember(Name = "ConnectSubscriptionType", IsRequired = true, EmitDefaultValue = false)]
		public string ConnectSubscriptionTypeString
		{
			get
			{
				return EnumUtilities.ToString<ConnectSubscriptionType>(this.ConnectSubscriptionType);
			}
			set
			{
				this.ConnectSubscriptionType = EnumUtilities.Parse<ConnectSubscriptionType>(value);
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x001090D6 File Offset: 0x001072D6
		// (set) Token: 0x06004FE7 RID: 20455 RVA: 0x001090DE File Offset: 0x001072DE
		[DataMember(IsRequired = true)]
		public string RedirectUri { get; set; }

		// Token: 0x06004FE8 RID: 20456 RVA: 0x001090E7 File Offset: 0x001072E7
		public override string ToString()
		{
			return string.Format("GetSocialNetworksOAuthInfoRequest: {0}", this.ConnectSubscriptionType);
		}
	}
}
