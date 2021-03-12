using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000383 RID: 899
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetPersonaType : BaseRequestType
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00029F72 File Offset: 0x00028172
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x00029F7A File Offset: 0x0002817A
		public ItemIdType PersonaId
		{
			get
			{
				return this.personaIdField;
			}
			set
			{
				this.personaIdField = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x00029F83 File Offset: 0x00028183
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x00029F8B File Offset: 0x0002818B
		public EmailAddressType EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x040012D6 RID: 4822
		private ItemIdType personaIdField;

		// Token: 0x040012D7 RID: 4823
		private EmailAddressType emailAddressField;
	}
}
