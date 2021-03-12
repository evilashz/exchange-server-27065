using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E3 RID: 2275
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public enum VacationResponseMode
	{
		// Token: 0x04002A44 RID: 10820
		NoVacationResponse,
		// Token: 0x04002A45 RID: 10821
		OncePerSender,
		// Token: 0x04002A46 RID: 10822
		OncePerContact
	}
}
