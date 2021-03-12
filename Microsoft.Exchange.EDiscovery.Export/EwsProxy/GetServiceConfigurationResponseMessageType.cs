using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D2 RID: 466
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetServiceConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000255B3 File Offset: 0x000237B3
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x000255BB File Offset: 0x000237BB
		[XmlArrayItem(IsNullable = false)]
		public ServiceConfigurationResponseMessageType[] ResponseMessages
		{
			get
			{
				return this.responseMessagesField;
			}
			set
			{
				this.responseMessagesField = value;
			}
		}

		// Token: 0x04000D98 RID: 3480
		private ServiceConfigurationResponseMessageType[] responseMessagesField;
	}
}
