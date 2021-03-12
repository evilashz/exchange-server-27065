using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000148 RID: 328
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class EnhancedLocationType
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00022875 File Offset: 0x00020A75
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x0002287D File Offset: 0x00020A7D
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00022886 File Offset: 0x00020A86
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0002288E File Offset: 0x00020A8E
		public string Annotation
		{
			get
			{
				return this.annotationField;
			}
			set
			{
				this.annotationField = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00022897 File Offset: 0x00020A97
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0002289F File Offset: 0x00020A9F
		public PersonaPostalAddressType PostalAddress
		{
			get
			{
				return this.postalAddressField;
			}
			set
			{
				this.postalAddressField = value;
			}
		}

		// Token: 0x040009DA RID: 2522
		private string displayNameField;

		// Token: 0x040009DB RID: 2523
		private string annotationField;

		// Token: 0x040009DC RID: 2524
		private PersonaPostalAddressType postalAddressField;
	}
}
