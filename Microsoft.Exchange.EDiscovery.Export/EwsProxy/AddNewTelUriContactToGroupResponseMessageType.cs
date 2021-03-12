using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000103 RID: 259
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddNewTelUriContactToGroupResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00021286 File Offset: 0x0001F486
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0002128E File Offset: 0x0001F48E
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

		// Token: 0x04000865 RID: 2149
		private PersonaType personaField;
	}
}
