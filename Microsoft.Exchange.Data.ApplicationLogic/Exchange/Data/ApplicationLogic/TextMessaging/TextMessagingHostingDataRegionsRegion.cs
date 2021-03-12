using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007C RID: 124
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[Serializable]
	public class TextMessagingHostingDataRegionsRegion
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00015B4C File Offset: 0x00013D4C
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00015B54 File Offset: 0x00013D54
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00015B5D File Offset: 0x00013D5D
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00015B65 File Offset: 0x00013D65
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string PhoneNumberExample
		{
			get
			{
				return this.phoneNumberExampleField;
			}
			set
			{
				this.phoneNumberExampleField = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00015B6E File Offset: 0x00013D6E
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00015B76 File Offset: 0x00013D76
		[XmlAttribute]
		public string Iso2
		{
			get
			{
				return this.iso2Field;
			}
			set
			{
				this.iso2Field = value;
			}
		}

		// Token: 0x04000265 RID: 613
		private string countryCodeField;

		// Token: 0x04000266 RID: 614
		private string phoneNumberExampleField;

		// Token: 0x04000267 RID: 615
		private string iso2Field;
	}
}
