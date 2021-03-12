using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200025C RID: 604
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetPersonaResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00026D26 File Offset: 0x00024F26
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x00026D2E File Offset: 0x00024F2E
		public PersonaType Persona
		{
			get
			{
				return this.personaField;
			}
			set
			{
				this.personaField = value;
			}
		}

		// Token: 0x04000F56 RID: 3926
		private PersonaType personaField;
	}
}
