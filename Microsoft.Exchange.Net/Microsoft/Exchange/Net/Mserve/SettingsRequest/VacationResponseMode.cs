using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C8 RID: 2248
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public enum VacationResponseMode
	{
		// Token: 0x0400299D RID: 10653
		NoVacationResponse,
		// Token: 0x0400299E RID: 10654
		OncePerSender,
		// Token: 0x0400299F RID: 10655
		OncePerContact
	}
}
