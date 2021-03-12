using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000080 RID: 128
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class TextMessagingHostingDataServices
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00015BF4 File Offset: 0x00013DF4
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00015BFC File Offset: 0x00013DFC
		[XmlElement("Service", Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesService[] Service
		{
			get
			{
				return this.serviceField;
			}
			set
			{
				this.serviceField = value;
			}
		}

		// Token: 0x0400026D RID: 621
		private TextMessagingHostingDataServicesService[] serviceField;
	}
}
