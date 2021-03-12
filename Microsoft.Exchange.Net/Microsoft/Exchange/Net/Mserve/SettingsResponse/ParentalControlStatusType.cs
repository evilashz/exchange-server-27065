using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DB RID: 2267
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum ParentalControlStatusType
	{
		// Token: 0x04002A1C RID: 10780
		None,
		// Token: 0x04002A1D RID: 10781
		FullAccess,
		// Token: 0x04002A1E RID: 10782
		RestrictedAccess,
		// Token: 0x04002A1F RID: 10783
		NoAccess
	}
}
