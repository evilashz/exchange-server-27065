using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000375 RID: 885
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetPhoneCallInformationType : BaseRequestType
	{
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00029CE2 File Offset: 0x00027EE2
		// (set) Token: 0x06001C1D RID: 7197 RVA: 0x00029CEA File Offset: 0x00027EEA
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

		// Token: 0x040012A4 RID: 4772
		private PhoneCallIdType phoneCallIdField;
	}
}
