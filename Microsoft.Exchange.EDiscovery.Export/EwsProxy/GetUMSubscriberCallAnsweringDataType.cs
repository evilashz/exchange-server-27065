using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200030E RID: 782
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMSubscriberCallAnsweringDataType : BaseRequestType
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00028966 File Offset: 0x00026B66
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x0002896E File Offset: 0x00026B6E
		[XmlElement(DataType = "duration")]
		public string Timeout
		{
			get
			{
				return this.timeoutField;
			}
			set
			{
				this.timeoutField = value;
			}
		}

		// Token: 0x0400115C RID: 4444
		private string timeoutField;
	}
}
