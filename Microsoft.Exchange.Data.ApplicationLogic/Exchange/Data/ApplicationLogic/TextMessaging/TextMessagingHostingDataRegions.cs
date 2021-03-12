using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007B RID: 123
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[XmlType(AnonymousType = true)]
	[Serializable]
	public class TextMessagingHostingDataRegions
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00015B33 File Offset: 0x00013D33
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00015B3B File Offset: 0x00013D3B
		[XmlElement("Region", Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataRegionsRegion[] Region
		{
			get
			{
				return this.regionField;
			}
			set
			{
				this.regionField = value;
			}
		}

		// Token: 0x04000264 RID: 612
		private TextMessagingHostingDataRegionsRegion[] regionField;
	}
}
