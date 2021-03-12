using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000374 RID: 884
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DisconnectPhoneCallType : BaseRequestType
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00029CC9 File Offset: 0x00027EC9
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x00029CD1 File Offset: 0x00027ED1
		public PhoneCallIdType PhoneCallId
		{
			get
			{
				return this.phoneCallIdField;
			}
			set
			{
				this.phoneCallIdField = value;
			}
		}

		// Token: 0x040012A3 RID: 4771
		private PhoneCallIdType phoneCallIdField;
	}
}
