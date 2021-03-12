using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000114 RID: 276
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ErrorCode
	{
		// Token: 0x040005A8 RID: 1448
		NoError,
		// Token: 0x040005A9 RID: 1449
		RedirectAddress,
		// Token: 0x040005AA RID: 1450
		RedirectUrl,
		// Token: 0x040005AB RID: 1451
		InvalidUser,
		// Token: 0x040005AC RID: 1452
		InvalidRequest,
		// Token: 0x040005AD RID: 1453
		InvalidSetting,
		// Token: 0x040005AE RID: 1454
		SettingIsNotAvailable,
		// Token: 0x040005AF RID: 1455
		ServerBusy,
		// Token: 0x040005B0 RID: 1456
		InvalidDomain,
		// Token: 0x040005B1 RID: 1457
		NotFederated,
		// Token: 0x040005B2 RID: 1458
		InternalServerError
	}
}
