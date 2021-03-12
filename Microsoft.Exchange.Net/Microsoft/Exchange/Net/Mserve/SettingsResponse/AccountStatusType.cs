using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DA RID: 2266
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum AccountStatusType
	{
		// Token: 0x04002A18 RID: 10776
		OK,
		// Token: 0x04002A19 RID: 10777
		Blocked,
		// Token: 0x04002A1A RID: 10778
		RequiresHIP
	}
}
