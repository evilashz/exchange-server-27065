using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000245 RID: 581
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PlayOnPhoneResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000267E9 File Offset: 0x000249E9
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x000267F1 File Offset: 0x000249F1
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

		// Token: 0x04000F01 RID: 3841
		private PhoneCallIdType phoneCallIdField;
	}
}
