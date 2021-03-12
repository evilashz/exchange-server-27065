using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000880 RID: 2176
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlCompanyVerifiedDomain : DirectoryPropertyXml
	{
		// Token: 0x06006D48 RID: 27976 RVA: 0x0017556F File Offset: 0x0017376F
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x00175585 File Offset: 0x00173785
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueCompanyVerifiedDomain[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x06006D4A RID: 27978 RVA: 0x001755B5 File Offset: 0x001737B5
		// (set) Token: 0x06006D4B RID: 27979 RVA: 0x001755BD File Offset: 0x001737BD
		[XmlElement("Value", Order = 0)]
		public XmlValueCompanyVerifiedDomain[] Value
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

		// Token: 0x04004773 RID: 18291
		private XmlValueCompanyVerifiedDomain[] valueField;
	}
}
