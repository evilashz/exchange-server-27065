using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000197 RID: 407
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Region : DirectoryObject
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00021DB0 File Offset: 0x0001FFB0
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00021DB8 File Offset: 0x0001FFB8
		public DirectoryPropertyStringLength1To3 CountryLetterCodes
		{
			get
			{
				return this.countryLetterCodesField;
			}
			set
			{
				this.countryLetterCodesField = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00021DC1 File Offset: 0x0001FFC1
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00021DC9 File Offset: 0x0001FFC9
		public DirectoryPropertyStringSingleLength1To16 RegionName
		{
			get
			{
				return this.regionNameField;
			}
			set
			{
				this.regionNameField = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00021DD2 File Offset: 0x0001FFD2
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00021DDA File Offset: 0x0001FFDA
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040005A0 RID: 1440
		private DirectoryPropertyStringLength1To3 countryLetterCodesField;

		// Token: 0x040005A1 RID: 1441
		private DirectoryPropertyStringSingleLength1To16 regionNameField;

		// Token: 0x040005A2 RID: 1442
		private XmlAttribute[] anyAttrField;
	}
}
