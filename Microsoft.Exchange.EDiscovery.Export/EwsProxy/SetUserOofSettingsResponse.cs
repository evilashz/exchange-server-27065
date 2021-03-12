using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000A9 RID: 169
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetUserOofSettingsResponse
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001FB76 File Offset: 0x0001DD76
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001FB7E File Offset: 0x0001DD7E
		public ResponseMessageType ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x04000366 RID: 870
		private ResponseMessageType responseMessageField;
	}
}
