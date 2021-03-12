using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000082 RID: 130
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ErrorCode
	{
		// Token: 0x04000311 RID: 785
		NoError,
		// Token: 0x04000312 RID: 786
		RedirectAddress,
		// Token: 0x04000313 RID: 787
		RedirectUrl,
		// Token: 0x04000314 RID: 788
		InvalidUser,
		// Token: 0x04000315 RID: 789
		InvalidRequest,
		// Token: 0x04000316 RID: 790
		InvalidSetting,
		// Token: 0x04000317 RID: 791
		SettingIsNotAvailable,
		// Token: 0x04000318 RID: 792
		ServerBusy,
		// Token: 0x04000319 RID: 793
		InvalidDomain,
		// Token: 0x0400031A RID: 794
		NotFederated,
		// Token: 0x0400031B RID: 795
		InternalServerError
	}
}
