using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007F RID: 127
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class TextMessagingHostingDataCarriersCarrierLocalizedInfo
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00015BCA File Offset: 0x00013DCA
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00015BD2 File Offset: 0x00013DD2
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00015BDB File Offset: 0x00013DDB
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00015BE3 File Offset: 0x00013DE3
		[XmlAttribute]
		public string Culture
		{
			get
			{
				return this.cultureField;
			}
			set
			{
				this.cultureField = value;
			}
		}

		// Token: 0x0400026B RID: 619
		private string displayNameField;

		// Token: 0x0400026C RID: 620
		private string cultureField;
	}
}
