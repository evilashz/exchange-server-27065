using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000241 RID: 577
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetPhoneCallInformationResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00026773 File Offset: 0x00024973
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0002677B File Offset: 0x0002497B
		public PhoneCallInformationType PhoneCallInformation
		{
			get
			{
				return this.phoneCallInformationField;
			}
			set
			{
				this.phoneCallInformationField = value;
			}
		}

		// Token: 0x04000EEC RID: 3820
		private PhoneCallInformationType phoneCallInformationField;
	}
}
