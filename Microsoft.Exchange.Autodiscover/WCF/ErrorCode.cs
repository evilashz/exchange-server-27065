using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A0 RID: 160
	[DataContract(Name = "ErrorCode", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public enum ErrorCode
	{
		// Token: 0x04000346 RID: 838
		[EnumMember]
		NoError,
		// Token: 0x04000347 RID: 839
		[EnumMember]
		RedirectAddress,
		// Token: 0x04000348 RID: 840
		[EnumMember]
		RedirectUrl,
		// Token: 0x04000349 RID: 841
		[EnumMember]
		InvalidUser,
		// Token: 0x0400034A RID: 842
		[EnumMember]
		InvalidRequest,
		// Token: 0x0400034B RID: 843
		[EnumMember]
		InvalidSetting,
		// Token: 0x0400034C RID: 844
		[EnumMember]
		SettingIsNotAvailable,
		// Token: 0x0400034D RID: 845
		[EnumMember]
		ServerBusy,
		// Token: 0x0400034E RID: 846
		[EnumMember]
		InvalidDomain,
		// Token: 0x0400034F RID: 847
		[EnumMember]
		NotFederated,
		// Token: 0x04000350 RID: 848
		[EnumMember]
		InternalServerError
	}
}
