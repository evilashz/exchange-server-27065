using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000104 RID: 260
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AddNewImContactToGroupResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002129F File Offset: 0x0001F49F
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x000212A7 File Offset: 0x0001F4A7
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

		// Token: 0x04000866 RID: 2150
		private PersonaType personaField;
	}
}
