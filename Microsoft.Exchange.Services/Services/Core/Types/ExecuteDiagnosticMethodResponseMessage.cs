using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D3 RID: 1235
	[XmlType("ExecuteDiagnosticMethodResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExecuteDiagnosticMethodResponseMessage : ResponseMessage
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x000A4875 File Offset: 0x000A2A75
		public ExecuteDiagnosticMethodResponseMessage()
		{
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000A487D File Offset: 0x000A2A7D
		internal ExecuteDiagnosticMethodResponseMessage(ServiceResultCode code, ServiceError error, XmlNode returnValue) : base(code, error)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000A488E File Offset: 0x000A2A8E
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x000A4896 File Offset: 0x000A2A96
		[XmlElement]
		public XmlNode ReturnValue { get; set; }
	}
}
