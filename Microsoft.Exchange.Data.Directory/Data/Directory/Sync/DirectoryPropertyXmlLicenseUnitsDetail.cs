using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000887 RID: 2183
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle))]
	[Serializable]
	public abstract class DirectoryPropertyXmlLicenseUnitsDetail : DirectoryPropertyXml
	{
		// Token: 0x17002708 RID: 9992
		// (get) Token: 0x06006D6B RID: 28011 RVA: 0x001757A7 File Offset: 0x001739A7
		// (set) Token: 0x06006D6C RID: 28012 RVA: 0x001757AF File Offset: 0x001739AF
		[XmlElement("Value", Order = 0)]
		public XmlValueLicenseUnitsDetail[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400477A RID: 18298
		private XmlValueLicenseUnitsDetail[] valueField;
	}
}
